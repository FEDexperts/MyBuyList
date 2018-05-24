using System.Collections.Generic;
using MyBuyList.Shared.Entities;

namespace MyBuyList.BusinessLayer.Managers
{
    public class SummeryListManager
    {
        internal int AddList(int userId)
        {
            return DataLayer.DataFacade.Instance.AddSummeryList(userId);
        }

        internal bool AddListItem(SRL_Ingredient ingredient, int listId, int sourceId)
        {
            return DataLayer.DataFacade.Instance.AddSummeryListItem(ingredient, listId, sourceId);
        }

        public int GetSummeryList(int userId)
        {
            return DataLayer.DataFacade.Instance.GetSummeryList(userId);
        }

        internal List<SRL_Ingredient> GetListDetails(int listId)
        {
            return DataLayer.DataFacade.Instance.GetSummeryListDetails(listId);
        }

        internal void DeleteList(int listId)
        {
            DataLayer.DataFacade.Instance.DeleteSummeryList(listId);
        }

        internal void DeleteListItem(int summeryId, int sourceId, SRL_Ingredient ingredient)
        {
            DataLayer.DataFacade.Instance.DeleteSummeryListItem(summeryId, sourceId, ingredient);
        }
    }
}