using System.Collections.Generic;
using System.Linq;
using MyBuyList.Shared.Entities;
using MyBuyList.DataLayer;

namespace MyBuyList.BusinessLayer.Managers
{
    class SavedListManager
    {
        internal SavedList AddList(int userId, string name)
        {
            return DataFacade.Instance.AddSavedList(userId, name);
        }

        internal SavedListDetail AddListItem(string name, int quantity, int listId)
        {
            return DataFacade.Instance.AddSavedListItem(name, quantity, listId);
        }

        internal IQueryable<SavedListDetail> GetListDetails(int listId)
        {
            return DataFacade.Instance.GetSavedListDetails(listId);
        }

        public void DeleteListItem(int ingredientId)
        {
            DataFacade.Instance.DeleteSavedListItem(ingredientId);
        }

        public IQueryable<SavedList> GetLists(int userId)
        {
            return DataFacade.Instance.GetSavedLists(userId);
        }

        public bool DeleteList(int listId)
        {
            return DataFacade.Instance.DeleteSavedList(listId);
        }

        internal void UpdateSaveList(int listId, bool shoppingList)
        {
            DataFacade.Instance.UpdateSaveList(listId, shoppingList);
        }

        internal void UpdateSavedListItem(int id, int quantity)
        {
            DataFacade.Instance.UpdateSavedListItem(id, quantity);
        }
    }
}
