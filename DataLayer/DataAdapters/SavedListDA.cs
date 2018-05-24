using System.Collections.Generic;
using System.Linq;
using MyBuyList.Shared.Entities;
using System;

namespace MyBuyList.DataLayer.DataAdapters
{
    class SavedListDA : BaseContextDataAdapter<MyBuyListDBContext>
    {
        internal SavedList AddList(int userId, string name)
        {
            using (DataContext)
            {
                SavedList savedList = new SavedList
                                      {
                                          CREATED_BY = userId,
                                          CREATE_DATE = DateTime.Today,
                                          ACTIVE = true,
                                          NAME = name,
                                          SHOPPING_LIST = false
                                      };

                DataContext.SavedLists.InsertOnSubmit(savedList);
                DataContext.SubmitChanges();

                return savedList;
            }
        }

        internal SavedListDetail AddListItem(string name, int quantity, int listId)
        {
            Food food = new AdminDA().GetFood(name);

            if (food == null) return null;

            SavedListDetail savedListDetails = (from p in DataContext.SavedListDetails
                                                where p.LIST_ID == listId && p.FOOD_ID == food.FoodId
                                                select p).SingleOrDefault();

            if (savedListDetails == null)
            {
                savedListDetails = new SavedListDetail
                                                            {
                                                                FOOD_ID = food.FoodId,
                                                                LIST_ID = listId,
                                                                QUANTITY = quantity,
                                                                MEASUREMENT_UNIT_ID = food.CalculateUnitId
                                                            };

                DataContext.SavedListDetails.InsertOnSubmit(savedListDetails);
            }
            else
            {
                savedListDetails.QUANTITY = quantity;
            }

            DataContext.SubmitChanges();

            return savedListDetails;
        }

        internal IQueryable<SavedList> GetLists(int userId)
        {
            IQueryable<SavedList> savedLists = from p in DataContext.SavedLists
                                               where p.CREATED_BY == userId && p.ACTIVE == true
                                               select p;
            return savedLists;
        }

        internal IQueryable<SavedListDetail> GetListDetails(int listId)
        {
            IQueryable<SavedListDetail> savedListDetails = from p in DataContext.SavedListDetails
                                                           where p.LIST_ID == listId
                                                           select p;
            return savedListDetails;
        }

        internal bool DeleteList(int listId)
        {
            bool result = false;
            IQueryable<SavedListDetail> savedListDetails = (from p in DataContext.SavedListDetails
                                                            where p.LIST_ID == listId
                                                            select p);

            DataContext.SavedListDetails.DeleteAllOnSubmit(savedListDetails);

            SavedList savedLists = (from p in DataContext.SavedLists
                                    where p.ID == listId
                                    select p).SingleOrDefault();

            DataContext.SavedLists.DeleteOnSubmit(savedLists);

            DataContext.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);
            result = true;

            return result;
        }

        internal void DeleteListItem(int ingredientId)
        {
            using (DataContext)
            {
                SavedListDetail savedListDetail = (from p in DataContext.SavedListDetails
                                                   where p.ID == ingredientId
                                                   select p).SingleOrDefault();

                DataContext.SavedListDetails.DeleteOnSubmit(savedListDetail);

                bool isEmpty = DataContext.SavedListDetails.Where(p => p.LIST_ID == savedListDetail.LIST_ID).Count() == 1;
                if (isEmpty)
                {
                    SavedList savedList = DataContext.SavedLists.SingleOrDefault(p => p.ID == savedListDetail.LIST_ID);
                    DataContext.SavedLists.DeleteOnSubmit(savedList);
                }

                DataContext.SubmitChanges();
            }
        }

        internal void UpdateSaveList(int listId, bool shoppingList)
        {
            using (DataContext)
            {
                SavedList savedList = DataContext.SavedLists.SingleOrDefault(p => p.ID == listId);
                if (savedList != null)
                {
                    savedList.SHOPPING_LIST = shoppingList;
                }
                DataContext.SubmitChanges();
            }
        }

        internal void UpdateSavedListItem(int id, int quantity)
        {
            using (DataContext)
            {
                SavedListDetail savedListDetails = DataContext.SavedListDetails.SingleOrDefault(p => p.ID == id);
                if (savedListDetails != null)
                {
                    savedListDetails.QUANTITY = quantity;
                    DataContext.SubmitChanges();
                }
            }
        }
    }
}
