using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyBuyList.Shared.Enums;
using MyBuyList.Shared.Entities;

public partial class UserControls_ucRecipesFilter : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string currentDisplay = Request.QueryString["disp"] == string.Empty ? "All" : Request.QueryString["disp"];

        //lnkRecipes.Style["text-decoration"] = "none";
        //lnkMyRecipes.Style["text-decoration"] = "none";
        //lnkMyFavoriteRecipes.Style["text-decoration"] = "none";

        //switch (currentDisplay)
        //{
        //    case "All":
        //        lnkRecipes.Style["text-decoration"] = "underline";
        //        break;
        //    case "MyRecipes":
        //        lnkMyRecipes.Style["text-decoration"] = "underline";
        //        break;
        //    case "MyFavoriteRecipes":
        //        lnkMyFavoriteRecipes.Style["text-decoration"] = "underline";
        //        break;
        //}
    }

    public void FillList(SRL_Category[] categoryList, string RecipeCategoryChangeBaseUrl)
    {
        lstCategories.Items.Clear();
        lstCategories.Items.Add(new ListItem("בחר קטגוריה"));
        foreach (SRL_Category mCategory in categoryList)
        {
            string Text = string.Format("{0} ({1})", mCategory.CategoryName, mCategory.RecipesCount);
            string Url = string.Format(RecipeCategoryChangeBaseUrl, mCategory.CategoryId);
            lstCategories.Items.Add(new ListItem(Text, Url));
        }
    }

    //protected void lnkAllMenus_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Recipes.aspx");
    //}

    //protected void lnkMyMenus_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}", RecipeDisplayEnum.MyRecipes.ToString()));
    //}

    //protected void lnkFavMenus_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}", RecipeDisplayEnum.MyFavoriteRecipes.ToString()));
    //}

    //public void EmphasizeCurrentSearch(RecipeDisplayEnum currentDisplay)
    //{
    //    this.lnkAllMenus.Style["text-decoration"] = "none";
    //    this.lnkMyMenus.Style["text-decoration"] = "none";
    //    this.lnkFavMenus.Style["text-decoration"] = "none";

    //    switch (currentDisplay)
    //    {
    //        case RecipeDisplayEnum.All:
    //            this.lnkAllMenus.Style["text-decoration"] = "underline";
    //            break;
    //        case RecipeDisplayEnum.MyRecipes:
    //            this.lnkMyMenus.Style["text-decoration"] = "underline";
    //            break;
    //        case RecipeDisplayEnum.MyFavoriteRecipes:
    //            this.lnkFavMenus.Style["text-decoration"] = "underline";
    //            break;
    //    }
    //}
    protected void lstCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect(lstCategories.Items[lstCategories.SelectedIndex].Value);
    }
    //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp=BySearchSimple&term={0}", txtSearchTerm.Text));
    //}
}