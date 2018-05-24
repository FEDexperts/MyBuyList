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
using ProperControls.Pages;
using Resources;

public partial class PageMenuRecipes : BasePage
{
    protected override PagePersisterSettings PagePersisterSelection
    {
        get
        {
            return PagePersisterSettings.Session;
        }
    }

    int MenuId
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
        if (!this.IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.Request["menuId"]))
            {

                this.MenuId = int.Parse(this.Request["menuId"]);

                int? userId = BusinessFacade.Instance.GetMenuUserId(this.MenuId);

                if (userId.HasValue)
                {
                    
                    if ((userId.Value == int.Parse(ConfigurationManager.AppSettings["anonymous"])) || (userId.Value == ((BasePage)Page).UserId))
                    {
                        this.MenuRecipes_DataBind();

                        int menuTypeId = BusinessFacade.Instance.GetMenuType(this.MenuId).MenuTypeId;
                        this.Master.SetLeftBackgroundImage(menuTypeId);
                        this.MenuTypeId = menuTypeId;
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

            if (((BasePage)this.Page).CurrUser == null)
            {

                this.lblSeparator3.Visible = false;
                this.btnMyRecipes.Visible = false;
                this.lblSeparator4.Visible = false;
                this.btnMyFavoritesRecipes.Visible = false;
            }

            if (!string.IsNullOrEmpty(this.Request["view"]))
            {
                int view = int.Parse(this.Request["view"]);

                if (view == (int)PersonalAreaViewEnum.MyRecipesSearch)
                {
                    this.btnMyRecipes_Click(this.btnMyRecipes, new EventArgs());
                }
                else if (view == (int)PersonalAreaViewEnum.MyFavoritesRecipesSearch)
                {
                    this.btnMyFavoritesRecipes_Click(this.btnMyFavoritesRecipes, new EventArgs());
                }
                else if (view == (int)PersonalAreaViewEnum.SimpleSearch)
                {
                    this.SearchSimple();
                }
                else if (view == (int)PersonalAreaViewEnum.ComplexSearch)
                {
                    this.SearchComplex();
                }
                else
                {
                    this.SearchByCategories();
                }
            }
            else
            {
                this.SearchByCategories();
            }
        }
    }

    private void MenuRecipes_DataBind()
    {
        this.rptMenuRecipes.DataSource = BusinessFacade.Instance.GetMenuRecipes(this.MenuId);
        this.rptMenuRecipes.DataBind();
        this.updpMenuRecipes.Update();
    }

    protected void btnSearchSimple_Click(object sender, EventArgs e)
    {
        this.SearchSimple();
    }

    protected void btnMyRecipes_Click(object sender, EventArgs e)
    {
        this.btnSearchSimple.BackColor = System.Drawing.Color.Transparent;
        this.btnSearchSimple.ForeColor = System.Drawing.Color.Crimson;

        this.btnSearchComplex.BackColor = System.Drawing.Color.Transparent;
        this.btnSearchComplex.ForeColor = System.Drawing.Color.Crimson;

        this.btnSearchByCategories.BackColor = System.Drawing.Color.Transparent;
        this.btnSearchByCategories.ForeColor = System.Drawing.Color.Crimson;

        this.btnMyFavoritesRecipes.BackColor = System.Drawing.Color.Transparent;
        this.btnMyFavoritesRecipes.ForeColor = System.Drawing.Color.Crimson;

        this.btnMyRecipes.BackColor = System.Drawing.Color.Crimson;
        this.btnMyRecipes.ForeColor = System.Drawing.Color.White;


        this.ucSearchComplex.Visible = false;
        this.ucSearchSimple.Visible = false;
        this.ucSearchByCategories.Visible = false;
        this.ucMyRecipes.Visible = true;

        this.ucMyRecipes.ShowData(1);
    }

    protected void btnMyFavoritesRecipes_Click(object sender, EventArgs e)
    {
        this.btnSearchSimple.BackColor = System.Drawing.Color.Transparent;
        this.btnSearchSimple.ForeColor = System.Drawing.Color.Crimson;

        this.btnSearchComplex.BackColor = System.Drawing.Color.Transparent;
        this.btnSearchComplex.ForeColor = System.Drawing.Color.Crimson;

        this.btnSearchByCategories.BackColor = System.Drawing.Color.Transparent;
        this.btnSearchByCategories.ForeColor = System.Drawing.Color.Crimson;

        this.btnMyRecipes.BackColor = System.Drawing.Color.Transparent;
        this.btnMyRecipes.ForeColor = System.Drawing.Color.Crimson;

        this.btnMyFavoritesRecipes.BackColor = System.Drawing.Color.Crimson;
        this.btnMyFavoritesRecipes.ForeColor = System.Drawing.Color.White;


        this.ucSearchComplex.Visible = false;
        this.ucSearchSimple.Visible = false;
        this.ucSearchByCategories.Visible = false;
        this.ucMyRecipes.Visible = true;

        this.ucMyRecipes.ShowData(2);
    }

    private void SearchSimple()
    {
        this.btnSearchSimple.BackColor = System.Drawing.Color.Crimson;
        this.btnSearchSimple.ForeColor = System.Drawing.Color.White;

        this.btnSearchComplex.BackColor = System.Drawing.Color.Transparent;
        this.btnSearchComplex.ForeColor = System.Drawing.Color.Crimson;

        this.btnSearchByCategories.BackColor = System.Drawing.Color.Transparent;
        this.btnSearchByCategories.ForeColor = System.Drawing.Color.Crimson;

        this.btnMyRecipes.BackColor = System.Drawing.Color.Transparent;
        this.btnMyRecipes.ForeColor = System.Drawing.Color.Crimson;

        this.btnMyFavoritesRecipes.BackColor = System.Drawing.Color.Transparent;
        this.btnMyFavoritesRecipes.ForeColor = System.Drawing.Color.Crimson;

        this.ucSearchSimple.Visible = true;
        this.ucSearchComplex.Visible = false;
        this.ucSearchByCategories.Visible = false;
        this.ucMyRecipes.Visible = false;
    }
    protected void btnSearchComplex_Click(object sender, EventArgs e)
    {
        this.SearchComplex();
    }

    private void SearchComplex()
    {
        this.btnSearchSimple.BackColor = System.Drawing.Color.Transparent;
        this.btnSearchSimple.ForeColor = System.Drawing.Color.Crimson;

        this.btnSearchComplex.BackColor = System.Drawing.Color.Crimson;
        this.btnSearchComplex.ForeColor = System.Drawing.Color.White;

        this.btnSearchByCategories.BackColor = System.Drawing.Color.Transparent;
        this.btnSearchByCategories.ForeColor = System.Drawing.Color.Crimson;

        this.btnMyRecipes.BackColor = System.Drawing.Color.Transparent;
        this.btnMyRecipes.ForeColor = System.Drawing.Color.Crimson;

        this.btnMyFavoritesRecipes.BackColor = System.Drawing.Color.Transparent;
        this.btnMyFavoritesRecipes.ForeColor = System.Drawing.Color.Crimson;

        this.ucSearchComplex.Visible = true;
        this.ucSearchSimple.Visible = false;
        this.ucSearchByCategories.Visible = false;
        this.ucMyRecipes.Visible = false;
    }

    protected void btnSearchByCategories_Click(object sender, EventArgs e)
    {
        this.SearchByCategories();
    }

    private void SearchByCategories()
    {
        this.btnSearchSimple.BackColor = System.Drawing.Color.Transparent;
        this.btnSearchSimple.ForeColor = System.Drawing.Color.Crimson;

        this.btnSearchComplex.BackColor = System.Drawing.Color.Transparent;
        this.btnSearchComplex.ForeColor = System.Drawing.Color.Crimson;

        this.btnSearchByCategories.BackColor = System.Drawing.Color.Crimson;
        this.btnSearchByCategories.ForeColor = System.Drawing.Color.White;

        this.btnMyRecipes.BackColor = System.Drawing.Color.Transparent;
        this.btnMyRecipes.ForeColor = System.Drawing.Color.Crimson;

        this.btnMyFavoritesRecipes.BackColor = System.Drawing.Color.Transparent;
        this.btnMyFavoritesRecipes.ForeColor = System.Drawing.Color.Crimson;

        this.ucSearchComplex.Visible = false;
        this.ucSearchSimple.Visible = false;
        this.ucSearchByCategories.Visible = true;
        this.ucMyRecipes.Visible = false;
    }

    protected void Search_AddRecipeToCallback(int recipeId)
    {
        this.AddOrRemoveMenuRecipe(recipeId, "add");
    }

    protected void btnAddRecipeToMenu_Click(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        int recipeId = int.Parse(btn.Attributes["recipeId"]);
        this.AddOrRemoveMenuRecipe(recipeId, "add");
    }

    protected void btnRemoveRecipeFromMenu_Click(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        int recipeId = int.Parse(btn.Attributes["recipeId"]);
        this.AddOrRemoveMenuRecipe(recipeId, "remove");
    }

    private void AddOrRemoveMenuRecipe(int recipeId, string action)
    {
        bool result = false;
        if (action == "remove")
        {
            bool exist = BusinessFacade.Instance.CheckIfMenuRecipeExistInMeals(this.MenuId, recipeId);

            if (!exist)
            {
                result = BusinessFacade.Instance.RemoveMenuRecipe(this.MenuId, recipeId);
            }
        }

        if(action == "add")
            result = BusinessFacade.Instance.AddMenuRecipe(this.MenuId, recipeId);
        
        if (result)
        {
            this.MenuRecipes_DataBind();
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        string url;
        if (this.MenuTypeId == (int)MenuTypeEnum.QuickMenu) 
        {
            Meal meal = BusinessFacade.Instance.GetMeal(this.MenuId, 0);
            if (meal == null || meal.MealRecipes.Count == 0)
            {
                BusinessFacade.Instance.CreateQuickListMealRecipes(this.MenuId);

                if (this.Master.IsFromHome)
                {
                    url = string.Format("~/ShoppingList.aspx?menuId={0}&isfromhome={1}", this.MenuId, 1);
                }
                else
                {
                    url = string.Format("~/ShoppingList.aspx?menuId={0}", this.MenuId);
                }

                this.Response.Redirect(url);
                return;
            }
        }

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

    protected void btnPrev_Click(object sender, EventArgs e)
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

    protected void btnRecipe_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        RepeaterItem rptItem = btn.Parent as RepeaterItem;
    }

    protected void btnRefreshMenuRecipes_Click(object sender, EventArgs e)
    {
        this.MenuRecipes_DataBind();
    }

    [WebMethod]
    public static object[] CheckMenuRecipeBeforeDeletion(int menuId, int recipeId)
    {
        bool exist = BusinessFacade.Instance.CheckIfMenuRecipeExistInMeals(menuId, recipeId);
        return new object[] { exist, menuId, recipeId, ValidationResources.ConfirmMenuRecipeDelete };
    }

    [WebMethod]
    public static void DeleteMenuRecipe(int menuId, int recipeId)
    {       
        BusinessFacade.Instance.RemoveMenuRecipe(menuId, recipeId);
    }
}

