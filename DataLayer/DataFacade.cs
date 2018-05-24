using System.Collections.Generic;
using System.Linq;

using MyBuyList.Shared.Entities;
using MyBuyList.DataLayer.DataAdapters;
using MyBuyList.Shared.Enums;

namespace MyBuyList.DataLayer
{
    public sealed class DataFacade
    {
        #region Singleton
        private static DataFacade singleton = new DataFacade();

        private DataFacade() { }

        public static DataFacade Instance
        {
            get { return DataFacade.singleton; }
        }
        #endregion

        #region Menus

        public Menu[] GetUserFavoritesMenus(int userId)
        {
            return new MenusDA().GetUserFavoritesMenus(userId);
        }

        public Menu[] SearchMenus(string searchValue)
        {
            return new MenusDA().SearchMenus(searchValue);
        }

        public Menu[] GetUserMenusList(int userId)
        {
            return new MenusDA().GetUserMenusList(userId);
        }

        public Menu[] GetMenusList(int userId)
        {
            return new MenusDA().GetMenusList(userId);
        }

        public Menu GetMenu(int menuId)
        {
            return new MenusDA().GetMenu(menuId);
        }

        public Menu GetMenuEx(int menuId)
        {
            return new MenusDA().GetMenuEx(menuId);
        }

        public MenuType[] GetMenuTypes()
        {
            return new MenusDA().GetMenuTypes();
        }

        public MenuType GetMenuType(int menuId)
        {
            return new MenusDA().GetMenuType(menuId);
        }

        public MenuRecipe[] GetMenuRecipes(int menuId)
        {
            return new MenusDA().GetMenuRecipes(menuId);
        }

        public MealRecipe[] GetMenuMealsRecipes(int menulId)
        {
            return new MenusDA().GetMenuMealsRecipes(menulId);
        }

        public bool CreateMenu(int menuTypeId, int userId, int tempUser, out int menuId)
        {
            return new MenusDA().CreateMenu(menuTypeId, userId, tempUser, out menuId);
        }

        public bool CreateMenuEx(int menuTypeId, int userId, int tempUser, string menuName, string description, bool isPublic, out int menuId)
        {
            return new MenusDA().CreateMenuEx(menuTypeId, userId, tempUser, menuName, description, isPublic, out menuId);
        }

        public bool CreateMenuEx1(Menu menu, int tempUser, out int menuId)
        {
            return new MenusDA().CreateMenuEx1(menu, tempUser, out menuId);
        }

        public void CreateOrUpdateMenu(Menu menu, out int menuId)
        {
            new MenusDA().CreateOrUpdateMenu(menu, out menuId);
        }

        public bool UpdateMenuNameAndDescription(int menuId, string menuName, string description)
        {
            return new MenusDA().UpdateMenuNameAndDescription(menuId, menuName, description);
        }

        public bool DeleteMenu(int menuId)
        {
            return new MenusDA().DeleteMenu(menuId);
        }

        public bool RemoveMenuRecipe(int menuId, int recipeId)
        {
            return new MenusDA().RemoveMenuRecipe(menuId, recipeId);
        }

        public bool AddMenuRecipe(int menuId, int recipeId)
        {
            return new MenusDA().AddMenuRecipe(menuId, recipeId);
        }

        public bool CheckIfMenuRecipeExistInMeals(int menuId, int recipeId)
        {
            return new MenusDA().CheckIfMenuRecipeExistInMeals(menuId, recipeId);
        }

        #endregion Menus

        #region Meals

        public MealRecipe[] GetMealsWeeklyList(int menuId, int startDayIndex, int endDayIndex)
        {
            return new MealsDA().GetMealsWeeklyList(menuId, startDayIndex, endDayIndex);
        }

        public CourseType[] GetCourseTypes()
        {
            return new MealsDA().GetCourseTypes();
        }

        public MealType[] GetMealTypes()
        {
            return new MealsDA().GetMealTypes();
        }
        public Meal[] GetMealsList(int menuId)
        {
            return new MealsDA().GetMealsList(menuId);
        }

        public Meal GetMeal(int mealId)
        {
            return new MealsDA().GetMeal(mealId);
        }

        public Meal GetMeal(int menuId, int courseTypeId)
        {
            return new MealsDA().GetMeal(menuId, courseTypeId);
        }

        public Meal GetMeal(int menuId, int dayIndex, int mealTypeId)
        {
            return new MealsDA().GetMeal(menuId, dayIndex, mealTypeId);
        }

        public bool SaveMeal(int menuId, int courseTypeId, int? diners)
        {
            return new MealsDA().SaveMeal(menuId, courseTypeId, diners);
        }

        public bool SaveMeal(int menuId, int dayIndex, int mealTypeId, int? diners)
        {
            return new MealsDA().SaveMeal(menuId, dayIndex, mealTypeId, diners);
        }

        public MealRecipe[] GetMealRecipesList(int menuId, int dayIndex, int mealTypeId)
        {
            return new MealsDA().GetMealRecipesList(menuId, dayIndex, mealTypeId);
        }

        public bool CreateQuickListMealRecipes(int menuId)
        {
            return new MealsDA().CreateQuickListMealRecipes(menuId);
        }

        public bool AddMealRecipe(int menuId, int courseTypeId, int recipeId, out int mealId)
        {
            return new MealsDA().AddMealRecipe(menuId, courseTypeId, recipeId, out mealId);
        }

        public bool AddMealRecipe(int menuId, int dayIndex, int mealTypeId, int recipeId, out int mealId)
        {
            return new MealsDA().AddMealRecipe(menuId, dayIndex, mealTypeId, recipeId, out mealId);
        }

        public bool RemoveMealRecipe(int mealId, int recipeId)
        {
            return new MealsDA().RemoveMealRecipe(mealId, recipeId);
        }

        public MealRecipe GetMealRecipe(int mealId, int recipeId)
        {
            return new MealsDA().GetMealRecipe(mealId, recipeId);
        }

        public bool ClearAllMeals(int menuId)
        {
            return new MealsDA().ClearAllMeals(menuId);
        }

        public bool SaveMealRecipe(int mealId, int recipeId, int servings)
        {
            return new MealsDA().SaveMealRecipe(mealId, recipeId, servings);
        }

        #endregion Meals

        #region Recipes
        public Recipe[] GetRecipesListByFoodId(int foodId)
        {
            return new RecipesDA().GetRecipesListByFoodId(foodId);
        }

        public IQueryable<Recipe> GetRecipesListByFreeText(string freeText)
        {
            return new RecipesDA().GetRecipesListByFreeText(freeText);
        }

        public Recipe[] GetRecipesListByComplexSearch(string freeText, int? servings, int[] recipeCats, int userId)
        {
            //return new RecipesDA().GetRecipesListByComplexSearch(freeText, servings, recipeCats, userId);
            return null;
        }

        public void AllowRecipe(int recipeId)
        {
            new RecipesDA().AllowRecipe(recipeId);
        }

        public Recipe[] GetUserRecipesList(int userId)
        {
            return new RecipesDA().GetUserRecipesList(userId);
        }

        public Recipe[] GetUserFavoritesRecipes(int userId)
        {
            return new RecipesDA().GetUserFavoritesRecipes(userId);
        }

        public Category[] GetRecipesCategoriesList(int userId)
        {
            return new RecipesDA().GetRecipesCategoriesList(userId);
        }

        public Recipe[] GetRecipesByCategory(int categoryId, int userId)
        {
            //return new RecipesDA().GetRecipesByCategory(categoryId, userId);
            return null;
        }

        public Recipe GetRecipe(int recipeId)
        {
            return new RecipesDA().GetRecipe(recipeId);
        }

        public Recipe[] GetRecipesList()
        {
            return new RecipesDA().GetRecipesList();
        }

        public int GetRecipesNum()
        {
            return new RecipesDA().GetRecipesNum();
        }

        public RecipeIngredientsView[] GetRecipeIngredientsViewList(int recipeId)
        {
            return new RecipesDA().GetRecipeIngredientsViewList(recipeId);
        }

        public Ingredient[] GetRecipeIngredientsList(int recipeId)
        {
            return new RecipesDA().GetRecipeIngredientsList(recipeId);
        }

        public Dictionary<int, string> GetFoodList(string prefixText)
        {
            return new RecipesDA().GetFoodList(prefixText);
        }

        public bool AddRecipeToUserFavorites(int userId, int recipeId, out int favRecipesNum)
        {
            return new RecipesDA().AddRecipeToUserFavorites(userId, recipeId, out favRecipesNum);
        }

        public bool RemoveUserFavoritesRecipe(int userId, int recipeId, out int favRecipesNum)
        {
            return new RecipesDA().RemoveUserFavoritesRecipe(userId, recipeId, out favRecipesNum);
        }

        public bool DeleteRecipe(int recipeId)
        {
            return new RecipesDA().DeleteRecipe(recipeId);
        }

        public bool SaveRecipe(Recipe recipe, out int recipeId)
        {
            return new RecipesDA().SaveRecipe(recipe, out recipeId);
        }

        public bool UpdateRecipePreparationMethod(int recipeId, string preparationMethod)
        {
            return new RecipesDA().UpdateRecipePreparationMethod(recipeId, preparationMethod);
        }

        public bool SaveRecipeCategories(int recipeId, RecipeCategory[] categories)
        {
            return new RecipesDA().SaveRecipeCategories(recipeId, categories);
        }

        public RecipeTotalNutValues[] GetRecipeTotalNutValues(int recipeId, out bool isCompleteCalculation)
        {
            return new RecipesDA().GetRecipeTotalNutValues(recipeId, out isCompleteCalculation);
        }

        #endregion Recipes

        #region Shopping List

        public void RemoveItemFromShoppingList(int userId, int foodId)
        {
            new ShoppingsListDA().RemoveItemFromShoppingList(userId, foodId);
        }

        public ShopDepartment[] GetMenuShopDepartments(int menuId)
        {
            return new ShoppingsListDA().GetMenuShopDepartments(menuId);
        }

        public ShoppingFood[] GetMenuShoppingList(int menuId)
        {
            return new ShoppingsListDA().GetMenuShoppingList(menuId);
        }

        public ShoppingListAdditionalItem[] GetShoppingListAdditionalItems(int menuId)
        {
            return new ShoppingsListDA().GetShoppingListAdditionalItems(menuId);
        }

        public bool DeleteShoppingListAdditionalItem(int itemId)
        {
            return new ShoppingsListDA().DeleteShoppingListAdditionalItem(itemId);
        }

        public bool AddShoppingListAdditionalItem(int menuId, string itemName)
        {
            return new ShoppingsListDA().AddShoppingListAdditionalItem(menuId, itemName);
        }

        public string[] GetGeneralItemsList(string prefixText)
        {
            return new ShoppingsListDA().GetGeneralItemsList(prefixText);
        }

        public List<UserShoppingList> GetShoppingList(int userId)
        {
            return new ShoppingsListDA().GetShoppingList(userId);
        }

        #endregion Shopping List

        #region Shortage List
        public int AddMissingList(int userId)
        {
            return new MissingItemsListDA().AddList(userId);
        }

        public void AddMissinListItem(string name, int quantity, int measureUnit, int userId)
        {
            new MissingItemsListDA().AddListItem(name, quantity, measureUnit, userId);
        }

        public IQueryable<MissingListDetail> GetMissingList(int userId)
        {
            return new MissingItemsListDA().GetList(userId);
        }

        public void DeleteMissingListItem(int id)
        {
            new MissingItemsListDA().DeleteListItem(id);
        }
        #endregion

        #region Routine List
        public SavedList AddSavedList(int userId, string name)
        {
            return new SavedListDA().AddList(userId, name);
        }

        public SavedListDetail AddSavedListItem(string name, int quantity, int listId)
        {
            return new SavedListDA().AddListItem(name, quantity, listId);
        }

        public IQueryable<SavedListDetail> GetSavedListDetails(int listId)
        {
            return new SavedListDA().GetListDetails(listId);
        }

        public void DeleteSavedListItem(int ingredientId)
        {
            new SavedListDA().DeleteListItem(ingredientId);
        }

        public IQueryable<SavedList> GetSavedLists(int userId)
        {
            return new SavedListDA().GetLists(userId);
        }

        public bool DeleteSavedList(int listId)
        {
            return new SavedListDA().DeleteList(listId);
        }
        #endregion

        #region General List
        public int AddGeneralList(int userId, ListTypes listType)
        {
            return new GeneralListDA().AddGeneralList(userId, listType);
        }

        public bool AddGeneralListItem(SRL_Ingredient ingredient, int listId)
        {
            return new GeneralListDA().AddGeneralListItem(ingredient, listId);
        }

        public List<SRL_Ingredient> GetGeneralList(int userId, ListTypes listType)
        {
            return new GeneralListDA().GetGeneralList(userId, listType);
        }

        public bool DeleteGeneralList(int listId)
        {
            return new GeneralListDA().DeleteGeneralList(listId);
        }

        public bool DeleteGeneralListItem(int ingredientId)
        {
            return new GeneralListDA().DeleteGeneralListItem(ingredientId);
        }
        #endregion


        #region Summery List
        public int AddSummeryList(int userId)
        {
            return new SummeryListDA().AddList(userId);
        }

        public bool AddSummeryListItem(SRL_Ingredient ingredient, int listId, int sourceId)
        {
            return new SummeryListDA().AddListItem(ingredient, listId, sourceId);
        }

        public int GetSummeryList(int userId)
        {
            return new SummeryListDA().GetList(userId);
        }

        public List<SRL_Ingredient> GetSummeryListDetails(int listId)
        {
            return new SummeryListDA().GetListDetails(listId);
        }

        public void DeleteSummeryList(int listId)
        {
            new SummeryListDA().DeleteList(listId);
        }

        public void DeleteSummeryListItem(int summeryId, int sourceId, SRL_Ingredient ingredient)
        {
            new SummeryListDA().DeleteListItem(summeryId, sourceId, ingredient);
        }
        #endregion

        #region Admin

        #region Category
        public bool CheckDuplicateCategoryName(int categoryId, string categoryName)
        {
            return new AdminDA().CheckDuplicateCategoryName(categoryId, categoryName);
        }

        public bool CheckDuplicateMCategoryName(int categoryId, string categoryName)
        {
            return new AdminDA().CheckDuplicateMCategoryName(categoryId, categoryName);
        }

        public Category[] GetCategoriesList()
        {
            return new AdminDA().GetCategoriesList();
        }

        public MCategory[] GetMCategoriesList()
        {
            return new AdminDA().GetMCategoriesList();
        }

        public Category GetCategory(int categoryId)
        {
            return new AdminDA().GetCategory(categoryId);
        }

        public MCategory GetMenuCategory(int categoryId)
        {
            return new AdminDA().GetMenuCategory(categoryId);
        }

        public bool SaveCategory(Category category)
        {
            return new AdminDA().SaveCategory(category);
        }

        public bool SaveCategory(int categoryId, string categoryName, int? parentCategoryId)
        {
            return new AdminDA().SaveCategory(categoryId, categoryName, parentCategoryId);
        }

        public bool SaveMenuCategory(int categoryId, string categoryName, int? parentCategoryId)
        {
            return new AdminDA().SaveMenuCategory(categoryId, categoryName, parentCategoryId);
        }

        public bool DeleteCategory(int categoryId)
        {
            return new AdminDA().DeleteCategory(categoryId);
        }

        public bool DeleteMenuCategory(int categoryId)
        {
            return new AdminDA().DeleteMenuCategory(categoryId);
        }
        #endregion Category


        #region FoodCategory
        public bool CheckDuplicateFoodCategoryName(int FoodCategoryId, string FoodCategoryName)
        {
            return new AdminDA().CheckDuplicateFoodCategoryName(FoodCategoryId, FoodCategoryName);
        }

        public bool CheckDuplicateFoodName(int FoodId, string FoodName)
        {
            return new AdminDA().CheckDuplicateFoodName(FoodId, FoodName);
        }

        public FoodCategory[] GetFoodCategoriesList()
        {
            return new AdminDA().GetFoodCategoriesList();
        }

        public FoodCategory GetFoodCategory(int FoodCategoryId)
        {
            return new AdminDA().GetFoodCategory(FoodCategoryId);
        }

        public bool SaveFoodCategory(FoodCategory FoodCategory)
        {
            return new AdminDA().SaveFoodCategory(FoodCategory);
        }

        public bool SaveFoodCategory(int FoodCategoryId, string FoodCategoryName, int? parentFoodCategoryId)
        {
            return new AdminDA().SaveFoodCategory(FoodCategoryId, FoodCategoryName, parentFoodCategoryId);
        }

        public bool DeleteFoodCategory(int FoodCategoryId)
        {
            return new AdminDA().DeleteFoodCategory(FoodCategoryId);
        }
        #endregion FoodCategory

        public Food[] GetFoodsList()
        {
            return new AdminDA().GetFoodsList();
        }

        public Food GetFood(string name)
        {
            return new AdminDA().GetFood(name);
        }

        public Food GetFood(int foodId)
        {
            return new AdminDA().GetFood(foodId);
        }


        public bool SaveFood(Food food)
        {
            return new AdminDA().SaveFood(food);
        }

        public bool DeleteFood(int foodId)
        {
            return new AdminDA().DeleteFood(foodId);
        }

        public bool CheckDuplicateShopDepartmentName(int departmentId, string departmentName)
        {
            return new AdminDA().CheckDuplicateShopDepartmentName(departmentId, departmentName);
        }

        public ShopDepartment[] GetShopDepartmentsList()
        {
            return new AdminDA().GetShopDepartmentsList();
        }

        public ShopDepartment GetShopDepartment(int departmentId)
        {
            return new AdminDA().GetShopDepartment(departmentId);
        }

        public bool DeleteShopDepartment(int shopDepartmentId)
        {
            return new AdminDA().DeleteShopDepartment(shopDepartmentId);
        }

        public bool SaveShopDepartment(ShopDepartment department)
        {
            return new AdminDA().SaveShopDepartment(department);
        }

        public bool ReorderShopDepartments(ShopDepartment[] arr)
        {
            return new AdminDA().ReorderShopDepartments(arr);
        }

        //item
        public bool CheckDuplicateGeneralItemName(int departmentId, string departmentName)
        {
            return new AdminDA().CheckDuplicateGeneralItemName(departmentId, departmentName);
        }

        public GeneralItem[] GetGeneralItemsList()
        {
            return new AdminDA().GetGeneralItemsList();
        }

        public GeneralItem GetGeneralItem(int departmentId)
        {
            return new AdminDA().GetGeneralItem(departmentId);
        }

        public bool DeleteGeneralItem(int GeneralItemId)
        {
            return new AdminDA().DeleteGeneralItem(GeneralItemId);
        }

        public bool SaveGeneralItem(GeneralItem department)
        {
            return new AdminDA().SaveGeneralItem(department);
        }

        public bool ReorderGeneralItems(GeneralItem[] arr)
        {
            return new AdminDA().ReorderGeneralItems(arr);
        }
        //item

        public bool CheckDuplicateMeasurementUnitName(int unitId, string unitName)
        {
            return new AdminDA().CheckDuplicateMeasurementUnitName(unitId, unitName);
        }

        public MeasurementUnit[] GetMeasurementUnitsList()
        {
            return new AdminDA().GetMeasurementUnitsList();
        }

        public MeasurementUnit GetMeasurementUnit(int unitId)
        {
            return new AdminDA().GetMeasurementUnit(unitId);
        }

        public bool DeleteMeasurementUnit(int unitId)
        {
            return new AdminDA().DeleteMeasurementUnit(unitId);
        }

        public bool SaveMeasurementUnit(MeasurementUnit unit)
        {
            return new AdminDA().SaveMeasurementUnit(unit);
        }

        public bool ReorderMeasurementUnits(MeasurementUnit[] arr)
        {
            return new AdminDA().ReorderMeasurementUnits(arr);
        }

        public MeasurementUnitsConvert[] GetMeasurementUnitsConvertList()
        {
            return new AdminDA().GetMeasurementUnitsConvertList();
        }

        public bool SaveMeasurementUnitsConvert(MeasurementUnitsConvert unit)
        {
            return new AdminDA().SaveMeasurementUnitsConvert(unit);
        }

        #endregion Admin

        public MeasurementUnitsConvert GetMeasurementUnitsConvert(int ConvertId)
        {
            return new AdminDA().GetMeasurementUnitsConvert(ConvertId);
        }

        public int GetNextTempUser(int anonymous)
        {
            return new AdminDA().GetNextTempUser(anonymous);
        }

        public User GetUserByName(string username)
        {
            return new AdminDA().GetUserByName(username);
        }

        public User GetUser(int Id)
        {
            return new AdminDA().GetUser(Id);
        }

        public User GetUser(string userName, string password)
        {
            return new AdminDA().GetUser(userName, password);
        }

        public User GetUserEx(int Id)
        {
            return new AdminDA().GetUserEx(Id);
        }

        public int GetUsersNum()
        {
            return new AdminDA().GetUsersNum();
        }



        public bool SaveUser(User currUser)
        {
            return new AdminDA().SaveUser(currUser);
        }

        public FoodCategory GetFoodCategoryByName(string name)
        {
            return new AdminDA().GetFoodCategoryByName(name);
        }

        public MealRecipe[] GetMealRecipes(int mealId)
        {
            return new MealsDA().GetMealRecipes(mealId);
        }

        public Menu[] GetMenusList(int userId, int tempUser)
        {
            return new MenusDA().GetMenusList(userId, tempUser);
        }

        public bool UpdateMenuUser(int menuId, int userId)
        {
            return new MenusDA().UpdateMenuUser(menuId, userId);
        }



        public int GetMenuMaxDay(int menuId)
        {
            return new MenusDA().GetMenuMaxDay(menuId);
        }

        public int? GetMenuUserId(int menuId)
        {
            return new MenusDA().GetMenuUserId(menuId);
        }

        public int? GetMenuTempUserId(int menuId)
        {
            return new MenusDA().GetMenuTempUserId(menuId); 
        }

        public int GetMenusNum()
        {
            return new MenusDA().GetMenusNum();
        }

        public void ReplaceUserIds(int sourceUserId, int targetUserId)
        {
            new AdminDA().ReplaceUserIds(sourceUserId, targetUserId);
        }

        public IEnumerable<Recipe> GetRecipes(RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages)
        {
            return new RecipesDA().GetRecipes(orderBy, page, pageSize, out totalPages);
        }

        public IEnumerable<Recipe> GetRecipesEx(RecipeDisplayEnum display, int userId, string freeText, int? categoryId, int? servings, int[] recipeCats, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages, out int numRecipes)
        {
            return new RecipesDA().GetRecipesEx(display, userId, freeText, categoryId, servings, recipeCats, orderBy, page, pageSize, out totalPages, out numRecipes);
        }

        public IEnumerable<Menu> GetMenus(int userid, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages)
        {
            return new MenusDA().GetMenus(userid, orderBy, page, pageSize, out totalPages);
        }

        public int? GetRecipeUserFavoritesCount(int recipeId)
        {
            return new RecipesDA().GetRecipeUserFavoritesCount(recipeId);
        }

        public int? GetRecipeMenusCount(int recipeId)
        {
            return new RecipesDA().GetRecipeMenusCount(recipeId);
        }

        public Meal[] GetMenuMeals(int menuId)
        {
            return new MenusDA().GetMenuMeals(menuId);
        }

        public MBLSettingsWrapper GetMBLSettingsWrapper()
        {
            return new SettingsDA().GetMBLSettingsWrapper();
        }

        public Article GetArticleById(int articleId)
        {
            return new AdminDA().GetArticleById(articleId);
        }

        public Article[] GetArticlesList()
        {
            return new AdminDA().GetArticlesList();
        }

        public bool AddMenuToUserFavorites(int userId, int menuId, out int favMenusNum)
        {
            return new MenusDA().AddMenuToUserFavorites(userId, menuId , out favMenusNum);
        }

        public bool RemoveMenuFromUserFavorites(int userId, int menuId, out int favMenusNum)
        {
            return new MenusDA().RemoveMenuFromUserFavorites(userId, menuId, out favMenusNum);
        }

        public IEnumerable<Menu> GetMenusEx(RecipeDisplayEnum display, int userId, string freeText, int? categoryId, int? diners, int[] menuCats, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages, out int numMenus)
        {
            return new MenusDA().GetMenusEx(display, userId, freeText, categoryId, diners, menuCats, orderBy, page, pageSize, out totalPages, out numMenus);
        }

        public MCategory[] GetMenusCategoriesList(int userId)
        {
            return new MenusDA().GetMenusCategoriesList(userId);
        }

        public bool SaveMBLSettingsRecentItem(string recentItems, string itemType)
        {
            return new SettingsDA().SaveMBLSettingsRecentItem(recentItems, itemType);
        }

        public bool CreateOrUpdateArticle(int id, string title, string abs, string body, string publisher, out int returnedId)
        {
            return new AdminDA().CreateOrUpdateArticle(id, title, abs, body, publisher, out returnedId);
        }

        public void CheckShoppingListItem(int userId, int foodId, bool active)
        {
            new ShoppingsListDA().CheckShoppingListItem(userId, foodId, active);
        }

        public IQueryable<RecipesView> GetRecipes(string searchValue, int userId)
        {
            return new RecipesDA().GetRecipes(searchValue, userId);
        }

        public void AddRecipeToShoppingList(int userId, int recipeId)
        {
            new RecipesDA().AddRecipeToShoppingList(userId, recipeId);
        }

        public IQueryable<RecipesInShoppingList> GetSelectedRecipes(int userId)
        {
            return new RecipesDA().GetSelectedRecipes(userId);
        }

        public void RemoveRecipeFromShoppingList(int userId, int recipeId)
        {
            new RecipesDA().RemoveRecipeFromShoppingList(userId, recipeId);
        }

        public void AddMenuToShoppingList(int userId, int menuId, bool check)
        {
            new MenusDA().AddMenuToShoppingList(userId, menuId, check);
        }

        public IQueryable<MenusInShoppingList> GetMenusInShoppingList(int userId)
        {
            return new MenusDA().GetMenusInShoppingList(userId);
        }

        public void RemoveMenuFromShoppingList(int userId, int menuId)
        {
            new MenusDA().RemoveMenuFromShoppingList(userId, menuId);
        }

        public void UpdateSaveList(int listId, bool shoppingList)
        {
            new SavedListDA().UpdateSaveList(listId, shoppingList);
        }

        public void UpdateMissingListItem(int id, int quantity)
        {
            new MissingItemsListDA().UpdateMissingListItem(id, quantity);
        }

        public void UpdateSavedListItem(int id, int quantity)
        {
            new SavedListDA().UpdateSavedListItem(id, quantity);
        }

        public IEnumerable<Recipe> SearchRecipes(string searchedText)
        {
            return new RecipesDA().SearchRecipes(searchedText);
        }
    }
}
