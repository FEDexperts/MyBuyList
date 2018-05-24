using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using ProperControls.Pages;
using ProperControls.General;
using ProperServices.Common.Extensions;

using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using MyBuyList.Shared.Enums;
using MyBuyList.Shared;

public partial class RecipesNew : BasePage
{

    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    if (this.CategoryId.HasValue)
    //    {
    //        int count = this.RebindCategories(this.CategoryId);
    //        this.categories.Visible = true;
    //        if (count == 0)
    //            this.pnlCategories.Visible = false;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.Display == RecipeDisplayEnum.ByCategory)
            {
                int count = this.RebindCategories(this.CategoryId);
                this.categories.Visible = true;
                if (count == 0)
                    this.pnlCategories.Visible = false;
            }
            //if (this.Display == RecipeDisplayEnum.BySearchSimple)
            //{
            //    this.simpleSearch.Visible = true;
            //    this.btnSearch.Visible = true;
            //    this.txtSearchTerm.Text = (!string.IsNullOrEmpty(Request["term"])) ? Request["term"] : "" ;
            //    this.btnSearch.CommandArgument = "BySearchSimple";
            //}
            if (this.Display == RecipeDisplayEnum.BySearchAdvanced)
            {
                //this.simpleSearch.Visible = true;
                this.advancedSearch.Visible = true;
                //this.btnSearch.Visible = true;
                //this.txtSearchTerm.Text = (!string.IsNullOrEmpty(Request["term"])) ? Request["term"] : "" ;
                this.txtServings.Text = (!string.IsNullOrEmpty(Request["serv"])) ? Request["serv"] : "";
                this.txtCategory.Text = "";
                //this.btnSearch.CommandArgument = "BySearchAdvanced";
            }

            EmphasizeCurrentSearch(this.Display);

            MyBuyList.Shared.Entities.User currentUser = BusinessFacade.Instance.GetUser(((BasePage)this.Page).UserId);
            string email = (currentUser != null) ? currentUser.Email : string.Empty;
            this.ucSendMailToFriend.BindItemDetails("Recipe", 0, string.Empty, email);

            RebindRecipes();

            // 13.10.2010
            // Load categories at page load
            Category[] categories = BusinessFacade.Instance.GetRecipesCategoriesList(((BasePage)this.Page).UserId);
            var list = from cat in categories.Where(cat => cat.ParentCategoryId == null).ToArray()
                       select new SRL_Category(cat.CategoryId, cat.CategoryName, cat.ParentCategoryId, cat.RecipesCount);

            SRL_Category[] categoryList = list.ToArray();
            ReciepesSearchControl1.FillList(categoryList, this.RecipeCategoryChangeBaseUrl);
        }
    }
    

    public int CurrentPage
    {
        get
        {
            int page;

            if (!string.IsNullOrEmpty(Request["page"]) && int.TryParse(Request["page"], out page))
            {
                return page;
            }
            else
            {
                return 1;
            }
        }
    }

    public RecipeOrderEnum OrderBy
    {
        get
        {
            RecipeOrderEnum orderBy;

            if (!string.IsNullOrEmpty(Request["orderBy"]))
            {
                try
                {
                    orderBy = (RecipeOrderEnum)Enum.Parse(typeof(RecipeOrderEnum), Request["orderBy"], true);
                    return orderBy;
                }
                catch
                {
                    return RecipeOrderEnum.LastUpdate;
                }
            }
            else
            {
                return RecipeOrderEnum.LastUpdate;
            }
        }
    }

    public RecipeDisplayEnum Display
    {
        get
        {
            RecipeDisplayEnum display;

            if (!string.IsNullOrEmpty(Request["disp"]))
            {
                try
                {
                    display = (RecipeDisplayEnum)Enum.Parse(typeof(RecipeDisplayEnum), Request["disp"], true);
                    return display;
                }
                catch
                {
                    return RecipeDisplayEnum.All;
                }
            }
            else
            {
                return RecipeDisplayEnum.All;
            }
        }
    }

    public string FreeText
    {
        get { return (!string.IsNullOrEmpty(Request["term"]) ? Request["term"] : null); }
    }

    public int? CategoryId
    {
        get
        {
            int catId = 0;

            if (!string.IsNullOrEmpty(Request["cat"]) && int.TryParse(Request["cat"], out catId))
            {
                return catId;
            }
            else
            {
                return null;
            }
        }
    }

    public int? Servings
    {
        get
        {
            int servings = 0;

            if (!string.IsNullOrEmpty(Request["serv"]) && int.TryParse(Request["serv"], out servings))
            {
                return servings;
            }
            else
            {
                return null;
            }
        }
    }

    public int[] RecipeCategories
    {
        get
        {
            if (!string.IsNullOrEmpty(Request["categories"]))
            {
                return this.GetCategoryIdsFromString(Request["categories"]);
            }
            else
            {
                return null;
            }
        }
    }

    public int PageSize { get { return 12; } }
    
    public int totalPages;

    protected string RecipeCategoryChangeBaseUrl
    {
        get
        {
            EnsureRecipeCategoryChangeBaseUrlExists();

            return this.recipeCategoryChangeBaseUrl;
        }
    }

    protected string OrderByLastUpdateUrl
    {
        get
        {
            EnsureUrlsForSortingAreGenerated();
            return orderByLastUpdateUrl;
        }
    }

    protected string OrderByNameUrl
    {
        get
        {
            EnsureUrlsForSortingAreGenerated();
            return orderByNameUrl;
        }
    }

    protected string OrderByPublisherUrl
    {
        get
        {
            EnsureUrlsForSortingAreGenerated();
            return orderByPublisherUrl;
        }
    }


    bool urlsGenerated = false;

    private string orderByLastUpdateUrl;
    private string orderByNameUrl;
    private string orderByPublisherUrl;

    private string recipeCategoryChangeBaseUrl;
    

    private void RebindRecipes()
    {
        int count;
        IEnumerable<RecipeView> recipes = from r in BusinessFacade.Instance.GetRecipesEx(this.Display, ((BasePage)this.Page).UserId, this.FreeText, this.CategoryId, this.Servings, this.RecipeCategories, this.OrderBy, this.CurrentPage, this.PageSize, out totalPages, out count)
                                          select new RecipeView()
                                          {
                                              RecipeId = r.RecipeId,
                                              RecipeTitle = this.AdjustLength(r.RecipeName, 45),
                                              MainCategoryName = this.AdjustLength(r.RecipeCategories[0].Category.CategoryName, 9),
                                              RecipeTags = (string.IsNullOrEmpty(r.Tags)) ? "&nbsp;" : r.Tags.TrimToMax(100),
                                              RecipeDescription = (string.IsNullOrEmpty(r.Description)) ? "" : r.Description.TrimToMax(200),
                                              PublisherId = r.User.UserId,
                                              PublisherName = r.User.DisplayName,
                                              PublishDate = r.ModifiedDate.ToString("dd/MM/yyyy"),
                                              RecipeThumbnail = ResolveUrl(r.Picture != null ? string.Format("~/ShowPicture.ashx?RecipeId={0}", r.RecipeId) : "~/Images/Img_Default_small.jpg"),
                                              NumMenusInclude = r.MenuRecipes.Count,
                                              NumUsersFavorite = r.UserFavoriteRecipes.Count,
                                              InMyFavorites = (r.UserFavoriteRecipes.SingleOrDefault(ufr => ufr.UserId == ((BasePage)this.Page).UserId) != null)
                                          };

        rptRecipes.DataSource = recipes;
        rptRecipes.ItemCreated += new RepeaterItemEventHandler(rptRecipes_ItemCreated);// for the pagers
        rptRecipes.ItemDataBound += new RepeaterItemEventHandler(rptRecipes_ItemDataBound);
        rptRecipes.DataBind();
        lblNumRecipes.Text = string.Format("נמצאו {0} מתכונים", count);
    }

    void rptRecipes_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        // init pagers
        if (e.Item.ItemType == ListItemType.Header || e.Item.ItemType == ListItemType.Footer)
        {
            PlaceHolder phPager = (PlaceHolder)e.Item.FindControl("phPager");

            phPager.Controls.Clear();

            bool isEnd = false, isStart = false;

            int startPage = 20 * ((this.CurrentPage - 1) / 20) + 1;
            int endpage = 20 * ((this.CurrentPage - 1) / 20) + 20;

            if (startPage == 1)
            {
                isStart = true;
            }
            if (endpage >= this.totalPages)
            {
                isEnd = true;
                endpage = this.totalPages;
            }

            if (!isStart)
            {
                HyperLink lnkArrow = new HyperLink();
                lnkArrow.Text = "&lt;&lt;";
                lnkArrow.NavigateUrl = string.Format("~/RecipesNew.aspx?page={0}&orderby={1}{2}{3}{4}{5}{6}", (startPage - 20).ToString(), this.OrderBy.ToString(), (!string.IsNullOrEmpty(Request["disp"])) ? string.Format("&disp={0}", Request["disp"]) : "", (!string.IsNullOrEmpty(Request["term"])) ? string.Format("&term={0}", Request["term"]) : "", (!string.IsNullOrEmpty(Request["serv"])) ? string.Format("&serv={0}", Request["serv"]) : "", (!string.IsNullOrEmpty(Request["categories"])) ? string.Format("&categories={0}", Request["categories"]) : "", (!string.IsNullOrEmpty(Request["cat"])) ? string.Format("&cat={0}", Request["cat"]) : "");
                phPager.Controls.Add(lnkArrow);
                phPager.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
            }

            for (int page = startPage; page <= endpage; page++)
            {
                if (page == this.CurrentPage)
                {
                    Label lblPage = new Label();
                    lblPage.Text = page.ToString();
                    phPager.Controls.Add(lblPage);
                }
                else
                {
                    HyperLink lnkPage = new HyperLink();
                    lnkPage.NavigateUrl = string.Format("~/RecipesNew.aspx?page={0}&orderby={1}{2}{3}{4}{5}{6}", page, this.OrderBy.ToString(), (!string.IsNullOrEmpty(Request["disp"])) ? string.Format("&disp={0}", Request["disp"]) : "", (!string.IsNullOrEmpty(Request["term"])) ? string.Format("&term={0}", Request["term"]) : "", (!string.IsNullOrEmpty(Request["serv"])) ? string.Format("&serv={0}", Request["serv"]) : "", (!string.IsNullOrEmpty(Request["categories"])) ? string.Format("&categories={0}", Request["categories"]) : "", (!string.IsNullOrEmpty(Request["cat"])) ? string.Format("&cat={0}", Request["cat"]) : "");
                    lnkPage.Text = page.ToString();
                    phPager.Controls.Add(lnkPage);
                }

                phPager.Controls.Add(new LiteralControl("&nbsp;"));
            }

            if (!isEnd)
            {
                HyperLink lnkArrow = new HyperLink();
                lnkArrow.Text = "&gt;&gt;";
                lnkArrow.NavigateUrl = string.Format("~/RecipesNew.aspx?page={0}&orderby={1}{2}{3}{4}{5}{6}", (startPage + 20).ToString(), this.OrderBy.ToString(), (!string.IsNullOrEmpty(Request["disp"])) ? string.Format("&disp={0}", Request["disp"]) : "", (!string.IsNullOrEmpty(Request["term"])) ? string.Format("&term={0}", Request["term"]) : "", (!string.IsNullOrEmpty(Request["serv"])) ? string.Format("&serv={0}", Request["serv"]) : "", (!string.IsNullOrEmpty(Request["categories"])) ? string.Format("&categories={0}", Request["categories"]) : "", (!string.IsNullOrEmpty(Request["cat"])) ? string.Format("&cat={0}", Request["cat"]) : "");
                phPager.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
                phPager.Controls.Add(lnkArrow);
            }

            //for (int page = 1; page <= this.totalPages; page++)
            //{
            //    if (page == this.CurrentPage)
            //    {
            //        Label lblPage = new Label();
            //        lblPage.Text = page.ToString();
            //        phPager.Controls.Add(lblPage);
            //    }
            //    else
            //    {
            //        HyperLink lnkPage = new HyperLink();
            //        lnkPage.NavigateUrl = string.Format("~/Recipes.aspx?page={0}&orderby={1}", page, this.OrderBy.ToString());
            //        lnkPage.Text = page.ToString();
            //        phPager.Controls.Add(lnkPage);
            //    }

            //    if (page < this.totalPages)
            //    {
            //        phPager.Controls.Add(new LiteralControl("&nbsp;"));
            //    }
            //}
        }
    }

    void rptRecipes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink lnkRecipe = (HyperLink)e.Item.FindControl("lnkRecipe");
            lnkRecipe.NavigateUrl = ResolveUrl(string.Format("~/RecipeDetails.aspx?RecipeId={0}", ((RecipeView)e.Item.DataItem).RecipeId));

            LinkButton linkBtn = e.Item.FindControl("blkAddRemove") as LinkButton;
            if (linkBtn != null)
            {
                linkBtn.Attributes["recipeID"] = ((RecipeView)e.Item.DataItem).RecipeId.ToString();
            }            

            Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;
            if (selectedRecipes.ContainsKey(((RecipeView)e.Item.DataItem).RecipeId))
            {
                linkBtn.Text = "הסר מרשימת קניות";
                linkBtn.Style["color"] = "Red";
            }
            else
            {
                //linkBtn.Text = "הוסף לרשימת קניות";
                //linkBtn.Style["color"] = "#656565";
            }

            linkBtn = e.Item.FindControl("btnSendToFriend") as LinkButton;
            if (linkBtn != null)
            {
                linkBtn.Attributes["recipeId"] = ((RecipeView)e.Item.DataItem).RecipeId.ToString();
            }
            
            HtmlGenericControl divMyFavoritesInfoTag = e.Item.FindControl("myFavoritesInfoTag") as HtmlGenericControl;

            if (((RecipeView)e.Item.DataItem).InMyFavorites)
            {
                divMyFavoritesInfoTag.Visible = true;
            }
            else
            {
                divMyFavoritesInfoTag.Visible = false;
            }

            Label lblAllFavorites = e.Item.FindControl("lblAllFavorites") as Label;
            Label lblAllMenus = e.Item.FindControl("lblAllMenus") as Label;

            lblAllFavorites.Text = ((RecipeView)e.Item.DataItem).NumUsersFavorite.ToString();
            lblAllMenus.Text = ((RecipeView)e.Item.DataItem).NumMenusInclude.ToString();
        }
    }

    protected void blkAddRemove_click(object sender, EventArgs e)
    {
        LinkButton linkBtn = sender as LinkButton;
        int recipeId;
        string preStr = "";

        if (int.TryParse(linkBtn.Attributes["recipeID"], out recipeId))
        {
            Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeId);
            Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;
            if (selectedRecipes.Keys.Contains(recipe.RecipeId))
            {
                selectedRecipes.Remove(recipeId);
                Utils.SelectedRecipes = selectedRecipes;
                linkBtn.Text = "הוסף לרשימת קניות";
                linkBtn.Style["color"] = "#656565";
                preStr = "מרשימת הקניות / תפריט הוסר המתכון:";
            }
            else
            {
                selectedRecipes.Add(recipeId, recipe);
                Utils.SelectedRecipes = selectedRecipes;
                linkBtn.Text = "הסר מרשימת קניות";
                linkBtn.Style["color"] = "Red";
                preStr = "לרשימת הקניות / תפריט התווסף המתכון:";
            }

            this.RebindRecipes();

            this.upRecipes.Update();

            UserControl uc = (((this.Master).Master).Master).FindControl("HeaderControl1") as UserControl;
            if (uc != null)
            {
                Label lbl = uc.FindControl("lblSelectedRecipesNum") as Label;
                if (lbl != null)
                {
                    lbl.Text = "(" + Utils.SelectedRecipes.Count.ToString() + ")";
                }

                UpdatePanel up = uc.FindControl("upSelectedRecipesList") as UpdatePanel;
                if (up != null)
                {
                    up.Update();
                }
            }

            ((BasePage)this.Page).DisplayMessage = preStr + "<br/>" + recipe.RecipeName;
        }        
    }

    //protected void btnSendToFriend_Click(object sender, EventArgs e)
    //{
    //    LinkButton linkBtn = sender as LinkButton;
    //    int recipeId;

    //    if (linkBtn != null && int.TryParse(linkBtn.Attributes["recipeID"], out recipeId))
    //    {
    //        Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeId);

    //        string userEmail = string.Empty;

    //        MyBuyList.Shared.Entities.User currentUser = BusinessFacade.Instance.GetUser(((BasePage)this.Page).UserId);

    //        if (currentUser != null)
    //        {
    //            userEmail = currentUser.Email;
    //        }

    //        this.ucSendMailToFriend.BindItemDetails("Recipe", recipe.RecipeId, recipe.RecipeName, userEmail);

    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "popupSendToFriend", "showSendMailToFriendBox()", true);
    //    }        
    //}

    protected void ucSendMailToFriend_EmailSent(object sender, SendToFriendEventArgs e)
    {
        ((BasePage)this.Page).DisplayMessage = "אימייל נשלח ל- " + e.recipentName;
    }

    private void BindInfoTags(RepeaterItem item)
    {
        RecipeView recipeView = item.DataItem as RecipeView;

        Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeView.RecipeId);

        HtmlGenericControl divMyFavoritesInfoTag = item.FindControl("myFavoritesInfoTag") as HtmlGenericControl;
        Label lblAllFavorites = item.FindControl("lblAllFavorites") as Label;
        Label lblAllMenus = item.FindControl("lblAllMenus") as Label;

        if (recipe != null)
        {
            Recipe[] ufRecipes = BusinessFacade.Instance.GetUserFavoritesRecipes(((BasePage)Page).UserId);
            Recipe inMyFavorites = ufRecipes.SingleOrDefault(fr => fr.RecipeId == recipe.RecipeId);
            if (inMyFavorites == null)
            {
                divMyFavoritesInfoTag.Visible = false;
            }
            else
            {
                divMyFavoritesInfoTag.Visible = true;
            }
        }

        int? allUsersFavorite = BusinessFacade.Instance.GetRecipeUserFavoritesCount(recipe.RecipeId);
        if (allUsersFavorite != null)
        {
            lblAllFavorites.Text = allUsersFavorite.ToString();
        }

        int? allMenus = BusinessFacade.Instance.GetRecipeMenusCount(recipe.RecipeId);
        if (allMenus != null)
        {
            lblAllMenus.Text = allMenus.ToString();
        }
    }

    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        //this.btnSearch.CommandArgument = "BySearchSimple";

        //this.simpleSearch.Visible = true;
        //this.btnSearch.Visible = true;
        this.advancedSearch.Visible = false;
        this.categories.Visible = false;
        EmphasizeCurrentSearch(RecipeDisplayEnum.BySearchSimple);
        this.upSearch.Update();
    }

    protected void lnkAdvancedSearch_Click(object sender, EventArgs e)
    {
        //this.btnSearch.CommandArgument = "BySearchAdvanced";

        //this.simpleSearch.Visible = true;
        //this.btnSearch.Visible = true;
        this.advancedSearch.Visible = true;
        this.categories.Visible = false;
        EmphasizeCurrentSearch(RecipeDisplayEnum.BySearchAdvanced);
        this.upSearch.Update();
    }

    protected void btnSearch_Command(object sender, CommandEventArgs e)
    {
        string categoryIDs = "";

        if (e.CommandArgument.ToString() == "BySearchAdvanced")
        {
            categoryIDs = this.hdnCategorieIDs.Value;
        }

        //Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}{1}{2}{3}", e.CommandArgument.ToString(), (!string.IsNullOrEmpty(this.txtSearchTerm.Text)) ? string.Format("&term={0}", this.txtSearchTerm.Text) : "", (!string.IsNullOrEmpty(this.txtServings.Text)) ? string.Format("&serv={0}", this.txtServings.Text) : "", (!string.IsNullOrEmpty(categoryIDs)) ? string.Format("&categories={0}", categoryIDs) : ""));
    }

    protected void lnkAllRecipes_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Recipes.aspx");
    }

    protected void lnkMyRecipes_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}", RecipeDisplayEnum.MyRecipes.ToString()));
    }

    protected void lnkFavRecipes_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}", RecipeDisplayEnum.MyFavoriteRecipes.ToString()));
    }

    protected void lnkCategories_Click(object sender, EventArgs e)
    {
        this.RebindCategories(null);
        this.pnlCategories.Visible = true;

        //this.simpleSearch.Visible = false;
        //this.btnSearch.Visible = false;
        this.advancedSearch.Visible = false;
        this.categories.Visible = true;
        EmphasizeCurrentSearch(RecipeDisplayEnum.ByCategory);
        this.upSearch.Update();
    }

    private int RebindCategories(int? categoryId)
    {
        int count = 0;
        Category[] categories = BusinessFacade.Instance.GetRecipesCategoriesList(((BasePage)this.Page).UserId);
        var list = from cat in categories.Where(cat => cat.ParentCategoryId == categoryId).ToArray()
                   select new SRL_Category(cat.CategoryId, cat.CategoryName, cat.ParentCategoryId, cat.RecipesCount);
        this.rptCategories.DataSource = list.ToArray();
        this.rptCategories.DataBind();
        count = list.ToArray().Length;

        List<Category> pathList = new List<Category>();
        if (categoryId != null)
        {
            Category tmpCategory = categories.SingleOrDefault(ct => ct.CategoryId == categoryId.Value);

            do
            {
                pathList.Add(tmpCategory);
                if (tmpCategory.ParentCategoryId == null)
                    break;
                tmpCategory = tmpCategory.ParentCategory;
            }
            while (tmpCategory != null);
        }

        //LinkButton primaryCatLink = new LinkButton();
        //primaryCatLink.Text = "הכל";
        //primaryCatLink.Style["background-color"] = "#A4CB3A";
        //primaryCatLink.Click += new EventHandler(primaryCatLink_Click);

        HyperLink primaryCatLink = new HyperLink();
        primaryCatLink.Text = "הכל";
        primaryCatLink.Style["background-color"] = "#A4CB3A";
        primaryCatLink.NavigateUrl = string.Format("~/Recipes.aspx?disp={0}{1}", RecipeDisplayEnum.ByCategory.ToString(), (Request["orderBy"] != null)?string.Format("&orderBy={0}", Request["orderBy"]):"");
        this.pathLinks.Controls.Clear();
        this.pathLinks.Controls.Add(primaryCatLink);

        pathList.Reverse();
        foreach (Category category in pathList)
        {
            Label arrows = new Label();
            arrows.Text = " >> ";
            arrows.Style["background-color"] = "#A4CB3A";
            this.pathLinks.Controls.Add(arrows);

            HyperLink link = new HyperLink();
            link.Text = category.CategoryName;
            link.Style["background-color"] = "#A4CB3A";
            link.NavigateUrl = string.Format(this.RecipeCategoryChangeBaseUrl, category.CategoryId);
            //link.Command += new CommandEventHandler(link_Command);
            this.pathLinks.Controls.Add(link);
        }

        return count;
    }

    protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        SRL_Category category = e.Item.DataItem as SRL_Category;
        if (category != null)
        {
            //LinkButton btn = e.Item.FindControl("btnCategory") as LinkButton;
            //btn.Text += " (" + category.RecipesCount + ")";
            HyperLink lnk = (HyperLink)e.Item.FindControl("lnkCategory");
            lnk.Text = string.Format("{0} ({1})", category.CategoryName, category.RecipesCount);
            lnk.NavigateUrl = string.Format(this.RecipeCategoryChangeBaseUrl, category.CategoryId);
        }
    }    

    private void EnsureRecipeCategoryChangeBaseUrlExists()
    {
        if (!string.IsNullOrEmpty(this.recipeCategoryChangeBaseUrl))
        {
            return;
        }
        
        string currentUrl = Request.Url.OriginalString;

        if (currentUrl.ToLower().Contains("page"))
        {
            this.recipeCategoryChangeBaseUrl = currentUrl.ToLower().Replace(Request["page"].ToLower(), "1");
            currentUrl = this.recipeCategoryChangeBaseUrl;
        }

        if (currentUrl.ToLower().Contains("disp"))
        {
            if (!currentUrl.ToLower().Contains("disp=&")) 
            {
                this.recipeCategoryChangeBaseUrl = currentUrl.ToLower().Replace(Request["disp"].ToLower(), RecipeDisplayEnum.ByCategory.ToString());
                currentUrl = this.recipeCategoryChangeBaseUrl;
            }
            else  //should not happen - make sure and remove          
            {
                int index = currentUrl.ToLower().IndexOf("disp=&") + 5;
                this.recipeCategoryChangeBaseUrl = currentUrl.ToLower().Insert(index, RecipeDisplayEnum.ByCategory.ToString());
                currentUrl = this.recipeCategoryChangeBaseUrl;
            }
        }
        else
        {
            this.recipeCategoryChangeBaseUrl = currentUrl + (currentUrl.ToLower().Contains("?") ? "&" : "?") + "disp=" + RecipeDisplayEnum.ByCategory.ToString();
            currentUrl = this.recipeCategoryChangeBaseUrl;
        }

        if (currentUrl.ToLower().Contains("cat="))
        {
            if (!currentUrl.ToLower().Contains("cat=&")) 
            {              
                this.recipeCategoryChangeBaseUrl = currentUrl.ToLower().Replace("cat=" + Request["cat"].ToLower(), "cat={0}");
            }
            else //should not happen - make sure and remove
            {
                int index = currentUrl.ToLower().IndexOf("cat=&") + 4;
                this.recipeCategoryChangeBaseUrl = currentUrl.ToLower().Insert(index, "{0}");
            }
        }
        else
        {            
            this.recipeCategoryChangeBaseUrl = currentUrl + "&cat={0}";
        }
    }
    
    //protected void btnCategory_Command(object sender, CommandEventArgs e)
    //{
    //    Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}&cat={1}", RecipeDisplayEnum.ByCategory, e.CommandArgument.ToString()));
    //}

    protected void primaryCatLink_Click(object sender, EventArgs e)
    {
        this.RebindCategories(null);

        //this.simpleSearch.Visible = false;
        //this.btnSearch.Visible = false;
        this.advancedSearch.Visible = false;
        this.categories.Visible = true;
        this.upSearch.Update();
    }

    protected void link_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect(string.Format("~/Recipes.aspx?page=1&orderby=LastUpdate&disp={0}&cat={1}", RecipeDisplayEnum.ByCategory, e.CommandArgument.ToString()));
    }

    protected void ucRecipeCats_RefreshData(SRL_RecipeCategory[] arr)
    {
        string strIDs = "";
        string strNames = "";
        foreach (SRL_RecipeCategory cat in arr)
        {
            strIDs += cat.CategoryId.ToString() + ",";
            strNames += cat.CategoryName + ", ";
        }

        this.hdnCategorieIDs.Value = strIDs.Substring(0, strIDs.Length - 1);
        this.txtCategory.Text = strNames.Substring(0, strNames.Length - 2);
        this.upSearch.Update();
    }

    private int[] GetCategoryIdsFromString(string str)
    {
        string[] tmpArray = str.Split(new char[1] { ',' });
        int[] ids = new int[tmpArray.Length];
        bool allSucceeded = true;
        for (int i = 0; i < tmpArray.Length; i++)
        {
            int x;
            if (int.TryParse(tmpArray[i], out x))
            {
                ids[i] = x;
            }
            else
            {
                allSucceeded = false;
            }
        }
        if (allSucceeded)
        {
            return ids;
        }
        else
        {
            return null;
        }
    }

    private void EmphasizeCurrentSearch(RecipeDisplayEnum currentDisplay)
    {
        ReciepesSearchControl1.EmphasizeCurrentSearch(currentDisplay);

        //this.lnkAllRecipes.Style["background-color"] = "Transparent";
        //this.lnkMyRecipes.Style["background-color"] = "Transparent";
        //this.lnkFavRecipes.Style["background-color"] = "Transparent";
        //this.lnkSearch.Style["background-color"] = "Transparent";
        //this.lnkAdvancedSearch.Style["background-color"] = "Transparent";
        //this.lnkCategories.Style["background-color"] = "Transparent";

        //switch (currentDisplay)
        //{
        //    case RecipeDisplayEnum.All:
        //        this.lnkAllRecipes.Style["background-color"] = "#A4CB3A";
        //        break;
        //    case RecipeDisplayEnum.MyRecipes:
        //        this.lnkMyRecipes.Style["background-color"] = "#A4CB3A";
        //        break;
        //    case RecipeDisplayEnum.MyFavoriteRecipes:
        //        this.lnkFavRecipes.Style["background-color"] = "#A4CB3A";
        //        break;
        //    case RecipeDisplayEnum.BySearchSimple:
        //        this.lnkSearch.Style["background-color"] = "#A4CB3A";
        //        break;
        //    case RecipeDisplayEnum.BySearchAdvanced:
        //        this.lnkAdvancedSearch.Style["background-color"] = "#A4CB3A";
        //        break;
        //    case RecipeDisplayEnum.ByCategory:
        //        this.lnkCategories.Style["background-color"] = "#A4CB3A";
        //        break;
        //}
    }

    /// <summary>
    /// this function creates the relevant urls for sort changes
    /// </summary>
    private void EnsureUrlsForSortingAreGenerated()
    {
        if (urlsGenerated)
            return;

        string currentUrl = Request.Url.OriginalString;

        // replace orderby
        if (currentUrl.ToLower().Contains("orderby"))
        {
            orderByLastUpdateUrl = currentUrl.ToLower().Replace(Request["orderBy"].ToLower(), "LastUpdate");
            orderByNameUrl = currentUrl.ToLower().Replace(Request["orderBy"].ToLower(), "Name");
            orderByPublisherUrl = currentUrl.ToLower().Replace(Request["orderBy"].ToLower(), "Publisher");
        }
        else
        {
            orderByLastUpdateUrl = currentUrl + (currentUrl.ToLower().Contains("?") ? "&" : "?") + "orderBy=LastUpdate";
            orderByNameUrl = currentUrl + (currentUrl.ToLower().Contains("?") ? "&" : "?") + "orderBy=Name";
            orderByPublisherUrl = currentUrl + (currentUrl.ToLower().Contains("?") ? "&" : "?") + "orderBy=Publisher";
        }

        urlsGenerated = true;
    }

    [WebMethod]
    public static int GetServings(int recipeId)
    {
        Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeId);
        return recipe.Servings;
    }

    [WebMethod(EnableSession = true)]
    public static bool AddRecipeToList(int recipeId, int servings)
    {
        SRL_User user = (SRL_User)HttpContext.Current.Session[AppConstants.SITE_USER];
        if (user == null)
            return false;

        Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeId);

        int rem;
        int multiplyServiceFactor = Math.DivRem(servings, recipe.Servings, out rem);
        if (rem != 0)
        {
            multiplyServiceFactor += 1;
        }

        List<SRL_Ingredient> ingredients =
            (from p in recipe.Ingredients
             select
                 new SRL_Ingredient
                     {
                         //IngredientId = p.IngredientId,
                         FoodId = p.FoodId,
                         FoodName = p.Food.FoodName,
                         Quantity = (p.Quantity * multiplyServiceFactor),
                         MeasurementUnitId = p.MeasurementUnitId,
                         MeasurementUnitName = p.MeasurementUnit.UnitName
                     }).ToList();

        int summeryListId = BusinessFacade.Instance.AddSummeryList(user.UserId);

        foreach (SRL_Ingredient ingredient in ingredients)
        {
            bool addSummeryListItem = BusinessFacade.Instance.AddSummeryListItem(ingredient, summeryListId, recipeId);
        }

        return true;
    }

    //protected string OrderByLastUpdateUrl
    //{
    //    get
    //    {
    //        string currentUrl = Request.Url.OriginalString;
    //        string targetSortName = "LastUpdate";
    //        return GetSortUrl(currentUrl, targetSortName);
    //    }
    //}

    //protected string OrderByNameUrl
    //{
    //    get
    //    {
    //        string currentUrl = Request.Url.OriginalString;
    //        string targetSortName = "Name";
    //        return GetSortUrl(currentUrl, targetSortName);
    //    }
    //}

    //protected string OrderByPublisherUrl
    //{
    //    get
    //    {
    //        string currentUrl = Request.Url.OriginalString;
    //        string targetSortName = "Publisher";
    //        return GetSortUrl(currentUrl, targetSortName);
    //    }
    //}

    /// <summary>
    /// this function creates the relevant url for sort changes
    /// </summary>
    /// <param name="currentUrl"></param>
    /// <param name="targetSortName"></param>
    /// <returns></returns>
    //private string GetSortUrl(string currentUrl, string targetSortName)
    //{
    //    string resultUrl;

    //    // replace orderby
    //    if (currentUrl.ToLower().Contains("orderby"))
    //    {
    //        resultUrl = currentUrl.ToLower().Replace(Request["orderBy"].ToLower(), targetSortName);
    //    }
    //    else if (currentUrl.ToLower().Contains("?"))
    //    {
    //        resultUrl = currentUrl + "&orderBy=" + targetSortName;
    //    }
    //    else
    //    {
    //        resultUrl = currentUrl + "?orderBy=" + targetSortName;
    //    }

    //    return resultUrl;
    //}

    private string AdjustLength(string catName, int length)
    {
        if (catName.Length > length)
        {
            catName = catName.TrimToMax(length);
        }

        return catName;
    }

    protected class RecipeView
    {
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; }
        public string MainCategoryName { get; set; }
        public string RecipeTags { get; set; }
        public string RecipeDescription { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string PublishDate { get; set; }
        public string RecipeThumbnail { get; set; }
        public bool InMyFavorites { get; set; }
        public int NumUsersFavorite { get; set; }
        public int NumMenusInclude { get; set; }
    }
}
