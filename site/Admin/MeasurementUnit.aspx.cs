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
using MyBuyList.Shared.Entities;
using ProperControls.Pages;

public partial class PageMeasurementUnit : BasePage
{
    int? UnitId
    {
        get { return (int?)ViewState["UnitId"]; }
        set { ViewState["UnitId"] = value; }
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
                if (!string.IsNullOrEmpty(this.Request["unitId"]))
                {
                    this.UnitId = int.Parse(this.Request["unitId"]);
                    MeasurementUnit unit = BusinessFacade.Instance.GetMeasurementUnit(this.UnitId.Value);
                    if (unit != null)
                    {
                        this.txtUnitName.Text = unit.UnitName;
                    }
                }
            }
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        this.Validate();
        if (!this.IsValid)
        {
            return;
        }

        MeasurementUnit unit = new MeasurementUnit();
        unit.UnitId = this.UnitId == null ? -1 : this.UnitId.Value;
        unit.UnitName = this.txtUnitName.Text;

        if (BusinessFacade.Instance.SaveMeasurementUnit(unit))
        {
            this.Response.Redirect("~/Admin/MeausurementUnitsList.aspx");
        }    
    }
    protected void custUnitName_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(this.txtUnitName.Text))
        {
            args.IsValid = !BusinessFacade.Instance.CheckDuplicateMeasurementUnitName(this.UnitId == null ? -1 : this.UnitId.Value,  
                                                                                      this.txtUnitName.Text);
        }
        else
        {
            args.IsValid = true;
        }
    }
}
