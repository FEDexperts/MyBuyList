using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

using MyBuyList.Shared.Entities;

namespace MyBuyList.DataLayer.DataAdapters
{
    class ShoppingsListDA : BaseContextDataAdapter<MyBuyListDBContext>
    {
        internal List<UserShoppingList> GetShoppingList(int userId)
        {
            using (DataContext)
            {
                List<UserShoppingList> shoppingList = DataContext.ExecuteQuery<UserShoppingList>("exec spShoppingList @UserId={0}", userId).ToList();
                return shoppingList;
            }
        }


        internal ShopDepartment[] GetMenuShopDepartments(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    var departments =   from sd in DataContext.ShopDepartments
                                        join fc in DataContext.FoodCategories on sd.ShopDepartmentId equals fc.ShopDepartmentId
                                        join f in DataContext.Foods on fc.FoodCategoryId equals f.FoodCategoryId
                                        join i in DataContext.Ingredients on f.FoodId equals i.FoodId
                                        join r in DataContext.Recipes on i.RecipeId equals r.RecipeId
                                        join mr in DataContext.MealRecipes on r.RecipeId equals mr.RecipeId
                                        join m in DataContext.Meals.Where(m => m.MenuId == menuId) on mr.MealId equals m.MealId
                                        select sd;

                    var list = departments.Distinct();
                    return list.OrderBy(sd => sd.SortOrder).ToArray();
                                                        
                }
                catch
                {
                    return null;
                }
            }
        }

        
        internal ShoppingFood[] GetMenuShoppingList(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    var measurmentUnits = DataContext.MeasurementUnits;

                    var measureConverts = DataContext.MeasurementUnitsConverts;
                    
                    var ingredients = from i in DataContext.Ingredients
                                      join r in DataContext.Recipes on i.RecipeId equals r.RecipeId
                                      join mr in DataContext.MealRecipes on r.RecipeId equals mr.RecipeId
                                      join m in DataContext.Meals.Where(m => m.MenuId == menuId) on mr.MealId equals m.MealId
                                      select new { Ingredient = i, TotalServings = mr.Servings /1.00M/ r.Servings };

                    ShoppingFood[] list  = (from f in DataContext.Foods
                                            where ingredients.Any(i => i.Ingredient.FoodId == f.FoodId)
                                            select new ShoppingFood(f)).ToArray();

                    foreach (ShoppingFood sf in list)
                    {
                        Dictionary<int, decimal> dictMeasureUnitsTotalQty = new Dictionary<int, decimal>();
                        
                        //run on all ingredients with this food and fill dictinary of different measurement units
                        foreach (var item in ingredients.Where(f => f.Ingredient.FoodId == sf.FoodId))
                        {
                            decimal ingredientQty = item.Ingredient.Quantity * item.TotalServings;

                            if (item.Ingredient.MeasurementUnitId == sf.CalculateUnitId)
                            {
                                if (!dictMeasureUnitsTotalQty.ContainsKey(sf.CalculateUnitId))
                                {
                                    dictMeasureUnitsTotalQty.Add(sf.CalculateUnitId, ingredientQty);
                                }
                                else
                                {
                                    dictMeasureUnitsTotalQty[sf.CalculateUnitId] += ingredientQty;
                                }
                            }
                            else
                            {
                                MeasurementUnitsConvert convert = measureConverts.SingleOrDefault(mc => mc.FoodId == sf.FoodId &&
                                                                                                  mc.FromUnitId == item.Ingredient.MeasurementUnitId &&
                                                                                                  mc.ToUnitId == sf.CalculateUnitId);
                                if (convert != null)
                                {
                                    decimal unitQuantity = (convert.ToQuantity / convert.FromQuantity) * ingredientQty;

                                    if (!dictMeasureUnitsTotalQty.ContainsKey(sf.CalculateUnitId))
                                    {
                                        dictMeasureUnitsTotalQty.Add(sf.CalculateUnitId, unitQuantity);
                                    }
                                    else
                                    {
                                        dictMeasureUnitsTotalQty[sf.CalculateUnitId] += unitQuantity;
                                    }
                                }
                                else
                                {
                                    if (!dictMeasureUnitsTotalQty.ContainsKey(item.Ingredient.MeasurementUnitId))
                                    {
                                        dictMeasureUnitsTotalQty.Add(item.Ingredient.MeasurementUnitId, ingredientQty);
                                    }
                                    else
                                    {
                                        dictMeasureUnitsTotalQty[item.Ingredient.MeasurementUnitId] += ingredientQty;
                                    }
                                }
                            }                           
                        }

                        //fill DisplayQuantity of shopping food accordingly data in dictMeasureUnitsTotalQty
                        foreach(KeyValuePair<int, decimal> item in dictMeasureUnitsTotalQty)
                        {
                            MeasurementUnit measurementUnitUsed = measurmentUnits.Single(mu => mu.UnitId == item.Key);
                            sf.CalculateUnitId = measurementUnitUsed.UnitId;
                            sf.CalculateUnitName = measurementUnitUsed.UnitName;
                            sf.Quantity = item.Value;
                        }
                        
                    }
                    return list;

                }
                catch
                {
                    return null;
                }
            }
        }

        internal ShoppingListAdditionalItem[] GetShoppingListAdditionalItems(int menuId)
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<ShoppingListAdditionalItem>(slai => slai.GeneralItem);
                    DataContext.LoadOptions = dlo;

                    var list = DataContext.ShoppingListAdditionalItems.Where(slai => slai.MenuId == menuId);
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool DeleteShoppingListAdditionalItem(int itemId)
        {
            using (DataContext)
            {
                try
                {
                    ShoppingListAdditionalItem additionalItem = DataContext.ShoppingListAdditionalItems.Single(slai => slai.ShoppingListItemId == itemId);
                    DataContext.ShoppingListAdditionalItems.DeleteOnSubmit(additionalItem);
                    DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }                                   
            }
        }

        internal bool AddShoppingListAdditionalItem(int menuId, string itemName)
        {
            using (DataContext)
            {
                try
                {
                    ShoppingListAdditionalItem additionalItem = new ShoppingListAdditionalItem {MenuId = menuId};

                    GeneralItem generalItem = DataContext.GeneralItems.SingleOrDefault(gi => gi.GeneralItemName.Trim() == itemName.Trim());
                    if (generalItem != null)
                    {
                        additionalItem.GeneralItemId = generalItem.GeneralItemId;
                    }
                    else
                    {
                        additionalItem.ItemName = itemName;
                    }
                    
                    DataContext.ShoppingListAdditionalItems.InsertOnSubmit(additionalItem);
                    DataContext.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal string[] GetGeneralItemsList(string prefixText)
        {
            using (DataContext)
            {
                try
                {
                    var list = from e in DataContext.GeneralItems.Where(gi => gi.GeneralItemName.StartsWith(prefixText))
                               select e.GeneralItemName;
                    
                    return list.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        internal void CheckShoppingListItem(int userId, int foodId, bool active)
        {
            UserShoppingList userShoppingList = DataContext.UserShoppingLists.SingleOrDefault(p => p.USER_ID == userId && p.FOOD_ID == foodId);
            if (userShoppingList != null)
            {
                userShoppingList.ACTIVE = active;
            }

            DataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
        }

        internal void RemoveItemFromShoppingList(int userId, int foodId)
        {
            MissingList missingListRow = DataContext.MissingLists.SingleOrDefault(x => x.CREATED_BY == userId);
            if (missingListRow != null)
            {
                MissingListDetail missingListDetailsRow = DataContext.MissingListDetails.SingleOrDefault(x => x.LIST_ID == missingListRow.ID && x.FOOD_ID == foodId);
                if (missingListDetailsRow != null)
                {
                    DataContext.MissingListDetails.DeleteOnSubmit(missingListDetailsRow);
                    DataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
                }
            }
        }
    }
}
