using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    public class ShoppingFood
    {
        private string displayQty;
       // private string displayQtyInGrams;

        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int ShopDepartmentId { get; set; }
        public int CalculateUnitId { get; set; }
        public string CalculateUnitName { get; set; }
        public int FractionValue { get; set; }
        public decimal Quantity { get; set; }
        public string Remarks { get; set; }
        public string CompleteValue { get; set; }
        
        public Binary Picture { get; set; }
        public bool PrintPicture { get; set; }

        public string Display
        {
            get
            {
                string displayQuantity = "";
                this.Quantity = Decimal.Round(this.Quantity, 2);
                string[] arr = this.Quantity.ToString().Split(".".ToCharArray());
                if (arr.Length > 0)
                {
                    if (arr[0] != "0")
                        displayQuantity = arr[0];
                }
                if (arr.Length > 1 && arr[1] != "")
                {
                    
                    while (arr[1].EndsWith("0"))
                    {
                        arr[1] = arr[1].Remove(arr[1].Length - 1, 1);
                    }

                     
                    if (arr[1] == "25")
                    {
                        displayQuantity = "¼" + displayQuantity;
                    }
                    else if (arr[1] == "3" || arr[1] == "33" || arr[1] == "34")
                    {
                        displayQuantity = "⅓" + displayQuantity;
                    }
                    else if (arr[1] == "5")
                    {
                        displayQuantity = "½" + displayQuantity;
                    }
                    else if (arr[1] == "6" || arr[1] == "66" || arr[1] == "67")
                    {
                        displayQuantity = "⅔" + displayQuantity;
                    }
                    else if (arr[1] == "75")
                    {
                        displayQuantity = "¾" + displayQuantity;
                    }
                    else
                    {
                        displayQuantity = this.Quantity.ToString();
                        while (displayQuantity.EndsWith("0"))
                        {
                            displayQuantity = displayQuantity.Remove(displayQuantity.Length - 1, 1);
                        }
                        if (displayQuantity.EndsWith("."))
                        {
                            displayQuantity = displayQuantity.Remove(displayQuantity.Length - 1, 1);
                        }
                    }
                }

                return string.Format("{0} {1}", displayQuantity, this.CalculateUnitName);
                                                           
            }
            set 
            {
                this.displayQty = value;
            }
        }

        public ShoppingFood(Food food)
        {
            this.FoodId = food.FoodId;
            this.FoodName = food.FoodName;
            this.ShopDepartmentId = food.FoodCategory.ShopDepartmentId;
            this.CalculateUnitId = food.CalculateUnitId;
            
            this.Picture = food.Picture;
            this.PrintPicture = food.PrintPicture;
        }

    }
}
