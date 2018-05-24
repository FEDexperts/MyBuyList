using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyBuyList.DataLayer;
using MyBuyList.Shared.Entities;
using MyBuyList.Shared.Enums;
using System.Data;

namespace MyBuyList.BusinessLayer.Managers
{
    class MenusManager
    {
        internal Menu[] GetMenusList(int userId)
        {
            return DataFacade.Instance.GetMenusList(userId);
        }

        internal Menu GetMenu(int menuId)
        {
            return DataFacade.Instance.GetMenu(menuId);
        }

        internal Menu GetMenuEx(int menuId)
        {
            return DataFacade.Instance.GetMenuEx(menuId);
        }

        internal int GetMenusNum()
        {
            return DataFacade.Instance.GetMenusNum();
        }

        internal MenuType[] GetMenuTypes()
        {
            return DataFacade.Instance.GetMenuTypes();
        }

        internal MenuType GetMenuType(int menuId)
        {
            return DataFacade.Instance.GetMenuType(menuId);
        }

        internal MenuRecipe[] GetMenuRecipes(int menuId)
        {
            return DataFacade.Instance.GetMenuRecipes(menuId);
        }

        internal MealRecipe[] GetMenuMealsRecipes(int menulId)
        {
            return DataFacade.Instance.GetMenuMealsRecipes(menulId);
        }

        internal bool CreateMenu(int menuTypeId, int userId, int tempUser, out int menuId)
        {
            return DataFacade.Instance.CreateMenu(menuTypeId, userId, tempUser, out menuId);
        }

        internal bool CreateMenuEx(int menuTypeId, int userId, int tempUser, string menuName, string description, bool isPublic, out int menuId)
        {
            return DataFacade.Instance.CreateMenuEx(menuTypeId, userId, tempUser, menuName, description, isPublic, out menuId);
        }

        internal bool CreateMenuEx1(Menu menu, int tempUser, out int menuId)
        {
            return DataFacade.Instance.CreateMenuEx1(menu, tempUser, out menuId);
        }

        internal void CreateOrUpdateMenu(Menu menu, out int menuId)
        {
            DataFacade.Instance.CreateOrUpdateMenu(menu, out menuId);
        }

        internal bool UpdateMenuNameAndDescription(int menuId, string menuName, string description)
        {
            return DataFacade.Instance.UpdateMenuNameAndDescription(menuId, menuName, description);
        }       

        internal bool DeleteMenu(int menuId)
        {
            return DataFacade.Instance.DeleteMenu(menuId);
        }

        internal bool RemoveMenuRecipe(int menuId, int recipeId)
        {
            return DataFacade.Instance.RemoveMenuRecipe(menuId, recipeId);
        }
        internal bool AddMenuRecipe(int menuId, int recipeId)
        {
            return DataFacade.Instance.AddMenuRecipe(menuId, recipeId);
        }

        internal bool CheckIfMenuRecipeExistInMeals(int menuId, int recipeId)
        {
            return DataFacade.Instance.CheckIfMenuRecipeExistInMeals(menuId, recipeId);
        }



        internal Menu[] GetMenusList(int userId, int tempUser)
        {
            return DataFacade.Instance.GetMenusList(userId, tempUser);
        }

        public bool UpdateMenuUser(int menuId, int userId)
        {
           return DataFacade.Instance.UpdateMenuUser(menuId, userId);
        }

        internal int? GetMenuUserId(int menuId)
        {
            return DataFacade.Instance.GetMenuUserId( menuId);
        }

        internal int? GetMenuTempUserId(int menuId)
        {
            return DataFacade.Instance.GetMenuTempUserId(menuId);
        }

        public Dictionary<int, List<int>> GetMenuRecipesIngrid(int menulId)
        {
            MealRecipe[] menuMealsRecipes = this.GetMenuMealsRecipes(menulId);

            Dictionary<int, List<int>> recipeServ = new Dictionary<int, List<int>>();

            foreach (MealRecipe curr in menuMealsRecipes)
            {
                if (!recipeServ.ContainsKey(curr.RecipeId))
                {
                    List<int> arrSers = new List<int>();
                    arrSers.Add(curr.Servings);
                    recipeServ.Add(curr.RecipeId, arrSers);
                }
                else
                {
                    List<int> arrSers = recipeServ[curr.RecipeId];
                    if (!arrSers.Contains(curr.Servings))
                    {
                        arrSers.Add(curr.Servings);
                        arrSers.Sort();
                        recipeServ[curr.RecipeId] = arrSers;
                    }
                }
            }

            return recipeServ;
        }

        internal int[] GetMenuDays(int menuId)
        {
            int maxDay = DataFacade.Instance.GetMenuMaxDay(menuId);
            
            List<int> days = new List<int>(maxDay);
            for (int n = 1; n <= maxDay; n++)
            {
                days.Add(n);
            }

            return days.ToArray<int>();
        }

        internal IEnumerable<Menu> GetMenus(int userid, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages)
        {
            return DataFacade.Instance.GetMenus(userid, orderBy, page, pageSize, out totalPages);
        }

        internal Meal[] GetMenuMeals(int menuId)
        {
            return DataFacade.Instance.GetMenuMeals(menuId);
        }

        internal bool AddMenuToUserFavorites(int userId, int menuId, out int favMenusNum)
        {
            return DataFacade.Instance.AddMenuToUserFavorites(userId, menuId, out favMenusNum);
        }

        internal bool RemoveMenuFromUserFavorites(int userId, int menuId, out int favMenusNum)
        {
            return DataFacade.Instance.RemoveMenuFromUserFavorites(userId, menuId, out favMenusNum);
        }

        internal IEnumerable<Menu> GetMenusEx(RecipeDisplayEnum display, int userId, string freeText, int? categoryId, int? diners, int[] menuCats, RecipeOrderEnum orderBy, int page, int pageSize, out int totalPages, out int numMenus)
        {
            return DataFacade.Instance.GetMenusEx(display, userId, freeText, categoryId, diners, menuCats, orderBy, page, pageSize, out totalPages, out numMenus);
        }

        internal MCategory[] GetMenusCategoriesList(int userId)
        {
            return DataFacade.Instance.GetMenusCategoriesList(userId);
        }

        internal void AddMenuToShoppingList(int userId, int menuId, bool check)
        {
            DataFacade.Instance.AddMenuToShoppingList(userId, menuId, check);
        }

        internal IQueryable<MenusInShoppingList> GetMenusInShoppingList(int userId)
        {
            return DataFacade.Instance.GetMenusInShoppingList(userId);
        }

        internal void RemoveMenuFromShoppingList(int userId, int menuId)
        {
            DataFacade.Instance.RemoveMenuFromShoppingList(userId, menuId);
        }

        internal Menu[] GetUserFavoritesMenus(int userId)
        {
            return DataFacade.Instance.GetUserFavoritesMenus(userId);
        }

        internal Menu[] GetUserMenusList(int userId)
        {
            return DataFacade.Instance.GetUserMenusList(userId);
        }

        internal Menu[] SearchMenus(string searchValue)
        {
            return DataFacade.Instance.SearchMenus(searchValue);
        }
    }
}