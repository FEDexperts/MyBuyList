using System;
using System.Collections;
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
using System.Diagnostics;
using System.Threading;

using MyBuyList.BusinessLayer;
using MyBuyList.BusinessLayer.Managers;
using MyBuyList.Shared.Entities;
using ProperControls.Pages;

public partial class ucMyRecipesList : System.Web.UI.UserControl
{
    private Recipe[] Result
    {
        get { return (Recipe[])ViewState["Result"]; }
        set { ViewState["Result"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucRecipe.RefreshData += new ucRecipe.RefreshHandler(this.RefreshData);

        this.lblResult.Text = string.Empty;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }

    public void ShowData()
    {
        Recipe[] result;

        if (((BasePage)Page).UserType == AppEnv.USER_ADMIN)
        {
            result = BusinessFacade.Instance.GetRecipesList();
        }
        else
        {
            result = BusinessFacade.Instance.GetUserRecipesList(((BasePage)this.Page).UserId);
        }

        if (result != null)
        {
            this.rptRecipesList.DataSource = result.OrderBy(r => r.RecipeName).OrderBy(r => r.IsApproved);
            this.rptRecipesList.DataBind();
        }

    }


    public void RefreshData()
    {
        if (((BasePage)Page).UserType == AppEnv.USER_ADMIN)
        {
            this.Result = BusinessFacade.Instance.GetRecipesList();

        }
        else
        {
            this.Result = BusinessFacade.Instance.GetUserRecipesList(((BasePage)Page).UserId);
        }

        if (this.Result != null)
        {
            this.rptRecipesList.DataSource = this.Result.OrderBy(r => r.RecipeName).OrderBy(r => r.IsApproved);
            this.rptRecipesList.DataBind();
        }

        Response.Redirect("PersonalArea.aspx?view=" + (int)PersonalAreaViewEnum.MyRecipesList);
    }

    protected void Recipe_DataBound(object sender, RepeaterItemEventArgs e)
    {


        if (((BasePage)Page).UserType == AppEnv.USER_ADMIN)
        {
            RepeaterItem rptItem = e.Item as RepeaterItem;
            Recipe curr = (Recipe)rptItem.DataItem;
  
            Label lbl = (Label)rptItem.FindControl("lblUserName");
            lbl.Visible = true;

            Label lblDate = (Label)rptItem.FindControl("lblDate");
            lblDate.Visible = true;
            lblDate.Text = curr.CreatedDate.ToString("dd/MM/yyyy HH:mm");

            CheckBox cb = (CheckBox)rptItem.FindControl("cbIsApproved");
            cb.Visible = true;
            cb.Checked = curr.IsApproved;
            cb.Attributes["onclick"] = string.Format("changed({0});", curr.RecipeId);
        }
    }

    protected void btnNewRecipe_Click(object sender, EventArgs e)
    {
        this.ucRecipe.NewRecipe();
    }

    protected void btnViewRecipe_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if (!string.IsNullOrEmpty(btn.Attributes["recipeId"]))
        {
            int recipeId = int.Parse(btn.Attributes["recipeId"]);
            string url = string.Format("~/RecipeDetails.aspx?recipeId={0}&view={1}", recipeId, (int)PersonalAreaViewEnum.MyRecipesList);
            this.Response.Redirect(url);
        }
    }

    protected void btnEditRecipe_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if (!string.IsNullOrEmpty(btn.Attributes["recipeId"]))
        {
            int recipeId = int.Parse(btn.Attributes["recipeId"]);
            this.ucRecipe.EditRecipe(recipeId);
        }
    }

    protected void btnDeleteRecipe_Click(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        if (!string.IsNullOrEmpty(btn.Attributes["recipeId"]))
        {
            int recipeId = int.Parse(btn.Attributes["recipeId"]);
            if (BusinessFacade.Instance.DeleteRecipe(recipeId))
            {
                this.RefreshData();;
            }
            else
            {
                this.result.Visible = true;
                this.lblResult.Text = "מתכון זה מופיע בתפריט/ים ולכן אין באפשרותך למחוק אותו";
            }
        }
    }

    protected void ShowCategories_Click(int recipeId, SRL_RecipeCategory[] arr)
    {
        this.ucRecipeCats.ShowCategories(recipeId, arr);
    }

    protected void SelectPicture_Click(int recipeId)
    {
        this.ucRecipePicture.ShowPicture(recipeId);
    }

    protected void RecipeCategories_RefreshData(SRL_RecipeCategory[] arr)
    {
        this.ucRecipe.RefreshCategories(arr);
    }
}
