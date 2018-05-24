using System;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    partial class GeneralItem
    {
        public bool AllowDelete
        {
            get { return (this.ShoppingListAdditionalItems.Count == 0); }
        }
    }
}