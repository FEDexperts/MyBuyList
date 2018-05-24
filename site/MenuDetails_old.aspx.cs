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
using System.IO;
using System.Text;
using System.Web.Mail;

using MyBuyList.BusinessLayer.Managers;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using ProperControls.Pages;

public partial class PageMenuDetails_old : BasePage //System.Web.UI.Page
{
    int MenuId
    {
        get { return ViewState["MenuId"] == null ? 0 : (int)ViewState["MenuId"]; }
        set { ViewState["MenuId"] = value; }
    }

    MyBuyList.Shared.Entities.Menu Menu
    {
        get { return (MyBuyList.Shared.Entities.Menu)Session["Menu"]; }
        set { Session["Menu"] = value; }
    }

    bool NameEditMode
    {
        get { return ViewState["NameEditMode"] == null ? false : (bool)ViewState["NameEditMode"]; }
        set { ViewState["NameEditMode"] = value; }
    }

    bool DescriptionEditMode
    {
        get { return ViewState["DescriptionEditMode"] == null ? false : (bool)ViewState["DescriptionEditMode"]; }
        set { ViewState["DescriptionEditMode"] = value; }
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.Request["menuId"]))
            {
                this.MenuId = int.Parse(this.Request["menuId"]);

                this.Menu = BusinessFacade.Instance.GetMenu(this.MenuId);
                this.Master.SetLeftBackgroundImage(this.Menu.MenuTypeId);

                int anonymous = int.Parse(ConfigurationManager.AppSettings["anonymous"]);
                if ((this.Menu.UserId == anonymous) && (((BasePage)Page).UserId != -1))
                {
                    if ((this.Master.TempUser != 0) && (this.Master.TempUser == this.Menu.TempUserId))
                    {
                       // User newUser = BusinessFacade.Instance.GetUserByUserName(currUser.Name);
                        MyBuyList.Shared.Entities.Menu[] userMenus = BusinessFacade.Instance.GetMenusList(int.Parse(ConfigurationManager.AppSettings["anonymous"]), this.Master.TempUser);
                        if (userMenus != null)
                        {
                            foreach (MyBuyList.Shared.Entities.Menu currMenu in userMenus)
                            {
                                BusinessFacade.Instance.UpdateMenuUser(currMenu.MenuId, ((BasePage)Page).UserId);
                            }
                        }
                    }

                     this.Menu = BusinessFacade.Instance.GetMenu(this.MenuId);
                }

                if (this.Menu.UserId == ((BasePage)Page).UserId)
                {
                    this.txtMenuName.Text = this.Menu.MenuName;
                    SetMenuNameEditorMode(false);

                    this.txtDescription.Text = this.Menu.Description;
                    SetDescriptionEditorMode(false);

                    btnPrintMenuMeals.NavigateUrl = "~/PrintMenu.aspx?menuId=" + this.MenuId.ToString();
                    btnPrintMenuRecipes.NavigateUrl = "~/PrintRecipes.aspx?menuId=" + this.MenuId.ToString();
                    btnPrintShoppingList.NavigateUrl = "~/PrintShopingList.aspx?menuId=" + this.MenuId.ToString();
                }
                else
                {
                    AppEnv.MoveToDefaultPage();
                }

            }
            else
            {
                this.tblMenuDetail.Visible = false;
            }
        }

       

        //////////////////////
        //if (!string.IsNullOrEmpty(this.ReturnUrl))
        //{
        //    int n = this.ReturnUrl.IndexOf("menuid");

        //    string menuId = this.ReturnUrl.Substring(n + "menuId".Length + 1);

        //    if (!string.IsNullOrEmpty(menuId))
        //    {
        //        int menueIdNum = int.Parse(menuId);

        //        MyBuyList.Shared.Entities.Menu tempUserMenu = BusinessFacade.Instance.GetMenu(menueIdNum);

        //        //MyBuyList.Shared.Entities.Menu[] tempUserMenus = BusinessFacade.Instance.GetMenusList(0, AppEnv.TempUser);
        //        if (tempUserMenu.UserId == 0)
        //        {
        //            if (((BasePage)Page).UserId != -1)
        //            {
        //                //int userId = BusinessFacade.Instance.GetUserByUserName(User.Identity.Name).UserId;
        //                BusinessFacade.Instance.UpdateMenuUser(menueIdNum, ((BasePage)Page).UserId);
        //            }
        //        }
        //    }
        //}
        /////////////
    }

    private void Rebind()
    {
        
    }

    protected void btnChangeName_Click(object sender, EventArgs e)
    {
        this.SetMenuNameEditorMode(true);
    }

    protected void btnCancelName_Click(object sender, EventArgs e)
    {
        this.txtMenuName.Text = this.Menu.MenuName;
        this.SetMenuNameEditorMode(false);
    }

    protected void btnSaveName_Click(object sender, EventArgs e)
    {
        MyBuyList.Shared.Entities.Menu menu = this.Menu;

        if (BusinessFacade.Instance.UpdateMenuNameAndDescription(this.MenuId, this.txtMenuName.Text, menu.Description))
        {
            menu.MenuName = this.txtMenuName.Text;

            this.SetMenuNameEditorMode(false);
        }
    }

    private void SetMenuNameEditorMode(bool isEditMode)
    {
        if (!isEditMode)
        {
            this.NameEditMode = false;
            this.btnChangeName.Visible = true;
            this.btnSaveName.Visible = false;
            this.btnCancelName.Visible = false;
            this.txtMenuName.BorderWidth = new Unit(0);
            this.txtMenuName.ReadOnly = true;
        }
        else
        {
            this.NameEditMode = true;
            this.btnChangeName.Visible = false;
            this.btnSaveName.Visible = true;
            this.btnCancelName.Visible = true;
            this.txtMenuName.BorderWidth = new TextBox().BorderWidth;
            this.txtMenuName.ReadOnly = false;
        }
    }


    protected void btnChangeDescription_Click(object sender, EventArgs e)
    {
        this.SetDescriptionEditorMode(true);
    }

    protected void btnCancelDescription_Click(object sender, EventArgs e)
    {
        this.txtDescription.Text = this.Menu.Description;
        this.SetDescriptionEditorMode(false);
    }

    protected void btnSaveDescription_Click(object sender, EventArgs e)
    {
        MyBuyList.Shared.Entities.Menu menu = this.Menu;

        if (BusinessFacade.Instance.UpdateMenuNameAndDescription(this.MenuId, menu.MenuName, this.txtDescription.Text))
        {           
            menu.Description = this.txtDescription.Text;
            
            this.SetDescriptionEditorMode(false);
        }
    }

    private void SetDescriptionEditorMode(bool isEditMode)
    {
        if (!isEditMode)
        {
            this.DescriptionEditMode = false;
            this.btnChangeDescription.Visible = true;
            this.btnSaveDescription.Visible = false;
            this.btnCancelDescription.Visible = false;
            this.txtDescription.BorderWidth = new Unit(0);
            this.txtDescription.ReadOnly = true;
        }
        else
        {
            this.DescriptionEditMode = true;
            this.btnChangeDescription.Visible = false;
            this.btnSaveDescription.Visible = true;
            this.btnCancelDescription.Visible = true;
            this.txtDescription.BorderWidth = new TextBox().BorderWidth;
            this.txtDescription.ReadOnly = false;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/Users/PersonalArea.aspx");
    }

//    protected void btnMenuRecipesPrint_Click(object sender, EventArgs e)
//    {
//        this.print(this.PrintMenuRecipes());
//    }

//    protected void btnMenuMealsPrint_Click(object sender, EventArgs e)
//    {
//        bool b = true;

//        if (this.Menu.MenuTypeId == 1)
//        {
//            b = false;
//        }

//        string data = this.PrintMenuMeals(b);
//        this.print(data);
//    }

//    protected void btnShoppingListPrint_Click(object sender, EventArgs e)
//    {
//        string data = this.PrintShoppingList();
//        this.print(data);
//    }

//    private void print(string dataToPrint)
//    {
        
//  //      Literal data = new Literal();
////        data.Text = dataToPrint;

//        //Session["ctrl"] = data;

//        //Response.Redirect("~/Print?code=1&recipeId=" + this.reci

//       // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'Print.aspx', null, 'height=600,width=500,status=yes,toolbar=no,menubar=no,location=no, scrollbars=1' );", true);
//    }

    protected void btnMenuRecipesSendMail_Click(object sender, EventArgs e)
    {
        StringWriter html = new StringWriter();
        Server.Execute("~/PrintRecipes.aspx?menuId=" + this.MenuId.ToString(), html);
        SendMail("מתכונים ל" + this.Menu.MenuName, html.ToString(), ProperControls.General.Utils.FromEmail);
       
    }

    protected void btnMenuMealsSendMail_Click(object sender, EventArgs e)
    {
        StringWriter html = new StringWriter();
        Server.Execute("~/PrintMenu.aspx?menuId=" + this.MenuId.ToString(), html);

        SendMail(this.Menu.MenuName, html.ToString(), ProperControls.General.Utils.FromEmail);
    }

    protected void btnShoppingListSendMail_Click(object sender, EventArgs e)
    {
        StringWriter html = new StringWriter();
        Server.Execute("~/PrintShopingList.aspx?menuId=" + this.MenuId.ToString(), html);
        SendMail("רשימת קניות", html.ToString(), ProperControls.General.Utils.FromEmail);
    }

    protected void SendMail(string subject, string data, string from)
    {
        string to = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId).Email;

        try
        {
            ProperServices.Common.Mail.Mailer.SendMail(to, from, subject, data, true);

            this.lblResult.Visible = true;
            this.lblResult.Text = "הרשימה נשלחה ל-" + to;
        }
        catch
        {
            this.lblResult.Visible = true;
            this.lblResult.Text = "בעיה בשליחה";
        }
    }


    //private string PrintMenuRecipes()
    //{
    //    string data = "<html>" +
    //                             "<body dir='rtl'>" +
    //                                   "<table  dir='rtl'> " +
    //                                         "<tr>" +
    //                                             "<td style='width:400px;'> " +
    //                                                 " &nbsp; " +
    //                                             "</td> " +
    //                                             "<td>" +
    //                                                 "&nbsp;" +
    //                                             "</td>" +
    //                                             "<td rowspan='2'>" +
    //                                                 "<img src='Images/New/Logo.gif' />" +
    //                                             "</td>" +
    //                                        "</tr>" +
    //                                         "<tr>" +
    //                                             "<td colspan='2' align='center'>" +
    //                                                 "&nbsp;" +
    //                                                 "<b style='font-size:larger; color:Red;'>" + this.Menu.MenuName + "</b>" +

    //                                             "</td>" +
    //                                         "</tr>" +
    //                                         "<tr> " +
    //                                                 "<td>  ";


    //    MenuRecipe[] recipyList = BusinessFacade.Instance.GetMenuRecipes(this.MenuId).ToArray<MenuRecipe>();

    //    bool isFirst = true;

    //    foreach (MenuRecipe currMenuRecipe in recipyList)
    //    {
    //        if (isFirst)
    //        {
    //            isFirst = false;
    //        }
    //        else
    //        {
    //            data += "<hr>";
    //        }

    //        RecipeIngredientsView[] recipyIngred = BusinessFacade.Instance.GetRecipeIngredientsList(currMenuRecipe.Recipe.RecipeId);

    //        data += "<b>" + currMenuRecipe.Recipe.RecipeName + "- </b><br />";
    //        data += "<b>מצרכים-</b> <br />";
    //        foreach (RecipeIngredientsView cuurIngredient in recipyIngred)
    //        {
    //            data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + cuurIngredient.DisplayIngredient;
    //            data += "<br />";
    //        }

    //        data += "<br /><b>אופן הכנה - </b><br />";
    //        data += currMenuRecipe.Recipe.PreparationMethod.Replace(Environment.NewLine, "<br />") + "<br /><br />";
    //    }


    //    data += "</td> " +
    //          "</tr> " +
    //    "<table> " +
    //    "</body>" +
    //     "</html>";

    //    return data;
    //}

    //private string PrintMenuRecipes()
    //{

    //    MyBuyList.Shared.Entities.Menu menu = BusinessFacade.Instance.GetMenu(this.MenuId);
    //    string data = "<html>" +
    //                             "<body dir='rtl'>" +
    //                                   "<table  dir='rtl'> " +
    //                                         "<tr>" +
    //                                             "<td style='width:400px;'> " +
    //                                                 " &nbsp; " +
    //                                             "</td> " +
    //                                             "<td>" +
    //                                                 "&nbsp;" +
    //                                             "</td>" +
    //                                             "<td rowspan='2'>" +
    //                                                 "<img src='http://www.mybuylist.com/Images/New/Logo.gif' />" +
    //                                             "</td>" +
    //                                        "</tr>" +
    //                                         "<tr>" +
    //                                             "<td colspan='2' align='center'>" +
    //                                                 "&nbsp;" +
    //                                                 "<b style='font-size:larger; color:Red;'>" + menu.MenuName + "</b>" +

    //                                             "</td>" +
    //                                         "</tr>" +
    //                                         "<tr> " +
    //                                                 "<td>  ";


    //    MenuRecipe[] recipyList = BusinessFacade.Instance.GetMenuRecipes(this.MenuId).ToArray<MenuRecipe>();
    //    Dictionary<int, List<int>> recipeServList = BusinessFacade.Instance.GetMenuRecipesIngrid(this.MenuId);

    //    bool isFirst = true;
    //    foreach (MenuRecipe currMenuRecipe in recipyList)
    //    {
    //        //RecipeIngredientsView[] recipyIngred = BusinessFacade.Instance.GetRecipeIngredientsList(currMenuRecipe.Recipe.RecipeId);

    //        if (isFirst)
    //        {
    //            isFirst = false;
    //        }
    //        else
    //        {
    //            data += "<hr />";
    //        }
    //        if (recipeServList.ContainsKey(currMenuRecipe.RecipeId))
    //        {
    //            List<int> RecipeServings = recipeServList[currMenuRecipe.RecipeId];
    //            RecipeServings.Sort();
    //            data += "<b>" + currMenuRecipe.Recipe.RecipeName + "</b><br>";

    //            foreach (int serv in RecipeServings)
    //            {
    //                if (serv == currMenuRecipe.Recipe.Servings)
    //                {
    //                    data += "<b>מצרכים כפי שהוגדרו במתכון המקורי (" + serv.ToString() + " מנות):</b> <br>";
    //                    foreach (Ingredient cuurIngredient in currMenuRecipe.Recipe.Ingredients)
    //                    {
    //                        data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + new SRL_Ingredient(cuurIngredient).DisplayIngredient;
    //                        data += "<br>";
    //                    }
    //                }
    //                else
    //                {
    //                    data += "<b>מצרכים כפי שבחרת לארוחה עבור (" + serv.ToString() + " מנות):</b> <br>";
    //                    foreach (Ingredient cuurIngredient in currMenuRecipe.Recipe.Ingredients)
    //                    {
    //                        SRL_Ingredient cuurSRLIngredient = new SRL_Ingredient();
    //                        cuurSRLIngredient.FoodId = cuurIngredient.FoodId;
    //                        cuurSRLIngredient.FoodName = BusinessFacade.Instance.GetFood(cuurSRLIngredient.FoodId).FoodName;
    //                        cuurSRLIngredient.MeasurementUnitId = cuurIngredient.MeasurementUnitId;
    //                        cuurSRLIngredient.MeasurementUnitName = BusinessFacade.Instance.GetMeasurementUnit(cuurSRLIngredient.MeasurementUnitId).UnitName;
    //                        cuurSRLIngredient.Quantity = Decimal.Round((cuurIngredient.Quantity / currMenuRecipe.Recipe.Servings) * serv, 2);
    //                        cuurSRLIngredient.Remarks = cuurIngredient.Remarks;
    //                        data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + cuurSRLIngredient.DisplayIngredient;
    //                        data += "<br>";
    //                    }
    //                }

    //            }

    //            data += "<br><b>אופן הכנה: </b><br>";
    //            data += currMenuRecipe.Recipe.PreparationMethod + "<br><br>";

    //        }
    //        else
    //        {
    //            data += "<b>" + currMenuRecipe.Recipe.RecipeName + "</b><br>";
    //            data += "<b>מצרכים:</b> <br>";
    //            foreach (Ingredient cuurIngredient in currMenuRecipe.Recipe.Ingredients)
    //            {
    //                data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + new SRL_Ingredient(cuurIngredient).DisplayIngredient;
    //                data += "<br>";
    //            }

    //            data += "<br><b>אופן הכנה: </b><br>";
    //            data += currMenuRecipe.Recipe.PreparationMethod + "<br><br>";
    //        }
    //    }


    //    data += "</td> " +
    //          "</tr> " +
    //    "<table> " +
    //    "</body>" +
    //     "</html>";

    //    return data;
    //}

    //private string PrintMenuMeals(bool isWeekly)
    //{
    //    string data = "<html>" +
    //                            "<body dir='rtl'>" +
    //                                  "<table  dir='rtl'> " +
    //                                         "<tr>" +
    //                                            "<td style='width:400px;'> " +
    //                                                " &nbsp; " +
    //                                            "</td> " +
    //                                            "<td>" +
    //                                                "&nbsp;" +
    //                                            "</td>" +
    //                                            "<td rowspan='2'>" +
    //                                                "<img src='http://www.mybuylist.com/Images/New/Logo.gif' />" +
    //                                            "</td>" +
    //                                       "</tr>" +
    //                                        "<tr>" +
    //                                            "<td colspan='2' align='center'>" +
    //                                                "&nbsp;" +
    //                                                "<b style='font-size:larger; color:Red;'>" + this.Menu.MenuName + "</b>" +

    //                                            "</td>" +
    //                                        "</tr>" +

    //                                        "<tr> " +
    //                                                "<td>  ";

    //    // ShopDepartment[] listShopDepartments = BusinessFacade.Instance.GetMenuShopDepartments(this.MenuId);


    //    if (isWeekly)
    //    {
    //        MealType[] mealTypes = BusinessFacade.Instance.GetMealTypes();
    //        for (int i = 1; i <= 7; i++)
    //        {
    //            data += "<b> יום מספר " + i.ToString() + ":</b><br />";
    //            foreach (MealType currType in mealTypes)
    //            {
    //                Meal[] currDayMeals = (BusinessFacade.Instance.GetMealsList(this.Menu.MenuId)).Where(m => m.DayIndex == i && m.MealTypeId == currType.MealTypeId).ToArray<Meal>();
    //                data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;" + currType.MealTypeName + "- <br />";
    //                foreach (Meal cuurMeal in currDayMeals)
    //                {
    //                    foreach (MealRecipe currRecipe in cuurMeal.MealRecipes)
    //                    {
    //                        data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + currRecipe.Recipe.RecipeName;
    //                        data += "<br />";
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        CourseType[] courseTypeList = BusinessFacade.Instance.GetCourseTypes();
    //        foreach (CourseType currType in courseTypeList)
    //        {
    //            Meal[] currDayMeals = (BusinessFacade.Instance.GetMealsList(this.Menu.MenuId)).Where(m => m.CourseTypeId == currType.CourseTypeId).ToArray<Meal>();
    //            data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;" + currType.CourseTypeName + "- <br />";
    //            foreach (Meal cuurMeal in currDayMeals)
    //            {
    //                foreach (MealRecipe currRecipe in cuurMeal.MealRecipes)
    //                {
    //                    data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + currRecipe.Recipe.RecipeName;
    //                    data += "<br />";
    //                }
    //            }
    //        }
    //    }

    //    data += "</td> " +
    //          "</tr> " +
    //    "<table> " +
    //    "</body>" +
    //     "</html>";

    //    return data;
    //}

    //private string PrintShoppingList()
    //{
    //    string data = "<html>" +
    //                            "<body dir='rtl'>" +
    //                                  "<table  dir='rtl'> " +
    //                                         "<tr>" +
    //                                            "<td style='width:400px;'> " +
    //                                                " &nbsp; " +
    //                                            "</td> " +
    //                                            "<td>" +
    //                                                "&nbsp;" +
    //                                            "</td>" +
    //                                            "<td rowspan='2'>" +
    //                                                "<img src='http://www.mybuylist.com/Images/New/Logo.gif' />" +
    //                                            "</td>" +
    //                                       "</tr>" +
    //                                        "<tr>" +
    //                                            "<td colspan='2' align='center'>" +
    //                                                "&nbsp;" +
    //                                                "<b style='font-size:larger; color:Red;'>רשימת קניות</b>" +

    //                                            "</td>" +
    //                                        "</tr>" +
    //                                        "<tr> " +
    //                                                "<td colspan='3' dir='rtl'> " +
    //                                                    "<b>מצרכים: </b>" +
    //                                                "</td> " +
    //                                        "</tr> " +
    //                                        "<tr> " +
    //                                                "<td>  ";
    //    ShopDepartment[] listShopDepartments = BusinessFacade.Instance.GetMenuShopDepartments(this.MenuId);
    //    ShoppingFood[] allShoppingFoods = BusinessFacade.Instance.GetMenuShoppingList(this.MenuId);

    //    foreach (ShopDepartment cuurShopDepartments in listShopDepartments)
    //    {
    //        data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>" + cuurShopDepartments.ShopDepartmentName + ":</b>";
    //        data += "<br />";
    //        if (allShoppingFoods != null)
    //        {
    //            ShoppingFood[] departmentShopingFood = allShoppingFoods.Where(list => list.ShopDepartmentId == cuurShopDepartments.ShopDepartmentId).ToArray();
    //            foreach (ShoppingFood currFood in departmentShopingFood)
    //            {
    //                data += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + currFood.Display + " " + currFood.FoodName;
    //                data += "<br />";
    //            }
    //        }
    //    }

    //    data += "</td> " +
    //          "</tr> " +
    //          "<tr> " +
    //                  "<td> " +
    //                      "<br />" +
    //                      "<b> מצרכים נוספים:</b> " +
    //                  "</td> " +
    //          "</tr> " +
    //                  "<td colspan='3' dir='rtl'> ";
    //    ShoppingListAdditionalItem[] additionalItems = BusinessFacade.Instance.GetShoppingListAdditionalItems(this.MenuId);

    //    foreach (ShoppingListAdditionalItem cuurAdditionalItems in additionalItems)
    //    {
    //        // data += " ● " + cuurAdditionalItems.GeneralItem.GeneralItemName + "<br />";

    //        if ((cuurAdditionalItems != null) && (cuurAdditionalItems.GeneralItem != null))
    //        {
    //            data += " * " + cuurAdditionalItems.GeneralItem.GeneralItemName + "<br />";
    //        }
    //        else if ((cuurAdditionalItems != null) && (cuurAdditionalItems.ItemName != null))
    //        {
    //            data += " * " + cuurAdditionalItems.ItemName + "<br />";
    //        }
    //    }
    //    data += "</td> " +
    //             "</tr> " +
    //              "</tr> " +
    //             "<tr> <td> <br /> </td> </tr> " +
    //              "<tr> " +
    //                "<td colspan='3' dir='rtl'> " +
    //                    "<b>מתכונים: </b>" +
    //                 "</td> " +
    //             "</tr> " +
    //             "<tr> " +
    //                "<td>  ";

    //    Dictionary<string, Recipe> resipes = BusinessFacade.Instance.GetRecipesInMenuMeals(this.MenuId);

    //    foreach (Recipe cuurMenuRecipe in resipes.Values)
    //    {
    //        data += cuurMenuRecipe.RecipeName;
    //        data += "<br />";
    //    }

    //    data += "</td> " +
    //          "</tr> " +
    //            "<table> " +
    //            "</body>" +
    //            "</html>";

    //    return data;
    //}

    protected void btnMenuRecipesEdit_Click(object sender, EventArgs e)
    {
        string url;
        if (this.Master.IsFromHome)
        {
            url = string.Format("~/MenuRecipes.aspx?menuId={0}&isfromhome={1}", this.MenuId, 1);
        }
        else
        {
         url = string.Format("~/MenuRecipes.aspx?menuId={0}", this.MenuId);
        }
        this.Response.Redirect(url);
    }

    protected void btnMenuMealsEdit_Click(object sender, EventArgs e)
    {
        string url;
        if (this.Master.IsFromHome)
        {
            url = string.Format("~/MenuMeals.aspx?menuId={0}&isfromhome={1}", this.MenuId, 1);
        }
        else
        {
            url = string.Format("~/MenuMeals.aspx?menuId={0}", this.MenuId);
        }
        this.Response.Redirect(url);
    }

    protected void btnShoppingListEdit_Click(object sender, EventArgs e)
    {
        string url;
        if (this.Master.IsFromHome)
        {
            url = string.Format("~/ShoppingList.aspx?menuId={0}&isfromhome={1}", this.MenuId, 1);
        }
        else
        {
             url = string.Format("~/ShoppingList.aspx?menuId={0}", this.MenuId);
        }
        this.Response.Redirect(url);
    }
}
