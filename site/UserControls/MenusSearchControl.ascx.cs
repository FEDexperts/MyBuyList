using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyBuyList.Shared.Enums;
using MyBuyList.Shared.Entities;

public partial class UserControls_MenusSearchControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void FillList(MCategory[] categoryList, string MenuCategoryChangeBaseUrl)
    {
        lstCategories.Items.Clear();
        lstCategories.Items.Add(new ListItem("בחר קטגוריה"));
        foreach (MCategory mCategory in categoryList)
        {
            string Text = string.Format("{0} ({1})", mCategory.MCategoryName, mCategory.MenusCount);
            string Url = string.Format(MenuCategoryChangeBaseUrl, mCategory.MCategoryId);
            lstCategories.Items.Add(new ListItem(Text, Url));
        }
    }

    protected void lnkAllMenus_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Menus.aspx");
    }

    protected void lnkMyMenus_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/Menus.aspx?page=1&orderby=LastUpdate&disp={0}", RecipeDisplayEnum.MyRecipes.ToString()));
    }

    protected void lnkFavMenus_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/Menus.aspx?page=1&orderby=LastUpdate&disp={0}", RecipeDisplayEnum.MyFavoriteRecipes.ToString()));
    }

    public void EmphasizeCurrentSearch(RecipeDisplayEnum currentDisplay)
    {
        this.lnkAllMenus.Style["text-decoration"] = "none";
        this.lnkMyMenus.Style["text-decoration"] = "none";
        this.lnkFavMenus.Style["text-decoration"] = "none";

        switch (currentDisplay)
        {
            case RecipeDisplayEnum.All:
                this.lnkAllMenus.Style["text-decoration"] = "underline";
                break;
            case RecipeDisplayEnum.MyRecipes:
                this.lnkMyMenus.Style["text-decoration"] = "underline";
                break;
            case RecipeDisplayEnum.MyFavoriteRecipes:
                this.lnkFavMenus.Style["text-decoration"] = "underline";
                break;
        }
    }
    protected void lstCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect(lstCategories.Items[lstCategories.SelectedIndex].Value);
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(string.Format("~/Menus.aspx?page=1&orderby=LastUpdate&disp=BySearchSimple&term={0}", txtSearchTerm.Text));
    }
}