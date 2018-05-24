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

public partial class ucAllRecipesList : System.Web.UI.UserControl
{
    int viewOption;
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

    public void ShowData(int option)
    {
        this.viewOption = option;
        
        if (!string.IsNullOrEmpty(this.Request["paging"]))
        {
            if (this.Request["paging"] == "0")
            {
                this.gridRecipesList.AllowPaging = false;
            }
            else
            {
                this.gridRecipesList.AllowPaging = true;

            }
        }
        else
        {
            this.gridRecipesList.AllowPaging = true;
        }

        Recipe[] data;

        if (option == 1) //user recipes
        {
            if (((BasePage)Page).UserType == AppEnv.USER_ADMIN)
            {
                data = BusinessFacade.Instance.GetRecipesList();
            }
            else
            {
                data = BusinessFacade.Instance.GetUserRecipesList(((BasePage)Page).UserId);
            }
        }
        else if (option == 2) //user favorites recipes
        {
            data = BusinessFacade.Instance.GetUserFavoritesRecipes(((BasePage)Page).UserId);
        }
        else //all recipes
        {
            if (((BasePage)Page).UserType == AppEnv.USER_ADMIN)
            {

                data = BusinessFacade.Instance.GetRecipesList();
            }
            else
            {
                //data = BusinessFacade.Instance.GetRecipesListByFreeText("", ((BasePage)Page).UserId).ToArray();
                data = BusinessFacade.Instance.GetRecipesListByFreeText("").ToArray();
            }
        }

        if (data != null)
        {
            this.Results = (from item in data.OrderBy(r => r.RecipeName)
                            select new SRL_Recipe(item)).ToArray();
            gridRecipesList.DataSource = this.Results;
            gridRecipesList.DataBind();
            lblResultsCount.Text = string.Format(MyGlobalResources.FoundNoRecipes, this.Results.Length);
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

    protected void gridRecipesList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SRL_Recipe recipe = (SRL_Recipe)e.Row.DataItem;

            HyperLink hl = (HyperLink)e.Row.FindControl("btnViewRecipe");
            hl.NavigateUrl = string.Format("~/RecipeDetails.aspx?recipeId={0}&view={1}", recipe.RecipeId,
                (this.viewOption == 1 ? (int)PersonalAreaViewEnum.MyRecipesSearch : (int)PersonalAreaViewEnum.MyFavoritesRecipesSearch));

            Label lbl = (Label)e.Row.FindControl("lblUser");
            lbl.Text = @"נוסף על ידי " + ((SRL_Recipe)e.Row.DataItem).Name;
        }
    }

    protected void gridRecipesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridRecipesList.DataSource = this.Results;
        this.gridRecipesList.PageIndex = e.NewPageIndex;
        this.gridRecipesList.DataBind();
    }

    public delegate void AddRecipeToMenuHandler(int recipeId);
    public event AddRecipeToMenuHandler AddRecipeToCallback;

    #region IPostBackEventHandler Members

    //public void RaisePostBackEvent(string eventArgument)
    //{
    //    int receipeId = int.Parse(eventArgument);

    //    User currUser = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId);

    //    if (currUser != null)
    //    {
    //        Recipe currRecipe = BusinessFacade.Instance.GetRecipe(receipeId);

    //        if ((currUser.UserTypeId == 1) || (currUser.UserId == currRecipe.UserId))
    //        {
    //            this.ucRecipe.ReadOnly = false;
    //        }
    //    }

    //    this.ucRecipe.EditRecipe(receipeId);
    //}

    #endregion
}
