using System;
using System.Configuration;

namespace MyBuyList.Shared.Entities
{

    [Serializable]
    public class SRL_Ingredient
    {
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int MeasurementUnitId { get; set; }
        public string MeasurementUnitName { get; set; }
        public decimal Quantity { get; set; }
        public string Remarks { get; set; }
        public string CompleteValue { get; set; }
        public string FractionValue { get; set; }

        public string DisplayIngredient
        {
            get
            {
                //string numberSeperator = ConfigurationManager.AppSettings["NumberSeperator"];
                string displayQuantity = "";
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
                return string.Format("{0} {1} {2} {3}", displayQuantity,
                                     this.MeasurementUnitId == 0 ? "" : MeasurementUnitName,
                                     this.FoodName, string.IsNullOrEmpty(this.Remarks) ? "" : this.Remarks);
            }
        }

        public SRL_Ingredient()
        {
        }

        public SRL_Ingredient(Ingredient ingredient)
        {
            this.IngredientId = ingredient.IngredientId;
            this.RecipeId = ingredient.RecipeId;
            this.FoodId = ingredient.FoodId;
            this.FoodName = ingredient.Food.FoodName;
            this.MeasurementUnitId = ingredient.MeasurementUnitId;
            this.MeasurementUnitName = ingredient.MeasurementUnit.UnitName;
            this.Quantity = ingredient.Quantity;
            this.Remarks = ingredient.Remarks;

            string numberSeperator = ConfigurationManager.AppSettings["NumberSeperator"];
            string[] arr = this.Quantity.ToString().Split(numberSeperator.ToCharArray());
            if (arr.Length > 0)
            {
                if (arr[0] != "0")
                    this.CompleteValue = arr[0];
            }
            if (arr.Length > 1 && arr[1] != "")
            {
                while (arr[1].EndsWith("0"))
                {
                    arr[1] = arr[1].Remove(arr[1].Length - 1, 1);
                }

                if (arr[1] == "25")
                {
                    this.FractionValue = "0.25";
                }
                else if (arr[1] == "3" || arr[1] == "33" || arr[1] == "34")
                {
                    this.FractionValue = "0.33";
                }
                else if (arr[1] == "5")
                {
                    this.FractionValue = "0.50";
                }
                else if (arr[1] == "6" || arr[1] == "66" || arr[1] == "67")
                {
                    this.FractionValue = "0.66";
                }
                else if (arr[1] == "75")
                {
                    this.FractionValue = "0.75";
                }
                else
                {
                    string displayQuantity = this.Quantity.ToString();
                    while (displayQuantity.EndsWith("0"))
                    {
                        displayQuantity = displayQuantity.Remove(displayQuantity.Length - 1, 1);
                    }

                    if (displayQuantity.EndsWith("."))
                    {
                        displayQuantity = displayQuantity.Remove(displayQuantity.Length - 1, 1);
                    }

                    this.CompleteValue = displayQuantity;
                }
            }


        }


    }
}