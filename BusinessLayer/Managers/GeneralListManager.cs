using System.Collections.Generic;
using MyBuyList.Shared.Entities;
using MyBuyList.Shared.Enums;

namespace MyBuyList.BusinessLayer.Managers
{
    class GeneralListManager
    {
        internal int AddGeneralList(int userId, ListTypes listType)
        {
            return DataLayer.DataFacade.Instance.AddGeneralList(userId, listType);
        }

        internal bool AddGeneralListItem(SRL_Ingredient ingredient, int listId)
        {
            return DataLayer.DataFacade.Instance.AddGeneralListItem(ingredient, listId);
        }

        internal List<SRL_Ingredient> GetGeneralList(int userId, ListTypes listType)
        {
            return DataLayer.DataFacade.Instance.GetGeneralList(userId, listType);
        }

        public bool DeleteGeneralList(int listId)
        {
            return DataLayer.DataFacade.Instance.DeleteGeneralList(listId);
        }

        public bool DeleteGeneralListItem(int ingredientId)
        {
            return DataLayer.DataFacade.Instance.DeleteGeneralListItem(ingredientId);
        }
    }
}