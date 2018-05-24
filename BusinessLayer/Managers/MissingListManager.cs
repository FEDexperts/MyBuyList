using System;
using System.Collections.Generic;
using MyBuyList.Shared.Entities;
using System.Linq;
using MyBuyList.DataLayer;

namespace MyBuyList.BusinessLayer.Managers
{
    class MissingListManager
    {
        internal int AddMissingList(int userId)
        {
            return DataFacade.Instance.AddMissingList(userId);
        }

        internal void AddMissingListItem(string name, int quantity, int measureUnit, int userId)
        {
            DataFacade.Instance.AddMissinListItem(name, quantity, measureUnit, userId);
        }

        internal void DeleteShortageListItem(int ingredientId)
        {
            DataFacade.Instance.DeleteMissingListItem(ingredientId);
        }

        internal IQueryable<MissingListDetail> GetMissingList(int userId)
        {
            return DataFacade.Instance.GetMissingList(userId);
        }

        internal void DeleteFromMissingList(int id)
        {
            DataFacade.Instance.DeleteMissingListItem(id);
        }

        internal void UpdateMissingListItem(int id, int quantity)
        {
            DataFacade.Instance.UpdateMissingListItem(id, quantity);
        }
    }
}
