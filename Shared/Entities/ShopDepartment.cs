using System;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    partial class ShopDepartment
    {
        public bool AllowDelete
        {
            get
            {
                bool b = true;

                foreach (FoodCategory currCat in this.FoodCategories)
                {
                    if (currCat.Foods != null)
                    {
                        if (currCat.Foods.Count > 0)
                        {
                            b = false;
                            break;
                        }

                    }
                }

                return b;
            }
        }
    }
}