using System;
using System.Configuration;
using System.Web;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using ProperControls.Pages;

public partial class MasterPages_MBL : System.Web.UI.MasterPage
{
    const int FOOTER_ARTICLE_ID = 6; 

    public bool IsFromHome
    {
        get { return ViewState["IsFromHome"] == null ? false : (bool)ViewState["IsFromHome"]; }
        set { ViewState["IsFromHome"] = value; }
    }

    public int TempUser
    {
        set
        {
            HttpContext.Current.Session["tempUser"] = value;
        }

        get
        {
            if (HttpContext.Current.Session["tempUser"] == null)
            {
                return 0;
            }
            else
            {
                return (int)HttpContext.Current.Session["tempUser"];
            }
        }
    }

    public SRL_User CurrUser
    {
        get
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            {
                HttpContext.Current.Session["CurrUser"] = null;
                return null;
            }
            else
            {
                if ((HttpContext.Current.Session != null) && (HttpContext.Current.Session["CurrUser"] == null))
                {

                    if (HttpContext.Current.User.Identity.IsAuthenticated == true)
                    {
                        User user = BusinessFacade.Instance.GetUserByUserName(HttpContext.Current.User.Identity.Name);

                        if (user != null)
                        {
                            HttpContext.Current.Session["CurrUser"] = new SRL_User(user);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            if (HttpContext.Current.Session != null)
            {
                return (SRL_User)HttpContext.Current.Session["CurrUser"];
            }
            else
            {
                return null;
            }

        }
        set
        {
            HttpContext.Current.Session["CurrUser"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["SearchFor"] != null && Session["SearchIn"] != null)
        //{
        //    HeaderControl1.SearchFor = Session["SearchFor"].ToString();
        //    HeaderControl1.SearchIn = Session["SearchIn"].ToString();
        //}
        
        //MyBuyList.Shared.Entities.Article footerBody = BusinessFacade.Instance.GetArticleById(FOOTER_ARTICLE_ID);
        //if (footerBody != null)
        //{
        //    this.footerHtml.InnerHtml = footerBody.Body;
        //}
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        RenderDisappearingMessage();
    }

    private string currentBannerUrl;

    public string CurrentBannerUrl
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

    #region Disappearing message
    /// <summary>
    /// Renders a message if one exists.
    /// </summary>
    private void RenderDisappearingMessage()
    {
        BasePage page = Page as BasePage;
        this.DisappearingMessage1.Displayed = false;
        if (page != null && page.DisplayMessage != null)
        {
            this.DisappearingMessage1.Displayed = true;
            this.DisappearingMessage1.Text = page.DisplayMessage;
        }
    }
    #endregion
}
