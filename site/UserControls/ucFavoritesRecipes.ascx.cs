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
using ProperControls.Pages;

public partial class ucFavoritesRecipes : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void ShowData()
    {
        this.rptRecipesList.DataSource = BusinessFacade.Instance.GetUserFavoritesRecipes(((BasePage)Page).UserId);
        this.rptRecipesList.DataBind();
    }

    protected void btnViewRecipe_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if (!string.IsNullOrEmpty(btn.Attributes["recipeId"]))
        {
            int recipeId = int.Parse(btn.Attributes["recipeId"]);
            string url = string.Format("~/RecipeDetails.aspx?recipeId={0}&view={1}", recipeId, (int)PersonalAreaViewEnum.FavoritesRecipes);
            this.Response.Redirect(url);
        }
    }

    protected void btnRemoveFavoriteRecipe_Click(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        if (!string.IsNullOrEmpty(btn.Attributes["recipeId"]))
        {
            int recipeId = int.Parse(btn.Attributes["recipeId"]);
            int x = 0; //added code
            if (BusinessFacade.Instance.RemoveUserFavoritesRecipe(((BasePage)Page).UserId, recipeId, out x)) //added code
            {
                this.ShowData();
            }
        }
    }
}
