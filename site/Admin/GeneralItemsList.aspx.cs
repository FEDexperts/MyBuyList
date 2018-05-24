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
using ProperControls.Pages;

public partial class PageGeneralItemsList : BasePage
{
    GeneralItem[] Data
    {
        get { return (GeneralItem[]) this.Cache["GeneralItems"]; }
        set { this.Cache["GeneralItems"] = value; }
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
        this.Data = BusinessFacade.Instance.GetGeneralItemsList();
        this.rolGeneralItems.DataSource = this.Data;
        this.rolGeneralItems.DataBind();
    }


    protected void rolGeneralItems_ItemDataBound(object sender, ReorderListItemEventArgs e)
    {
        ReorderListItem rolItem = e.Item as ReorderListItem;
        LinkButton btn = rolItem.FindControl("btnUpdate") as LinkButton;
        GeneralItem Item = e.Item.DataItem as GeneralItem;
        if (Item != null)
        {
            btn.PostBackUrl = string.Format("~/Admin/GeneralItem.aspx?ItemId={0}", Item.GeneralItemId);
        }

        btn = rolItem.FindControl("btnDelete") as LinkButton;
        btn.Visible = Item.AllowDelete;
        btn.Attributes.Add("ItemId", Item.GeneralItemId.ToString());
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if (!string.IsNullOrEmpty(btn.Attributes["ItemId"]))
        {
            int ItemId = int.Parse(btn.Attributes["ItemId"]);
            if (BusinessFacade.Instance.DeleteGeneralItem(ItemId))
            {
                this.Rebind();
            }
        }
    }

    protected void rolGeneralItems_ItemReorder(object sender, ReorderListItemReorderEventArgs e)
    {
       this.DoReorder(e.OldIndex, e.NewIndex);
    }

    private void DoReorder(int oldIndex, int newIndex)
    {
        GeneralItem[] arr = this.Data;

        if (oldIndex < newIndex) //item moved down
        {
            int tempOrder = arr[newIndex].SortOrder;
            
            for (int i = newIndex; i > oldIndex; i--)
            {
                arr[i].SortOrder = arr[i-1].SortOrder;
            }

            arr[oldIndex].SortOrder = tempOrder;
        }
        else  //item moved up
        {
            int tempOrder = arr[newIndex].SortOrder;

            for (int i = newIndex; i < oldIndex; i++)
            {
                arr[i].SortOrder = arr[i+1].SortOrder;
            }

            arr[oldIndex].SortOrder = tempOrder;
        }

        if (BusinessFacade.Instance.ReorderGeneralItems(arr))
        {
            this.Rebind();
        }
    }
}
