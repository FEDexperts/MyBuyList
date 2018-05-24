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
using System.Web.Services;
using MyBuyList.BusinessLayer;
using MyBuyList.BusinessLayer.Managers;
using MyBuyList.Shared.Entities;
using ProperControls.Pages;

public partial class PagePersonalArea : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Master.SetLeftBackgroundImage(0);

            if (!string.IsNullOrEmpty(this.Request["view"]))
            {
                int viewId = int.Parse(this.Request["view"]);
                if (viewId == (int)PersonalAreaViewEnum.MyRecipesList)
                {
                    this.btnMyRecipesList_Click(this.btnMyRecipesList, new EventArgs());
                }
                else if (viewId == (int)PersonalAreaViewEnum.FavoritesRecipes)
                {
                    this.btnMyFavoritesRecipes_Click(this.btnMyFavoritesRecipes, new EventArgs());
                }
                else
                {
                    this.btnMenusList_Click(this.btnMenusList, new EventArgs());
                }
            }
            else
            {
                this.btnMenusList_Click(this.btnMenusList, new EventArgs());
            }
        }
    }

    protected void btnMenusList_Click(object sender, EventArgs e)
    {
        this.btnMenusList.BackColor = System.Drawing.Color.Crimson;
        this.btnMenusList.ForeColor = System.Drawing.Color.White;   
        
        this.btnMyRecipesList.BackColor = System.Drawing.Color.Transparent;
        this.btnMyRecipesList.ForeColor = System.Drawing.Color.Crimson;

        this.btnMyFavoritesRecipes.BackColor = System.Drawing.Color.Transparent;
        this.btnMyFavoritesRecipes.ForeColor = System.Drawing.Color.Crimson;
        
        this.ucMyRecipesList.Visible = false;
        this.ucFavoritesRecipes.Visible = false;

        this.ucMenusList.Visible = true;
        this.ucMenusList.ShowData();
    }

    protected void btnMyRecipesList_Click(object sender, EventArgs e)
    {
        this.btnMyRecipesList.BackColor = System.Drawing.Color.Crimson;
        this.btnMyRecipesList.ForeColor = System.Drawing.Color.White;
        
        this.btnMenusList.BackColor = System.Drawing.Color.Transparent;
        this.btnMenusList.ForeColor = System.Drawing.Color.Crimson;

        this.btnMyFavoritesRecipes.BackColor = System.Drawing.Color.Transparent;
        this.btnMyFavoritesRecipes.ForeColor = System.Drawing.Color.Crimson;

        this.ucMenusList.Visible = false;
        this.ucFavoritesRecipes.Visible = false;

        this.ucMyRecipesList.Visible = true;
        this.ucMyRecipesList.ShowData();
    }

    protected void btnMyFavoritesRecipes_Click(object sender, EventArgs e)
    {
        this.btnMyFavoritesRecipes.BackColor = System.Drawing.Color.Crimson;
        this.btnMyFavoritesRecipes.ForeColor = System.Drawing.Color.White;

        this.btnMenusList.BackColor = System.Drawing.Color.Transparent;
        this.btnMenusList.ForeColor = System.Drawing.Color.Crimson;

        this.btnMyRecipesList.BackColor = System.Drawing.Color.Transparent;
        this.btnMyRecipesList.ForeColor = System.Drawing.Color.Crimson;

        this.ucMenusList.Visible = false;
        this.ucMyRecipesList.Visible = false;

        this.ucFavoritesRecipes.Visible = true;
        this.ucFavoritesRecipes.ShowData();
    }

    [WebMethod]
    public static void AllowRecipe(int recipeId)
    {
        BusinessFacade.Instance.AllowRecipe(recipeId);
    }

}
