using System;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    partial class Ingredient
    {
        private string _foodName;
        public string FoodName
        {
            get { return _foodName; }
            set { _foodName = value; }
        }
    }
}