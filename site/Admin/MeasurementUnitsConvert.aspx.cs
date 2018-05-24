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

public partial class PageMeasurementUnitsConvert : BasePage
{
    int ConvertId
    {
        get 
        { 
            return ViewState["ConvertId"] == null ? 0 : (int) ViewState["ConvertId"];
        }
        set
        {
            ViewState["ConvertId"] = value;
        }
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
                this.ddlFromMeasurementUnits.DataSource = BusinessFacade.Instance.GetMeasurementUnitsList();
                this.ddlFromMeasurementUnits.DataTextField = "UnitName";
                this.ddlFromMeasurementUnits.DataValueField = "UnitId";
                this.ddlFromMeasurementUnits.DataBind();

                this.ddlToMeasurementUnits.DataSource = BusinessFacade.Instance.GetMeasurementUnitsList();
                this.ddlToMeasurementUnits.DataTextField = "UnitName";
                this.ddlToMeasurementUnits.DataValueField = "UnitId";
                this.ddlToMeasurementUnits.DataBind();

                if (!string.IsNullOrEmpty(this.Request["ConvertId"]))
                {
                    this.ConvertId = int.Parse(this.Request["ConvertId"]);

                    MeasurementUnitsConvert convert = BusinessFacade.Instance.GetMeasurementUnitsConvert(ConvertId);

                    if (convert != null)
                    {
                        this.txtIngredientName.Text = convert.Food.FoodName;
                        this.txtFromQuantity.Text = convert.FromQuantity.ToString();
                        this.ddlFromMeasurementUnits.SelectedValue = convert.FromUnitId.ToString();
                        this.txtToQuantity.Text = convert.ToQuantity.ToString();
                        this.ddlToMeasurementUnits.SelectedValue = convert.ToUnitId.ToString();
                    }
                }
            }
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        this.Validate();
        if (this.IsValid)
        {
            Food selectedFood = BusinessFacade.Instance.GetFood(this.txtIngredientName.Text);
            MeasurementUnitsConvert convert = new MeasurementUnitsConvert();
            convert.ConvertId = this.ConvertId;
            convert.FoodId = selectedFood.FoodId;
            convert.FromQuantity = decimal.Parse(this.txtFromQuantity.Text);
            convert.FromUnitId = int.Parse(this.ddlFromMeasurementUnits.SelectedValue);
            convert.ToQuantity = decimal.Parse(this.txtToQuantity.Text);
            convert.ToUnitId = int.Parse(this.ddlToMeasurementUnits.SelectedValue);


            if (BusinessFacade.Instance.SaveMeasurementUnitsConvert(convert))
            {
                this.Response.Redirect("~/Admin/MeasurementUnitsConvertList.aspx");
            }
        }
    }


    protected void custValidIngredientName_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(this.txtIngredientName.Text))
        {
            Food selected = BusinessFacade.Instance.GetFood(this.txtIngredientName.Text);
            if (selected == null)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        else
        {
            args.IsValid = true;
        }
    }

    protected void custValidatorQty_ServerValidate(object source, ServerValidateEventArgs args)
    {
        decimal result = 0;
        args.IsValid = decimal.TryParse(args.Value, out result);
    }
}
