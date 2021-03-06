namespace MyBuyList.Shared
{
    partial class MeasurementUnit
    {
    }
}

namespace MyBuyList.Shared.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq.Mapping;
    using System.Runtime.Serialization;

    partial class MyBuyListDBContext
    {
        public void ExecuteProcedure(string procName, object[] objects)
        {
            string command = string.Format("exec {0} ", procName);

            for (int i = 0; i < objects.Length; i++)
            {
                if (i != 0)
                    command += ",";
                command += "{" + string.Format("{0}", i) + "}";
            }

            ExecuteCommand(command, objects);
        }

        public IEnumerable<T> ExecuteProcedure<T>(string procName, object[] objects) where T : class
        {
            string command = string.Format("exec {0} ", procName);

            for (int i = 0; i < objects.Length; i++)
            {
                if (i != 0)
                    command += ",";
                command += "{" + string.Format("{0}", i) + "}";
            }

            //IEnumerable<T> executeCommand = ExecuteQuery<T>(command, objects);
            //return executeCommand;

			return null;
        }
    }

    [Serializable]
    public partial class MeasurementUnitsConvert
    {
        [DataMember]
        public string FOOD_NAME
        {
            get
            {
                return Food.FoodName;
            }
        }
        [DataMember]
        public string SOURCE_UNIT_NAME
        {
            get
            {
                return MeasurementUnit.UnitName;
            }
        }
        [DataMember]
        public string TARGET_UNIT_NAME
        {
            get
            {
                return MeasurementUnit.UnitName;
            }
        }
    }

    [Serializable]
    public partial class MissingList
    {
    }

    public partial class MissingListDetail
    {
        [DataMember]
        public string FOOD_NAME
        {
            get
            {
                return Food.FoodName;
            }
            set
            {
            }
        }
        [DataMember]
        public string UNIT_NAME
        {
            get
            {
                return MeasurementUnit.UnitName;
            }
            set
            {

            }
        }
    }

    public partial class Recipe
    {
        [DataMember]
        public bool SHOPPING_LIST 
        {
            get
            {
                return RecipesInShoppingLists != null ? RecipesInShoppingLists.Count != 0 : false;
            }
            set
            {

            }
        }
    }

    public partial class RecipesInShoppingList
    {
        [DataMember]
        public string RECIPE_NAME
        {
            get
            {
                return Recipe.RecipeName;
            }
            set
            {

            }
        }
    }

    public partial class MenusInShoppingList
    {
        [DataMember]
        public string MENU_NAME
        {
            get
            {
                return Menu.MenuName;
            }
            set
            {

            }
        }
    }

    public partial class SavedListDetail
    {
        [DataMember]
        public string FOOD_NAME
        {
            get
            {
                return Food.FoodName;
            }
            set { }
        }
        [DataMember]
        public string UNIT_NAME
        {
            get
            {
                return MeasurementUnit.UnitName;
            }
            set { }
        }
    }

    public partial class Ingredient
    {
        [DataMember]
        public string FOOD_NAME
        {
            get
            {
                return Food.FoodName;
            }
            set { }
        }

        [DataMember]
        public string MEASUREMENT_NAME
        {
            get
            {
                return MeasurementUnit.UnitName;
            }
            set { }
        }
        [DataMember]
        public string DISPLAY_NAME
        {
            get
            {
                string displayQuantity = "";
                string[] arr = Quantity.ToString().Split(new string[]{".",","}, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    if (arr[0] != "0")
                        displayQuantity = arr[0];
                }
                if (arr.Length > 1 && arr[1] != "" && arr[1] != "00")
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
                return string.Format("{0} {1} {2} {3}", displayQuantity, MeasurementUnit.UnitName, Food.FoodName, Remarks);
            }
            set { }
        }
        [DataMember]
        public string CompleteValue { get; set; }
        [DataMember]
        public string FractionValue { get; set; }
    }

    public partial class UserShoppingList
    {
        [DataMember]
        public string FriendlyQuantity
        {
            get
            {
                string displayQuantity = "";
                string[] arr = QUANTITY.ToString().Split(new string[] { ".", "," }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    if (arr[0] != "0")
                        displayQuantity = arr[0];
                }
                if (arr.Length > 1 && arr[1] != "" && arr[1] != "00")
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
                        displayQuantity = QUANTITY.ToString();
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
                return displayQuantity;
            }
            set { }
        }
    }
}
