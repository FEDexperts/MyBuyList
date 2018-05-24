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

using ProperControls.Pages;

using MyBuyList.BusinessLayer;

public partial class Login : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Session["AnonymousUsersMenu"] != null)
            {
                this.lblTitle.Text = "על מנת לשמור את התפריט, עליך להתחבר/להירשם";

                Session.Remove("AnonymousUsersMenu");
            }
            else if (this.ReturnUrl.Contains("personalarea.aspx"))
            {
                this.lblTitle.Text = "על מנת לגשת לרשימות השמורות, עליך להתחבר/להירשם";
            }

            if (this.ReturnUrl.Contains("quickmenu.aspx") || this.ReturnUrl.Contains("shoppinglist.aspx"))         
            {
                this.lblTitle.Text = "על מנת להשלים את הפעולה, עליך להתחבר/להירשם";
                this.lblTitle.Visible = true;
            }
            if (this.ReturnUrl.Contains("recipeedit.aspx"))
            {
                this.lblTitle.Text = "על מנת ליצור מתכון חדש, עליך להתחבר/להירשם";
                this.lblTitle.Visible = true;
            }
            if (this.ReturnUrl.Contains("menuedit.aspx"))
            {
                this.lblTitle.Text = "על מנת ליצור תפריט חדש, עליך להתחבר/להירשם";
                this.lblTitle.Visible = true;
            }

            if (!string.IsNullOrEmpty(this.ReturnUrl))
            {
                HyperLink lnk = (HyperLink)this.Login1.FindControl("CreateUserLink");
                lnk.NavigateUrl += string.Format("?ReturnUrl={0}",Request["ReturnUrl"]);

                lnk = (HyperLink)this.Login1.FindControl("PasswordRecoveryLink");
                lnk.NavigateUrl += string.Format("?ReturnUrl={0}", Request["ReturnUrl"]);
            }
        }

        Form.DefaultButton = Login1.FindControl("LoginButton").UniqueID;
    }

    public string ReturnUrl
    {
        get
        {
            return (string.IsNullOrEmpty(Request["ReturnUrl"]) ? string.Empty : Request["ReturnUrl"].Trim().ToLower());
        }
    }
    
    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        if ((string.IsNullOrEmpty(this.ReturnUrl)) || (this.ReturnUrl.Contains("default.aspx")))
        {
            FormsAuthentication.Authenticate(Login1.UserName, Login1.Password);
            FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            // nothing to do - go to the return url regularly
            FormsAuthentication.Authenticate(Login1.UserName, Login1.Password);
            FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);
        }
    }
}
