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

public partial class ucMenusList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void ShowData()
    {
        this.rptMenusList.DataSource = BusinessFacade.Instance.GetMenusList(((BasePage)Page).UserId);
        this.rptMenusList.DataBind();
    }

    protected void btnShoppingList_Click(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        if (!string.IsNullOrEmpty(btn.Attributes["menuId"]))
        {
            int menuId = int.Parse(btn.Attributes["menuId"]);
            string url = string.Format("~/ShoppingList.aspx?menuId={0}", menuId);
            this.Response.Redirect(url);
        }
    }


    protected void btnDeleteMenu_Click(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        if (!string.IsNullOrEmpty(btn.Attributes["menuId"]))
        {
            int menuId = int.Parse(btn.Attributes["menuId"]);
            if (BusinessFacade.Instance.DeleteMenu(menuId))
            {
                this.ShowData();
            }
        }
    }

    protected void btnMenuDetails_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if (!string.IsNullOrEmpty(btn.Attributes["menuId"]))
        {
            int menuId = int.Parse(btn.Attributes["menuId"]);
            string url = string.Format("~/MenuDetails.aspx?menuId={0}", menuId);
            this.Response.Redirect(url);
        }
    }
    protected void btnQuickMenu_Click(object sender, EventArgs e)
    {
        this.CreateMenu(MenuTypeEnum.QuickMenu);
    }

    protected void btnMealMenu_Click(object sender, EventArgs e)
    {
        this.CreateMenu(MenuTypeEnum.OneMeal);
    }
    
    protected void btnWeeklyMenu_Click(object sender, EventArgs e)
    {
        this.CreateMenu(MenuTypeEnum.Weekly);
    }
    
    protected void btnManyWeeksMenu_Click(object sender, EventArgs e)
    {
        this.CreateMenu(MenuTypeEnum.ManyWeeks);
    }

    private void CreateMenu(MenuTypeEnum menuTypeId)
    {
        int menuId;
        bool result = BusinessFacade.Instance.CreateMenu((int)menuTypeId, ((BasePage)Page).UserId, 0, out menuId);
        if (result)
        {
            string url = string.Format("~/MenuRecipes.aspx?menuId={0}", menuId);
            this.Response.Redirect(url);
        }
    }
}
