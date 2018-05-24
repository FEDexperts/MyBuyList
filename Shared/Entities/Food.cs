using System;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    partial class Food
    {
        public bool AllowDelete
        {
            get
            {
                bool b = true;

                //if (this.Ingredients != null)
                //{
                //    if (this.Ingredients.Count > 0)
                //    {
                //        b = false;
                //    }
                //}

                return b;
            }
        }
    }
}