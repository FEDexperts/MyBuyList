using System;
using System.Data.Linq;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    partial class FoodCategory
    {
        public EntitySet<FoodCategory> ParentFoodCategories
        {
            get
            {
                return this._FoodCategories;
            }
        }

        private int foodCount;
        public int FoodCount
        {
            get { return foodCount; }
            set { foodCount = value; }
        }

        private bool allowDelete;
        public bool AllowDelete
        {
            get { return allowDelete; }
            internal set { allowDelete = value; }
        }
    }
}