using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text;
        string password = txtPassword.Text;

        CurrUser = BusinessFacade.Instance.GetUser(userName, password);
        if (CurrUser != null)
        {
            Session[AppConstants.CURR_USER] = CurrUser;
            Response.Redirect("Admin.aspx");
        }
    }
}