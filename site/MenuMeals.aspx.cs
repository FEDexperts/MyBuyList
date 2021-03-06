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
using System.Web.Mail;
using System.IO;
using MyBuyList.BusinessLayer.Managers;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;

using Resources;
using ProperControls.General;
using ProperControls.Pages;


public partial class PageMenuMeals: BasePage 
{
    protected override PagePersisterSettings PagePersisterSelection
    {
        get
        {
            return PagePersisterSettings.Session;
        }
    }

    private static int _menuId;
    public int MenuId
    {
        get { return ViewState["MenuId"] == null ? 0 : (int)ViewState["MenuId"]; }
        set { ViewState["MenuId"] = value; }
    }

    public int MenuTypeId
    {
        get { return ViewState["MenuTypeId"] == null ? 0 : (int)ViewState["MenuTypeId"]; }
        set { ViewState["MenuTypeId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (((BasePage)Page).UserId == -1)
            {
                this.btnSendMail.Visible = false;
                this.lblSeparator2.Visible = false;
            }
            
            if (!string.IsNullOrEmpty(this.Request["menuId"]))
            {
                this.MenuId = int.Parse(this.Request["menuId"]);
                int? userId = BusinessFacade.Instance.GetMenuUserId(this.MenuId);

                if (userId.HasValue)
                {

                    if ((userId.Value == int.Parse(ConfigurationManager.AppSettings["anonymous"])) || (userId.Value == ((BasePage)Page).UserId))
                    {
                        MenuType menuType = BusinessFacade.Instance.GetMenuType(this.MenuId);

                        if (menuType != null)
                        {
                            this.MenuTypeId = menuType.MenuTypeId;
                            this.Master.SetLeftBackgroundImage(menuType.MenuTypeId);

                            if (menuType.MenuTypeId == (int)MenuTypeEnum.OneMeal || 
                                menuType.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
                            {
                                this.ucMealMenu.Visible = true;
                            }
                            else
                            {
                                this.ucWeeklyMenu.Visible = true;
                                this.ucWeeklyMenu.MenuTypeId = menuType.MenuTypeId;
                                this.pnlMenuRecipes.Height = new Unit(802);
                                this.Master.ChangeTopNotePos();
                            }
                        }

                        this.rptMenuRecipes.DataSource = BusinessFacade.Instance.GetMenuRecipes(this.MenuId);
                        this.rptMenuRecipes.DataBind();

                        btnPrintMenuMeals.NavigateUrl = "~/PrintMenu.aspx?menuId=" + this.MenuId.ToString();


                        _menuId = this.MenuId;
                    }
                    else
                    {
                        AppEnv.MoveToDefaultPage();
                    }
                }
                else
                {
                    AppEnv.MoveToDefaultPage();
                }
            }
            else
            {
                AppEnv.MoveToDefaultPage();
            }
            
        }

        
        ClientScript.RegisterClientScriptInclude("MyScript", "Scripts/MenuMealsDnD.js");

       
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        // if not logged in, I'd like to know this since I'm going to the login page
        Session["AnonymousUsersMenu"] = "true";

        string url;
        if (this.Master.IsFromHome)
        {
            url = string.Format("~/MenuDetails.aspx?menuId={0}&isfromhome={1}", this.MenuId, 1);
        }
        else
        {
             url = string.Format("~/MenuDetails.aspx?menuId={0}", this.MenuId);
        }
        this.Response.Redirect(url);

    }

    protected void btnPrev_Click(object sender, EventArgs e)
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

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        StringWriter html = new StringWriter();
        Server.Execute("~/PrintMenu.aspx?menuId=" + this.MenuId.ToString(), html);

        string to = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId).Email;
        string subject = BusinessFacade.Instance.GetMenu(this.MenuId).MenuName;

        try
        {
            ProperServices.Common.Mail.Mailer.SendMail(to, Utils.FromEmail, subject, html.ToString(), true);

            this.lblResult.Visible = true;
            this.lblResult.Text = "הרשימה נשלחה ל-" + to;
        }
        catch(Exception exp)
        {
            this.lblResult.Visible = true;
            this.lblResult.Text = "בעיה בשליחה";
        }
    }

    protected void btnShoppingList_Click(object sender, EventArgs e)
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

    [WebMethod]
    public static int?[] AddMealRecipe(int dayIndex, int mealTypeId, int recipeId)
    {
        int mealId = 0;
        if (BusinessFacade.Instance.AddMealRecipe(_menuId, dayIndex, mealTypeId, recipeId, out mealId))
        {
            Meal meal = BusinessFacade.Instance.GetMeal(mealId);
            Recipe addedRecipe = BusinessFacade.Instance.GetRecipe(recipeId);
            return new int?[] { meal.MealId, meal.Diners, addedRecipe.Servings};
        }
        else
            return null;
    }

    [WebMethod]
    public static int?[] AddMeal_Recipe(int courseTypeId, int recipeId)
    {
        int mealId = 0;
        if (BusinessFacade.Instance.AddMealRecipe(_menuId, courseTypeId, recipeId, out mealId))
        {
            Meal meal = BusinessFacade.Instance.GetMeal(mealId);
            Recipe addedRecipe = BusinessFacade.Instance.GetRecipe(recipeId);
            return new int?[] { meal.MealId, meal.Diners, addedRecipe.Servings };
        }
        else
            return null;
    }

    [WebMethod]
    public static void RemoveMealRecipe(int mealId, int recipeId)
    {
        BusinessFacade.Instance.RemoveMealRecipe(mealId, recipeId);      
    }

    [WebMethod]
    public static object[] SaveServings(int? servings, int mealId, int recipeId)
    {        
        string errorMsg = ValidationResources.WrongServingsFieldValue;
        if (servings != null && servings != 0)
        {
            if (BusinessFacade.Instance.SaveMealRecipe(mealId, recipeId, servings.Value))
            {
                return new object[] { true, null, servings };
            }
            else
            {
                MealRecipe mealRecipe = BusinessFacade.Instance.GetMealRecipe(mealId, recipeId);
                return new object[] { false, errorMsg, mealRecipe.Servings };
            }
        }
        else
        {
            MealRecipe mealRecipe = BusinessFacade.Instance.GetMealRecipe(mealId, recipeId);
            return new object[] { false, errorMsg, mealRecipe.Servings };
        }
    }

    [WebMethod]
    public static object[] SaveDiners(int? diners, int menuId, int dayIndex, int mealTypeId)
    {
        Meal meal = BusinessFacade.Instance.GetMeal(menuId, dayIndex, mealTypeId);
        string errorMsg = ValidationResources.WrongServingsFieldValue;

        if (diners != null && diners != 0)
        {
            if (BusinessFacade.Instance.SaveMeal(menuId, dayIndex, mealTypeId, diners))
            {
                return new object[] { true, null, diners };
            }
            else
            {
                return new object[] { false, errorMsg, meal != null ? meal.Diners : null };
            }
        }
        else
        {
            if (meal == null)
            {
                return new object[] { false, errorMsg, null };
            }
            else
            {
                if (diners == 0)
                {
                    return new object[] { false, errorMsg, meal.Diners };
                }
                else
                {
                    if (meal.MealRecipes.Count > 0)
                    {
                        return new object[] { false, errorMsg, meal.Diners };
                    }
                    else
                    {
                        if (BusinessFacade.Instance.SaveMeal(menuId, dayIndex, mealTypeId, diners))
                        {
                            return new object[] { true, null, diners };
                        }
                        else
                        {
                            return new object[] { false, errorMsg, meal.Diners };
                        }
                    }
                }
            }
        }
    }

    [WebMethod]
    public static object[] Save_Diners(int? diners, int menuId, int courseTypeId)
    {
        Meal meal = BusinessFacade.Instance.GetMeal(menuId, courseTypeId);
        string errorMsg = ValidationResources.WrongServingsFieldValue;

        if (diners != null && diners != 0)
        {
            if (BusinessFacade.Instance.SaveMeal(menuId, courseTypeId, diners))
            {
                return new object[] { true, null, diners };
            }
            else
            {
                return new object[] { false, errorMsg, meal != null ? meal.Diners : null };
            }
        }
        else
        {
            if (meal == null)
            {
                return new object[] { false, errorMsg, null };
            }
            else
            {
                if (diners == 0)
                {
                    return new object[] { false, errorMsg, meal.Diners };
                }
                else
                {
                    if (meal.MealRecipes.Count > 0)
                    {
                        return new object[] { false, errorMsg, meal.Diners };
                    }
                    else
                    {
                        if (BusinessFacade.Instance.SaveMeal(menuId, courseTypeId, diners))
                        {
                            return new object[] { true, null, diners };
                        }
                        else
                        {
                            return new object[] { false, errorMsg, meal.Diners };
                        }
                    }
                }
            }
        }
    }
}