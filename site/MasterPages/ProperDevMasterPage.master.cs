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
using System.Web.Mail;
using MyBuyList.BusinessLayer;
using MyBuyList.BusinessLayer.Managers;
using MyBuyList.Shared.Entities;
using System.Reflection;
using ProperServices.Common.Extensions;
using ProperControls.Pages;
using MyBuyList.Shared;

public partial class ProperDevMasterPage : System.Web.UI.MasterPage
{
    
    public bool IsFromHome
    {
        get { return ViewState["IsFromHome"] == null ? false : (bool)ViewState["IsFromHome"]; }
        set { ViewState["IsFromHome"] = value; }
    }


    //public int TempUser
    //{
    //    set
    //    {
    //        Session["tempUser"] = value;
    //    }

    //    get
    //    {
    //        if (Session["tempUser"] == null)
    //        {
    //            return 0;
    //        }
    //        else
    //        {
    //            return (int)Session["tempUser"];
    //        }
    //    }
    //}

    //public SRL_User CurrUser
    //{
    //    get
    //    {
    //        if (Session[AppConstants.CURR_USER] != null)
    //            return (SRL_User)Session[AppConstants.CURR_USER];
    //        return null;
    //    }
    //    set
    //    {
    //        Session["CurrUser"] = value;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (CurrUser != null)
        //{
        //    Panel pnlConnected = (Panel)LoginView1.FindControl("pnlConnected");
        //    if (pnlConnected != null)
        //    {
        //        Label lblWelcome = (Label)pnlConnected.FindControl("lblWelcome");

        //        lblWelcome.Text = ("שלום " + this.CurrUser.Name).TrimToMax(20);

        //        if (this.CurrUser.UserType == 1)
        //        {
        //            HtmlTableRow rowAdmin = (HtmlTableRow)pnlConnected.FindControl("rowAdmin");
        //            rowAdmin.Visible = true;

        //        }
        //    }       
        //}

        if (!IsPostBack)
        {
            this.SetBasketImage();
        }

    }

    public void SetBasketImage()
    {

        if ((Request["menuId"] != null) && (Request.UrlReferrer != null) && (Request.UrlReferrer.AbsoluteUri.ToLower().Contains("default.aspx")))
        {
            if (Request.Url.AbsoluteUri.ToLower().Contains("menurecipes.aspx"))
            {
                this.basketImg1.Visible = true;
                this.basketImg2.Visible = true;
                this.basketImg3.Visible = true;
                this.basketImg4.Visible = true;
                this.ImageArrow.Visible = true;

                this.Flyout1.Visible = true;
                this.Flyout2.Visible = true;
                this.Flyout3.Visible = true;
                this.Flyout4.Visible = true;

                this.basket2.Style["background-image"] = string.Format("url('{0}')", ResolveClientUrl("~/Images/New/SelectedBasketBG.gif"));
                this.IsFromHome = true;
            }
        }
        else if ((Request["menuId"] != null) && (Request["isfromhome"] == "1"))
        {
            this.basketImg1.Visible = true;
            this.basketImg2.Visible = true;
            this.basketImg3.Visible = true;
            this.basketImg4.Visible = true;
            this.ImageArrow.Visible = true;

            this.Flyout1.Visible = true;
            this.Flyout2.Visible = true;
            this.Flyout3.Visible = true;
            this.Flyout4.Visible = true;

            this.IsFromHome = true;

            if (Request.Url.AbsoluteUri.ToLower().Contains("menurecipes.aspx"))
            {
                this.basket2.Style["background-image"] = string.Format("url('{0}')", ResolveClientUrl("~/Images/New/SelectedBasketBG.gif"));
            }
            else if (Request.Url.AbsoluteUri.ToLower().Contains("menumeals.aspx"))
            {
                this.basket3.Style["background-image"] = string.Format("url('{0}')", ResolveClientUrl("~/Images/New/SelectedBasketBG.gif"));
            }
            else if (Request.Url.AbsoluteUri.ToLower().Contains("shoppinglist.aspx"))
            {
                this.basket4.Style["background-image"] = string.Format("url('{0}')", ResolveClientUrl("~/Images/New/SelectedBasketBG.gif"));
            }
        }
        else
        {
            if (this.IsFromHome)
            {
                this.IsFromHome = false;
            }



            if (Request.Url.AbsoluteUri.ToLower().Contains("default.aspx"))
            {
                this.basketImg1.Visible = true;
                this.basketImg2.Visible = true;
                this.basketImg3.Visible = true;
                this.basketImg4.Visible = true;
                this.ImageArrow.Visible = true;

                this.Flyout1.Visible = true;
                this.Flyout2.Visible = true;
                this.Flyout3.Visible = true;
                this.Flyout4.Visible = true;

                this.lblChoose.Visible = true;

                if (this.basketImg1.Enabled)
                {
                    this.basketImg1.Enabled = false;
                    this.basketImg2.Enabled = false;
                    this.basketImg3.Enabled = false;
                    this.basketImg4.Enabled = false;
                    this.ImageArrow.Enabled = false;


                }
            }
            else
            {
                this.Flyout1.Visible = false;
                this.Flyout2.Visible = false;
                this.Flyout3.Visible = false;
                this.Flyout4.Visible = false;
            }

        }
    }

    public void SetLeftBackgroundImage(int menuTypeId)
    {
        string urlTop = null;
        string urlMain = null;
        string urlBottom = null;
        string urlTopNote = null;

        if (menuTypeId == (int)MenuTypeEnum.OneMeal)
        {
            urlTop = "~/Images/New/NotepadLeftSingleAreaTop.gif";
            urlMain = "~/Images/New/NotepadLeftSingleAreaBG.gif";
            urlBottom = "~/Images/New/NotepadLeftSingleAreaBottom.gif";
            urlTopNote = "~/Images/New/MealMenuNoteSelected.gif";
        }
        else if (menuTypeId == (int)MenuTypeEnum.Weekly)
        {
            urlTop = "~/Images/New/NotepadLeftWeeklyAreaTop.gif";
            urlMain = "~/Images/New/NotepadLeftWeeklyAreaBG.gif";
            urlBottom = "~/Images/New/NotepadLeftWeeklyAreaBottom.gif";
            urlTopNote = "~/Images/New/WeeklyNoteSelected.gif";
        }
        else if (menuTypeId == (int)MenuTypeEnum.ManyWeeks)
        {
            urlTop = "~/Images/New/NotepadLeftMonthlyAreaTop.gif";
            urlMain = "~/Images/New/NotepadLeftMonthlyAreaBG.gif";
            urlBottom = "~/Images/New/NotepadLeftMonthlyAreaBottom.gif";
            urlTopNote = "~/Images/New/MonthlyNoteSelected.gif";
        }
        else if (menuTypeId == (int)MenuTypeEnum.QuickMenu)
        {
            urlTop = "~/Images/New/NotepadLeftQuickListAreaTop.gif";
            urlMain = "~/Images/New/NotepadLeftQuickListAreaBG.gif";
            urlBottom = "~/Images/New/NotepadLeftQuickListAreaBottom.gif";
            urlTopNote = "~/Images/New/QuickListSelected.gif";
        }
        else
        {
            urlTop = "~/Images/New/NotepadLeftMyListAreaTop.gif";
            urlMain = "~/Images/New/NotepadLeftMyListAreaBG.gif";
            urlBottom = "~/Images/New/NotepadLeftMyListAreaBottom.gif";
            urlTopNote = "~/Images/MyLists.gif";
        }

        //if (urlMain != null)
        //{
        //    this.tdLeftContentTop.Style["background-image"] = string.Format("url('{0}')", ResolveClientUrl(urlTop));
        //    this.tdLeftContent.Style["background-image"] = string.Format("url('{0}')", ResolveClientUrl(urlMain));
        //    this.tdLeftContentBootom.Style["background-image"] = string.Format("url('{0}')", ResolveClientUrl(urlBottom));
        //    this.leftTopNote.Style["background-image"] = string.Format("url('{0}')", ResolveClientUrl(urlTopNote));
        //}
    }

    protected void Login1_LoggedIn(object sender, EventArgs e)
    {/*
        Panel pnlLoginbox = (Panel)LoginView1.FindControl("pnlLoginbox");
        Login MasterLogin = (Login)pnlLoginbox.FindControl("MasterLogin");
        CheckBox RememberMe = (CheckBox)MasterLogin.FindControl("RememberMe");
        FormsAuthentication.RedirectFromLoginPage(MasterLogin.UserName, RememberMe.Checked);

        if (Request.Url.AbsoluteUri.Contains("default.aspx"))
        {
            Response.Redirect("~/Users/PersonalArea.aspx");
        }
        else if (!string.IsNullOrEmpty(Request["MenuId"]))
        {
            int menuId = int.Parse(this.Request["MenuId"]);
            //  User currUser = BusinessFacade.Instance.GetUserByUserName(MasterLogin.Name);
            //            BusinessFacade.Instance.UpdateMenuUser(menuId, currUser.UserId);
        }
      * */
    }

    public void ChangeTopNotePos()
    {
        //this.leftTopNote.Style["top"] = "-973px";
    }

    protected void MasterLoginStatus_LoggedOut(object sender, EventArgs e)
    {
        //FormsAuthentication.SignOut();
        //FormsAuthentication.RedirectToLoginPage("~/Default.aspx");
    }

    private string currentBannerUrl;

    protected string CurrentBannerUrl
    {
        get
        {
            if (string.IsNullOrEmpty(this.currentBannerUrl))
            {
                Random r = new Random();

                int bannerNumber = r.Next(int.Parse(ConfigurationManager.AppSettings["MinBannerNumber"]), int.Parse(ConfigurationManager.AppSettings["MaxBannerNumber"]) + 1);

                this.currentBannerUrl = ResolveClientUrl(string.Format("~/banners/{0}.swf", bannerNumber));
            }

            return this.currentBannerUrl;
        }
    }
    protected void basketImg1_Click(object sender, ImageClickEventArgs e)
    {
        AppEnv.MoveToDefaultPage();
    }

    protected void basketImg2_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(this.Request["menuId"]))
        {
            Response.Redirect("~/MenuRecipes.aspx?menuId=" + Request["menuId"]);
        }
    }

    protected void basketImg3_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(this.Request["menuId"]))
        {
            Response.Redirect("~/MenuMeals.aspx?menuId=" + Request["menuId"]);
        }
    }

    protected void basketImg4_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(this.Request["menuId"]))
        {
            Response.Redirect("~/ShoppingList.aspx?menuId=" + Request["menuId"]);
        }
    }
}
