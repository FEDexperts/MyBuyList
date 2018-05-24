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

public partial class PageMeasurementUnitsConvertList : BasePage
{
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
        this.rolMeasurementUnitsConverts.DataSource = BusinessFacade.Instance.GetMeasurementUnitsConvertList();
        this.rolMeasurementUnitsConverts.DataBind();
    }


    protected void rolMeasurementUnitsConverts_ItemDataBound(object sender, ReorderListItemEventArgs e)
    {
        ReorderListItem rolItem = e.Item as ReorderListItem;
        LinkButton btn = rolItem.FindControl("btnUpdate") as LinkButton;
        Label textLine = rolItem.FindControl("lbMeasurementUnitsConvertText") as Label;
        MeasurementUnitsConvert MeasuresConvert = e.Item.DataItem as MeasurementUnitsConvert;
        if (MeasuresConvert != null)
        {
            btn.PostBackUrl = string.Format("~/Admin/MeasurementUnitsConvert.aspx?ConvertId={0}", MeasuresConvert.ConvertId);
            string from = MeasuresConvert.FromQuantity.ToString();
            string[] fromStr = from.Split('.');
            if (fromStr[1] == "00")
            {
                from = fromStr[0];
            }

            string to = MeasuresConvert.ToQuantity.ToString();
            string[] toStr = to.Split('.');
            if (toStr[1] == "00")
            {
                to = toStr[0];
            }

            
            textLine.Text = string.Format("{0} {1} {2} = {3} {4}", from, MeasuresConvert.SOURCE_UNIT_NAME, MeasuresConvert.FOOD_NAME, to, MeasuresConvert.TARGET_UNIT_NAME);

        }

        //btn = rolItem.FindControl("btnDelete") as LinkButton;
       // btn.Visible = MeasuresConvert.AllowDelete;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        MeasurementUnitsConvert newItem = new MeasurementUnitsConvert();
        newItem.SortOrder = 1;
        newItem.FoodId = 0;
        newItem.FromUnitId = 0;
        newItem.ToUnitId = 0;
        BusinessFacade.Instance.SaveMeasurementUnitsConvert(newItem);
        this.Rebind();
    }
}
