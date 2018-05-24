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

public partial class ucSearchSimple : System.Web.UI.UserControl, IPostBackEventHandler
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

                if ((this.ResultsStored != null) && (int.Parse(this.Request["view"]) == (int)PersonalAreaViewEnum.SimpleSearch))
                {

                    if (!string.IsNullOrEmpty(this.Request["text"]))
                    {
                        this.txtFreeText.Text = this.Request["text"];
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
        //this.Results = (from item in BusinessFacade.Instance.GetRecipesListByFreeText(this.txtFreeText.Text, ((BasePage)Page).UserId)
        //                select new SRL_Recipe(item)).ToArray();
        this.Results = (from item in BusinessFacade.Instance.GetRecipesListByFreeText(this.txtFreeText.Text)
                        select new SRL_Recipe(item)).ToArray();
        this.gridRecipesList.DataSource = this.Results;
        this.gridRecipesList.DataBind();
        this.lblResultsCount.Text = string.Format(MyGlobalResources.FoundNoRecipes, this.Results.Length);
        this.lblComment.Visible = (this.Results.Length > 0);
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
            SRL_Recipe receipe = (SRL_Recipe)e.Row.DataItem;

            //Label lnkReceipe = (Label)e.Row.FindControl("lnkReceipe");
            //lnkReceipe.Text = receipe.RecipeName;

            e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#dddddd'";
            e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='Transparent'";
            
            //string script = Page.ClientScript.GetPostBackEventReference(this, receipe.RecipeId.ToString(), false);
            //lnkReceipe.Attributes["onclick"] = script;
            //lnkReceipe.Style.Add("cursor", "pointer");

            Label lbl = (Label)e.Row.FindControl("lblUser");
            //lbl.Text = @"נוסף על ידי " + (BusinessFacade.Instance.GetRecipe(receipe.RecipeId)).User.DisplayName;
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

            //this.ResultsStored = (from item in BusinessFacade.Instance.GetRecipesListByFreeText(this.txtFreeText.Text, ((BasePage)Page).UserId)
            //                      select new SRL_Recipe(item)).ToArray();
            this.ResultsStored = (from item in BusinessFacade.Instance.GetRecipesListByFreeText(this.txtFreeText.Text)
                                  select new SRL_Recipe(item)).ToArray();
            
        }

        LinkButton btn = (LinkButton)sender;
        int recipeId = int.Parse(btn.Attributes["recipeId"]);

        Response.Redirect(string.Format("~/RecipeDetails.aspx?recipeId={0}&view=3&text={1}", recipeId, this.txtFreeText.Text));
    }

    public delegate void AddRecipeToMenuHandler(int recipeId);
    public event AddRecipeToMenuHandler AddRecipeToCallback;

    #region IPostBackEventHandler Members

    public void RaisePostBackEvent(string eventArgument)
    {
        int receipeId = int.Parse(eventArgument);

        User currUser = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId);
        
        if (currUser != null)
        {
            Recipe currRecipe = BusinessFacade.Instance.GetRecipe(receipeId);

            if ((currUser.UserTypeId == 1) || (currUser.UserId == currRecipe.UserId))
            {
                this.ucRecipe.ReadOnly = false;
            }
        }

        this.ucRecipe.EditRecipe(receipeId);
    }

    #endregion
}
