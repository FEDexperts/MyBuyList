using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

using MyBuyList.Shared.Entities;
using MyBuyList.Shared.Enums;
using MyBuyList.Shared;

namespace MyBuyList.DataLayer.DataAdapters
{
    class RecipesDA : BaseContextDataAdapter<MyBuyListDBContext>
    {
        public const int USER_ADMIN = 1;

        internal Recipe[] GetRecipesListByFoodId(int foodId)
        {
            using (DataContext)
            {
                try
                {

                    var list = (from r in DataContext.Recipes
                                join i in DataContext.Ingredients.Where(i => i.Food.FoodId == foodId) on r.RecipeId equals i.RecipeId
                                select r).Distinct();

                    return list.ToArray();

                }
                catch
                {
                    return null;
                }
            }
        }

        internal IQueryable<Recipe> GetRecipesListByFreeText(string freeText)
        {
            IQueryable<Recipe> recipes = DataContext.Recipes.Where(p => p.RecipeName.Contains(freeText));
            return recipes;
        }

        internal Recipe[] GetRecipesListByComplexSearch(string freeText, int? servings, int[] recipeCats, int userId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Recipe>(r => r.User);
                    dlo.LoadWith<Recipe>(r => r.UserFavoriteRecipes);
                    dlo.LoadWith<Recipe>(r => r.MenuRecipes);
                    dlo.LoadWith<Recipe>(r => r.RecipeCategories);
                    dlo.LoadWith<RecipeCategory>(rc => rc.Category);
                    DataContext.LoadOptions = dlo;

                    var ingredients = from r in DataContext.Recipes.Where(r => r.IsPublic == true || r.UserId == userId || userId == USER_ADMIN)
                                      join i in DataContext.Ingredients.Where(i => (i.Food.FoodName.Trim().IndexOf(freeText.Trim()) != -1) ||
                                                                                   (i.Remarks.Trim().IndexOf(freeText.Trim()) != -1)
                                                                             ) on r.RecipeId equals i.RecipeId
                                      select r.RecipeId;

                    var recipes = from r1 in DataContext.Recipes.Where(r => (r.IsPublic == true || r.UserId == userId) &&
                                                          ((r.RecipeName.Trim().IndexOf(freeText.Trim()) != -1) ||
                                                           (r.Remarks.Trim().IndexOf(freeText.Trim()) != -1) ||
                                                           (r.PreparationMethod.Trim().IndexOf(freeText.Trim()) != -1) ||
                                                            (r.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1) ||
                                                            (r.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1)
                                                           ))
                                  select r1.RecipeId;

                    var list2 = from r2 in DataContext.Recipes.Where(r => (r.IsPublic == true || r.UserId == userId || userId == USER_ADMIN))
                                join rc in DataContext.RecipeCategories.Where(rc => (rc.Category.CategoryName.Trim().IndexOf(freeText.Trim()) != -1))
                                                                                    on r2.RecipeId equals rc.RecipeId
                                select r2.RecipeId;

                    //var list1 = recipes.Union(ingredients);
                    var tmpList = recipes.Union(ingredients);
                    var list1 = tmpList.Union(list2);

                    var list = DataContext.Recipes.Where(r => list1.Contains(r.RecipeId));

                    if (servings.HasValue)
                    {
                        list = list.Where(r => r.Servings >= servings.Value);
                    }

                    if (recipeCats != null && recipeCats.Length != 0)
                    {
                        List<Recipe> temp = new List<Recipe>();

                        foreach (int categoryId in recipeCats)
                        {
                            foreach (RecipeCategory rcat in DataContext.RecipeCategories.Where(rc => rc.CategoryId == categoryId ||
                                                                                                    rc.Category.ParentCategoryId == categoryId))
                            {
                                if (!temp.Contains(rcat.Recipe))
                                {
                                    temp.Add(rcat.Recipe);
                                }
                            }
                        }

                        list = (from r in list
                                where temp.Contains(r)
                                select r);
                    }

                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Recipe[] GetUserRecipesList(int userId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Recipe>(r => r.User);
                    dlo.LoadWith<Recipe>(r => r.UserFavoriteRecipes);
                    dlo.LoadWith<Recipe>(r => r.MenuRecipes);
                    dlo.LoadWith<Recipe>(r => r.RecipeCategories);
                    dlo.LoadWith<RecipeCategory>(rc => rc.Category);
                    DataContext.LoadOptions = dlo;

                    var list = DataContext.Recipes.Where(r => r.UserId == userId);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Recipe[] GetUserFavoritesRecipes(int userId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Recipe>(r => r.User);
                    dlo.LoadWith<Recipe>(r => r.UserFavoriteRecipes);
                    dlo.LoadWith<Recipe>(r => r.MenuRecipes);
                    dlo.LoadWith<Recipe>(r => r.RecipeCategories);
                    dlo.LoadWith<RecipeCategory>(rc => rc.Category);
                    DataContext.LoadOptions = dlo;

                    var list = from r in DataContext.Recipes
                               join ufr in DataContext.UserFavoriteRecipes.Where(ufr => ufr.UserId == userId) on r.RecipeId equals ufr.RecipeId
                               select r;
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal int? GetRecipeUserFavoritesCount(int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<UserFavoriteRecipe>(ufr => ufr.RecipeId);
                    DataContext.LoadOptions = dlo;

                    var list = from ufr in DataContext.UserFavoriteRecipes
                               where ufr.RecipeId == recipeId
                               select ufr;
                    return list.ToArray().Length;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Category[] GetRecipesCategoriesList(int userId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Recipe>(r => r.User);
                    DataContext.LoadOptions = dlo;
                    var list = DataContext.Categories.OrderBy(cat => cat.SortOrder);
                    foreach (Category item in list)
                    {
                        item.ParentCategories.Load();
                        item.RecipesCount = GetRecipesByCategory(DataContext, item.CategoryId, userId).Length;
                    }
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Recipe[] GetRecipesByCategory(int categoryId, int userId)
        {
            using (DataContext)
            {
                DataLoadOptions dlo = new DataLoadOptions();
                dlo.LoadWith<Recipe>(r => r.User);
                dlo.LoadWith<Recipe>(r => r.UserFavoriteRecipes);
                dlo.LoadWith<Recipe>(r => r.MenuRecipes);
                dlo.LoadWith<Recipe>(r => r.RecipeCategories);
                dlo.LoadWith<RecipeCategory>(rc => rc.Category);
                DataContext.LoadOptions = dlo;
                return GetRecipesByCategory(DataContext, categoryId, userId);
            }
        }

        private Recipe[] GetRecipesByCategory(MyBuyListDBContext dc, int categoryId, int userId)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();

            dc.Log = sw;

            try
            {
                var list = from r in dc.Recipes.Where(r => r.IsPublic == true || r.UserId == userId || userId == USER_ADMIN)
                           join rcat in dc.RecipeCategories.Where(rc => rc.CategoryId == categoryId ||
                                                                        rc.Category.ParentCategoryId == categoryId)
                           on r.RecipeId equals rcat.RecipeId
                           select r;

                return (from r in dc.Recipes
                        where list.Contains(r)
                        select r).ToArray();

            }
            catch
            {
                return null;
            }
        }

        internal Recipe[] GetRecipesList()
        {
            //DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<Recipe>(r => r.User);
            //DataContext.LoadOptions = dlo;

            Recipe[] list = (from r in DataContext.Recipes select r).ToArray();

            return list.ToArray();
        }

        internal void AllowRecipe(int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    Recipe recipe = DataContext.Recipes.Single(r => r.RecipeId == recipeId);
                    recipe.IsApproved = !recipe.IsApproved;
                    DataContext.SubmitChanges();
                }
                catch
                {

                }
            }
        }

        internal Recipe GetRecipe(int recipeId)
        {
            DataLoadOptions dlo = new DataLoadOptions();
            dlo.LoadWith<Recipe>(r => r.User);
            dlo.LoadWith<Recipe>(r => r.RecipeCategories);
            dlo.LoadWith<RecipeCategory>(rc => rc.Category);
            dlo.LoadWith<Recipe>(r => r.UserFavoriteRecipes);
            dlo.LoadWith<Recipe>(r => r.MenuRecipes);
            dlo.LoadWith<Recipe>(r => r.Ingredients);
            dlo.LoadWith<Ingredient>(i => i.Food);
            dlo.LoadWith<Ingredient>(i => i.MeasurementUnit);


            DataContext.LoadOptions = dlo;

            Recipe recipe = DataContext.Recipes.Single(r => r.RecipeId == recipeId);

            if (recipe != null)
            {
                UserFavoriteRecipe ufrep = DataContext.UserFavoriteRecipes.SingleOrDefault(ufr => ufr.RecipeId == recipe.RecipeId &&
                                                                                                  ufr.UserId == recipe.UserId);
                recipe.ShowInFavorites = (ufrep != null);

                //recipe.SHOPPING_LIST = recipe.RecipesInShoppingLists.Any();
            }
            else
            {
                recipe.ShowInFavorites = false;
            }

            return recipe;
        }

        internal RecipeIngredientsView[] GetRecipeIngredientsViewList(int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.RecipeIngredientsViews.Where(ri => ri.RecipeId == recipeId);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Ingredient[] GetRecipeIngredientsList(int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();

                    dlo.LoadWith<Ingredient>(i => i.Food);
                    dlo.LoadWith<Ingredient>(i => i.MeasurementUnit);
                    dlo.LoadWith<Ingredient>(i => i.Remarks);


                    DataContext.LoadOptions = dlo;

                    var list = DataContext.Ingredients.Where(ri => ri.RecipeId == recipeId);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Dictionary<int, string> GetFoodList(string prefixText)
        {
            using (DataContext)
            {
                try
                {
                    return (from e in DataContext.Foods.Where(f => f.FoodName.StartsWith(prefixText))
                               select new { id = e.FoodId, name = e.FoodName }).ToDictionary(k=>k.id, v=> v.name);
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool AddRecipeToUserFavorites(int userId, int recipeId, out int favRecipesNum)
        {
            using (DataContext)
            {
                try
                {

                    UserFavoriteRecipe ufrep = DataContext.UserFavoriteRecipes.SingleOrDefault(ufr => ufr.UserId == userId &&
                                                                                                      ufr.RecipeId == recipeId);
                    if (ufrep == null)
                    {
                        ufrep = new UserFavoriteRecipe();
                        ufrep.UserId = userId;
                        ufrep.RecipeId = recipeId;
                        DataContext.UserFavoriteRecipes.InsertOnSubmit(ufrep);
                        DataContext.SubmitChanges();
                    }
                    favRecipesNum = DataContext.UserFavoriteRecipes.Where(ufr => ufr.UserId == userId && (ufr.Recipe.UserId == userId || ufr.Recipe.IsPublic)).Count();

                    return true;
                }
                catch
                {
                    favRecipesNum = 0;
                    return false;
                }
            }
        }

        internal bool RemoveUserFavoritesRecipe(int userId, int recipeId, out int favRecipesNum)
        {
            using (DataContext)
            {
                try
                {
                    UserFavoriteRecipe ufrep = DataContext.UserFavoriteRecipes.Single(ufr => ufr.UserId == userId &&
                                                                                             ufr.RecipeId == recipeId);
                    DataContext.UserFavoriteRecipes.DeleteOnSubmit(ufrep);
                    DataContext.SubmitChanges();

                    favRecipesNum = DataContext.UserFavoriteRecipes.Where(ufr => ufr.UserId == userId && (ufr.Recipe.UserId == userId || ufr.Recipe.IsPublic)).Count();

                    return true;
                }
                catch
                {
                    favRecipesNum = 0;
                    return false;
                }
            }
        }

        internal bool DeleteRecipe(int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    Recipe recipe = DataContext.Recipes.Single(r => r.RecipeId == recipeId);
                    DataContext.Recipes.DeleteOnSubmit(recipe);
                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool SaveRecipe(Recipe data, out int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    //Foods
                    foreach (Ingredient ing in data.Ingredients)
                    {
                        //find food by name for getting FoodId
                        Food food = DataContext.Foods.SingleOrDefault(f => f.FoodName.Trim() == ing.FoodName);
                        if (food != null)
                        {
                            ing.FoodId = food.FoodId;
                        }
                        else
                        {
                            //Create new food and mark it temporary
                            food = new Food();
                            food.FoodName = ing.FoodName;
                            food.IsTemporary = true;
                            food.FoodCategoryId = 0;
                            food.CalculateUnitId = 0;
                            food.CreatedBy = data.UserId;
                            food.CreatedDate = DateTime.Now;
                            food.ModifiedBy = data.UserId;
                            food.ModifiedDate = DateTime.Now;
                            DataContext.Foods.InsertOnSubmit(food);
                            DataContext.SubmitChanges();

                            ing.FoodId = food.FoodId;
                        }
                    }

                    Recipe recipe = DataContext.Recipes.SingleOrDefault(r => r.RecipeId == data.RecipeId);
                    if (recipe == null)
                    {
                        recipe = new Recipe(data);
                        recipe.CreatedDate = DateTime.Now;

                        int max = 0;

                        if (DataContext.Recipes.Where(r => r.UserId == recipe.UserId).Count() > 0)
                        {
                            max = DataContext.Recipes.Where(r => r.UserId == recipe.UserId).Max(r => r.SortOrder);
                        }

                        recipe.SortOrder = max + 1;



                        recipe.RecipeCategories.AddRange(data.RecipeCategories);
                        recipe.Ingredients.AddRange(data.Ingredients);

                        DataContext.Recipes.InsertOnSubmit(recipe);
                    }
                    else
                    {
                        recipe.SetValues(data);
                        DataContext.RecipeCategories.DeleteAllOnSubmit(recipe.RecipeCategories);
                        recipe.RecipeCategories.AddRange(data.RecipeCategories);

                        DataContext.Ingredients.DeleteAllOnSubmit(recipe.Ingredients);
                        recipe.Ingredients.AddRange(data.Ingredients);
                    }

                    recipe.ModifiedDate = DateTime.Now;
                    DataContext.SubmitChanges();

                    recipeId = recipe.RecipeId;

                    //Show recipe in favorites
                    UserFavoriteRecipe ufrep = DataContext.UserFavoriteRecipes.SingleOrDefault(fr => fr.RecipeId == recipe.RecipeId &&
                                                                                                     fr.UserId == recipe.UserId);
                    if (recipe.ShowInFavorites)
                    {
                        if (ufrep == null)
                        {
                            ufrep = new UserFavoriteRecipe();
                            ufrep.RecipeId = recipe.RecipeId;
                            ufrep.UserId = recipe.UserId;
                            DataContext.UserFavoriteRecipes.InsertOnSubmit(ufrep);
                            DataContext.SubmitChanges();
                        }
                    }
                    else
                    {
                        if (ufrep != null)
                        {
                            DataContext.UserFavoriteRecipes.DeleteOnSubmit(ufrep);
                            DataContext.SubmitChanges();
                        }
                    }

                    return true;
                }
                catch
                {
                    recipeId = 0;
                    return false;
                }
            }
        }

        internal bool UpdateRecipePreparationMethod(int recipeId, string preparationMethod)
        {
            using (DataContext)
            {
                try
                {
                    Recipe recipe = DataContext.Recipes.Single(r => r.RecipeId == recipeId);
                    recipe.PreparationMethod = preparationMethod;
                    recipe.ModifiedDate = DateTime.Now;
                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool SaveRecipeCategories(int recipeId, RecipeCategory[] categories)
        {
            using (DataContext)
            {
                try
                {
                    Recipe recipe = DataContext.Recipes.Single(r => r.RecipeId == recipeId);
                    DataContext.RecipeCategories.DeleteAllOnSubmit(recipe.RecipeCategories);
                    DataContext.SubmitChanges();
                    recipe.RecipeCategories.AddRange(categories);
                    recipe.ModifiedDate = DateTime.Now;
                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal Food GetFood(int id)
        {
            using (DataContext)
            {
                try
                {
                    Food item = DataContext.Foods.SingleOrDefault(f => f.FoodId == id);


                    return item;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal int GetRecipesNum()
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.Recipes.Count();
                }
                catch
                {
                    return -1;
                }
            }
        }

        internal RecipeTotalNutValues[] GetRecipeTotalNutValues(int recipeId, out bool isCompleteCalculation)
        {
            isCompleteCalculation = false;

            int ingredientsCount = DataContext.Recipes.Single(r => r.RecipeId == recipeId).Ingredients.Count;

            var measureUnitConverts = DataContext.MeasurementUnitsConverts;

            var query = from r in DataContext.Recipes.Where(r => r.RecipeId == recipeId && r.Servings > 0)
                        join i in DataContext.Ingredients on r.RecipeId equals i.RecipeId
                        join f in DataContext.Foods on i.FoodId equals f.FoodId
                        join nv in DataContext.NutValues on f.FoodId equals nv.FoodId
                        join ni in DataContext.NutItems on nv.NutItemId equals ni.NutItemId
                        orderby ni.NutCategoryId, ni.NutItemId
                        select new
                        {
                            ni.NutItemId,
                            ni.NutItemName,
                            ni.DisplayUnit,
                            ni.NutCategoryId,
                            nv.Value,
                            r.Servings,
                            i.Quantity,
                            f.FoodId,
                            i.MeasurementUnitId,
                            i.IngredientId //added
                        };


            List<RecipeTotalNutValues> list = new List<RecipeTotalNutValues>();

            decimal totalRecipeWeight = 0;
            Dictionary<int, int> NutValuesIncludedInCalaculation = new Dictionary<int, int>();

            foreach (var row in query)
            {
                bool calculateDataFound = false;
                decimal convertRatio = 0;

                if (row.MeasurementUnitId == AppConstants.GRAMM_UNIT_ID)
                {
                    convertRatio = 1;
                    calculateDataFound = true;
                }
                else if (row.MeasurementUnitId == AppConstants.KILOGRAMM_UNIT_ID)
                {
                    convertRatio = 1000;
                    calculateDataFound = true;
                }
                else
                {
                    MeasurementUnitsConvert unitConvert = measureUnitConverts.SingleOrDefault(muc => muc.FoodId == row.FoodId &&
                                                                                              muc.FromUnitId == row.MeasurementUnitId &&
                                                                                              muc.ToUnitId == AppConstants.GRAMM_UNIT_ID);
                    if (unitConvert != null)
                    {
                        if (unitConvert.FromQuantity > 0)
                        {
                            convertRatio = unitConvert.ToQuantity / unitConvert.FromQuantity;
                            calculateDataFound = true;
                        }
                    }
                    else
                    {
                        unitConvert = measureUnitConverts.SingleOrDefault(muc => muc.FoodId == row.FoodId &&
                                                                          muc.FromUnitId == row.MeasurementUnitId &&
                                                                          muc.ToUnitId == AppConstants.KILOGRAMM_UNIT_ID);
                        if (unitConvert != null)
                        {
                            convertRatio = unitConvert.ToQuantity / unitConvert.FromQuantity * 1000;
                            calculateDataFound = true;
                        }
                    }
                }

                if (calculateDataFound)
                {
                    RecipeTotalNutValues item = new RecipeTotalNutValues
                    {
                        NutCategoryId = row.NutCategoryId,
                        NutItemId = row.NutItemId,
                        NutItemName = row.NutItemName,
                        DisplayUnit = row.DisplayUnit,
                        //TotalValue = (row.Value == null ? null : row.Quantity / row.Servings * convertRatio * row.Value / 100)
                        //calculate nutvalue for the whole recipe, not per serving as above:
                        TotalValue = (row.Value == null ? null : row.Quantity * convertRatio * (row.Value / 100))
                    };

                    //this if statement is not needed, since DisplayTotalValue should only be set after final TotalValue is determined.
                    if (item.TotalValue != null)
                    {
                        if (item.TotalValue != 0)
                        {
                            item.DisplayTotalValue = item.TotalValue.Value.ToString("F");
                        }
                        else
                        {
                            item.DisplayTotalValue = "0";
                        }
                    }

                    list.Add(item);

                    if (!NutValuesIncludedInCalaculation.Keys.Contains(row.IngredientId))
                    {
                        NutValuesIncludedInCalaculation.Add(row.IngredientId, 1);

                    }
                    else
                    {
                        NutValuesIncludedInCalaculation[row.IngredientId]++;
                    }
                }
            }
            // added code: calculate total "recipe mass":
            var ingredientsList = from ing in DataContext.Ingredients
                                  where ing.RecipeId == recipeId
                                  select ing;

            foreach (Ingredient ingredient in ingredientsList)
            {
                bool calculateDataFound = false;
                decimal convertRatio = 0;

                if (ingredient.MeasurementUnitId == AppConstants.GRAMM_UNIT_ID)
                {
                    convertRatio = 1;
                    calculateDataFound = true;
                }
                else if (ingredient.MeasurementUnitId == AppConstants.KILOGRAMM_UNIT_ID)
                {
                    convertRatio = 1000;
                    calculateDataFound = true;
                }
                else
                {
                    MeasurementUnitsConvert unitConvert = measureUnitConverts.SingleOrDefault(muc => muc.FoodId == ingredient.FoodId &&
                                                                                              muc.FromUnitId == ingredient.MeasurementUnitId &&
                                                                                              muc.ToUnitId == MyBuyList.Shared.AppConstants.GRAMM_UNIT_ID);
                    if (unitConvert != null)
                    {
                        if (unitConvert.FromQuantity > 0)
                        {
                            convertRatio = unitConvert.ToQuantity / unitConvert.FromQuantity;
                            calculateDataFound = true;
                        }
                    }
                    else
                    {
                        unitConvert = measureUnitConverts.SingleOrDefault(muc => muc.FoodId == ingredient.FoodId &&
                                                                          muc.FromUnitId == ingredient.MeasurementUnitId &&
                                                                          muc.ToUnitId == AppConstants.KILOGRAMM_UNIT_ID);
                        if (unitConvert != null)
                        {
                            convertRatio = unitConvert.ToQuantity / unitConvert.FromQuantity * 1000;
                            calculateDataFound = true;
                        }
                    }
                }

                if (calculateDataFound)
                {
                    totalRecipeWeight += (ingredient.Quantity * convertRatio);
                }
            }
            //end of added code.

            Dictionary<int, RecipeTotalNutValues> dict = new Dictionary<int, RecipeTotalNutValues>();
            foreach (RecipeTotalNutValues item in list)
            {
                if (!dict.ContainsKey(item.NutItemId))
                {
                    if (item.TotalValue != null)
                    {
                        dict.Add(item.NutItemId, item);
                    }
                }
                else
                {
                    dict[item.NutItemId].TotalValue += item.TotalValue;
                    //if (dict[item.NutItemId].TotalValue != null)
                    //{
                    //    if (dict[item.NutItemId].TotalValue.Value != 0)
                    //    {
                    //        dict[item.NutItemId].DisplayTotalValue = dict[item.NutItemId].TotalValue.Value.ToString("F");
                    //    }
                    //    else
                    //    {
                    //        dict[item.NutItemId].DisplayTotalValue = "0";
                    //    }
                    //}
                }
            }
            RecipeTotalNutValues[] array = dict.Values.ToArray();

            //divide each nutValue by the weight of the whole recipe in grams and then multiply by 100, to get the nutValues for a 100 grams of "recipe mass"...
            if (totalRecipeWeight != 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].TotalValue.HasValue)
                    {
                        decimal x = array[i].TotalValue.Value;
                        array[i].TotalValue = (x / totalRecipeWeight) * 100;

                        if (array[i].TotalValue != 0)
                        {
                            array[i].DisplayTotalValue = array[i].TotalValue.Value.ToString("F");
                        }
                        else
                        {
                            array[i].DisplayTotalValue = "0";
                        }
                    }
                }
            }

            if (ingredientsCount == NutValuesIncludedInCalaculation.Count())
            {
                isCompleteCalculation = true;

                foreach (KeyValuePair<int, int> pair in NutValuesIncludedInCalaculation)
                {
                    if (pair.Value < 26)
                    {
                        isCompleteCalculation = false;
                    }
                }
            }

            return array;
        }

        internal IEnumerable<Recipe> GetRecipes(RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages)
        {
            using (MyBuyListDBContext context = new MyBuyListDBContext(DBUtils.GetConnection()))
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Recipe>(r => r.User);
                    DataContext.LoadOptions = dlo;

                    var count = (from r in DataContext.Recipes
                                 select r).Count();

                    totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

                    var list = from r in DataContext.Recipes
                               select r;


                    switch (orderBy)
                    {
                        case RecipeOrderEnum.Name:
                            list = list.OrderBy(r => r.RecipeName);
                            break;
                        case RecipeOrderEnum.Publisher:
                            list = list.OrderBy(r => r.User.DisplayName);
                            break;
                        case RecipeOrderEnum.LastUpdate:
                            list = list.OrderByDescending(r => r.ModifiedDate);
                            break;
                    }

                    // paging
                    var list2 = list.Skip((page - 1) * pageSize).Take(pageSize);

                    return list2.ToList();

                }
                catch
                {
                    totalPages = 0;
                    return null;
                }
            }
        }

        internal List<Recipe> GetRecipesEx(RecipeDisplayEnum display, int userId, string freeText, int? categoryId, int? servings, int[] recipeCats, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages, out int numRecipes)
        {
            using (MyBuyListDBContext context = new MyBuyListDBContext(DBUtils.GetConnection()))
            {
                try
                {
                    List<Recipe> recipesList = null;

                    switch (display)
                    {
                        case RecipeDisplayEnum.All:
                            recipesList = GetAllRecipes(userId).ToList();
                            break;
                        case RecipeDisplayEnum.MyRecipes:
                            if (!string.IsNullOrEmpty(freeText))
                                recipesList = GetUserRecipesList(userId).Where(r => r.RecipeName.Contains(freeText)).ToList();
                            else
                                recipesList = GetUserRecipesList(userId).ToList();
                            break;
                        case RecipeDisplayEnum.MyFavoriteRecipes:
                            if (!string.IsNullOrEmpty(freeText))
                                recipesList = GetUserFavoritesRecipes(userId).Where(r => r.RecipeName.Contains(freeText)).ToList();
                            else
                                recipesList = GetUserFavoritesRecipes(userId).ToList();
                            break;
                        case RecipeDisplayEnum.ByCategory:
                            if (categoryId.HasValue)
                            {
                                recipesList = this.GetRecipesByCategory(categoryId.Value, userId).ToList();
                            }
                            else
                            {
                                recipesList = this.GetAllRecipes(userId).ToList();
                            }
                            break;
                        case RecipeDisplayEnum.BySearchSimple:
                            recipesList = GetRecipesListByFreeText(freeText).ToList();
                            break;
                        case RecipeDisplayEnum.BySearchAdvanced:
                            recipesList = this.GetRecipesListByComplexSearch(freeText, servings, recipeCats, userId).ToList();
                            break;
                    }

                    var count = (from r in recipesList
                                 select r).Count();

                    numRecipes = count;

                    totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

                    var list = from r in recipesList
                               select r;


                    switch (orderBy)
                    {
                        case RecipeOrderEnum.Name:
                            list = list.OrderBy(r => r.RecipeName);
                            break;
                        case RecipeOrderEnum.Publisher:
                            list = list.OrderBy(r => r.User.DisplayName);
                            break;
                        case RecipeOrderEnum.LastUpdate:
                            list = list.OrderByDescending(r => r.ModifiedDate);
                            break;
                    }

                    // paging
                    var list2 = list.Skip((page - 1) * pageSize).Take(pageSize);

                    return list2.ToList();

                }
                catch
                {
                    totalPages = 0;
                    numRecipes = 0;
                    return null;
                }
            }
        }

        internal Recipe[] GetAllRecipes(int userId)
        {
            //var recipes = from r in
            //            DataContext.Recipes.Where(r => r.IsPublic || r.UserId == userId || userId == USER_ADMIN)
            //            join a in DataContext.RecipeCategories on r.RecipeId equals a.RecipeId
            //              select new
            //              {
            //                  recipe
            //              };

            //return recipes.ToArray();


            //using (DataContext)
            //{
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Recipe>(r => r.User);
                    dlo.LoadWith<Recipe>(r => r.UserFavoriteRecipes);
                    dlo.LoadWith<Recipe>(r => r.MenuRecipes);
                    dlo.LoadWith<Recipe>(r => r.RecipeCategories);
                    dlo.LoadWith<RecipeCategory>(rc => rc.Category);
                    dlo.LoadWith<RecipesInShoppingList>(r => r.RECIPE_ID);
                    DataContext.LoadOptions = dlo;

                    var list =
                        from r in
                            DataContext.Recipes.Where(r => r.IsPublic || r.UserId == userId || userId == USER_ADMIN)
                    select r;

                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            //}
        }

        internal int? GetRecipeMenusCount(int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<MenuRecipe>(mr => mr.RecipeId);
                    DataContext.LoadOptions = dlo;

                    var list = from mr in DataContext.MenuRecipes
                               where mr.RecipeId == recipeId
                               select mr;

                    return list.ToArray().Length;
                }
                catch
                {
                    return null;
                }
            }
        }


        internal IQueryable<RecipesView> GetRecipes(string searchValue, int userId)
        {
            IQueryable<RecipesView> recipes = DataContext.RecipesViews.Where(p => p.RecipeName.Contains(searchValue));
            return recipes;
        }

        internal void AddRecipeToShoppingList(int userId, int recipeId)
        {
            RecipesInShoppingList recipesInShoppingList = DataContext.RecipesInShoppingLists.SingleOrDefault(p => p.USER_ID == userId && p.RECIPE_ID == recipeId);

            if (recipesInShoppingList == null)
            {
                Recipe recipe = DataContext.Recipes.SingleOrDefault(p => p.RecipeId == recipeId);

                recipesInShoppingList = new RecipesInShoppingList()
                {
                    USER_ID = userId,
                    RECIPE_ID = recipeId,
                    SERVINGS = recipe.Servings
                };

                DataContext.RecipesInShoppingLists.InsertOnSubmit(recipesInShoppingList);
                DataContext.SubmitChanges();
            }
        }

        internal IQueryable<RecipesInShoppingList> GetSelectedRecipes(int userId)
        {
            IQueryable<RecipesInShoppingList> recipesInShoppingList = DataContext.RecipesInShoppingLists.Where(p => p.USER_ID == userId);
            return recipesInShoppingList;
        }

        internal void RemoveRecipeFromShoppingList(int userId, int recipeId)
        {
            RecipesInShoppingList recipesInShoppingList = DataContext.RecipesInShoppingLists.SingleOrDefault(p => p.USER_ID == userId && p.RECIPE_ID == recipeId);
            DataContext.RecipesInShoppingLists.DeleteOnSubmit(recipesInShoppingList);
            DataContext.SubmitChanges();
        }

        internal IEnumerable<Recipe> SearchRecipes(string searchedText)
        {
            IEnumerable<Recipe> result1 = DataContext.Recipes.Where(p=>p.RecipeName.Contains(searchedText));
            IEnumerable<Recipe> result2 = from p in DataContext.Recipes
                                          join p1 in DataContext.Ingredients on p.RecipeId equals p1.RecipeId
                                          where p1.Food.FoodName.Contains(searchedText)
                                          select p;
            IEnumerable<Recipe> recipes = result1.Concat(result2);

            return recipes;
        }
    }
}
