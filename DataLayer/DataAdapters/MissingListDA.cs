using System.Collections.Generic;
using System.Linq;
using MyBuyList.Shared.Entities;
using System;
using System.Data.Linq;

namespace MyBuyList.DataLayer.DataAdapters
{
    class MissingItemsListDA : BaseContextDataAdapter<MyBuyListDBContext>
    {
        internal int AddList(int userId)
        {
            using (DataContext)
            {
                MissingList missingList = DataContext.MissingLists.SingleOrDefault(p => p.CREATED_BY == userId && p.ACTIVE.HasValue ? p.ACTIVE.Value : false);

                if (missingList == null)
                {
                    missingList = new MissingList { CREATED_BY = userId, CREATE_DATE = DateTime.Today, ACTIVE = true };
                    DataContext.MissingLists.InsertOnSubmit(missingList);
                    DataContext.SubmitChanges();
                }

                return missingList.ID;
            }
        }

        internal void AddListItem(string name, int quantity, int measureUnit, int userId)
        {
            int missingListId;
            using (DataContext)
            {
                MissingList missingList = DataContext.MissingLists.SingleOrDefault(p => p.CREATED_BY == userId && p.ACTIVE.HasValue ? p.ACTIVE.Value : false);

                if (missingList != null)
                {
                    missingListId = missingList.ID;

                    MissingListDetail missingListDetail = null;

                    Food food = DataContext.Foods.SingleOrDefault(p => p.FoodName == name);

                    if (food == null)
                    {
                        food = new Food
                        {
                            FoodName = name,
                            CreatedBy = userId,
                        };
                        new AdminDA().SaveFood(food);
                        food = DataContext.Foods.SingleOrDefault(p => p.FoodName == name);
                    }

                    missingListDetail = DataContext.MissingListDetails.SingleOrDefault(p => p.LIST_ID == missingListId && p.FOOD_ID == food.FoodId);

                    if (missingListDetail != null)
                    {
                        missingListDetail.QUANTITY = quantity;
                    }
                    else
                    {
                        missingListDetail = new MissingListDetail
                        {
                            FOOD_ID = food.FoodId,
                            LIST_ID = missingListId,
                            QUANTITY = quantity,
                            MEASUREMENT_UNIT_ID = measureUnit
                        };
                        DataContext.MissingListDetails.InsertOnSubmit(missingListDetail);
                    }
                }

                DataContext.SubmitChanges();
            }
        }

        internal IQueryable<MissingListDetail> GetList(int userId)
        {
            MissingList missingList = DataContext.MissingLists.SingleOrDefault(p => p.CREATED_BY == userId);

            if (missingList != null)
            {
                IQueryable<MissingListDetail> missingListDetail = DataContext.MissingListDetails.Where(p => p.LIST_ID == missingList.ID);
                return missingListDetail;
            }

            return null;
        }

        internal void DeleteList(int listId)
        {
            using (DataContext)
            {
                MissingList missingList = DataContext.MissingLists.SingleOrDefault(p => p.ID == listId);
                if (missingList != null)
                {
                    DataContext.MissingLists.DeleteOnSubmit(missingList);
                    DataContext.SubmitChanges();
                }
            }
        }

        internal void DeleteListItem(int id)
        {
            using (DataContext)
            {
                int listId = 0;
                MissingListDetail missingListDetail = DataContext.MissingListDetails.SingleOrDefault(p => p.ID == id);
                if (missingListDetail != null)
                {
                    listId = missingListDetail.LIST_ID;
                    DataContext.MissingListDetails.DeleteOnSubmit(missingListDetail);
                    DataContext.SubmitChanges();
                }

                if (!DataContext.MissingListDetails.Any())
                {
                    DeleteList(listId);
                }
            }
        }

        internal void UpdateMissingListItem(int id, int quantity)
        {
            using (DataContext)
            {
                MissingListDetail missingListDetail = DataContext.MissingListDetails.SingleOrDefault(p => p.ID == id);
                if (missingListDetail != null)
                {
                    missingListDetail.QUANTITY = quantity;
                    DataContext.SubmitChanges();
                }
            }
        }
    }
}
