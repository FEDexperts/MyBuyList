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
using AjaxControlToolkit;

using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;

using Resources;
using ProperControls.Pages;

public partial class PageFoodsList : BasePage
{
    string ActiveFilter
    {
        get { return this.ViewState["ActiveFilter"] == null ? "" : this.ViewState["ActiveFilter"].ToString(); }
        set { this.ViewState["ActiveFilter"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (((BasePage)Page).UserType != AppEnv.USER_ADMIN)
            {
                AppEnv.MoveToDefaultPage();
            }
            else
            {
                this.Rebind();
            }             
        }
    }

    private void Rebind()
    {
        var list = BusinessFacade.Instance.GetFoodsList();
        if (list == null || list.Length == 0)
        {
            this.rptFoods.DataSource = null;
            this.rptFoods.DataBind();
            return;
        }

        TabPanel pnl = this.Tabs.ActiveTab;
        if(pnl.HeaderText == MyGlobalResources.Other)
        {
            Food[] foods = list.Where(f => f.FoodName.Trim() != string.Empty &&
                                          (f.FoodName.Trim()[0] < 'א' ||
                                           f.FoodName.Trim()[0] > 'ת')).ToArray();

            this.rptFoods.DataSource = foods;
            this.rptFoods.DataBind();
        }
        else if(pnl.HeaderText == MyGlobalResources.Temporary)
        {
            Food[] foods = list.Where(f => f.IsTemporary).ToArray();
            this.rptFoods.DataSource = foods;
            this.rptFoods.DataBind();
        }
        else
        {
            Food[] foods = list.Where(f => f.FoodName.Trim().StartsWith(pnl.HeaderText[0].ToString()) || 
                                           f.FoodName.Trim().StartsWith(pnl.HeaderText[1].ToString())).ToArray();
            this.rptFoods.DataSource = foods;
            this.rptFoods.DataBind();
        }
    }

    protected void rptFoods_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header) return;

        RepeaterItem rptItem = e.Item as RepeaterItem;
        LinkButton btn = rptItem.FindControl("btnUpdate") as LinkButton;
        LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;
        Food food = e.Item.DataItem as Food;
        if (food != null)
        {
            btn.PostBackUrl = string.Format("~/Admin/Food.aspx?foodId={0}", food.FoodId);
            btnDelete.Visible = food.AllowDelete;
        }
    }
    public void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if (!string.IsNullOrEmpty(btn.Attributes["FoodId"]))
        {
            int FoodId = int.Parse(btn.Attributes["FoodId"]);
            BusinessFacade.Instance.DeleteFood(FoodId);
            this.Rebind();
        }
    }

    protected void Tabs_ActiveTabChanged(object sender, EventArgs e)
    {
        if (this.Tabs.ActiveTab.HeaderText != this.ActiveFilter)
        {
            this.Rebind();
            this.ActiveFilter = this.Tabs.ActiveTab.HeaderText;
        }
    }
}
