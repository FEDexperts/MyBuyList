using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using MyBuyList.BusinessLayer;
using MyBuyList.BusinessLayer.Managers;
using MyBuyList.Shared.Entities;

using Resources;
using ProperControls.Pages;

public partial class ucSearchComplex : System.Web.UI.UserControl
{
    SRL_Recipe[] Results
    {
        get
        {
            return (SRL_Recipe[])this.ViewState["Results"];
        }
        set
        {
            this.ViewState["Results"] = value;
        }
    }
    
    SRL_RecipeCategory[] RecipeCategories
    {
        get { return (SRL_RecipeCategory[])ViewState["RecipeCategories"]; }
        set { ViewState["RecipeCategories"] = value; }
    }

    SRL_RecipeCategory[] RecipeCategoriesStored
    {
        get { return (SRL_RecipeCategory[])Session["RecipeCategoriesStored"]; }
        set { Session["RecipeCategoriesStored"] = value; }
    }


    SRL_Recipe[] ResultsStored
    {
        get
        {

            return (SRL_Recipe[])this.Session["ResultsStored"];
        }
        set
        {
            this.Session["ResultsStored"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.Request["view"]))
            {

                if ((this.ResultsStored != null) && (int.Parse(this.Request["view"]) == (int)PersonalAreaViewEnum.ComplexSearch))
                {
                    if (!string.IsNullOrEmpty(this.Request["text"]))
                    {
                        this.txtFreeText.Text = this.Request["text"];
                        
                    }

                    if (!string.IsNullOrEmpty(this.Request["servings"]))
                    {
                        this.txtServingsNum.Text = this.Request["servings"];

                    }

                    if (this.RecipeCategoriesStored != null)
                    {
                        this.RecipeCategories = this.RecipeCategoriesStored;
                        RecipeCategories_Rebind(this.RecipeCategories);
                        Session.Remove("RecipeCategoriesStored");

                    }

                    this.gridRecipesList.DataSource = this.ResultsStored;
                    this.gridRecipesList.DataBind();
                    this.lblResultsCount.Text = string.Format(MyGlobalResources.FoundNoRecipes, this.ResultsStored.Length);
                    this.lblComment.Visible = (this.ResultsStored.Length > 0);
                    this.Session.Remove("ResultsStored");
                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int? servings = null;
        if (!string.IsNullOrEmpty(this.txtServingsNum.Text))
        {
            servings = int.Parse(this.txtServingsNum.Text);
        }
        int[] recipeCats = null;
        if (this.RecipeCategories != null && this.RecipeCategories.Length > 0)
        {
            recipeCats = (from item in this.RecipeCategories
                         select item.CategoryId).ToArray();
        }

        this.Results = (from item in BusinessFacade.Instance.GetRecipesListByComplexSearch(this.txtFreeText.Text, servings, recipeCats, ((BasePage)Page).UserId)
                        select new SRL_Recipe(item)).ToArray();
        this.gridRecipesList.DataSource = this.Results;
        this.gridRecipesList.DataBind();
        this.lblResultsCount.Text = string.Format(MyGlobalResources.FoundNoRecipes, this.Results.Length);
        this.lblComment.Visible = (this.Results.Length > 0);
    }

    protected void btnCategories_Click(object sender, EventArgs e)
    {
        ucRecipeCats.ShowCategories(this.RecipeCategories);
    }

    protected void RecipeCategories_Rebind(SRL_RecipeCategory[] arr)
    {
        this.RecipeCategories = arr;
        string cats = "";
        foreach (SRL_RecipeCategory rc in arr)
        {
            cats += rc.CategoryName + " ,";
        }
        if (!string.IsNullOrEmpty(cats))
        {
            cats = cats.Remove(cats.Length - 2, 2);
        }
        this.txtCategories.Text = cats;
    }

    protected void gridRecipesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridRecipesList.DataSource = this.Results;
        this.gridRecipesList.PageIndex = e.NewPageIndex;
        this.gridRecipesList.DataBind();
    }

    protected void gridRecipesList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#dddddd'";
            e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='Transparent'";

            Label lbl = e.Row.FindControl("lblUser") as Label;
            //lbl.Text = @"נוסף על ידי " + (BusinessFacade.Instance.GetRecipe(((SRL_Recipe)e.Row.DataItem).RecipeId)).User.DisplayName;
            //Recipe recipe = BusinessFacade.Instance.GetRecipe(((SRL_Recipe)e.Row.DataItem).RecipeId);
            //string name = string.Empty;

            //if (!string.IsNullOrEmpty(recipe.User.DisplayName))
            //{
            //    name = recipe.User.DisplayName;
            //}
            //else
            //{
            //    name = recipe.User.Name;
            //}
            lbl.Text = @"נוסף על ידי " + ((SRL_Recipe)e.Row.DataItem).Name;
        }
    }

    protected void btnAddRecipeToMenu_Click(object sender, EventArgs e)
    {
        if (this.AddRecipeToCallback != null)
        {
            ImageButton btn = sender as ImageButton;
            int recipeId = int.Parse(btn.Attributes["recipeId"]);
            this.AddRecipeToCallback(recipeId);
        }
    }

    protected void lbRecipeName_Clicked(object sender, EventArgs e)
    {
        if (this.Results != null)
        {
            this.ResultsStored = this.Results;
        }
        else
        {
            int? servings = null;
            if (!string.IsNullOrEmpty(this.txtServingsNum.Text))
            {
                servings = int.Parse(this.txtServingsNum.Text);
            }
            int[] recipeCats = null;
            if (this.RecipeCategories != null && this.RecipeCategories.Length > 0)
            {
                recipeCats = (from item in this.RecipeCategories
                              select item.CategoryId).ToArray();
            }

            this.ResultsStored  = (from item in BusinessFacade.Instance.GetRecipesListByComplexSearch(this.txtFreeText.Text, servings, recipeCats, ((BasePage)Page).UserId)
                            select new SRL_Recipe(item)).ToArray();
            
        }

        //if (this.RecipeCategories != null)
        //{
            this.RecipeCategoriesStored = this.RecipeCategories;
        //}
        //else
        //{
        //    this.RecipeCategoriesStored
        //}

        LinkButton btn = (LinkButton)sender;
        int recipeId = int.Parse(btn.Attributes["recipeId"]);

        Response.Redirect(string.Format("~/RecipeDetails.aspx?recipeId={0}&view=4&text={1}&servings={2}", recipeId, this.txtFreeText.Text, this.txtServingsNum.Text));
    }


    public delegate void AddRecipeToMenuHandler(int recipeId);
    public event AddRecipeToMenuHandler AddRecipeToCallback;
}
