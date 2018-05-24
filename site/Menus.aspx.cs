using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using ProperControls.Pages;
using ProperServices.Common.Extensions;

using MyBuyList.BusinessLayer;
using MyBuyList.BusinessLayer.Managers;
using MyBuyList.Shared.Entities;
using MyBuyList.Shared.Enums;
using ProperControls.General;

public partial class Menus : BasePage
{
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
            if (this.Display == RecipeDisplayEnum.BySearchSimple)
            {
                //this.simpleSearch.Visible = true;
                //this.btnSearch.Visible = true;
                //this.txtSearchTerm.Text = (!string.IsNullOrEmpty(Request["term"])) ? Request["term"] : "";
                //this.btnSearch.CommandArgument = "BySearchSimple";
            }
            if (this.Display == RecipeDisplayEnum.BySearchAdvanced)
            {
                this.BindMenuCategories();
                //this.simpleSearch.Visible = true;
                this.advancedSearch.Visible = true;
                //this.btnSearch.Visible = true;
                //this.txtSearchTerm.Text = (!string.IsNullOrEmpty(Request["term"])) ? Request["term"] : "";
                this.txtDiners.Text = (!string.IsNullOrEmpty(Request["serv"])) ? Request["serv"] : "";
                this.txtCategory.Text = "";
                //this.btnSearch.CommandArgument = "BySearchAdvanced";
            }

            EmphasizeCurrentSearch(this.Display);

            MyBuyList.Shared.Entities.User currentUser = BusinessFacade.Instance.GetUser(((BasePage)this.Page).UserId);
            string email = (currentUser != null) ? currentUser.Email : string.Empty;
            this.ucSendMailToFriend.BindItemDetails("Menu", 0, string.Empty, email);

            RebindRecipes();

            // 17.09.2010
            // Load categories at page load
            MCategory[] categories = BusinessFacade.Instance.GetMenusCategoriesList(((BasePage)this.Page).UserId);
            var list = from mcat in categories.Where(mcat => mcat.ParentMCategoryId == null).ToArray()
                       select mcat;

            MCategory[] categoryList = list.ToArray();
            MenusSearchControl1.FillList(categoryList, this.MenuCategoryChangeBaseUrl);

            if (CurrUser != null) ucShoppingList1.UserId = CurrUser.UserId;
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

    public int? Diners
    {
        get
        {
            int diners = 0;

            if (!string.IsNullOrEmpty(Request["serv"]) && int.TryParse(Request["serv"], out diners))
            {
                return diners;
            }
            else
            {
                return null;
            }
        }
    }

    public int[] MenuCategories
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

    public int PageSize { get { return 7; } }

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

    public int totalPages;

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

    protected string MenuCategoryChangeBaseUrl
    {
        get
        {
            EnsureMenuCategoryChangeBaseUrlExists();

            return this.menuCategoryChangeBaseUrl;
        }
    }


    bool urlsGenerated = false;

    private string orderByLastUpdateUrl;
    private string orderByNameUrl;
    private string orderByPublisherUrl;
    private string menuCategoryChangeBaseUrl;


    private void RebindRecipes()
    {
        int count = 0;
        int userid = ((BasePage)this).UserId;
        //IEnumerable<MenuView> menus = from m in BusinessFacade.GetMenus(userid, this.OrderBy, this.CurrentPage, this.PageSize, out totalPages)
        IEnumerable<MenuView> menus = from m in BusinessFacade.Instance.GetMenusEx(this.Display, userid, this.FreeText, this.CategoryId, this.Diners, this.MenuCategories, this.OrderBy, this.CurrentPage, this.PageSize, out totalPages, out count)
                                      select new MenuView()
                                          {
                                              MenuId = m.MenuId,
                                              MenuTitle = this.AdjustLength(m.MenuName, 45),                                              
                                              MenuTypeId = m.MenuTypeId,
                                              MenuCategoryName = "",
                                              MenuTypeNameShort = this.GetShortMenuTypeName(m.MenuTypeId),
                                              MenuTags = (!string.IsNullOrEmpty(m.Tags))? m.Tags.TrimToMax(100) : "&nbsp;",
                                              MenuDescription = (m.Description != null) ? m.Description.TrimToMax(200) : "",
                                              PublisherId = m.User.UserId,
                                              PublisherName = m.User.DisplayName,
                                              PublishDate = m.ModifiedDate.ToString("dd/MM/yyyy"),
                                              MenuThumbnail = ResolveUrl(m.Picture != null ? string.Format("~/ShowPicture.ashx?MenuId={0}", m.MenuId) : "~/Images/Img_Default_small.jpg"),
                                              IsInFavorites = (m.UserFavoriteMenus.SingleOrDefault(ufm => ufm.UserId == ((BasePage)this.Page).UserId) != null),
                                              NumUserFavorites = m.UserFavoriteMenus.Count
                                          };

        rptRecipes.DataSource = menus;
        rptRecipes.ItemCreated += rptRecipes_ItemCreated;// for the pagers
        rptRecipes.ItemDataBound += rptRecipes_ItemDataBound;
        rptRecipes.DataBind();
        lblNumMenus.Text = string.Format("נמצאו {0} תפריטים", count);
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
                lnkArrow.NavigateUrl = string.Format("~/Menus.aspx?page={0}&orderby={1}{2}{3}{4}{5}{6}", (startPage - 20).ToString(), this.OrderBy.ToString(), (!string.IsNullOrEmpty(Request["disp"])) ? string.Format("&disp={0}", Request["disp"]) : "", (!string.IsNullOrEmpty(Request["term"])) ? string.Format("&term={0}", Request["term"]) : "", (!string.IsNullOrEmpty(Request["serv"])) ? string.Format("&serv={0}", Request["serv"]) : "", (!string.IsNullOrEmpty(Request["categories"])) ? string.Format("&categories={0}", Request["categories"]) : "", (!string.IsNullOrEmpty(Request["cat"])) ? string.Format("&cat={0}", Request["cat"]) : "");
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
                    lnkPage.NavigateUrl = string.Format("~/Menus.aspx?page={0}&orderby={1}{2}{3}{4}{5}{6}", page, this.OrderBy.ToString(), (!string.IsNullOrEmpty(Request["disp"])) ? string.Format("&disp={0}", Request["disp"]) : "", (!string.IsNullOrEmpty(Request["term"])) ? string.Format("&term={0}", Request["term"]) : "", (!string.IsNullOrEmpty(Request["serv"])) ? string.Format("&serv={0}", Request["serv"]) : "", (!string.IsNullOrEmpty(Request["categories"])) ? string.Format("&categories={0}", Request["categories"]) : "", (!string.IsNullOrEmpty(Request["cat"])) ? string.Format("&cat={0}", Request["cat"]) : "");
                    lnkPage.Text = page.ToString();
                    phPager.Controls.Add(lnkPage);
                }

                phPager.Controls.Add(new LiteralControl("&nbsp;"));
            }

            if (!isEnd)
            {
                HyperLink lnkArrow = new HyperLink();
                lnkArrow.Text = "&gt;&gt;";
                lnkArrow.NavigateUrl = string.Format("~/Menus.aspx?page={0}&orderby={1}{2}{3}{4}{5}{6}", (startPage + 20).ToString(), this.OrderBy.ToString(), (!string.IsNullOrEmpty(Request["disp"])) ? string.Format("&disp={0}", Request["disp"]) : "", (!string.IsNullOrEmpty(Request["term"])) ? string.Format("&term={0}", Request["term"]) : "", (!string.IsNullOrEmpty(Request["serv"])) ? string.Format("&serv={0}", Request["serv"]) : "", (!string.IsNullOrEmpty(Request["categories"])) ? string.Format("&categories={0}", Request["categories"]) : "", (!string.IsNullOrEmpty(Request["cat"])) ? string.Format("&cat={0}", Request["cat"]) : "");
                phPager.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
                phPager.Controls.Add(lnkArrow);
            }
        }
    }

    void rptRecipes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int menuId = 0;
        int menuTypeId = 0;

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            menuId = ((MenuView)e.Item.DataItem).MenuId;
            menuTypeId = ((MenuView)e.Item.DataItem).MenuTypeId;
            HyperLink lnkRecipe = (HyperLink)e.Item.FindControl("lnkRecipe");
            lnkRecipe.NavigateUrl = ResolveUrl(string.Format("~/MenuDetails.aspx?menuId={0}", menuId.ToString()));

            LinkButton lnkBtn = e.Item.FindControl("btnAddRemoveFromFavorites") as LinkButton;
            if (lnkBtn != null)
            {
                if (((BasePage)this.Page).UserId != -1)
                {
                    lnkBtn.CommandArgument = ((MenuView)e.Item.DataItem).MenuId.ToString();

                    if (((MenuView)e.Item.DataItem).IsInFavorites)
                    {
                        lnkBtn.Text = "הסר מהמועדפים שלי";
                        lnkBtn.Style["color"] = "Red";
                        lnkBtn.CommandName = "Remove";
                    }
                    else
                    {
                        lnkBtn.Text = "הוסף למועדפים שלי";
                        lnkBtn.Style["color"] = "#656565";
                        lnkBtn.CommandName = "Add";
                    }
                }
                else
                {
                    lnkBtn.Visible = false;
                    Literal lit = (Literal)e.Item.FindControl("seperator");
                    if (lit != null)
                        lit.Visible = false;
                }
            }
            
            HtmlGenericControl divMyFavoritesInfoTag = e.Item.FindControl("myFavoritesInfoTag") as HtmlGenericControl;

            if (((MenuView)e.Item.DataItem).IsInFavorites)
            {
                divMyFavoritesInfoTag.Visible = true;
            }
            else
            {
                divMyFavoritesInfoTag.Visible = false;
            }

            Label lblAllFavorites = e.Item.FindControl("lblAllFavorites") as Label; 
            lblAllFavorites.Text = ((MenuView)e.Item.DataItem).NumUserFavorites.ToString();            

            Label lbl1 = e.Item.FindControl("lblMealTitle1") as Label;
            Label lbl2 = e.Item.FindControl("lblMealTitle2") as Label;
            Label lbl3 = e.Item.FindControl("lblMealTitle3") as Label;

            DataList dl1 = e.Item.FindControl("dlRecipes1") as DataList;
            DataList dl2 = e.Item.FindControl("dlRecipes2") as DataList;
            DataList dl3 = e.Item.FindControl("dlRecipes3") as DataList;

            Meal[] menuMeals = BusinessFacade.Instance.GetMenuMeals(menuId);

            //present "meals preview" in a different way for each menu type:

            if (menuTypeId == (int)MenuTypeEnum.OneMeal)
            {
                Meal meal1 = menuMeals.SingleOrDefault(me => me.CourseTypeId == 0);
                Meal meal2 = menuMeals.SingleOrDefault(me => me.CourseTypeId == 1);
                Meal meal3 = menuMeals.SingleOrDefault(me => me.CourseTypeId == 3);

                if (lbl1 != null && lbl2 != null && lbl3 != null)
                {
                    lbl1.Text = "מנה ראשונה";
                    lbl2.Text = "מנה עיקרית";
                    lbl3.Text = "קינוח";
                }

                MealRecipe[] tempRecipesList = new MealRecipe[2];

                if (meal1 != null && meal1.MealRecipes.Count > 0)                
                {
                    tempRecipesList[0] = meal1.MealRecipes[0];
                    if (meal1.MealRecipes.Count > 1)
                    {
                        tempRecipesList[1] = meal1.MealRecipes[1];
                    }

                    dl1.DataSource = tempRecipesList;
                    dl1.DataBind();
                }
                if (meal2 != null && meal2.MealRecipes.Count > 0)                
                {
                    tempRecipesList[0] = meal2.MealRecipes[0];
                    if (meal2.MealRecipes.Count > 1)
                    {
                        tempRecipesList[1] = meal2.MealRecipes[1];
                    }

                    dl2.DataSource = tempRecipesList;
                    dl2.DataBind();
                }
                if (meal3 != null && meal3.MealRecipes.Count > 0)               
                {
                    tempRecipesList[0] = meal3.MealRecipes[0];
                    if (meal3.MealRecipes.Count > 1)
                    {
                        tempRecipesList[1] = meal3.MealRecipes[1];
                    }
                    dl3.DataSource = tempRecipesList;
                    dl3.DataBind();
                }
            }
            else if (menuTypeId == (int)MenuTypeEnum.Weekly) 
            {
                IEnumerable<Meal> meals1 = menuMeals.Where(mm => mm.DayIndex == 1);
                IEnumerable<Meal> meals2 = menuMeals.Where(mm => mm.DayIndex == 2);
                IEnumerable<Meal> meals3 = menuMeals.Where(mm => mm.DayIndex == 3);

                if (lbl1 != null && lbl2 != null && lbl3 != null)
                {
                    lbl1.Text = "יום ראשון";
                    lbl2.Text = "יום שני";
                    lbl3.Text = "יום שלישי";
                }

                MealRecipe[] tempRecipesList = new MealRecipe[2];

                if (meals1 != null)
                {
                    int i = 0;
                    foreach (Meal meal in meals1)
                    {
                        foreach (MealRecipe mr in meal.MealRecipes)
                        {
                            if (i < 2)
                            {
                                tempRecipesList[i] = mr;
                                i++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    dl1.DataSource = tempRecipesList;
                    dl1.DataBind();
                }
                if (meals2 != null)
                {
                    int i = 0;
                    foreach (Meal meal in meals2)
                    {
                        foreach (MealRecipe mr in meal.MealRecipes)
                        {
                            if (i < 2)
                            {
                                tempRecipesList[i] = mr;
                                i++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    dl2.DataSource = tempRecipesList;
                    dl2.DataBind();
                }
                if (meals3 != null)
                {
                    int i = 0;
                    foreach (Meal meal in meals3)
                    {
                        foreach (MealRecipe mr in meal.MealRecipes)
                        {
                            if (i < 2)
                            {
                                tempRecipesList[i] = mr;
                                i++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    dl3.DataSource = tempRecipesList;
                    dl3.DataBind();
                }
            }
            else if (menuTypeId == (int)MenuTypeEnum.QuickMenu) //only one meal in menu  
            {
                Meal meal1 = menuMeals.SingleOrDefault(me => me.MealTypeId == 4);                

                if (meal1 != null)
                {
                    MealRecipe[] allRecipesList = new MealRecipe[meal1.MealRecipes.Count];

                    int i = 0;
                    foreach (MealRecipe mr in meal1.MealRecipes)
                    {
                        allRecipesList[i] = mr;
                        i++;                        
                    }

                    MealRecipe[] tempRecipesList = new MealRecipe[2];

                    if (allRecipesList.Length > 0)
                    {
                        tempRecipesList[0] = allRecipesList[0];

                        if (allRecipesList.Length >= 2)
                        {
                            tempRecipesList[1] = allRecipesList[1];
                        }
                        else
                        {
                            tempRecipesList[1] = null;
                        }

                        dl1.DataSource = tempRecipesList;
                        dl1.DataBind();
                    }                    

                    if (allRecipesList.Length >= 3)
                    {
                        tempRecipesList[0] = allRecipesList[2];

                        if (allRecipesList.Length >= 4)
                        {
                            tempRecipesList[1] = allRecipesList[3];
                        }
                        else
                        {
                            tempRecipesList[1] = null; 
                        }
                        dl2.DataSource = tempRecipesList;
                        dl2.DataBind();
                    } 
                    

                    if (allRecipesList.Length >= 5)
                    {
                        tempRecipesList[0] = allRecipesList[4];
                        if (allRecipesList.Length >= 6)
                        {
                            tempRecipesList[1] = allRecipesList[5];
                        }
                        else
                        {
                            tempRecipesList[1] = null;
                        }
                        dl3.DataSource = tempRecipesList;
                        dl3.DataBind();
                    }                    
                }
            }
        }       
    }
    
    protected void dlRecipes_ItemDataBound(object sender, DataListItemEventArgs e)
    {       
        MealRecipe mealRecipe = e.Item.DataItem as MealRecipe;        
        HyperLink hlkRecipeDetails = e.Item.FindControl("hlkRecipeDetails") as HyperLink;
        Label lblRecipeArrow = e.Item.FindControl("lblRecipeArrow") as Label;

        if (mealRecipe != null && hlkRecipeDetails != null)
        {
            hlkRecipeDetails.Text = mealRecipe.Recipe.RecipeName.TrimToMax(21);
            hlkRecipeDetails.NavigateUrl += mealRecipe.RecipeId.ToString();
        }
        else
        {
            lblRecipeArrow.Visible = false;
            hlkRecipeDetails.Visible = false;
        }        
    }

    protected void ucSendMailToFriend_EmailSent(object sender, SendToFriendEventArgs e)
    {
        ((BasePage)this.Page).DisplayMessage = "אימייל נשלח ל- " + e.recipentName;
    }

    protected void btnAddRemoveFromFavorites_Command(object sender, CommandEventArgs e)
    {
        LinkButton lnkBtn = (LinkButton)sender;
        int menuId;
        if (int.TryParse(e.CommandArgument.ToString(), out menuId))
        {
            int favMenusNum = 0;
            List<int> favMenusList = Utils.FavoriteMenusAdded;

            if (e.CommandName == "Add" && BusinessFacade.Instance.AddMenuToUserFavorites(((BasePage)this.Page).UserId, menuId, out favMenusNum))
            {
                lnkBtn.CommandName = "Remove";
                lnkBtn.Text = "הסר מהמועדפים שלי";
                lnkBtn.Style["color"] = "Red";
                
                favMenusList.Add(menuId);
                Utils.FavoriteMenusAdded = favMenusList;
            }
            else if (e.CommandName == "Remove" && BusinessFacade.Instance.RemoveMenuFromUserFavorites(((BasePage)this.Page).UserId, menuId, out favMenusNum))
            {
                lnkBtn.CommandName = "Add";
                lnkBtn.Text = "הוסף למועדפים שלי";
                lnkBtn.Style["color"] = "#656565";

                if (favMenusList.Contains(menuId))
                {
                    favMenusList.Remove(menuId);
                }
                Utils.FavoriteMenusAdded = favMenusList;
            }

            this.RefreshInfoTags(menuId);
            this.RebindRecipes();
            this.upMenus.Update();

            UserControl uc = (((this.Master).Master).Master).FindControl("HeaderControl1") as UserControl;

            if (uc != null)
            {
                Label lbl = uc.FindControl("lblFavMenusNum") as Label;
                if (lbl != null)
                {
                    lbl.Text = "(" + Utils.FavoriteMenusAdded.Count + ")";
                }

                UpdatePanel up = uc.FindControl("upFavorites") as UpdatePanel;
                if (up != null)
                {
                    up.Update();
                }
            }
        }        
    }

    private void RefreshInfoTags(int menuId)
    {
        //get the correct info tags to update.
        HtmlGenericControl divMyFavoritesInfoTag = null;
        Label lblAllFavorites = null;

        foreach (RepeaterItem item in this.rptRecipes.Items)
        {
            HiddenField hf = item.FindControl("hfMenuId") as HiddenField;
            if (hf != null && int.Parse(hf.Value) == menuId)
            {
                divMyFavoritesInfoTag = item.FindControl("myFavoritesInfoTag") as HtmlGenericControl;
                lblAllFavorites = item.FindControl("lblAllFavorites") as Label;
            }
        }
        //get the menu
        MyBuyList.Shared.Entities.Menu menu = BusinessFacade.Instance.GetMenu(menuId);

        if (menu != null && divMyFavoritesInfoTag != null && lblAllFavorites != null)
        {  
            if (menu.UserFavoriteMenus.SingleOrDefault(ufm => ufm.UserId == ((BasePage)this.Page).UserId) != null)
            {
                divMyFavoritesInfoTag.Visible = true;
            }
            else
            {
                divMyFavoritesInfoTag.Visible = false;
            }
           
            lblAllFavorites.Text = menu.UserFavoriteMenus.Count.ToString();
        }
    }

    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        //this.btnSearch.CommandArgument = "BySearchSimple";

        //this.simpleSearch.Visible = true;
        //this.btnSearch.Visible = true;
        this.advancedSearch.Visible = false;
        this.categories.Visible = false;
        this.EmphasizeCurrentSearch(RecipeDisplayEnum.BySearchSimple);
        this.upSearch.Update();
    }

    protected void lnkAdvancedSearch_Click(object sender, EventArgs e)
    {
        //this.btnSearch.CommandArgument = "BySearchAdvanced";

        this.BindMenuCategories();
        //this.simpleSearch.Visible = true;
        //this.btnSearch.Visible = true;
        this.advancedSearch.Visible = true;
        this.categories.Visible = false;
        this.EmphasizeCurrentSearch(RecipeDisplayEnum.BySearchAdvanced);
        this.upSearch.Update();
    }

    protected void btnSearch_Command(object sender, CommandEventArgs e)
    {
        string categoryIDs = "";

        if (e.CommandArgument.ToString() == "BySearchAdvanced")
        {
            categoryIDs = this.hdnCategorieIDs.Value;
        }

        //Response.Redirect(string.Format("~/Menus.aspx?page=1&orderby=LastUpdate&disp={0}{1}{2}{3}", e.CommandArgument.ToString(), (!string.IsNullOrEmpty(this.txtSearchTerm.Text)) ? string.Format("&term={0}", this.txtSearchTerm.Text) : "", (!string.IsNullOrEmpty(this.txtDiners.Text)) ? string.Format("&serv={0}", this.txtDiners.Text) : "", (!string.IsNullOrEmpty(categoryIDs)) ? string.Format("&categories={0}", categoryIDs) : ""));
    }

    protected void lnkAllMenus_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Menus.aspx");
    }

    protected void lnkMyMenus_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/Menus.aspx?page=1&orderby=LastUpdate&disp={0}", RecipeDisplayEnum.MyRecipes.ToString()));
    }

    protected void lnkFavMenus_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/Menus.aspx?page=1&orderby=LastUpdate&disp={0}", RecipeDisplayEnum.MyFavoriteRecipes.ToString()));
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
        MCategory[] categories = BusinessFacade.Instance.GetMenusCategoriesList(((BasePage)this.Page).UserId);
        var list = from mcat in categories.Where(mcat => mcat.ParentMCategoryId == categoryId).ToArray()
                   select mcat;
        this.rptCategories.DataSource = list.ToArray();
        this.rptCategories.DataBind();

        count = list.ToArray().Length;

        List<MCategory> pathList = new List<MCategory>();
        if (categoryId != null)
        {
            MCategory tmpCategory = categories.SingleOrDefault(mct => mct.MCategoryId == categoryId.Value);

            do
            {
                pathList.Add(tmpCategory);
                if (tmpCategory.ParentMCategoryId == null)
                    break;
                tmpCategory = categories.SingleOrDefault(mc => mc.MCategoryId == tmpCategory.ParentMCategoryId);
            }
            while (tmpCategory != null);
        }

        //LinkButton primaryCatLink = new LinkButton();
        //primaryCatLink.Text = "הכל";
        //primaryCatLink.Style["background-color"] = "#A4CB3A";
        //primaryCatLink.Click += new EventHandler(primaryCatLink_Click);

        HyperLink primaryCatLink = new HyperLink();
        primaryCatLink.Text = "הכל";
        primaryCatLink.Style["background-color"] = "#FBAB14";
        primaryCatLink.NavigateUrl = string.Format("~/Menus.aspx?disp={0}{1}", RecipeDisplayEnum.ByCategory.ToString(), (Request["orderBy"] != null) ? string.Format("&orderBy={0}", Request["orderBy"]) : "");
        this.pathLinks.Controls.Clear();
        this.pathLinks.Controls.Add(primaryCatLink);

        pathList.Reverse();
        foreach (MCategory category in pathList)
        {
            Label arrows = new Label();
            arrows.Text = " >> ";
            arrows.Style["background-color"] = "#FBAB14";
            this.pathLinks.Controls.Add(arrows);

            HyperLink link = new HyperLink();
            link.Text = category.MCategoryName;
            link.Style["background-color"] = "#FBAB14";
            link.NavigateUrl = string.Format(this.MenuCategoryChangeBaseUrl, category.MCategoryId);
            //link.Command += new CommandEventHandler(link_Command);
            this.pathLinks.Controls.Add(link);
        }

        return count;
    }

    protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        MCategory category = e.Item.DataItem as MCategory;
        if (category != null)
        {
            //LinkButton btn = e.Item.FindControl("btnCategory") as LinkButton;
            //btn.Text += " (" + category.RecipesCount + ")";
            HyperLink lnk = (HyperLink)e.Item.FindControl("lnkCategory");
            lnk.Text = string.Format("{0} ({1})", category.MCategoryName, category.MenusCount);
            lnk.NavigateUrl = string.Format(this.MenuCategoryChangeBaseUrl, category.MCategoryId);
        }
    }    

    protected void btnCatOK_Click(object sender, EventArgs e)
    {
        string strIDs = "";
        string strNames = "";
        foreach (TreeNode node in this.tvCategories.CheckedNodes)
        {            
            strIDs += node.Value + ",";
            strNames += node.Text + ", ";
        }

        this.hdnCategorieIDs.Value = strIDs.Substring(0, strIDs.Length - 1);
        this.txtCategory.Text = strNames.Substring(0, strNames.Length - 2);
        this.upSearch.Update();
    }

    protected void BindMenuCategories()
    {
        this.tvCategories.Nodes.Clear();
        MCategory[] categories = BusinessFacade.Instance.GetMCategoriesList();
        this.BuildTree(categories, null, null);

        this.tvCategories.ShowCheckBoxes = TreeNodeTypes.All;
        this.tvCategories.DataBind();

        this.upTreeViewCategories.Update();
    }

    private void BuildTree(MCategory[] cats, int? parentCategoryId, TreeNode rootNode)
    {
        var list = cats.Where(mc => mc.ParentMCategoryId == parentCategoryId);
        foreach (MCategory item in list)
        {
            TreeNode node = new TreeNode(item.MCategoryName, item.MCategoryId.ToString());  

            if (rootNode == null)
            {
                this.tvCategories.Nodes.Add(node);
            }
            else
            {
                rootNode.ChildNodes.Add(node);
            }

            BuildTree(cats, item.MCategoryId, node);
        }
    }


    protected string GetShortMenuTypeName(int menuTypeId)
    {
        string shortName = "";
        switch (menuTypeId)
        {
            case ((int)MenuTypeEnum.OneMeal):
                shortName = "ארוחה";
                break;
            case ((int)MenuTypeEnum.QuickMenu):
                shortName = "מהיר";
                break;
            case ((int)MenuTypeEnum.Weekly):
                shortName = "שבועי";
                break;
            case ((int)MenuTypeEnum.ManyWeeks):
                shortName = "רב שבועי";
                break;
        }
        return shortName;
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

    private void EnsureMenuCategoryChangeBaseUrlExists()
    {
        if (!string.IsNullOrEmpty(this.menuCategoryChangeBaseUrl))
        {
            return;
        }

        string currentUrl = Request.Url.OriginalString;

        if (currentUrl.ToLower().Contains("page"))
        {
            this.menuCategoryChangeBaseUrl = currentUrl.ToLower().Replace(Request["page"].ToLower(), "1");
            currentUrl = this.menuCategoryChangeBaseUrl;
        }

        if (currentUrl.ToLower().Contains("disp"))
        {
            if (!currentUrl.ToLower().Contains("disp=&"))
            {
                this.menuCategoryChangeBaseUrl = currentUrl.ToLower().Replace(Request["disp"].ToLower(), RecipeDisplayEnum.ByCategory.ToString());
                currentUrl = this.menuCategoryChangeBaseUrl;
            }
            else  //should not happen - make sure and remove          
            {
                int index = currentUrl.ToLower().IndexOf("disp=&") + 5;
                this.menuCategoryChangeBaseUrl = currentUrl.ToLower().Insert(index, RecipeDisplayEnum.ByCategory.ToString());
                currentUrl = this.menuCategoryChangeBaseUrl;
            }
        }
        else
        {
            this.menuCategoryChangeBaseUrl = currentUrl + (currentUrl.ToLower().Contains("?") ? "&" : "?") + "disp=" + RecipeDisplayEnum.ByCategory.ToString();
            currentUrl = this.menuCategoryChangeBaseUrl;
        }

        if (currentUrl.ToLower().Contains("cat="))
        {
            if (!currentUrl.ToLower().Contains("cat=&"))
            {
                this.menuCategoryChangeBaseUrl = currentUrl.ToLower().Replace("cat=" + Request["cat"].ToLower(), "cat={0}");
            }
            else //should not happen - make sure and remove
            {
                int index = currentUrl.ToLower().IndexOf("cat=&") + 4;
                this.menuCategoryChangeBaseUrl = currentUrl.ToLower().Insert(index, "{0}");
            }
        }
        else
        {
            this.menuCategoryChangeBaseUrl = currentUrl + "&cat={0}";
        }
    }

    private void EmphasizeCurrentSearch(RecipeDisplayEnum currentDisplay)
    {
        MenusSearchControl1.EmphasizeCurrentSearch(currentDisplay);

        //this.lnkAllMenus.Style["text-decoration"] = "none";
        //this.lnkMyMenus.Style["text-decoration"] = "none";
        //this.lnkFavMenus.Style["text-decoration"] = "none";
        //this.lnkAllMenus.Style["background-color"] = "Transparent";
        //this.lnkMyMenus.Style["background-color"] = "Transparent";
        //this.lnkFavMenus.Style["background-color"] = "Transparent";
        //this.lnkSearch.Style["background-color"] = "Transparent";
        //this.lnkAdvancedSearch.Style["background-color"] = "Transparent";
        //this.lnkCategories.Style["background-color"] = "Transparent";

        switch (currentDisplay)
        {
            case RecipeDisplayEnum.All:
                //this.lnkAllMenus.Style["background-color"] = "#FBAB14";
                //this.lnkAllMenus.Style["text-decoration"] = "underline";
                break;
            case RecipeDisplayEnum.MyRecipes:
                //this.lnkMyMenus.Style["background-color"] = "#FBAB14";
                //this.lnkMyMenus.Style["text-decoration"] = "underline";
                break;
            case RecipeDisplayEnum.MyFavoriteRecipes:
                //this.lnkFavMenus.Style["background-color"] = "#FBAB14";
                //this.lnkFavMenus.Style["text-decoration"] = "underline";
                break;
            //case RecipeDisplayEnum.BySearchSimple:
            //    this.lnkSearch.Style["background-color"] = "#FBAB14";
            //    break;
            //case RecipeDisplayEnum.BySearchAdvanced:
            //    this.lnkAdvancedSearch.Style["background-color"] = "#FBAB14";
            //    break;
            //case RecipeDisplayEnum.ByCategory:
            //    this.lnkCategories.Style["background-color"] = "#FBAB14";
            //    break;
        }
    }

    private string AdjustLength(string catName, int length)
    {
        if (catName.Length > length)
        {
            catName = catName.TrimToMax(length);
        }

        return catName;
    }
    
    protected class MenuView
    {
        public int MenuId { get; set; }
        public string MenuTitle { get; set; }
        public int MenuTypeId { get; set; }
        public string MenuCategoryName { get; set; }
        public string MenuTypeNameShort { get; set; }
        public string MenuTags { get; set; }
        public string MenuDescription { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string PublishDate { get; set; }
        public string MenuThumbnail { get; set; }
        public bool IsInFavorites { get; set; }
        public int NumUserFavorites { get; set; }
    }
}
