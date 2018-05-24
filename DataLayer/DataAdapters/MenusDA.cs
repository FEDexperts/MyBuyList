using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

using MyBuyList.Shared.Entities;
using MyBuyList.Shared.Enums;

namespace MyBuyList.DataLayer.DataAdapters
{
    class MenusDA : BaseContextDataAdapter<MyBuyListDBContext>
    {
        public const int USER_ADMIN = 1;

        internal Menu[] SearchMenus(string searchValue)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Menu>(m => m.MenuType);
                    DataContext.LoadOptions = dlo;

                    var list = DataContext.Menus.Where(mnu => mnu.MenuName.Contains(searchValue) && mnu.IsDeleted == false);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Menu[] GetMenusList(int userId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Menu>(m => m.MenuType);
                    DataContext.LoadOptions = dlo;

                    var list = DataContext.Menus.Where(mnu => mnu.UserId == userId && mnu.IsDeleted == false);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Menu[] GetMenusList(int userId, int tempUser)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Menu>(m => m.MenuType);
                    DataContext.LoadOptions = dlo;

                    var list = DataContext.Menus.Where(mnu => mnu.UserId == userId && mnu.TempUserId == tempUser);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Menu GetMenu(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Menu>(m => m.MenuType);
                    dlo.LoadWith<Menu>(m => m.User);
                    dlo.LoadWith<Menu>(m => m.UserFavoriteMenus);
                    DataContext.LoadOptions = dlo;

                    Menu menu = DataContext.Menus.Single(m => m.MenuId == menuId);
                    return menu;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Menu GetMenuEx(int menuId)
        {
            //using (MyBuyListDBContext context = new MyBuyListDBContext(DBUtils.GetConnection()))
            //using (DataContext)
            //{
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Menu>(m => m.MenuType);
                    dlo.LoadWith<Menu>(m => m.User);
                    dlo.LoadWith<Menu>(m => m.Meals);
                    dlo.LoadWith<Menu>(m => m.MenuRecipes);
                    dlo.LoadWith<Menu>(m => m.MenuCategories);
                    dlo.LoadWith<Menu>(m => m.UserFavoriteMenus);
                    dlo.LoadWith<MenuRecipe>(mr => mr.Recipe);
                    dlo.LoadWith<Meal>(m => m.MealRecipes);
                    dlo.LoadWith<Meal>(m => m.MealType);
                    dlo.LoadWith<MealRecipe>(m => m.Recipe);
                    dlo.LoadWith<MenuCategory>(mc => mc.MCategory);


                    DataContext.LoadOptions = dlo;

                    Menu menu = DataContext.Menus.Single(m => m.MenuId == menuId);
                    return menu;
                }
                catch
                {
                    return null;
                }
            //}
        }

        internal Meal[] GetMenuMeals(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();

                    dlo.LoadWith<Menu>(m => m.Meals);
                    dlo.LoadWith<Meal>(m => m.MealRecipes);
                    dlo.LoadWith<MealRecipe>(m => m.Recipe);

                    DataContext.LoadOptions = dlo;

                    var list = from meal in DataContext.Meals
                               where meal.MenuId == menuId
                               select meal;
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal int GetMenusNum()
        {
            using (DataContext)
            {
                try
                {
                    return DataContext.Menus.Count();
                }
                catch
                {
                    return -1;
                }
            }
        }

        internal MenuType[] GetMenuTypes()
        {
            using (DataContext)
            {
                try
                {
                    var list = DataContext.MenuTypes.OrderBy(mt => mt.SortOrder);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal MealRecipe[] GetMenuMealsRecipes(int menulId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();

                    dlo.LoadWith<MealRecipe>(mr => mr.Recipe);
                    dlo.LoadWith<Recipe>(r => r.Ingredients);
                    dlo.LoadWith<Ingredient>(i => i.Food);
                    dlo.LoadWith<Ingredient>(i => i.FoodName);
                    dlo.LoadWith<Ingredient>(i => i.MeasurementUnit);
                    DataContext.LoadOptions = dlo;

                    var mealsIdList = from meal in DataContext.Meals
                                      where meal.MenuId == menulId
                                      select meal.MealId;

                    var list = DataContext.MealRecipes.Where(m => mealsIdList.Contains(m.MealId));

                    return list.ToArray();

                }
                catch
                {
                    return null;
                }
            }
        }

        internal MenuType GetMenuType(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    Menu menu = DataContext.Menus.Single(m => m.MenuId == menuId);
                    return menu.MenuType;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal int? GetMenuUserId(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    Menu menu = DataContext.Menus.Single(m => m.MenuId == menuId);
                    return menu.UserId;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal int? GetMenuTempUserId(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    Menu menu = DataContext.Menus.Single(m => m.MenuId == menuId);
                    return menu.TempUserId;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal MenuRecipe[] GetMenuRecipes(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<MenuRecipe>(mr => mr.Recipe);
                    dlo.LoadWith<Recipe>(r => r.Ingredients);
                    dlo.LoadWith<Ingredient>(i => i.Food);
                    dlo.LoadWith<Ingredient>(i => i.FoodName);
                    dlo.LoadWith<Ingredient>(i => i.MeasurementUnit);
                    dlo.LoadWith<Ingredient>(i => i.Remarks);
                    //dlo.LoadWith<Ingredient>(i => i.);
                    DataContext.LoadOptions = dlo;

                    var list = DataContext.MenuRecipes.Where(mr => mr.MenuId == menuId);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        //internal MenuRecipe[] GetMenuRecipes(int menuId)
        //{
        //    using (DataContext)
        //    {
        //        try
        //        {
        //            DataLoadOptions dlo = new DataLoadOptions();
        //            dlo.LoadWith<MenuRecipe>(mr => mr.Recipe);
        //            dlo.LoadWith<Recipe>(r => r.Ingredients);
        //            dlo.LoadWith<Ingredient>(i => i.Food);
        //            dlo.LoadWith<Ingredient>(i => i.FoodName);
        //            dlo.LoadWith<Ingredient>(i => i.MeasurementUnit);
        //            //dlo.LoadWith<Ingredient>(i => i.);
        //            DataContext.LoadOptions = dlo;

        //            var list = DataContext.MenuRecipes.Where(mr => mr.MenuId == menuId);
        //            return list.ToArray();
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}                

        internal bool CreateMenu(int menuTypeId, int userId, int tempUser, out int menuId)
        {
            using (DataContext)
            {
                try
                {

                    string menuTypeName = DataContext.MenuTypes.Single(mt => mt.MenuTypeId == menuTypeId).MenuTypeName;
                    DateTime currentDate = DateTime.Now;

                    Menu menu = new Menu();
                    menu.MenuName = menuTypeName + " " + currentDate.ToString("dd/MM/yyyy HH:mm:ss");
                    menu.MenuTypeId = menuTypeId;
                    menu.CreatedDate = currentDate;
                    menu.ModifiedDate = currentDate;
                    menu.UserId = userId;

                    if (tempUser != 0)
                    {
                        menu.TempUserId = tempUser;
                    }

                    var list = DataContext.Menus.Where(m => m.UserId == userId);
                    if (list.ToArray().Length == 0)
                    {
                        menu.SortOrder = 1;
                    }
                    else
                    {
                        menu.SortOrder = list.Max(m => m.SortOrder) + 1;
                    }

                    DataContext.Menus.InsertOnSubmit(menu);
                    DataContext.SubmitChanges();

                    menuId = menu.MenuId;
                    return true;
                }
                catch
                {
                    menuId = 0;
                    return false;
                }
            }
        }

        internal bool CreateMenuEx(int menuTypeId, int userId, int tempUser, string menuName, string description, bool isPublic, out int menuId)
        {
            using (DataContext)
            {
                try
                {

                    string menuTypeName = DataContext.MenuTypes.Single(mt => mt.MenuTypeId == menuTypeId).MenuTypeName;
                    DateTime currentDate = DateTime.Now;

                    if (menuName == string.Empty)
                    {
                        menuName = menuTypeName + " " + currentDate.ToString("dd/MM/yyyy HH:mm:ss");
                    }

                    Menu menu = new Menu();
                    menu.MenuName = menuName;
                    menu.MenuTypeId = menuTypeId;
                    menu.CreatedDate = currentDate;
                    menu.ModifiedDate = currentDate;
                    menu.UserId = userId;
                    menu.Description = description;
                    menu.IsPublic = isPublic;

                    if (tempUser != 0)
                    {
                        menu.TempUserId = tempUser;
                    }

                    var list = DataContext.Menus.Where(m => m.UserId == userId);
                    if (list.ToArray().Length == 0)
                    {
                        menu.SortOrder = 1;
                    }
                    else
                    {
                        menu.SortOrder = list.Max(m => m.SortOrder) + 1;
                    }

                    DataContext.Menus.InsertOnSubmit(menu);
                    DataContext.SubmitChanges();

                    menuId = menu.MenuId;
                    return true;
                }
                catch
                {
                    menuId = 0;
                    return false;
                }
            }
        }

        internal bool CreateMenuEx1(Menu menu, int tempUser, out int menuId)
        {
            using (DataContext)
            {
                try
                {
                    string menuTypeName = DataContext.MenuTypes.Single(mt => mt.MenuTypeId == menu.MenuTypeId).MenuTypeName;
                    DateTime currentDate = DateTime.Now;

                    if (menu.MenuName == string.Empty)
                    {
                        menu.MenuName = menuTypeName + " " + currentDate.ToString("dd/MM/yyyy HH:mm:ss");
                    }

                    menu.CreatedDate = currentDate;
                    menu.ModifiedDate = currentDate;

                    if (tempUser != 0)
                    {
                        menu.TempUserId = tempUser;
                    }

                    var list = DataContext.Menus.Where(m => m.UserId == menu.UserId);
                    if (list.ToArray().Length == 0)
                    {
                        menu.SortOrder = 1;
                    }
                    else
                    {
                        menu.SortOrder = list.Max(m => m.SortOrder) + 1;
                    }

                    Menu newMenu = new Menu();
                    newMenu.MenuName = menu.MenuName;
                    newMenu.MenuTypeId = menu.MenuTypeId;
                    newMenu.Description = menu.Description;
                    newMenu.CreatedDate = menu.CreatedDate;
                    newMenu.ModifiedDate = menu.ModifiedDate;
                    newMenu.SortOrder = menu.SortOrder;
                    newMenu.UserId = menu.UserId;
                    newMenu.IsPublic = menu.IsPublic;

                    Array.ForEach(
                        menu.MenuRecipes.ToArray(),
                        mr =>
                            newMenu.MenuRecipes.Add(
                                                    new MenuRecipe()
                                                    {
                                                        //Menu = newMenu,
                                                        RecipeId = mr.RecipeId
                                                    }));

                    DataContext.Menus.InsertOnSubmit(newMenu);
                    DataContext.SubmitChanges();

                    menuId = newMenu.MenuId;
                    return true;
                }
                catch
                {
                    menuId = 0;
                    return false;
                }
            }
        }

        internal void CreateOrUpdateMenu(Menu menu, out int menuId)
        {
            using (MyBuyListDBContext context = new MyBuyListDBContext(DBUtils.GetConnection()))
            //using (DataContext)
            {
                //try
                //{
                    string menuTypeName = DataContext.MenuTypes.Single(mt => mt.MenuTypeId == menu.MenuTypeId).MenuTypeName;
                    DateTime currentDate = DateTime.Now;
                    if (menu.MenuName == string.Empty)
                    {
                        menu.MenuName = menuTypeName + " " + currentDate.ToString("dd/MM/yyyy HH:mm:ss");
                    }

                    Menu menuToSave;

                    if (menu.MenuId != 0)
                    {
                        DataLoadOptions dlo = new DataLoadOptions();
                        dlo.LoadWith<Menu>(m => m.MenuType);
                        dlo.LoadWith<Menu>(m => m.User);
                        dlo.LoadWith<Menu>(m => m.Meals);
                        dlo.LoadWith<Menu>(m => m.MenuRecipes);
                        dlo.LoadWith<Menu>(m => m.MenuCategories);
                        dlo.LoadWith<MenuRecipe>(mr => mr.Recipe);
                        dlo.LoadWith<Meal>(m => m.MealRecipes);
                        dlo.LoadWith<Meal>(m => m.MealType);
                        dlo.LoadWith<MealRecipe>(m => m.Recipe);
                        dlo.LoadWith<MenuCategory>(mc => mc.MCategory);

                        context.LoadOptions = dlo;

                        menuToSave = context.Menus.Single(m => m.MenuId == menu.MenuId);

                        menuToSave.ModifiedDate = currentDate;
                    }
                    else
                    {
                        menuToSave = new Menu
                            {
                                CreatedDate = currentDate,
                                ModifiedDate = currentDate,
                                UserId = menu.UserId
                            };

                        var list = context.Menus.Where(m => m.UserId == menu.UserId);
                        if (list.ToArray().Length == 0)
                        {
                            menu.SortOrder = 1;
                        }
                        else
                        {
                            menu.SortOrder = list.Max(m => m.SortOrder) + 1;
                        }

                        menuToSave.SortOrder = menu.SortOrder;

                        menuToSave.MenuTypeId = menu.MenuTypeId; //for now - it's not possible to change menu type for an existing menu.
                    }

                    menuToSave.MenuName = menu.MenuName;
                    menuToSave.Description = menu.Description;
                    menuToSave.Tags = menu.Tags;
                    menuToSave.EmbeddedVideo = menu.EmbeddedVideo;
                    menuToSave.Picture = menu.Picture;
                    menuToSave.IsPublic = menu.IsPublic;

                    foreach (MenuRecipe mr in menu.MenuRecipes.ToArray())
                    {
                        if (menuToSave.MenuRecipes.SingleOrDefault(recipe => recipe.RecipeId == mr.RecipeId) == null)
                        {
                            menuToSave.MenuRecipes.Add(new MenuRecipe() { RecipeId = mr.RecipeId });
                        }
                    }
                    for (int i = 0; i < menuToSave.MenuRecipes.ToArray().Length; i++)
                    {
                        if (menu.MenuRecipes.SingleOrDefault(recipe => recipe.RecipeId == menuToSave.MenuRecipes.ToArray()[i].RecipeId) == null)
                        {
                            context.MenuRecipes.DeleteOnSubmit(menuToSave.MenuRecipes.ToArray()[i]); //it works, but is it correct?
                            //menuToSave.MenuRecipes.Remove(menuToSave.MenuRecipes.ToArray()[i]);
                        }
                    }

                    foreach (MenuCategory mr in menu.MenuCategories.ToArray())
                    {
                        if (menuToSave.MenuCategories.SingleOrDefault(mcat => mcat.MCategoryId == mr.MCategoryId) == null)
                        {
                            menuToSave.MenuCategories.Add(new MenuCategory() { MCategoryId = mr.MCategoryId });
                        }
                    }
                    for (int i = 0; i < menuToSave.MenuCategories.ToArray().Length; i++)
                    {
                        if (menu.MenuCategories.SingleOrDefault(mcat => mcat.MCategoryId == menuToSave.MenuCategories.ToArray()[i].MCategoryId) == null)
                        {
                            context.MenuCategories.DeleteOnSubmit(menuToSave.MenuCategories.ToArray()[i]);
                        }
                    }

                    foreach (Meal meal in menu.Meals.ToArray())
                    {
                        if (meal.MealId == 0)
                        {
                            Meal newMeal = new Meal
                                {
                                    CourseTypeId = meal.CourseTypeId,
                                    DayIndex = meal.DayIndex,
                                    MealTypeId = meal.MealTypeId,
                                    CreatedDate = meal.CreatedDate,
                                    ModifiedDate = meal.ModifiedDate,
                                    Diners = meal.Diners
                                };

                            foreach (MealRecipe mealRecipe in meal.MealRecipes.ToArray())
                            {
                                //newMeal.MealRecipes.Add(new MealRecipe() { RecipeId = mealRecipe.RecipeId, Servings = mealRecipe.Servings });
                                newMeal.MealRecipes.Add(new MealRecipe() { RecipeId = mealRecipe.RecipeId, Servings = mealRecipe.ExpectedServings });
                            }

                            menuToSave.Meals.Add(newMeal);
                        }
                        else
                        {
                            Meal sameMeal = menuToSave.Meals.SingleOrDefault(sm => sm.MealId == meal.MealId);
                            if (sameMeal != null)
                            {
                                bool modified = false;
                                if (sameMeal.Diners != meal.Diners)
                                {
                                    sameMeal.Diners = meal.Diners;
                                    modified = true;
                                }
                                foreach (MealRecipe mealRecipe in meal.MealRecipes.ToArray())
                                {
                                    MealRecipe sameMealRecipe = sameMeal.MealRecipes.SingleOrDefault(smr => smr.RecipeId == mealRecipe.RecipeId);
                                    if (sameMealRecipe == null)
                                    {
                                        sameMeal.MealRecipes.Add(new MealRecipe() { RecipeId = mealRecipe.RecipeId, Servings = mealRecipe.Servings });
                                        modified = true;
                                    }
                                    else if (sameMealRecipe.Servings != mealRecipe.Servings)
                                    {
                                        sameMealRecipe.Servings = mealRecipe.Servings;
                                        modified = true;
                                    }
                                }

                                if (modified)
                                {
                                    sameMeal.ModifiedDate = currentDate;
                                }
                            }
                        }
                    }

                    if (menu.MenuId != 0)
                    {
                        for (int i = 0; i < menuToSave.Meals.ToArray().Length; i++)
                        {
                            Meal menuToSaveMeal = menuToSave.Meals.ToArray()[i];

                            Meal menuMeal = null;
                            if (menuToSave.MenuTypeId == 1) //one meal
                            {
                                menuMeal = menu.Meals.SingleOrDefault(mm => mm.CourseTypeId == menuToSaveMeal.CourseTypeId);
                            }
                            if (menuToSave.MenuTypeId == 4) //quick meal
                            {
                                menuMeal = menu.Meals.SingleOrDefault(mm => mm.MealTypeId == menuToSaveMeal.MealTypeId);
                            }
                            if (menuToSave.MenuTypeId == 2) //weekly
                            {
                                menuMeal = menu.Meals.SingleOrDefault(mm => mm.DayIndex == menuToSaveMeal.DayIndex && mm.MealTypeId == menuToSaveMeal.MealTypeId);
                            }

                            if (menuMeal == null)   //meal that was erased (emptied) during edit - delete all meal recipes and then delete meal.
                            {
                                for (int j = 0; j < menuToSaveMeal.MealRecipes.ToArray().Length; j++)
                                {
                                    context.MealRecipes.DeleteOnSubmit(menuToSaveMeal.MealRecipes.ToArray()[j]);
                                }

                                context.Meals.DeleteOnSubmit(menuToSaveMeal);
                            }
                            else    //meal that still exists - check all meal recipes if any were erased. is so - erase meal recipe from menuToSaveMeal.
                            {
                                for (int j = 0; j < menuToSaveMeal.MealRecipes.ToArray().Length; j++)
                                {
                                    if (menuMeal.MealRecipes.SingleOrDefault(mmr => mmr.RecipeId == menuToSaveMeal.MealRecipes.ToArray()[j].RecipeId) == null)
                                    {
                                        context.MealRecipes.DeleteOnSubmit(menuToSaveMeal.MealRecipes.ToArray()[j]);
                                        menuToSaveMeal.MealRecipes.Remove(menuToSaveMeal.MealRecipes.ToArray()[j]);
                                    }
                                }
                            }
                        }
                    }

                    if (menu.MenuId == 0)
                    {
                        context.Menus.InsertOnSubmit(menuToSave);
                    }

                    context.SubmitChanges();

                    menuId = menuToSave.MenuId;
                    //return true;
                //}
                //catch
                //{
                //    menuId = 0;
                //    return false;
                //}
            }
        }

        internal bool UpdateMenuNameAndDescription(int menuId, string menuName, string description)
        {
            using (DataContext)
            {
                try
                {
                    Menu menu = DataContext.Menus.Single(m => m.MenuId == menuId);
                    menu.MenuName = menuName;
                    menu.Description = description;
                    menu.ModifiedDate = DateTime.Now;

                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateMenuUser(int menuId, int userId)
        {
            using (DataContext)
            {
                try
                {
                    Menu menu = DataContext.Menus.Single(m => m.MenuId == menuId);
                    menu.UserId = userId;
                    //menu.ModifiedDate = DateTime.Now;

                    DataContext.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool DeleteMenu(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    Menu menu = DataContext.Menus.Single(m => m.MenuId == menuId);
                    menu.IsDeleted = true;
                    menu.ModifiedDate = DateTime.Now;

                    var menuRecipesToDelete = from mr in DataContext.MenuRecipes
                                              where mr.MenuId == menuId
                                              select mr;

                    DataContext.MenuRecipes.DeleteAllOnSubmit(menuRecipesToDelete);

                    var mealsToDelete = from ml in DataContext.Meals
                                        where ml.MenuId == menuId
                                        select ml;

                    DataContext.Meals.DeleteAllOnSubmit(mealsToDelete);

                    DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool RemoveMenuRecipe(int menuId, int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    MenuRecipe mnuRecipe = DataContext.MenuRecipes.Single(mr => mr.MenuId == menuId && mr.RecipeId == recipeId);
                    DataContext.MenuRecipes.DeleteOnSubmit(mnuRecipe);
                    DataContext.SubmitChanges();

                    //delete this recipe from all meals for menuId
                    MealRecipe[] mealRecipes = DataContext.MealRecipes.Where(mr => mr.RecipeId == recipeId &&
                                                                                   mr.Meal.MenuId == menuId).ToArray();
                    DataContext.MealRecipes.DeleteAllOnSubmit(mealRecipes);
                    DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool CheckIfMenuRecipeExistInMeals(int menuId, int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    bool check = DataContext.MealRecipes.Any(mr => mr.RecipeId == recipeId &&
                                                             mr.Meal.MenuId == menuId);
                    //if (list != null)
                    //{
                    //    return true;
                    //}
                    //else
                    //{
                    return check;
                    // }

                }
                catch
                {
                    return false;
                }
            }
        }

        internal bool AddMenuRecipe(int menuId, int recipeId)
        {
            using (DataContext)
            {
                try
                {
                    MenuRecipe mnuRecipe = DataContext.MenuRecipes.SingleOrDefault(mr => mr.MenuId == menuId && mr.RecipeId == recipeId);
                    if (mnuRecipe == null)
                    {
                        mnuRecipe = new MenuRecipe();
                        mnuRecipe.MenuId = menuId;
                        mnuRecipe.RecipeId = recipeId;
                        DataContext.MenuRecipes.InsertOnSubmit(mnuRecipe);
                    }

                    DataContext.SubmitChanges();
                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }

        internal bool AddMenuToUserFavorites(int userId, int menuId, out int favMenusNum)
        {
            using (DataContext)
            {
                try
                {
                    UserFavoriteMenu favMenu = DataContext.UserFavoriteMenus.SingleOrDefault(ufm => ufm.UserId == userId &&
                                                                                                      ufm.MenuId == menuId);
                    if (favMenu == null)
                    {
                        favMenu = new UserFavoriteMenu();
                        favMenu.UserId = userId;
                        favMenu.MenuId = menuId;
                        DataContext.UserFavoriteMenus.InsertOnSubmit(favMenu);
                        DataContext.SubmitChanges();
                    }

                    favMenusNum = DataContext.UserFavoriteMenus.Where(ufr => ufr.UserId == userId && ufr.Menu.IsDeleted == false && (ufr.Menu.IsPublic == true || ufr.Menu.UserId == userId || userId == USER_ADMIN)).Count();

                    return true;
                }
                catch
                {
                    favMenusNum = 0;
                    return false;
                }
            }
        }

        internal bool RemoveMenuFromUserFavorites(int userId, int menuId, out int favMenusNum)
        {
            using (DataContext)
            {
                try
                {
                    UserFavoriteMenu favMenu = DataContext.UserFavoriteMenus.SingleOrDefault(ufm => ufm.UserId == userId &&
                                                                                                      ufm.MenuId == menuId);
                    DataContext.UserFavoriteMenus.DeleteOnSubmit(favMenu);
                    DataContext.SubmitChanges();

                    favMenusNum = DataContext.UserFavoriteMenus.Where(ufr => ufr.UserId == userId && ufr.Menu.IsDeleted == false && (ufr.Menu.IsPublic == true || ufr.Menu.UserId == userId || userId == USER_ADMIN)).Count();

                    return true;
                }
                catch
                {
                    favMenusNum = 0;
                    return false;
                }
            }
        }

        internal int GetMenuMaxDay(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    var days = from meal in DataContext.Meals
                               where meal.MenuId == menuId
                               select meal.DayIndex;


                    return days.Max().Value;
                }
                catch
                {
                    return 0;
                }
            }
        }

        internal IEnumerable<Menu> GetMenus(int userid, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages)
        {
            using (MyBuyListDBContext context = new MyBuyListDBContext(DBUtils.GetConnection()))
            //using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Menu>(m => m.User);

                    DataContext.LoadOptions = dlo;

                    var count = (from m in DataContext.Menus
                                 where m.IsDeleted == false && (m.IsPublic == true || m.UserId == userid)
                                 select m).Count();

                    totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

                    var list = from m in DataContext.Menus
                               where m.IsDeleted == false && (m.IsPublic == true || m.UserId == userid)
                               select m;

                    if (userid == 1)
                    {
                        count = (from m in DataContext.Menus
                                 where m.IsDeleted == false
                                 select m).Count();

                        totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

                        list = from m in DataContext.Menus
                               where m.IsDeleted == false
                               select m;
                    }




                    switch (orderBy)
                    {
                        case RecipeOrderEnum.Name:
                            list = list.OrderBy(m => m.MenuName);
                            break;
                        case RecipeOrderEnum.Publisher:
                            list = list.OrderBy(m => m.User.DisplayName);
                            break;
                        case RecipeOrderEnum.LastUpdate:
                            list = list.OrderByDescending(m => m.ModifiedDate);
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

        internal IEnumerable<Menu> GetMenusEx(RecipeDisplayEnum display, int userId, string freeText, int? categoryId, int? diners, int[] menuCats, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages, out int numMenus)
        {
            using (MyBuyListDBContext context = new MyBuyListDBContext(DBUtils.GetConnection()))            
            {
                try
                {
                    List<Menu> MenusList = null;

                    switch (display)
                    {
                        case RecipeDisplayEnum.All:
                            MenusList = this.GetAllMenus(userId).ToList();
                            break;
                        case RecipeDisplayEnum.MyRecipes:
                            if (!string.IsNullOrEmpty(freeText))
                                MenusList = this.GetUserMenusList(userId).Where(r => r.MenuName.Contains(freeText)).ToList();
                            else
                                MenusList = this.GetUserMenusList(userId).ToList();
                            break;
                        case RecipeDisplayEnum.MyFavoriteRecipes:
                            if (!string.IsNullOrEmpty(freeText))
                                MenusList = this.GetUserFavoritesMenus(userId).Where(r => r.MenuName.Contains(freeText)).ToList();
                            else
                                MenusList = this.GetUserFavoritesMenus(userId).ToList();
                            break;
                        case RecipeDisplayEnum.ByCategory:
                            if (categoryId.HasValue)
                            {
                                MenusList = this.GetMenusByCategory(categoryId.Value, userId).ToList();
                            }
                            else
                            {
                                MenusList = this.GetAllMenus(userId).ToList();
                            }
                            break;
                        case RecipeDisplayEnum.BySearchSimple:
                            MenusList = this.GetMenusListByFreeText(freeText, userId).ToList();
                            break;
                        case RecipeDisplayEnum.BySearchAdvanced:
                            MenusList = this.GetMenusListByComplexSearch(freeText, diners, menuCats, userId).ToList();
                            break;
                    }

                    var count = (from m in MenusList
                                 select m).Count();

                    numMenus = count;

                    totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

                    var list = from m in MenusList
                               select m;

                    switch (orderBy)
                    {
                        case RecipeOrderEnum.Name:
                            list = list.OrderBy(m => m.MenuName);
                            break;
                        case RecipeOrderEnum.Publisher:
                            list = list.OrderBy(m => m.User.DisplayName);
                            break;
                        case RecipeOrderEnum.LastUpdate:
                            list = list.OrderByDescending(m => m.ModifiedDate);
                            break;
                    }

                    // paging
                    var list2 = list.Skip((page - 1) * pageSize).Take(pageSize);

                    return list2.ToList();

                }
                catch
                {
                    numMenus = 0;
                    totalPages = 0;
                    return null;
                }
            }
        }
        
        internal Menu[] GetAllMenus(int userId)
        {
            //using (DataContext)
            //{
            //    try
            //    {
                    //DataLoadOptions dlo = new DataLoadOptions();
                    //dlo.LoadWith<Menu>(m => m.User);
                    //dlo.LoadWith<Menu>(m => m.UserFavoriteMenus);                    
                    //DataContext.LoadOptions = dlo;

                    var list = from m in DataContext.Menus.Where(m => m.IsDeleted == false && m.IsPublic == true)
                               select m;

                    //var list = from m in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
                    //           select m;

                    return list.ToArray();
            //    }
            //    catch
            //    {
            //        return null;
            //    }
            //}
        }

        internal Menu[] GetUserMenusList(int userId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Menu>(m => m.User);
                    dlo.LoadWith<Menu>(m => m.UserFavoriteMenus);

                    DataContext.LoadOptions = dlo;

                    var list = DataContext.Menus.Where(m => m.IsDeleted == false && m.UserId == userId);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Menu[] GetUserFavoritesMenus(int userId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Menu>(m => m.User);
                    dlo.LoadWith<Menu>(m => m.UserFavoriteMenus);

                    DataContext.LoadOptions = dlo;

                    var list = from m in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
                               join ufm in DataContext.UserFavoriteMenus.Where(ufm => ufm.UserId == userId) on m.MenuId equals ufm.MenuId
                               select m;
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Menu[] GetMenusListByFreeText(string freeText, int userId)
        {
            var menuRecipes = from m in DataContext.Menus.Where(m => m.IsDeleted == false && m.IsPublic == true)
                              join mr in DataContext.MenuRecipes.Where(mr => mr.Recipe.RecipeName.Trim().IndexOf(freeText.Trim()) != -1 ||
                                          mr.Recipe.Remarks.Trim().IndexOf(freeText.Trim()) != -1 ||
                                          mr.Recipe.PreparationMethod.Trim().IndexOf(freeText.Trim()) != -1 ||
                                          mr.Recipe.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1 ||
                                          mr.Recipe.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1)
                              on m.MenuId equals mr.MenuId
                              select m.MenuId;

            var list = from m1 in DataContext.Menus.Where(m => m.IsDeleted == false && m.IsPublic &&
                                                (m.MenuName.Trim().IndexOf(freeText.Trim()) != -1 ||
                                                 m.MenuType.MenuTypeName.Trim().IndexOf(freeText.Trim()) != -1 ||
                                                 m.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1 ||
                                                 m.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1
                                                 ))
                       select m1.MenuId;

            var menuCategories = from m2 in DataContext.Menus.Where(m => m.IsDeleted == false && m.IsPublic == true)
                                 join mc in DataContext.MenuCategories.Where(mc => (mc.MCategory.MCategoryName.Trim().IndexOf(freeText.Trim()) != -1))
                                                                                     on m2.MenuId equals mc.MenuId
                                 select m2.MenuId;

            //var list1 = list.Union(menuRecipes);
            var tmpList = list.Union(menuRecipes);
            var list1 = tmpList.Union(menuCategories);

            return DataContext.Menus.Where(m => list1.Contains(m.MenuId)).ToArray();

        }


        //internal Menu[] GetMenusListByFreeText(string freeText, int userId)
        //{
        //    using (DataContext)
        //    {
        //        try
        //        {
        //            DataLoadOptions dlo = new DataLoadOptions();
        //            dlo.LoadWith<Menu>(m => m.User);
        //            dlo.LoadWith<Menu>(m => m.UserFavoriteMenus);

        //            DataContext.LoadOptions = dlo;

        //            var menuRecipes = from m in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
        //                              join mr in DataContext.MenuRecipes.Where(mr => mr.Recipe.RecipeName.Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                          mr.Recipe.Remarks.Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                          mr.Recipe.PreparationMethod.Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                          mr.Recipe.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                          mr.Recipe.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1)
        //                              on m.MenuId equals mr.MenuId
        //                              select m.MenuId;

        //            var list = from m1 in DataContext.Menus.Where(m => (m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN)) &&
        //                                                (m.MenuName.Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                                 m.MenuType.MenuTypeName.Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                                 m.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1 ||
        //                                                 m.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1
        //                                                 ))
        //                       select m1.MenuId;

        //            var menuCategories = from m2 in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
        //                        join mc in DataContext.MenuCategories.Where(mc => (mc.MCategory.MCategoryName.Trim().IndexOf(freeText.Trim()) != -1))
        //                                                                            on m2.MenuId equals mc.MenuId
        //                        select m2.MenuId;

        //            //var list1 = list.Union(menuRecipes);
        //            var tmpList = list.Union(menuRecipes);
        //            var list1 = tmpList.Union(menuCategories);

        //            return DataContext.Menus.Where(m => list1.Contains(m.MenuId)).ToArray();

        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

        internal Menu[] GetMenusListByComplexSearch(string freeText, int? diners, int[] menuCats, int userId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Menu>(m => m.User);
                    dlo.LoadWith<Menu>(m => m.UserFavoriteMenus);

                    DataContext.LoadOptions = dlo;

                    var menuRecipes = from m in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
                                      join mr in DataContext.MenuRecipes.Where(mr => mr.Recipe.RecipeName.Trim().IndexOf(freeText.Trim()) != -1 ||
                                                  mr.Recipe.Remarks.Trim().IndexOf(freeText.Trim()) != -1 ||
                                                  mr.Recipe.PreparationMethod.Trim().IndexOf(freeText.Trim()) != -1 ||
                                                  mr.Recipe.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1 ||
                                                  mr.Recipe.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1)
                                      on m.MenuId equals mr.MenuId                                      
                                      select m.MenuId;

                    var menus = from m1 in DataContext.Menus.Where(m => (m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN)) &&
                                                        (m.MenuName.Trim().IndexOf(freeText.Trim()) != -1 ||
                                                         m.MenuType.MenuTypeName.Trim().IndexOf(freeText.Trim()) != -1 ||
                                                         m.Description.ToString().Trim().IndexOf(freeText.Trim()) != -1 ||
                                                         m.Tags.ToString().Trim().IndexOf(freeText.Trim()) != -1
                                                         ))
                               select m1.MenuId;

                    var menuCategories = from m2 in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
                                         join mc in DataContext.MenuCategories.Where(mc => (mc.MCategory.MCategoryName.Trim().IndexOf(freeText.Trim()) != -1))
                                                                                             on m2.MenuId equals mc.MenuId
                                         select m2.MenuId;

                    //var list1 = menus.Union(menuRecipes);
                    var tmpList = menus.Union(menuRecipes);
                    var list1 = tmpList.Union(menuCategories);                    

                    var list = DataContext.Menus.Where(m => list1.Contains(m.MenuId));

                    if (diners.HasValue)
                    {
                        
                        //list = list.Where(m => m >= diners.Value);
                    }

                    if (menuCats != null && menuCats.Length != 0)
                    {
                        List<Menu> temp = new List<Menu>();

                        foreach (int categoryId in menuCats)
                        {
                            foreach (MenuCategory mcat in DataContext.MenuCategories.Where(mc => mc.MCategoryId == categoryId ||
                                                                                                    mc.MCategory.ParentMCategoryId == categoryId))
                            {
                                if (!temp.Contains(mcat.Menu))
                                {
                                    temp.Add(mcat.Menu);
                                }
                            }
                        }

                        list = (from m in list
                                where temp.Contains(m)
                                select m);
                    }

                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal Menu[] GetMenusByCategory(int categoryId, int userId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Menu>(m => m.User);
                    dlo.LoadWith<Menu>(m => m.UserFavoriteMenus);
                    DataContext.LoadOptions = dlo;

                    var list = from m in DataContext.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
                               join mcat in DataContext.MenuCategories.Where(mc => mc.MCategoryId == categoryId ||
                                                                            mc.MCategory.ParentMCategoryId == categoryId)
                               on m.MenuId equals mcat.MenuId
                               select m;

                    return (from m in DataContext.Menus
                            where list.Contains(m)
                            select m).ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal MCategory[] GetMenusCategoriesList(int userId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<Menu>(r => r.User);
                    DataContext.LoadOptions = dlo;
                    var list = DataContext.MCategories.OrderBy(mcat => mcat.SortOrder);
                    foreach (MCategory item in list)
                    {
                        item.ParentMCategories.Load();
                        item.MenusCount = GetMCategoryMenusNum(item.MCategoryId, userId);
                    }
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal int GetMCategoryMenusNum(int categoryId, int userId)
        {
            using (MyBuyListDBContext context = new MyBuyListDBContext(DBUtils.GetConnection()))
            {
                var list = from m in context.Menus.Where(m => m.IsDeleted == false && (m.IsPublic == true || m.UserId == userId || userId == USER_ADMIN))
                           join mcat in context.MenuCategories.Where(mc => mc.MCategoryId == categoryId ||
                                                                        mc.MCategory.ParentMCategoryId == categoryId)
                           on m.MenuId equals mcat.MenuId
                           select m;

                return (from m in context.Menus
                        where list.Contains(m)
                        select m).ToArray().Length;
            }
        }

        internal void AddMenuToShoppingList(int userId, int menuId, bool check)
        {
            MenusInShoppingList menusInShoppingList = DataContext.MenusInShoppingLists.SingleOrDefault(p => p.UserId == userId && p.MenuId == menuId);

            if (!check)
            {
                if (menusInShoppingList != null)
                {
                    DataContext.MenusInShoppingLists.DeleteOnSubmit(menusInShoppingList);
                }
            }
            else
            {
                if (menusInShoppingList == null)
                {
                    menusInShoppingList = new MenusInShoppingList
                    {
                        UserId = userId,
                        MenuId = menuId
                    };
                    DataContext.MenusInShoppingLists.InsertOnSubmit(menusInShoppingList);
                }
            }
            DataContext.SubmitChanges();
        }

        internal IQueryable<MenusInShoppingList> GetMenusInShoppingList(int userId)
        {
            IQueryable<MenusInShoppingList> menusInShoppingList = DataContext.MenusInShoppingLists.Where(p => p.UserId == userId);
            return menusInShoppingList;
        }

        internal void RemoveMenuFromShoppingList(int userId, int menuId)
        {
            MenusInShoppingList menusInShoppingList = DataContext.MenusInShoppingLists.SingleOrDefault(p => p.UserId == userId && p.MenuId == menuId);
            DataContext.MenusInShoppingLists.DeleteOnSubmit(menusInShoppingList);
            DataContext.SubmitChanges();
        }
    }
}
