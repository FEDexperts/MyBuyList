using System;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    partial class MealRecipe
    {
        private int expectedServings;
        public int ExpectedServings
        {
            get { return expectedServings; }
            set { expectedServings = value; }
        }
    }
}
