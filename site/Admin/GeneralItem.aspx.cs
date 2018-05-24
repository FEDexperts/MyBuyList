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

public partial class PageGeneralItem : BasePage
{
    int? ItemId
    {
        get { return (int?)ViewState["ItemId"]; }
        set { ViewState["ItemId"] = value; }
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
                if (!string.IsNullOrEmpty(this.Request["ItemId"]))
                {
                    this.ItemId = int.Parse(this.Request["ItemId"]);
                    GeneralItem Item = BusinessFacade.Instance.GetGeneralItem(this.ItemId.Value);
                    if (Item != null)
                    {
                        this.txtItemName.Text = Item.GeneralItemName;
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

        GeneralItem Item = new GeneralItem();
        Item.GeneralItemId = this.ItemId == null ? -1 : this.ItemId.Value;
        Item.GeneralItemName = this.txtItemName.Text;

        if (BusinessFacade.Instance.SaveGeneralItem(Item))
        {
            this.Response.Redirect("~/Admin/GeneralItemsList.aspx");
        }    
    }
    protected void custValidItemName_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(this.txtItemName.Text))
        {
            args.IsValid = !BusinessFacade.Instance.CheckDuplicateGeneralItemName(this.ItemId == null ? -1 : this.ItemId.Value,
                                                                                     this.txtItemName.Text);
        }
        else
        {
            args.IsValid = true;
        }
    }
}
