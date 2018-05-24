using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_ucPopupLogin : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LiteralUserName.Text = Resources.MyGlobalResources.UserName;
        LiteralPassword.Text = Resources.MyGlobalResources.UserPassword;
    }
}