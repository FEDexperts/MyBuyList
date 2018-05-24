using System;
using System.Collections.Generic;
using System.Linq;
using MyBuyList.Shared.Entities;

namespace MyBuyList.DataLayer.DataAdapters
{
    class SummeryListDA : BaseContextDataAdapter<MyBuyListDBContext>
    {
        internal int AddList(int userId)
        {
            using (DataContext)
            {
                try
                {
                    SummeryList summeryList =
                        (from p in DataContext.SummeryLists where p.CREATED_BY == userId && p.ACTIVE select p).
                            SingleOrDefault();

                    if (summeryList == null)
                    {
                        summeryList = new SummeryList { CREATED_BY = userId, CREATE_DATE = DateTime.Today, ACTIVE = true };

                        DataContext.SummeryLists.InsertOnSubmit(summeryList);
                        DataContext.SubmitChanges();
                    }

                    return summeryList.ID;
                }
                catch
                {
                    return -1;
                }
            }
        }

        internal bool AddListItem(SRL_Ingredient ingredient, int listId, int sourceId)
        {
            using (DataContext)
            {
                SummeryListDetail summeryListDetail;
                try
                {
                    // Get the food id
                    Food food = (from p in DataContext.Foods where p.FoodName == ingredient.FoodName select p).SingleOrDefault();
                    if (food != null)
                        ingredient.FoodId = food.FoodId;

                    // Add to Summery Sources
                    if (food != null)
                    {
                        SummerySource summerySource = (from p in DataContext.SummerySources
                                                       where
                                                           p.SUMMERY_ID == listId && p.SOURCE_ID == sourceId &&
                                                           p.FOOD_ID == food.FoodId
                                                       select p).SingleOrDefault();
                        if (summerySource == null)
                        {
                            summerySource = new SummerySource
                                                {
                                                    SUMMERY_ID = listId,
                                                    SOURCE_ID = sourceId,
                                                    FOOD_ID = food.FoodId,
                                                    QUANTITY = ingredient.Quantity
                                                };
                            DataContext.SummerySources.InsertOnSubmit(summerySource);
                        }
                        else
                        {
                            summerySource.QUANTITY = ingredient.Quantity;
                        }

                        DataContext.SubmitChanges();
                    }

                    // ReCalculate summery list
                    IQueryable<decimal?> summerySources = from p in DataContext.SummerySources where p.SUMMERY_ID == listId && p.FOOD_ID == ingredient.FoodId select p.QUANTITY;
                    decimal? sum = summerySources.Sum();

                    summeryListDetail = (from p in DataContext.SummeryListDetails
                                         where p.LIST_ID == listId && p.FOOD_ID == ingredient.FoodId
                                         select p).SingleOrDefault();

                    if (summeryListDetail == null)
                    {
                        summeryListDetail = new SummeryListDetail
                        {
                            FOOD_ID = ingredient.FoodId,
                            FOOD_NAME = ingredient.FoodName,
                            LIST_ID = listId,
                            QUANTITY = sum.HasValue ? sum.Value : 0,
                            MEASUREMENT_UNIT_ID = ingredient.MeasurementUnitId,
                            MEASUREMENT_UNIT = ingredient.MeasurementUnitName
                        };
                        DataContext.SummeryListDetails.InsertOnSubmit(summeryListDetail);
                    }
                    else
                    {
                        summeryListDetail.QUANTITY = sum.HasValue ? sum.Value : 0;
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

        internal int GetList(int userId)
        {
            using (DataContext)
            {
                try
                {
                    int listId = (from p in DataContext.SummeryLists where p.CREATED_BY == userId && p.ACTIVE select p.ID).Single();
                    return listId;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        internal List<SRL_Ingredient> GetListDetails(int listId)
        {
            using (DataContext)
            {
                try
                {
                    List<SRL_Ingredient> ingredients = (from p in DataContext.SummeryListDetails
                                                        where p.LIST_ID == listId
                                                        select
                                                            new SRL_Ingredient
                                                            {
                                                                IngredientId = p.ID,
                                                                FoodId = p.FOOD_ID,
                                                                FoodName = p.FOOD_NAME,
                                                                Quantity = p.QUANTITY,
                                                                MeasurementUnitId = p.MEASUREMENT_UNIT_ID,
                                                                MeasurementUnitName = p.MEASUREMENT_UNIT
                                                            }).ToList();
                    return ingredients;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        internal void DeleteList(int listId)
        {
            using (DataContext)
            {
                try
                {
                    SummeryList summeryList =
                        (from p in DataContext.SummeryLists where p.CREATED_BY == listId select p).Single();

                    DataContext.SummeryLists.DeleteOnSubmit(summeryList);
                    DataContext.SubmitChanges();
                }
                catch (Exception)
                {
                }
            }
        }

        internal void DeleteListItem(int summeryId, int sourceId, SRL_Ingredient ingredient)
        {
            using (DataContext)
            {
                try
                {
                    SummerySource summerySource = (from p in DataContext.SummerySources
                                                               where
                                                                   p.SUMMERY_ID == summeryId && p.SOURCE_ID == sourceId &&
                                                                   p.FOOD_ID == ingredient.FoodId
                                                               select p).SingleOrDefault();
                    if (summerySource != null)
                    {
                        summerySource.QUANTITY -= ingredient.Quantity;
                        if (summerySource.QUANTITY <= 0)
                        {
                            DataContext.SummerySources.DeleteOnSubmit(summerySource);
                        }
                    }

                    // ReCalculate summery list
                    IQueryable<decimal?> summerySources = from p in DataContext.SummerySources where p.SUMMERY_ID == summeryId && p.FOOD_ID == ingredient.FoodId select p.QUANTITY;
                    decimal? sum = summerySources.Sum() - ingredient.Quantity;

                    SummeryListDetail summeryListDetail = (from p in DataContext.SummeryListDetails
                                         where p.LIST_ID == summeryId && p.FOOD_ID == ingredient.FoodId
                                         select p).SingleOrDefault();

                    if (summeryListDetail == null)
                    {
                        if (sum > 0)
                        {
                            summeryListDetail = new SummeryListDetail
                                                    {
                                                        FOOD_ID = ingredient.FoodId,
                                                        FOOD_NAME = ingredient.FoodName,
                                                        LIST_ID = summeryId,
                                                        QUANTITY = sum.HasValue ? sum.Value : 0,
                                                        MEASUREMENT_UNIT_ID = ingredient.MeasurementUnitId,
                                                        MEASUREMENT_UNIT = ingredient.MeasurementUnitName
                                                    };
                            DataContext.SummeryListDetails.InsertOnSubmit(summeryListDetail);
                        }
                    }
                    else
                    {
                        if (sum == 0)
                        {
                            DataContext.SummeryListDetails.DeleteOnSubmit(summeryListDetail);
                        }
                        else
                        {
                            summeryListDetail.QUANTITY = sum.HasValue ? sum.Value : 0;
                        }
                    }

                    DataContext.SubmitChanges();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}