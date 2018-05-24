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

public partial class PageMeausurementUnitsList : BasePage
{
    MeasurementUnit[] Data
    {
        get { return (MeasurementUnit[])this.Cache["MeasurementUnits"]; }
        set { this.Cache["MeasurementUnits"] = value; }
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
        this.Data = BusinessFacade.Instance.GetMeasurementUnitsList();
        //Array.Sort(this.Data);
        this.rolMeasurementUnits.DataSource = this.Data;
        this.rolMeasurementUnits.DataBind();
    }


    protected void rolMeasurementUnits_ItemDataBound(object sender, ReorderListItemEventArgs e)
    {
        ReorderListItem rolItem = e.Item as ReorderListItem;
        LinkButton btn = rolItem.FindControl("btnUpdate") as LinkButton;
        MeasurementUnit unit = e.Item.DataItem as MeasurementUnit;
        if (unit != null)
        {
            btn.PostBackUrl = string.Format("~/Admin/MeasurementUnit.aspx?unitId={0}", unit.UnitId);
        }

        btn = rolItem.FindControl("btnDelete") as LinkButton;
        btn.Visible = unit.AllowDelete;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if (!string.IsNullOrEmpty(btn.Attributes["unitId"]))
        {
            int unitId = int.Parse(btn.Attributes["unitId"]);
            if (BusinessFacade.Instance.DeleteMeasurementUnit(unitId))
            {
                this.Rebind();
            }
        }
    }

    protected void rolMeasurementUnits_ItemReorder(object sender, ReorderListItemReorderEventArgs e)
    {
        this.DoReorder(e.OldIndex, e.NewIndex);
    }

    private void DoReorder(int oldIndex, int newIndex)
    {
        MeasurementUnit[] arr = this.Data;

        if (oldIndex < newIndex) //item moved down
        {
            int tempOrder = arr[newIndex].SortOrder;

            for (int i = newIndex; i > oldIndex; i--)
            {
                arr[i].SortOrder = arr[i - 1].SortOrder;
            }

            arr[oldIndex].SortOrder = tempOrder;
        }
        else  //item moved up
        {
            int tempOrder = arr[newIndex].SortOrder;

            for (int i = newIndex; i < oldIndex; i++)
            {
                arr[i].SortOrder = arr[i + 1].SortOrder;
            }

            arr[oldIndex].SortOrder = tempOrder;
        }

        if (BusinessFacade.Instance.ReorderMeasurementUnits(arr))
        {
            this.Rebind();
        }
    }
}
