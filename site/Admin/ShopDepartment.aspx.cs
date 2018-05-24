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

public partial class PageShopDepartment : BasePage
{
    int? DepartmentId
    {
        get { return (int?)ViewState["DepartmentId"]; }
        set { ViewState["DepartmentId"] = value; }
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
                if (!string.IsNullOrEmpty(this.Request["depId"]))
                {
                    this.DepartmentId = int.Parse(this.Request["depId"]);
                    ShopDepartment department = BusinessFacade.Instance.GetShopDepartment(this.DepartmentId.Value);
                    if (department != null)
                    {
                        this.txtDepName.Text = department.ShopDepartmentName;
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

        ShopDepartment dep = new ShopDepartment();
        dep.ShopDepartmentId = this.DepartmentId == null ? -1 : this.DepartmentId.Value;
        dep.ShopDepartmentName = this.txtDepName.Text;

        FoodCategory f = null;
        if (dep.ShopDepartmentId == -1)
        {
            f = new FoodCategory();
            f.FoodCategoryId = -1;
        }
        else
        {
            ShopDepartment department = BusinessFacade.Instance.GetShopDepartment(this.DepartmentId.Value);
            f = BusinessFacade.Instance.GetFoodCategoryByName(department.ShopDepartmentName);
        }

        if (BusinessFacade.Instance.SaveShopDepartment(dep))
        {
            f.FoodCategoryName = dep.ShopDepartmentName;
            f.ShopDepartmentId = dep.ShopDepartmentId;
            if (BusinessFacade.Instance.SaveFoodCategory(f))
            {
                this.Response.Redirect("~/Admin/ShopDepartmentsList.aspx");
            }
            else
            {
                BusinessFacade.Instance.DeleteShopDepartment(dep.ShopDepartmentId);
            }
        }    
    }
    protected void custValidDepartmentName_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(this.txtDepName.Text))
        {
            args.IsValid = !BusinessFacade.Instance.CheckDuplicateShopDepartmentName(this.DepartmentId == null ? -1 : this.DepartmentId.Value,
                                                                                     this.txtDepName.Text);
        }
        else
        {
            args.IsValid = true;
        }
    }
}
