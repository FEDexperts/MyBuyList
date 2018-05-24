using System;
using System.Collections.Generic;
using System.Linq;
using MyBuyList.Shared.Entities;
using MyBuyList.Shared.Enums;

namespace MyBuyList.DataLayer.DataAdapters
{
    class GeneralListDA : BaseContextDataAdapter<MyBuyListDBContext>
    {
        internal int AddGeneralList(int userId, ListTypes listType)
        {
            using (DataContext)
            {
                try
                {
                    int i =
                        (from p in DataContext.GeneralLists where p.CREATED_BY == userId && p.LIST_TYPE == (int)listType && p.ACTIVE == true select p).
                            Count();

                    GeneralList generalList;
                    if (i == 0)
                    {
                        generalList = new GeneralList
                                          {
                                              CREATED_BY = userId,
                                              CREATE_DATE = DateTime.Today,
                                              ACTIVE = true,
                                              LIST_TYPE = (int)listType
                                          };

                        DataContext.GeneralLists.InsertOnSubmit(generalList);
                        DataContext.SubmitChanges();
                    }
                    else
                    {
                        generalList =
                            (from p in DataContext.GeneralLists
                             where p.CREATED_BY == userId && p.ACTIVE == true && p.LIST_TYPE == (int)listType
                             select p).
                                Single();
                    }

                    return generalList.ID;
                }
                catch
                {
                    return -1;
                }
            }
        }

        internal bool AddGeneralListItem(SRL_Ingredient ingredient, int listId)
        {
            using (DataContext)
            {
                try
                {
                    int count = (from p in DataContext.GeneralListDetails
                                 where p.LIST_ID == listId && p.ITEM_NAME == ingredient.FoodName
                                 select p).Count();

                    if (count == 0)
                    {
                        GeneralListDetail generalListDetail = new GeneralListDetail
                        {
                            ITEM_NAME = ingredient.FoodName,
                            LIST_ID = listId,
                            COMPLETE_QUANTITY = ingredient.CompleteValue,
                            MEASUREMENT_UNIT_ID = ingredient.MeasurementUnitId,
                            MEASUREMENT_UNIT = ingredient.MeasurementUnitName
                        };
                        DataContext.GeneralListDetails.InsertOnSubmit(generalListDetail);
                    }
                    else
                    {
                        GeneralListDetail generalListDetail = (from p in DataContext.GeneralListDetails
                                                                 where
                                                                     p.LIST_ID == listId &&
                                                                     p.ITEM_NAME == ingredient.FoodName
                                                                 select p).Single();

                        generalListDetail.COMPLETE_QUANTITY = ingredient.CompleteValue;
                        generalListDetail.MEASUREMENT_UNIT_ID = ingredient.MeasurementUnitId;
                        generalListDetail.MEASUREMENT_UNIT = ingredient.MeasurementUnitName;
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

        internal List<SRL_Ingredient> GetGeneralList(int userId, ListTypes listType)
        {
            using (DataContext)
            {
                try
                {
                    GeneralList generalList = (from p in DataContext.GeneralLists
                                               where
                                                   p.CREATED_BY == userId && p.ACTIVE == true &&
                                                   p.LIST_TYPE == (int) listType
                                               select p).Single();
                    if (generalList != null)
                    {
                        List<SRL_Ingredient> ingredients = (from p in DataContext.GeneralListDetails
                                                            where p.LIST_ID == generalList.ID
                                                            select
                                                                new SRL_Ingredient
                                                                {
                                                                    IngredientId = p.ID,
                                                                    FoodName = p.ITEM_NAME,
                                                                    CompleteValue = p.COMPLETE_QUANTITY,
                                                                    Quantity = decimal.Parse(p.COMPLETE_QUANTITY),
                                                                    MeasurementUnitId = p.MEASUREMENT_UNIT_ID,
                                                                    MeasurementUnitName = p.MEASUREMENT_UNIT
                                                                }).ToList();
                        return ingredients;
                    }
                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        internal bool DeleteGeneralList(int listId)
        {
            using (DataContext)
            {
                try
                {
                    GeneralList generalList =
                        (from p in DataContext.GeneralLists where p.ID == listId select p).Single();

                    DataContext.GeneralLists.DeleteOnSubmit(generalList);
                    DataContext.SubmitChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        internal bool DeleteGeneralListItem(int ingredientId)
        {
            using (DataContext)
            {
                try
                {
                    GeneralListDetail generalListDetail = (from p in DataContext.GeneralListDetails
                                                             where p.ID == ingredientId
                                                             select p).Single();

                    DataContext.GeneralListDetails.DeleteOnSubmit(generalListDetail);
                    DataContext.SubmitChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}