using System.Collections.Generic;
using System.Linq;

using MyBuyList.DataLayer;
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;

namespace MyBuyList.BusinessLayer.Managers
{
    class ShoppingsListManager
    {
        internal List<UserShoppingList> GetShoppingList(int userId)
        {
            return DataFacade.Instance.GetShoppingList(userId);
        }

        internal ShopDepartment[] GetMenuShopDepartments(int menuId)
        {
            return DataFacade.Instance.GetMenuShopDepartments(menuId);
        }

        internal ShoppingFood[] GetMenuShoppingList(int menuId)
        {
            return DataFacade.Instance.GetMenuShoppingList(menuId);
        }

        internal ShoppingListAdditionalItem[] GetShoppingListAdditionalItems(int menuId)
        {
            return DataFacade.Instance.GetShoppingListAdditionalItems(menuId);
        }

        internal bool DeleteShoppingListAdditionalItem(int itemId)
        {
            return DataFacade.Instance.DeleteShoppingListAdditionalItem(itemId);
        }

        internal bool AddShoppingListAdditionalItem(int menuId, string itemName)
        {
            return DataFacade.Instance.AddShoppingListAdditionalItem(menuId, itemName);
        }

        internal string[] GetGeneralItemsList(string prefixText)
        {
            return DataFacade.Instance.GetGeneralItemsList(prefixText);
        }

        internal string GetIngredientInGrams(ShoppingFood food, int currUnit)
        {

            MeasurementUnitsConvert convert = BusinessFacade.Instance.GetMeasurementUnitsConvertList().SingleOrDefault(mc => mc.FoodId == food.FoodId &&
                                                                                                                             mc.FromUnitId == currUnit &&
                                                                                                                             mc.ToUnitId == (int)AppConstants.GRAMM_UNIT_ID);

            string result = string.Empty;
            if (convert != null)
            {
                decimal unitQuantity = (convert.ToQuantity / convert.FromQuantity) * food.Quantity;

                result = string.Format("{0} {1}", BusinessFacade.Instance.GetNumberWithFreg(unitQuantity), "גרם");
            }

            return result;
        }

        internal void CheckShoppingListItem(int userId, int foodId, bool active)
        {
            DataFacade.Instance.CheckShoppingListItem(userId, foodId, active);
        }

        internal void RemoveItemFromShoppingList(int userId, int foodId)
        {
            DataFacade.Instance.RemoveItemFromShoppingList(userId, foodId);
        }
    }
}