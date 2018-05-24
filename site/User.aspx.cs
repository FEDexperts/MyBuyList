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
using MyBuyList.BusinessLayer.Managers;
using MyBuyList.Shared.Entities;
using System.IO;
using ProperControls.Pages;

public partial class PageUser : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (((BasePage)Page).CurrUser != null)
            {
                if (((BasePage)Page).UserId != -1)
                {

                    User currUser = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId);
                    if (currUser != null)
                    {
                        this.txtDisplayName.Text = currUser.DisplayName;
                        this.txtEmail.Text = currUser.Email;
                        this.txtFirtstName.Text = currUser.FirstName;
                        this.txtLastName.Text = currUser.LastName;
                        this.txtUserName.Text = currUser.Name;
                        this.txtUserName.Enabled = false;
                        //this.txtUserPassword.TextMode = TextBoxMode.SingleLine;
                        //this.txtUserPassword.Text = currUser.Password;
                        //this.rfvtxtUserPassword.Enabled = false;
                        this.trPassword1.Visible = false;
                        this.trPassword2.Visible = false;
                        //this.txtUserPassword2.TextMode = TextBoxMode.SingleLine;
                        //this.txtUserPassword2.Text = currUser.Password;
                        //this.rfvtxtUserPassword2.Enabled = false;
                    }
                }
            }
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (BusinessFacade.Instance.GetUserByUserName(this.txtUserName.Text) != null)
        {
            lblResult.Text = "שם משתמש קיים במערכת";
            this.hlSignIn.Visible = true;
        }
        else
        {
            User currUser;
            //bool b = true;
            if (((BasePage)Page).UserId == -1)
            {
                currUser = new User();
                currUser.UserId = -1;
                currUser.Password = this.txtUserPassword.Text;
            }
            else
            {

                currUser = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId);

                currUser.UserId = ((BasePage)Page).UserId;
            }


            currUser.DisplayName = this.txtDisplayName.Text;
            currUser.Email = this.txtEmail.Text;
            currUser.FirstName = this.txtFirtstName.Text;
            currUser.LastName = this.txtLastName.Text;
            currUser.Name = this.txtUserName.Text;
            currUser.UserTypeId = 2;

            if (BusinessFacade.Instance.SaveUser(currUser))
            {
                if (((BasePage)Page).UserId == -1)
                {
                    //Conacting the user
                    FormsAuthentication.Authenticate(currUser.Name, currUser.Password);
                    FormsAuthentication.RedirectFromLoginPage(currUser.Name, false);

                    // Save temp menus of the new user - commented because causes compilation error.
                    //if (((ProperDevMasterPage)this.Master).TempUser != 0)
                    
                    //{
                    //    User newUser = BusinessFacade.Instance.GetUserByUserName(currUser.Name);
                    //    MyBuyList.Shared.Entities.Menu[] userMenus = BusinessFacade.Instance.GetMenusList(int.Parse(ConfigurationManager.AppSettings["anonymous"]), ((ProperDevMasterPage)this.Master).TempUser);
                    //    if (userMenus != null)
                    //    {
                    //        foreach (MyBuyList.Shared.Entities.Menu currMenu in userMenus)
                    //        {
                    //            BusinessFacade.Instance.UpdateMenuUser(currMenu.MenuId, newUser.UserId);
                    //        }
                    //    }
                    //}


                    StringWriter html = new StringWriter();
                    Server.Execute("~/WelcomePage.aspx", html);

                    try
                    {
                        ProperServices.Common.Mail.Mailer.SendMail(currUser.Email, ProperControls.General.Utils.FromEmail, "ברוך הבא לMyByList", html.ToString(), true);

                        Response.Redirect("~/Users/PersonalArea.aspx");
                    }
                    catch
                    {
                        this.lblResult.Text = "בעיה בכתובת הדואר האלקטרוני שהוכנסה";
                    }
                }

                AppEnv.MoveToDefaultPage();
            }
            else
            {
                lblResult.Text = "שם משתמש קיים במערכת";
                this.hlSignIn.Visible = true;
            }
        }
    }
}
