using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using MyBuyList.BusinessLayer.Managers;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;

public partial class Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Literal body = new Literal();
        if (Request["code"] != null)
        {
            int code = int.Parse(Request["code"].ToString());

            switch (code)
            {
                // print recipe
                case 1:
                    {
                        if (Request["recipeId"] != null)
                        {
                            int recipeId = int.Parse(Request["recipeId"].ToString());
                            body.Text = this.PrintRecipe(recipeId);
                        }
                        break;
                    }
                // print menuMeals
                case 2:
                    {
                        if (Request["menuId"] != null)
                        {
                            int menuId = int.Parse(Request["menuId"].ToString());
                            body.Text = this.PrintMenuMeals(menuId);
                        }
                        break;
                    }
                // print menuRecipes
                case 3:
                    {
                        if (Request["menuId"] != null)
                        {
                            int menuId = int.Parse(Request["menuId"].ToString());
                            body.Text = this.PrintMenuRecipes(menuId);
                        }
                        break;
                    }
                // print ShopingList
                case 4:
                    {
                        if (Request["menuId"] != null)
                        {
                            int menuId = int.Parse(Request["menuId"].ToString());
                            body.Text = this.PrintShoppingList(menuId);
                        }
                        break;
                    }

            }
        }

        PrintHelper.PrintWebControl(body);
    }

    private string PrintMenuRecipes(int menuId)
    {
        MyBuyList.Shared.Entities.Menu menu = BusinessFacade.Instance.GetMenu(menuId);
        string data = "<html>" +
                                 "<body dir='rtl'>" +
                                       "<table  dir='rtl'> " +
                                             "<tr>" +
                                                 "<td style='width:400px;'> " +
                                                     " &nbsp; " +
                                                 "</td> " +
                                                 "<td>" +
                                                     "&nbsp;" +
                                                 "</td>" +
                                                 "<td rowspan='2'>" +
                                                     "<img src='http://www.mybuylist.com/Images/New/Logo.gif' />" +
                                                 "</td>" +
                                            "</tr>" +
                                             "<tr>" +
                                                 "<td colspan='2' align='center'>" +
                                                     "&nbsp;" +
                                                     "<b style='font-size:larger; color:Red;'>" + menu.MenuName + "</b>" +

                                                 "</td>" +
                                             "</tr>" +
                                             "<tr> " +
                                                     "<td>  ";


        MenuRecipe[] recipeList = BusinessFacade.Instance.GetMenuRecipes(menuId).ToArray<MenuRecipe>();
        Dictionary<int, List<int>> recipeServList = BusinessFacade.Instance.GetMenuRecipesIngrid(menuId);
        bool isFirst = true;
        foreach (MenuRecipe currMenuRecipe in recipeList)
        {
            if (isFirst)
            {
                isFirst = false;
            }
            else
            {
                data += "<hr />";
            }

            if (recipeServList.ContainsKey(currMenuRecipe.RecipeId))
            {
                List<int> RecipeServings = recipeServList[currMenuRecipe.RecipeId];
                RecipeServings.Sort();
                data += "<b>" + currMenuRecipe.Recipe.RecipeName + "</b><br>";

                foreach(int serv in RecipeServings)
                {
                    if (serv == currMenuRecipe.Recipe.Servings)
                    {
                        data += "<b>מצרכים כפי שהוגדרו במתכון המקורי (" + serv.ToString() + " מנות):</b> <br>";
                        foreach (Ingredient cuurIngredient in currMenuRecipe.Recipe.Ingredients)
                        {
                            data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + new SRL_Ingredient(cuurIngredient).DisplayIngredient;
                            data += "<br>";
                        }
                    }
                    else
                    {
                        data += "<b>מצרכים כפי שבחרת לארוחה עבור (" + serv.ToString() + " מנות):</b> <br>";
                        foreach (Ingredient cuurIngredient in currMenuRecipe.Recipe.Ingredients)
                        {
                            SRL_Ingredient cuurSRLIngredient = new SRL_Ingredient();
                            cuurSRLIngredient.FoodId = cuurIngredient.FoodId;
                            cuurSRLIngredient.FoodName = BusinessFacade.Instance.GetFood(cuurSRLIngredient.FoodId).FoodName;
                            cuurSRLIngredient.MeasurementUnitId = cuurIngredient.MeasurementUnitId;
                            cuurSRLIngredient.MeasurementUnitName = BusinessFacade.Instance.GetMeasurementUnit(cuurSRLIngredient.MeasurementUnitId).UnitName;
                            cuurSRLIngredient.Quantity = Decimal.Round((cuurIngredient.Quantity / currMenuRecipe.Recipe.Servings) * serv, 2);
                            cuurSRLIngredient.Remarks = cuurIngredient.Remarks;
                            data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + cuurSRLIngredient.DisplayIngredient;
                            data += "<br>";
                        }
                    }

                }

                data += "<br><b>אופן הכנה: </b><br>";
                data += currMenuRecipe.Recipe.PreparationMethod + "<br><br>";

            }
            else
            {
                data += "<b>" + currMenuRecipe.Recipe.RecipeName + "</b><br>";
                data += "<b>מצרכים:</b> <br>";
                foreach (Ingredient cuurIngredient in currMenuRecipe.Recipe.Ingredients)
                {
                    data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + new SRL_Ingredient(cuurIngredient).DisplayIngredient;
                    data += "<br>";
                }

                data += "<br><b>אופן הכנה: </b><br>";
                data += currMenuRecipe.Recipe.PreparationMethod + "<br><br>";
            }
        }


        data += "</td> " +
              "</tr> " +
        "</table> " +
        "</body>" +
         "</html>";

        return data;
    }

    private string PrintMenuMeals(int menuId)
    {
        MyBuyList.Shared.Entities.Menu menu = BusinessFacade.Instance.GetMenu(menuId);

        bool isWeekly = false;

        if (menu.MenuTypeId != 1)
        {
            isWeekly = true;
        }

        string data = "<html>" +
                                "<body dir='rtl'>" +
                                      "<table  dir='rtl'> " +
                                             "<tr>" +
                                                "<td style='width:400px;'> " +
                                                    " &nbsp; " +
                                                "</td> " +
                                                "<td>" +
                                                    "&nbsp;" +
                                                "</td>" +
                                                "<td rowspan='2'>" +
                                                    "<img src='http://www.mybuylist.com/Images/New/Logo.gif' />" +
                                                "</td>" +
                                           "</tr>" +
                                            "<tr>" +
                                                "<td colspan='2' align='center'>" +
                                                    "&nbsp;" +
                                                    "<b style='font-size:larger; color:Red;'>" + menu.MenuName + "</b>" +

                                                "</td>" +
                                            "</tr>" +

                                            "<tr> " +
                                                    "<td>  ";

        if (isWeekly)
        {
            MealType[] mealTypes = BusinessFacade.Instance.GetMealTypes();
            for (int i = 1; i <= 7; i++)
            {
                data += "<b> יום מספר " + i.ToString() + ":</b><br>";
                foreach (MealType currType in mealTypes)
                {
                    Meal[] currDayMeals = (BusinessFacade.Instance.GetMealsList(menuId)).Where(m => m.DayIndex == i && m.MealTypeId == currType.MealTypeId).ToArray<Meal>();

                    data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;" + currType.MealTypeName + "- <br>";
                    foreach (Meal cuurMeal in currDayMeals)
                    {
                        foreach (MealRecipe currRecipe in cuurMeal.MealRecipes)
                        {
                            data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + currRecipe.Recipe.RecipeName;
                            data += "<br>";
                        }
                    }
                }
            }
        }
        else
        {
            CourseType[] courseTypeList = BusinessFacade.Instance.GetCourseTypes();
            foreach (CourseType currType in courseTypeList)
            {
                Meal[] currDayMeals = (BusinessFacade.Instance.GetMealsList(menuId)).Where(m => m.CourseTypeId == currType.CourseTypeId).ToArray<Meal>();
                data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;" + currType.CourseTypeName + "- <br>";
                foreach (Meal cuurMeal in currDayMeals)
                {
                    foreach (MealRecipe currRecipe in cuurMeal.MealRecipes)
                    {
                        data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + currRecipe.Recipe.RecipeName;
                        data += "<br>";
                    }
                }
            }
        }

        data += "</td> " +
              "</tr> " +
        "</table> " +
        "</body>" +
         "</html>";

        return data;
    }

    private string PrintShoppingList(int menuId)
    {
        string data = "<html>" +
                                "<body dir='rtl' style='font-family: arial; margin-top:0px;'>" +
                                      "<table  dir='rtl'> " +
                                            "<tr style='margin-top:0px;'>" +
                                                            "<td colspan='2' align='center'>" +
                                                                "<img src='http://www.mybuylist.com/Images/New/SpiralVertical.gif'/>" +
                                                            "</td>" +
                                                        "</tr>" +
                                             "<tr> " +
                                                "<td>" +
                                                    "<table>" +

                                                        "<tr>" +
                                                            "<td style='width:400px;'> " +
                                                                " &nbsp; " +
                                                            "</td> " +
                                                            "<td>" +
                                                                "&nbsp;" +
                                                            "</td>" +

                                                        "<tr>" +
                                                            "<td align='center'><b> הדרך המטעימה והנעימה שלך לרשימת קניות </b> </td>" +
                                                            "<td rowspan='2'>" +

                                                                "<img src='http://www.mybuylist.com/Images/New/Logo.gif' />" +
                                                            "</td>" +
                                                        "</tr>" +
                                                        "<tr>" +
                                                            "<td align='center'>" +

                                                                "<b style='font-size:larger; color:Red;'>רשימת קניות</b>" +

                                                            "</td>" +
                                                        "</tr>" +
                                                 "</table>" +
                                                 "</td>" +
                                            "</tr>" +
                                            "<tr> " +
                                                    "<td dir='rtl'> " +
                                                        "<b>מצרכים: </b>" +
                                                    "</td> " +
                                            "</tr> " +
                                            "<tr> " +
                                                    "<td>  ";
        ShopDepartment[] listShopDepartments = BusinessFacade.Instance.GetMenuShopDepartments(menuId);
        ShoppingFood[] allShoppingFoods = BusinessFacade.Instance.GetMenuShoppingList(menuId);

        foreach (ShopDepartment cuurShopDepartments in listShopDepartments)
        {
            data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>" + cuurShopDepartments.ShopDepartmentName + ":</b>";
            data += "<br/>";

            if (allShoppingFoods != null)
            {
                ShoppingFood[] departmentShopingFood = allShoppingFoods.Where(list => list.ShopDepartmentId == cuurShopDepartments.ShopDepartmentId).ToArray();
                foreach (ShoppingFood currFood in departmentShopingFood)
                {
                    //data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + currFood.DisplayQuantity.Replace(" ","&nbsp;") + "&nbsp;" + currFood.FoodName;
                    //data += "<br />";
                    
                    //data += string.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}</span>&nbsp;<span>{1}</span><br />", displayQuantity, currFood.FoodName);
                    data += string.Format("<table style='padding-right: 40px;' cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style='width:2px;'>•</td><td align=\"right\">{0}</td><td>&nbsp;{1}</td></tr></table>", currFood.Display, currFood.FoodName);
                }
            }
        }

        data += "</td> " +
              "</tr> " +
              "<tr> " +
                      "<td> " +
                          "<br />" +
                          "<b> מוצרים נוספים:</b> " +
                      "</td> " +
              "</tr> " +
              "<tr> " +
                      "<td dir='rtl' style='padding-right: 40px;'> ";
        ShoppingListAdditionalItem[] additionalItems = BusinessFacade.Instance.GetShoppingListAdditionalItems(menuId);

        foreach (ShoppingListAdditionalItem cuurAdditionalItems in additionalItems)
        {
            if ((cuurAdditionalItems != null) && (cuurAdditionalItems.GeneralItem != null))
            {
                data += " * " + cuurAdditionalItems.GeneralItem.GeneralItemName + "<br />";
            }
            else if ((cuurAdditionalItems != null) && (cuurAdditionalItems.ItemName != null))
            {
                data += " * " + cuurAdditionalItems.ItemName + "<br />";
            }
        }
        data += "</td> " +
                 "</tr> " +
                 "<tr style='Hight:40px;'> <td> &nbsp;<br/> </td> </tr> " +
                  "<tr> " +
                    "<td dir='rtl'> " +
                        "<b>מתכונים: </b>" +
                     "</td> " +
                 "</tr> " +
                 "<tr> " +
                    "<td style='padding-right: 40px' >  ";

        Dictionary<string, Recipe> resipes = BusinessFacade.Instance.GetRecipesInMenuMeals(menuId);

        foreach (Recipe cuurMenuRecipe in resipes.Values)
        {
            data += cuurMenuRecipe.RecipeName;
            data += "<br />";
        }

        data += "</td> " +
              "</tr> " +
                "</table> " +
                "</body>" +
                "</html>";

        return data;
    }

    protected string PrintRecipe(int recipeId)
    {
        Recipe currRecipe = BusinessFacade.Instance.GetRecipe(recipeId);

        string data = "<html>" +
                                "<body dir='rtl'>" +
                                      "<table  dir='rtl'> " +
                                           "<tr>" +
                                                "<td style='width:400px;'> " +
                                                    " &nbsp; " +
                                                "</td> " +
                                                "<td>" +
                                                    "&nbsp;" +
                                                "</td>" +
                                                "<td rowspan='2'>" +
                                                    "<img src='Images/New/Logo.gif' />" +
                                                "</td>" +
                                           "</tr>" +
                                            "<tr>" +
                                                "<td colspan='2' align='center'>" +
                                                    "&nbsp;" +
                                                    "<b style='font-size:larger; color:Red;'>" + currRecipe.RecipeName + "</b>" +

                                                "</td>" +
                                            "</tr>" +
                                            "<tr> " +
                                                    "<td>" +

        "<b>מצרכים-</b> <br>";
        foreach (Ingredient ing in currRecipe.Ingredients)
        {
            data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + new SRL_Ingredient(ing).DisplayIngredient;
            data += "<br>";
        }

        data += "<br><b>אופן הכנה -</b><br>";
        data += currRecipe.PreparationMethod + "<BR><BR>";
        data += "</td> " +
              "</tr> " +
        "</table> " +
        "</body>" +
         "</html>";

        return data;
    }
}
