using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyBuyList.DataLayer;
using MyBuyList.Shared.Entities;

namespace MyBuyList.BusinessLayer.Managers
{
    class AdminManager
    {
        #region Category
        internal bool CheckDuplicateCategoryName(int categoryId, string categoryName)
        {
            return DataFacade.Instance.CheckDuplicateCategoryName(categoryId, categoryName);
        }

        internal bool CheckDuplicateMCategoryName(int categoryId, string categoryName)
        {
            return DataFacade.Instance.CheckDuplicateMCategoryName(categoryId, categoryName);
        }

        internal Category[] GetCategoriesList()
        {
            return DataFacade.Instance.GetCategoriesList();
        }

        internal MCategory[] GetMCategoriesList()
        {
            return DataFacade.Instance.GetMCategoriesList();
        }

        internal Category GetCategory(int categoryId)
        {
            return DataFacade.Instance.GetCategory(categoryId);
        }

        internal MCategory GetMenuCategory(int categoryId)
        {
            return DataFacade.Instance.GetMenuCategory(categoryId);
        }

        internal bool SaveCategory(Category category)
        {
            return DataFacade.Instance.SaveCategory(category);
        }

        internal bool SaveCategory(int categoryId, string categoryName, int? parentCategoryId)
        {
            return DataFacade.Instance.SaveCategory(categoryId, categoryName, parentCategoryId);
        }

        internal bool SaveMenuCategory(int categoryId, string categoryName, int? parentCategoryId)
        {
            return DataFacade.Instance.SaveMenuCategory(categoryId, categoryName, parentCategoryId);
        }

        internal bool DeleteCategory(int categoryId)
        {
            return DataFacade.Instance.DeleteCategory(categoryId);
        }

        internal bool DeleteMenuCategory(int categoryId)
        {
            return DataFacade.Instance.DeleteMenuCategory(categoryId);
        }
        #endregion Category


        #region FoodCategory
        internal bool CheckDuplicateFoodCategoryName(int FoodCategoryId, string FoodCategoryName)
        {
            return DataFacade.Instance.CheckDuplicateFoodCategoryName(FoodCategoryId, FoodCategoryName);
        }

        internal bool CheckDuplicateFoodName(int FoodId, string FoodName)
        {
            return DataFacade.Instance.CheckDuplicateFoodName(FoodId, FoodName);
        }

        internal FoodCategory[] GetFoodCategoriesList()
        {
            return DataFacade.Instance.GetFoodCategoriesList();
        }

        internal FoodCategory GetFoodCategory(int FoodCategoryId)
        {
            return DataFacade.Instance.GetFoodCategory(FoodCategoryId);
        }

        internal bool SaveFoodCategory(FoodCategory FoodCategory)
        {
            return DataFacade.Instance.SaveFoodCategory(FoodCategory);
        }

        internal bool SaveFoodCategory(int FoodCategoryId, string FoodCategoryName, int? parentFoodCategoryId)
        {
            return DataFacade.Instance.SaveFoodCategory(FoodCategoryId, FoodCategoryName, parentFoodCategoryId);
        }

        internal bool DeleteFoodCategory(int FoodCategoryId)
        {
            return DataFacade.Instance.DeleteFoodCategory(FoodCategoryId);
        }
        #endregion FoodCategory

        internal Food[] GetFoodsList()
        {
            return DataFacade.Instance.GetFoodsList();
        }

        internal Food GetFood(int foodId)
        {
            return DataFacade.Instance.GetFood(foodId);
        }

        internal Food GetFood(string name)
        {
            return DataFacade.Instance.GetFood(name);
        }

        //internal FoodCategory[] GetFoodCategoriesList()
        //{
        //    return DataFacade.Instance.GetFoodCategoriesList();
        //}

        internal ShopDepartment[] GetShopDepartmentsList()
        {
            return DataFacade.Instance.GetShopDepartmentsList();
        }

        internal bool CheckDuplicateShopDepartmentName(int departmentId, string departmentName)
        {
            return DataFacade.Instance.CheckDuplicateShopDepartmentName(departmentId, departmentName);
        }

        internal ShopDepartment GetShopDepartment(int departmentId)
        {
            return DataFacade.Instance.GetShopDepartment(departmentId);
        }

        internal bool DeleteShopDepartment(int shopDepartmentId)
        {
            return DataFacade.Instance.DeleteShopDepartment(shopDepartmentId);
        }

        internal bool SaveShopDepartment(ShopDepartment department)
        {
            return DataFacade.Instance.SaveShopDepartment(department);
        }

        internal bool ReorderShopDepartments(ShopDepartment[] arr)
        {
            return DataFacade.Instance.ReorderShopDepartments(arr);
        }

        //item
        internal GeneralItem[] GetGeneralItemsList()
        {
            return DataFacade.Instance.GetGeneralItemsList();
        }

        internal bool CheckDuplicateGeneralItemName(int ItemId, string ItemName)
        {
            return DataFacade.Instance.CheckDuplicateGeneralItemName(ItemId, ItemName);
        }

        internal GeneralItem GetGeneralItem(int ItemId)
        {
            return DataFacade.Instance.GetGeneralItem(ItemId);
        }

        internal bool DeleteGeneralItem(int GeneralItemId)
        {
            return DataFacade.Instance.DeleteGeneralItem(GeneralItemId);
        }

        internal bool SaveGeneralItem(GeneralItem Item)
        {
            return DataFacade.Instance.SaveGeneralItem(Item);
        }

        internal bool ReorderGeneralItems(GeneralItem[] arr)
        {
            return DataFacade.Instance.ReorderGeneralItems(arr);
        }

        // item

        public bool CheckDuplicateMeasurementUnitName(int unitId, string unitName)
        {
            return DataFacade.Instance.CheckDuplicateMeasurementUnitName(unitId, unitName);
        }

        internal MeasurementUnit GetMeasurementUnit(int unitId)
        {
            return DataFacade.Instance.GetMeasurementUnit(unitId);
        }

        internal MeasurementUnit[] GetMeasurementUnitsList()
        {
            return DataFacade.Instance.GetMeasurementUnitsList();
        }

        internal bool DeleteMeasurementUnit(int unitId)
        {
            return DataFacade.Instance.DeleteMeasurementUnit(unitId);
        }

        internal bool SaveMeasurementUnit(MeasurementUnit unit)
        {
            return DataFacade.Instance.SaveMeasurementUnit(unit);
        }

        internal bool ReorderMeasurementUnits(MeasurementUnit[] arr)
        {
            return DataFacade.Instance.ReorderMeasurementUnits(arr);
        }

        internal bool SaveMeasurementUnitsConvert(MeasurementUnitsConvert unit)
        {
            return DataFacade.Instance.SaveMeasurementUnitsConvert(unit);
        }

        internal MeasurementUnitsConvert[] GetMeasurementUnitsConvertList()
        {
            return DataFacade.Instance.GetMeasurementUnitsConvertList();
        }

        internal MeasurementUnitsConvert GetMeasurementUnitsConvert(int ConvertId)
        {
            return DataFacade.Instance.GetMeasurementUnitsConvert(ConvertId);
        }

        public int GetNextTempUser(int anonymous)
        {
            return DataFacade.Instance.GetNextTempUser( anonymous);
        }

        internal User GetUser(int Id)
        {
            return DataFacade.Instance.GetUser(Id);
        }

        public User GetUser(string userName, string password)
        {
            return DataFacade.Instance.GetUser(userName, password);
        }

        internal User GetUserEx(int Id)
        {
            return DataFacade.Instance.GetUserEx(Id);
        }

        internal bool SaveUser(User currUser)
        {
            return DataFacade.Instance.SaveUser(currUser);
        }

        internal User GetUserByUserName(string userName)
        {
            return DataFacade.Instance.GetUserByName(userName);
        }

        internal FoodCategory GetFoodCategoryByName(string name)
        {
            return DataFacade.Instance.GetFoodCategoryByName(name);
        }

        internal int GetUsersNum()
        {
            return DataFacade.Instance.GetUsersNum();
        }

        internal bool DeleteFood(int foodId)
        {
             return DataFacade.Instance.DeleteFood(foodId);
        }

        internal Article GetArticleById(int articleId)
        {
            return DataFacade.Instance.GetArticleById(articleId);
        }

        internal Article[] GetArticlesList()
        {
            return DataFacade.Instance.GetArticlesList();
        }

        internal bool CreateOrUpdateArticle(int id, string title, string abs, string body, string publisher, out int returnedId)
        {
            return DataFacade.Instance.CreateOrUpdateArticle(id, title, abs, body, publisher, out returnedId);
        }
    }
}
