using System;
using System.Web;
using System.Web.UI;
using ProperControls.General;
using ProperControls.Pages.Compression;
using ProperControls.Pages.Persistence;
using MyBuyList.Shared.Entities;
using MyBuyList.Shared;

public class BasePage : Page
{
    public BasePage()
    {
        EntityState = new DataContractStateBag(this);
    }

    #region Load
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        ProperLoad();
    }

    protected virtual void ProperLoad() { }
    #endregion

    #region Localization
    /// <summary>
    /// Gets the localized resource string.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected string GetLocalizedString(string key)
    {
        return (string)GetLocalResourceObject(key);
    }
    #endregion

    #region Page direction (RTL/LTR)
    /// <summary>
    /// Returns the direction for this page in the format of "dir='LTR'" or "dir='RTL'".
    /// </summary>
    protected string Direction { get { return Utils.Direction; } }

    /// <summary>
    /// Returns whether the page is RTL or LTR.
    /// </summary>
    protected bool IsLTR { get { return Utils.IsLTR; } }
    #endregion

    #region Cache
    protected override void OnLoadComplete(EventArgs e)
    {
        SetPageCache();

        base.OnLoadComplete(e);
    }

    /// <summary>
    /// Each inheritted can override and provide cache settings other than this, or none at all.
    /// </summary>
    protected virtual void SetPageCache()
    {
        if (ProperConfig.DefaultCacheSeconds == 0)
        {
            // expire immediately
            ExpireImmediately();
        }
        else
        {
            // number of seconds for caching the page
            this.Response.Cache.SetExpires(DateTime.Now.AddSeconds(ProperConfig.DefaultCacheSeconds));

            // public caching
            this.Response.Cache.SetCacheability(HttpCacheability.Public);

            // ignore client invalidation cache requests
            this.Response.Cache.SetValidUntilExpires(true);

            // cache according to language (i.e. the page will have different versions cached according to browser language
            this.Response.Cache.VaryByHeaders["Accept-Language"] = true;
        }
    }

    /// <summary>
    /// Expires the page immediately.
    /// </summary>
    protected void ExpireImmediately()
    {
        this.Response.Cache.SetExpires(DateTime.MinValue);
        this.Response.Cache.SetNoStore();
        this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.Response.Cache.SetValidUntilExpires(false);
    }
    #endregion

    #region Page persistance
    public enum PagePersisterSettings
    {
        HiddenField,
        Session,
        GZip,
        SharpZip
    }

    /// <summary>
    /// Current persister instance.
    /// </summary>
    private PageStatePersister persister = null;

    /// <summary>
    /// Indicates whether or not to store the viewstate in the session.
    /// </summary>
    protected virtual PagePersisterSettings PagePersisterSelection { get { return PagePersisterSettings.HiddenField; } }

    /// <summary>
    /// Gets the persister for this page.
    /// </summary>
    protected override PageStatePersister PageStatePersister
    {
        get
        {
            // return the existing persister
            if (this.persister != null)
                return this.persister;

            switch (PagePersisterSelection)
            {
                case PagePersisterSettings.GZip:
                    // use gzip persister
                    this.persister = new GZipPersister(this);
                    break;

                case PagePersisterSettings.Session:
                    // force browser to require the control state to be stored in the session.
                    if (!Request.Browser.Capabilities.Contains("requiresControlStateInSession"))
                        Request.Browser.Capabilities.Add("requiresControlStateInSession", bool.TrueString);

                    // use session persister
                    this.persister = new SessionPageStatePersister(this);
                    break;

                //case PagePersisterSettings.SharpZip:
                //    this.persister = new SharpZipPersister(this);
                //    break;


                default:
                    // default hidden field persister
                    this.persister = base.PageStatePersister;
                    break;
            }

            return this.persister;
        }
    }

    public DataContractStateBag EntityState { get; private set; }

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
    public int UserId
    {
        get
        {
            if (CurrUser == null)
            {
                return -1;
            }
            else
            {
                return CurrUser.UserId;
            }
        }
    }

    public int UserType
    {
        get
        {
            if (CurrUser == null)
            {
                return 2;
            }
            else
            {
                return CurrUser.UserTypeId;
            }
        }
    }

    public User CurrUser
    {
        get
        {

            if (Session == null)
                return null;


            if (Session[AppConstants.CURR_USER] == null)
            {
                return null;
            }

            return (User)Session[AppConstants.CURR_USER];

        }
        set
        {
            Session[AppConstants.CURR_USER] = value;
        }
    }


    #endregion

    #region Display disappearing message
    /// <summary>
    /// Flag which indicates whether or not a "disappearing message" should be displayed.
    /// </summary>
    public string DisplayMessage
    {
        get { return HttpContext.Current.Items["DisplayMessage"] as string; }
        set { HttpContext.Current.Items["DisplayMessage"] = value; }
    }

    public void PopupMessage(string message)
    {
        string script = string.Format("alert({0})", message);
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), string.Empty, script, true);
    }

    #endregion
}
