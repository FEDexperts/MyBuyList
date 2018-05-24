using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

using MyBuyList.Shared.Entities;

namespace MyBuyList.DataLayer.DataAdapters
{
    class MealsDA : BaseContextDataAdapter<MyBuyListDBContext>
    {
        internal MealRecipe[] GetMealsWeeklyList(int menuId, int startDayIndex, int endDayIndex)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<MealRecipe>(mr => mr.Meal);
                    dlo.LoadWith<MealRecipe>(mr => mr.Recipe);
                    DataContext.LoadOptions = dlo;

                    var meals = DataContext.Meals.Where(m => m.MenuId == menuId &&
                                                        m.DayIndex >= startDayIndex &&
                                                        m.DayIndex <= endDayIndex);

                    var list = from mr in DataContext.MealRecipes
                               join m in meals on mr.MealId equals m.MealId
                               select mr;
                    return list.ToArray();

                }
                catch
                {
                    return null;
                }
            }
        }

        internal CourseType[] GetCourseTypes()
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.CourseTypes.OrderBy(ct => ct.SortOrder);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal MealType[] GetMealTypes()
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.MealTypes.OrderBy(mt => mt.SortOrder);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Meal[] GetMealsList(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<MealRecipe>(mr => mr.Recipe);
                    DataContext.LoadOptions = dlo;

                    var list = DataContext.Meals.Where(m => m.MenuId == menuId);
                    foreach (Meal item in list)
                    {
                        item.MealRecipes.Load();
                    }
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Meal GetMeal(int mealId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Meal>(m => m.MealRecipes);
                    DataContext.LoadOptions = dlo;

                    Meal item = DataContext.Meals.SingleOrDefault(m => m.MealId == mealId);

                    return item;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Meal GetMeal(int menuId, int dayIndex, int mealTypeId)
        {
            using (DataContext)
            { 
                return GetMeal(DataContext, menuId, dayIndex, mealTypeId);
            }
        }

        internal Meal GetMeal(int menuId, int courseTypeId)
        {
            using (DataContext)
            {
                return GetMeal(DataContext, menuId, courseTypeId);
            }
        }

        private Meal GetMeal(MyBuyListDBContext dc, int menuId, int courseTypeId)
        {
            try
            {
                DataLoadOptions dlo = new DataLoadOptions();
                dlo.LoadWith<Meal>(m => m.MealRecipes);
                dc.LoadOptions = dlo;

                Meal item = dc.Meals.SingleOrDefault(m => m.MenuId == menuId &&
                                                          m.CourseTypeId == courseTypeId);

                return item;
            }
            catch
            {
                return null;
            }
        }

        private Meal GetMeal(MyBuyListDBContext dc, int menuId, int dayIndex, int mealTypeId)
        {
            try
            {
                DataLoadOptions dlo = new DataLoadOptions();
                dlo.LoadWith<Meal>(m => m.MealRecipes);
                dc.LoadOptions = dlo;

                Meal item = dc.Meals.SingleOrDefault(m => m.MenuId == menuId &&
                                                          m.DayIndex == dayIndex &&
                                                          m.MealTypeId == mealTypeId);
                return item;
            }
            catch
            {
                return null;
            }
        }

        internal bool SaveMeal(int menuId, int courseTypeId, int? diners)
        {
            using (DataContext)
            {
                int mealId = 0;
                return SaveMeal(DataContext, menuId, courseTypeId, diners, out mealId);
            }
        }

        internal bool SaveMeal(int menuId, int dayIndex, int mealTypeId, int? diners)
        {
            using (DataContext)
            {
                int mealId = 0;
                return SaveMeal(DataContext, menuId, dayIndex, mealTypeId, diners, out mealId);
            }
        }

        private bool SaveMeal(MyBuyListDBContext dc, int menuId, int courseTypeId, int? diners, out int mealId)
        {
            try
            {
                Meal item = dc.Meals.SingleOrDefault(m => m.MenuId == menuId &&
                                                          m.CourseTypeId == courseTypeId);

                int mealTypeId = 4; //Others
                return this.SaveMeal(dc, item, menuId, mealTypeId, null, courseTypeId, diners, out mealId);
            }

            catch
            {
                mealId = 0;
                return false;
            }
        }
        
        private bool SaveMeal(MyBuyListDBContext dc, int menuId, int dayIndex, int mealTypeId, int? diners, out int mealId)
        {
            try
            {
                Meal item = dc.Meals.SingleOrDefault(m => m.MenuId == menuId &&
                                                          m.DayIndex == dayIndex &&
                                                          m.MealTypeId == mealTypeId);

                return this.SaveMeal(dc, item, menuId, mealTypeId, dayIndex, null, diners, out mealId);
            }

            catch
            {
                mealId = 0;
                return false;
            }
        }

        private bool SaveMeal(MyBuyListDBContext dc, Meal item, int menuId, int mealTypeId, int? dayIndex, int? courseTypeId, int? diners, out int mealId)
        {
            try
            {
                if (item == null)
                {
                    if (diners != null)
                    {
                        item = new Meal();
                        item.MenuId = menuId;
                        item.MealTypeId = mealTypeId;
                        item.DayIndex = dayIndex;
                        item.CourseTypeId = courseTypeId;
                        
                        item.Diners = diners;
                        item.CreatedDate = DateTime.Now;
                        item.ModifiedDate = DateTime.Now;
                        dc.Meals.InsertOnSubmit(item);                        
                        dc.SubmitChanges();

                        mealId = item.MealId;
                        return true;
                    }
                    else
                    {
                        mealId = 0;
                        return false;
                    }
                }
                else
                {
                    if (diners != null)
                    {
                        item.Diners = diners;
                        item.ModifiedDate = DateTime.Now;                       
                        mealId = item.MealId;                       
                    }
                    else
                    {
                        dc.Meals.DeleteOnSubmit(item);
                        mealId = 0;
                    }

                    dc.SubmitChanges();
                    return true;
                }               
            }

            catch
            {
                mealId = 0;
                return false;
            }
        }

        internal MealRecipe[] GetMealRecipesList(int menuId, int dayIndex, int mealTypeId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<MealRecipe>(mr => mr.Meal);
                    dlo.LoadWith<MealRecipe>(mr => mr.Recipe);
                    DataContext.LoadOptions = dlo;

                    var meals = DataContext.Meals.Where(m => m.MenuId == menuId &&
                                                             m.DayIndex == dayIndex &&
                                                             m.MealTypeId == mealTypeId);

                    var list = from mr in DataContext.MealRecipes
                               join m in meals on mr.MealId equals m.MealId
                               select mr;
                    return list.ToArray();

                }
                catch
                {
                    return null;
                }
            }
        }

        internal MealRecipe[] GetMealRecipes(int mealId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<MealRecipe>(mr => mr.Meal);
                    dlo.LoadWith<MealRecipe>(mr => mr.Recipe);
                    DataContext.LoadOptions = dlo;

                    var list = DataContext.MealRecipes.Where(m => m.MealId == mealId);

                    return list.ToArray();

                }
                catch
                {
                    return null;
                }
            }
        }
        private int GetRecipeServings(MyBuyListDBContext dc, int recipeId)
        {
            try
            {
                Recipe recipe = dc.Recipes.Single(r => r.RecipeId == recipeId);
                return recipe.Servings;
            }

            catch
            {
                return 0;
            }
        }
        private bool AddMealRecipe(MyBuyListDBContext dc, int mealId, int recipeId, int servings)
        {
            try
            {
                MealRecipe mealRecipe = dc.MealRecipes.SingleOrDefault(mr => mr.MealId == mealId && mr.RecipeId == recipeId);
                if (mealRecipe == null)
                {
                    mealRecipe = new MealRecipe();
                    mealRecipe.MealId = mealId;
                    mealRecipe.RecipeId = recipeId;
                    mealRecipe.Servings = servings;
                    dc.MealRecipes.InsertOnSubmit(mealRecipe);

                    dc.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }               
            }

            catch
            {
                return false;
            }
        }

        internal bool AddMealRecipe(int mealId, int recipeId)
        {
            using (DataContext)
            {
                return this.AddMealRecipe(DataContext, mealId, recipeId, 1);
            }
        }
        internal bool AddMealRecipe(int menuId, int courseTypeId, int recipeId, out int mealId)
        {
            using (DataContext)
            {
                try
                {
                    Meal meal = this.GetMeal(DataContext, menuId, courseTypeId);

                    int servings = this.GetRecipeServings(DataContext, recipeId);
                    if (meal == null)
                    {
                        

                        mealId = 0;
                        if (SaveMeal(DataContext, menuId, courseTypeId, servings, out mealId))
                        {
                           return this.AddMealRecipe(DataContext, mealId, recipeId, servings);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        mealId = meal.MealId;
                        return this.AddMealRecipe(DataContext, meal.MealId, recipeId, servings);
                    }
                }
                catch
                {
                    mealId = 0;
                    return false;
                }
            }
        }

        internal bool AddMealRecipe(int menuId, int dayIndex, int mealTypeId, int recipeId, out int mealId)
        {
            using (DataContext)
            {
                try
                {
                    Meal meal = this.GetMeal(DataContext, menuId, dayIndex, mealTypeId);

                    int servings = this.GetRecipeServings(DataContext, recipeId);

                    if (meal == null)
                    {
                        mealId = 0;
                        if (SaveMeal(DataContext, menuId, dayIndex, mealTypeId, servings, out mealId))
                        {
                            return this.AddMealRecipe(DataContext, mealId, recipeId, servings);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        mealId = meal.MealId;
                        return this.AddMealRecipe(DataContext, meal.MealId, recipeId, servings);
                    }
                }
                catch
                {
                    mealId = 0;
                    return false;
                }
            }
        }

        internal MealRecipe GetMealRecipe(int mealId, int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    MealRecipe mealRecipe = DataContext.MealRecipes.Single(mr => mr.MealId == mealId && mr.RecipeId == recipeId);
                    return mealRecipe;
                }
                catch
                {
                    return null;
                }
            }
        }

        

        internal bool RemoveMealRecipe(int mealId, int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    MealRecipe mealRecipe = DataContext.MealRecipes.Single(mr => mr.MealId == mealId && mr.RecipeId == recipeId);
                    DataContext.MealRecipes.DeleteOnSubmit(mealRecipe);
                    DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool ClearAllMeals(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.Meals.Where(m => m.MenuId == menuId);
                    DataContext.Meals.DeleteAllOnSubmit(list); //Delete also all recipes for each meal (DeleteRule Constraint between tables)
                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool SaveMealRecipe(int mealId, int recipeId, int servings)
        {
            using (DataContext)
            {
                try
                {
                    MealRecipe mealRecipe = DataContext.MealRecipes.Single(mr => mr.MealId == mealId && mr.RecipeId == recipeId);
                    mealRecipe.Servings = servings;

                    DataContext.SubmitChanges();
                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }

        internal bool CreateQuickListMealRecipes(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    MenuRecipe[] menuRecipes = DataContext.MenuRecipes.Where(mr => mr.MenuId == menuId).ToArray();
                    if (menuRecipes.Length == 0) 
                        return false;
                    
                    int mealId = 0;
                    Meal meal = this.GetMeal(DataContext, menuId, 0);
                    if (meal == null)
                    {
                        if (!SaveMeal(DataContext, menuId, 0, 1, out mealId))
                            return false;
                    }
                    else
                    {
                        mealId = meal.MealId;
                    }
                    foreach (MenuRecipe item in menuRecipes)
                    {
                        this.AddMealRecipe(DataContext, mealId, item.RecipeId, item.Recipe.Servings);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
