using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProperControls.Pages;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using System.Web.Security;
using System.Configuration;
using System.IO;

public partial class Register : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (((BasePage)Page).CurrUser != null)
            {
                if (((BasePage)Page).UserId != -1)
                {
                    this.imgHeader.ImageUrl = "~/Images/Header_EditDetails.png";
                    this.imgBtnRegister.Visible = false;
                    this.imgBtnSend.Visible = true;

                    User currUser = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId);
                    if (currUser != null)
                    {                        
                        this.txtDisplayName.Text = currUser.DisplayName;
                        this.txtEmail.Text = currUser.Email;
                        this.txtFirtstName.Text = currUser.FirstName;
                        this.txtLastName.Text = currUser.LastName;
                        this.txtUserName.Text = currUser.Name;
                        this.txtUserName.Enabled = false;                        
                        this.trPassword1.Visible = false;
                        this.trPassword2.Visible = false;

                        if (currUser.AgreeToMail)
                        {
                            this.cbxEmail.Text = "נא להפסיק לשלוח לי דיוור מהאתר";
                            this.cbxEmail.Checked = false;
                        }
                        else
                        {
                            this.cbxEmail.Text = "אני מסכימ/ה לקבל דיוור מהאתר";
                            this.cbxEmail.Checked = false;
                        }
                    }
                }
            }
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        User currUser = null;

        if (((BasePage)Page).UserId == -1 && BusinessFacade.Instance.GetUserByUserName(this.txtUserName.Text) != null)
        {
            lblResult.Text = "שם משתמש קיים במערכת";
            this.hlSignIn.Visible = true;
        }
        else
        {
            if (((BasePage)Page).UserId == -1)
            {
                currUser = new User();
                currUser.UserId = -1;
                currUser.Name = this.txtUserName.Text;
                currUser.Password = this.txtUserPassword.Text;
                currUser.UserTypeId = 2;
                currUser.AgreeToMail = this.cbxEmail.Checked;
            }

            else
            {
                currUser = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId);
                if (currUser.AgreeToMail && this.cbxEmail.Checked)
                    currUser.AgreeToMail = false;
                else if (!currUser.AgreeToMail && this.cbxEmail.Checked)
                    currUser.AgreeToMail = true;
            }

            currUser.DisplayName = this.txtDisplayName.Text;
            currUser.Email = this.txtEmail.Text;
            currUser.FirstName = this.txtFirtstName.Text;
            currUser.LastName = this.txtLastName.Text;
        }

        if (BusinessFacade.Instance.SaveUser(currUser))
        {
            if (((BasePage)Page).UserId == -1)
            {
                //Conacting the user
                FormsAuthentication.Authenticate(currUser.Name, currUser.Password);
                FormsAuthentication.RedirectFromLoginPage(currUser.Name, false);

                // Save temp menus of the new user
                if (((MasterPages_MBL)this.Master).TempUser != 0)
                {
                    User newUser = BusinessFacade.Instance.GetUserByUserName(currUser.Name);
                    MyBuyList.Shared.Entities.Menu[] userMenus = BusinessFacade.Instance.GetMenusList(int.Parse(ConfigurationManager.AppSettings["anonymous"]), ((MasterPages_MBL)this.Master).TempUser);
                    if (userMenus != null)
                    {
                        foreach (MyBuyList.Shared.Entities.Menu currMenu in userMenus)
                        {
                            BusinessFacade.Instance.UpdateMenuUser(currMenu.MenuId, newUser.UserId);
                        }
                    }
                }

                StringWriter html = new StringWriter();
                Server.Execute("~/WelcomePage.aspx", html);

                try
                {
                    ProperServices.Common.Mail.Mailer.SendMail(currUser.Email, ProperControls.General.Utils.FromEmail, "ברוך הבא לMyByList", html.ToString(), true);
                    if (string.IsNullOrEmpty(Request["ReturnUrl"]))
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                    else
                    {
                        Response.Redirect(Request["ReturnUrl"]);
                    }
                }
                catch
                {
                    this.lblResult.Text = "בעיה בכתובת הדואר האלקטרוני שהוכנסה";
                }
            }
            else
            {
                ((BasePage)Page).CurrUser = null;
            }

            AppEnv.MoveToDefaultPage();
        }
    }
        
        //if (BusinessFacade.Instance.GetUserByUserName(this.txtUserName.Text) != null)
        //{
        //    lblResult.Text = "שם משתמש קיים במערכת";
        //    this.hlSignIn.Visible = true;
        //}
        //else
        //{
        //    User currUser;
        //    //bool b = true;
        //    if (((BasePage)Page).UserId == -1)
        //    {
        //        currUser = new User();
        //        currUser.UserId = -1;
        //        currUser.Password = this.txtUserPassword.Text;
        //    }
        //    else
        //    {

        //        currUser = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId);

        //        currUser.UserId = ((BasePage)Page).UserId;
        //    }


        //    currUser.DisplayName = this.txtDisplayName.Text;
        //    currUser.Email = this.txtEmail.Text;
        //    currUser.FirstName = this.txtFirtstName.Text;
        //    currUser.LastName = this.txtLastName.Text;
        //    currUser.Name = this.txtUserName.Text;
        //    currUser.AgreeToMail = this.cbxEmail.Checked;
        //    currUser.UserTypeId = 2;

            //if (BusinessFacade.Instance.SaveUser(currUser))
            //{
            //    if (((BasePage)Page).UserId == -1)
            //    {
            //        //Conacting the user
            //        FormsAuthentication.Authenticate(currUser.Name, currUser.Password);
            //        FormsAuthentication.RedirectFromLoginPage(currUser.Name, false);

            //        // Save temp menus of the new user
            //        if (((MasterPages_MBLRightPaneOnly)this.Master).TempUser != 0)
            //        {
            //            User newUser = BusinessFacade.Instance.GetUserByUserName(currUser.Name);
            //            MyBuyList.Shared.Entities.Menu[] userMenus = BusinessFacade.Instance.GetMenusList(int.Parse(ConfigurationManager.AppSettings["anonymous"]), ((MasterPages_MBLRightPaneOnly)this.Master).TempUser);
            //            if (userMenus != null)
            //            {
            //                foreach (MyBuyList.Shared.Entities.Menu currMenu in userMenus)
            //                {
            //                    BusinessFacade.Instance.UpdateMenuUser(currMenu.MenuId, newUser.UserId);
            //                }
            //            }
            //        }


            //        StringWriter html = new StringWriter();
            //        Server.Execute("~/WelcomePage.aspx", html);

            //        try
            //        {
            //            ProperServices.Common.Mail.Mailer.SendMail(currUser.Email, ProperControls.General.Utils.FromEmail, "ברוך הבא לMyByList", html.ToString(), true);

            //            Response.Redirect("~/Users/PersonalArea.aspx");
            //        }
            //        catch
            //        {
            //            this.lblResult.Text = "בעיה בכתובת הדואר האלקטרוני שהוכנסה";
            //        }
            //    }

            //    AppEnv.MoveToDefaultPage();
            //}
            //else
            //{
            //    lblResult.Text = "שם משתמש קיים במערכת";
            //    this.hlSignIn.Visible = true;
            //}
    //    }
    //}

}
