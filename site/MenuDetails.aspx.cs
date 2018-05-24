using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ACAWebThumbLib;
using MyBuyList.BusinessLayer;
using MyBuyList.BusinessLayer.Managers;
using MyBuyList.Shared.Entities;
using ProperControls.Pages;
using ProperControls.General;
using Resources;
using Menu = MyBuyList.Shared.Entities.Menu;

public partial class MenuDetails : BasePage
{
    protected string FBUrl;

    public int? CurrentMenuId
    {
        get
        {
            int id;
            if (Request["menuId"] != null && int.TryParse(Request["menuId"], out id))
            {
                return id;
            }
            else
            {
                return null;
            }
        }
    }

    private MyBuyList.Shared.Entities.Menu currMenu;

    public MyBuyList.Shared.Entities.Menu currentMenu
    {
        get
        {
            return currMenu;
        }
        set
        {
            currMenu = value;
        }
    }

    public int MenuTypeId
    {
        get 
        {
            return ViewState["menuTypeId"] == null ? 0 : int.Parse(ViewState["menuTypeId"].ToString());
        }
        set 
        {
            ViewState["menuTypeId"] = value;
        }
    }

    private Meal currMeal;

    public Meal CurrentMeal
    {
        get
        {
            return currMeal;
        }
        set
        {
            currMeal = value;
        }
    }

    [Serializable]
    public class CourseOrMealType
    {
        public int CourseOrMealTypeId { get; set; }
        public string CourseOrMealTypeName { get; set; }

        public CourseOrMealType(int id, string name)
        {
            this.CourseOrMealTypeId = id;
            this.CourseOrMealTypeName = name;
        }
    }

    MealDayOfWeek[] MealDayOfWeek
    {
        get
        {
            MealDayOfWeek[] array = new MealDayOfWeek[] 
            {
                new MealDayOfWeek(1, MyGlobalResources.Sunday),
                new MealDayOfWeek(2, MyGlobalResources.Monday),
                new MealDayOfWeek(3, MyGlobalResources.Tuesday),
                new MealDayOfWeek(4, MyGlobalResources.Wednesday),
                new MealDayOfWeek(5, MyGlobalResources.Thursday),
                new MealDayOfWeek(6, MyGlobalResources.Friday),
                new MealDayOfWeek(7, MyGlobalResources.Saturday)
            };

            return array;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FBUrl = Request.Url.AbsoluteUri;

            if (string.IsNullOrEmpty(this.Request["menuId"]))
            {
                AppEnv.MoveToDefaultPage();
            }

            int menuId = int.Parse(Request["menuId"]);
            Menu menu = BusinessFacade.Instance.GetMenuEx(menuId);

            if (menu == null)
            {
                AppEnv.MoveToDefaultPage();
            }

            if ((((BasePage)Page).UserType != 1) && (menu.UserId != ((BasePage)Page).UserId) && !(menu.IsPublic) || menu.IsDeleted)
            {
                AppEnv.MoveToDefaultPage();
            }
            else
            {
                Page.Title = string.Format(" ארגון רשימת הקניות שלך - MyBuyList - {0}", menu.MenuName);
                hlEditMenu.NavigateUrl += menu.MenuId.ToString();
                hlEditMenu_bottom.NavigateUrl += menu.MenuId.ToString();
                currentMenu = menu;
                MenuTypeId = menu.MenuTypeId;
                BindMenuData();

                //-TODO???
                //this.PageDescription.Attributes["content"] = string.Format("פרטי מתכון - {0} מאת {1}", currRecipe.RecipeName, currRecipe.User.DisplayName);

                //-Should I store values in viewstate?
                //if (!string.IsNullOrEmpty(this.Request["view"]))
                //{
                //    this.ViewId = int.Parse(this.Request["view"]);
                //}


            }
        }
    }

    private void BindMenuData()
    {
        Menu menu = currentMenu;
        if (menu != null)
        {
            lblMenuName.Text = menu.MenuName;
            lblCategories.Text = this.GetCategoriesString(menu.MenuCategories.ToArray());
            lnkPublisher.Text = menu.User.DisplayName;
            lblPublishDate.Text = menu.ModifiedDate.ToString("dd/MM/yyyy");
            lblDescription.Text = menu.Description;
            lblMenuTags.Text = menu.Tags;

            //TODO: add code to change title-image "שם התפריט" to "רשימת קניות מהירה" and change lblMenuName to the date only "מתאריך ..." when the menu
            //is a quick menu.

            if (menu.Picture != null)
            {
                this.imgMenuPicture.ImageUrl = "~/ShowPicture.ashx?MenuId=" + menu.MenuId;
            }
            else if (!string.IsNullOrEmpty(menu.EmbeddedVideo) && menu.EmbeddedVideo.Contains("object") && menu.EmbeddedVideo.Contains("embed"))
            {
                this.imgMenuPicture.Visible = false;
                this.menu_video.Style["margin-top"] = "10px";
            }
            else
            {
                this.imgMenuPicture.ImageUrl = "~/Images/Img_Default.jpg";   //needs to be changed. ask Dalit. 
            }

            if (!string.IsNullOrEmpty(menu.EmbeddedVideo) && menu.EmbeddedVideo.Contains("object") && menu.EmbeddedVideo.Contains("embed"))
            {
                //adjustment method may not work for embedded videos that are not from youtube.
                this.menu_video.InnerHtml = this.AdjustVideo(menu.EmbeddedVideo);
            }
            else
            {
                this.menu_video.Visible = false;
            }

            bool isInMyFavorites = (this.currentMenu.UserFavoriteMenus.SingleOrDefault(ufm => ufm.UserId == ((BasePage)this.Page).UserId) != null);

            if (isInMyFavorites)
            {
                this.myFavoritesTopTag.Visible = true;
            }
            else
            {
                this.myFavoritesTopTag.Visible = false;
            }

            this.lblAllFavorites.Text = menu.UserFavoriteMenus.Count.ToString();

            bool isInUserFavorites = (this.currentMenu.UserFavoriteMenus.SingleOrDefault(ufm => ufm.UserId == ((BasePage)this.Page).UserId) != null);
            if (isInUserFavorites)
            {
                this.btnAddMenuToFavorites.Visible = false;
                this.btnAddMenuToFavorites_bottom.Visible = false;
            }
            else
            {
                this.btnRemoveMenuFromFavorites.Visible = false;
                this.btnRemoveMenuFromFavorites_bottom.Visible = false;
            }

            hlShowShoppingList.NavigateUrl = string.Format("~/ShoppingList.aspx?menuId={0}", menu.MenuId);
            hlShowShoppingList_bottom.NavigateUrl = string.Format("~/ShoppingList.aspx?menuId={0}", menu.MenuId);
            hlPrintMenu.NavigateUrl = string.Format("~/PrintMenu.aspx?menuId={0}", menu.MenuId);
            hlPrintMenu_bottom.NavigateUrl = string.Format("~/PrintMenu.aspx?menuId={0}", menu.MenuId);

            if (((BasePage)Page).UserId == -1)
            {
                //this.btnSendMail.Visible = false;
                //this.lblPrintMenuSeperator.Visible = false;
                //this.btnSendMail_bottom.Visible = false;
                //this.lblPrintMenuSeperator_bottom.Visible = false;
                btnAddMenuToFavorites.Visible = false;
                btnAddMenuToFavorites_bottom.Visible = false;
                lblAddToFavoritesSeparator.Visible = false;
                lblAddToFavoritesSeparator_bottom.Visible = false;
            }
            if (((BasePage)Page).UserId != menu.User.UserId && (((BasePage)Page).UserType != 1))
            {
                hlEditMenu.Visible = false;
                btnDeleteMenu.Visible = false;
                lblSendMailSeparator.Visible = false;
                lblEditMenuSeperator.Visible = false;
                hlEditMenu_bottom.Visible = false;
                btnDeleteMenu_bottom.Visible = false;
                lblSendMailSeparator_bottom.Visible = false;
                lblEditMenuSeperator_bottom.Visible = false;
            }

            if (menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
            {
                hlEditMenu.Visible = false;
                hlEditMenu_bottom.Visible = false;
                lblSendMailSeparator.Visible = false;
                lblSendMailSeparator_bottom.Visible = false;
            }

            if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal || menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
            {
                MenuDinersNum.Visible = true;

                if (menu.Meals.Count > 0 && menu.Meals.ToArray()[0].Diners != null)
                {
                    this.lblNoDiner.Text = menu.Meals.ToArray()[0].Diners.ToString();
                }
                MealDayOfWeek[] fakeDaysOfWeek = new MealDayOfWeek[1] { new MealDayOfWeek(1, MyGlobalResources.Sunday) };
                rptDays.DataSource = fakeDaysOfWeek;
                rptDays.DataBind();
            }

            if (menu.MenuTypeId == (int)MenuTypeEnum.Weekly)
            {
                this.rptDays.DataSource = this.MealDayOfWeek;
                this.rptDays.DataBind();
            }

            string userEmail = string.Empty;
            User currentUser = BusinessFacade.Instance.GetUser(((BasePage)this.Page).UserId);
            if (currentUser != null)
            {
                userEmail = currentUser.Email;
            }
            ucSendMailToFriend.BindItemDetails("Menu", menu.MenuId, menu.MenuName, userEmail);
        }
    }

    private void RefreshTopTags()
    {        
        MyBuyList.Shared.Entities.Menu menu = BusinessFacade.Instance.GetMenu(this.CurrentMenuId.Value);
       
        if (menu != null)
        {
            bool isInMyFavorites = (menu.UserFavoriteMenus.SingleOrDefault(ufm => ufm.UserId == ((BasePage)this.Page).UserId) != null);

            if (isInMyFavorites)
            {
                this.myFavoritesTopTag.Visible = true;
            }
            else
            {
                this.myFavoritesTopTag.Visible = false;
            }

            this.lblAllFavorites.Text = menu.UserFavoriteMenus.Count.ToString();
        }
    }

    protected void rptDays_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Menu menu = this.currentMenu;

        if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal || menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
        {
            System.Web.UI.WebControls.Image tableTopImag = e.Item.FindControl("imgTableTop") as System.Web.UI.WebControls.Image;
            tableTopImag.ImageUrl = "~/Images/bgr_TableMenu.jpg";

            Repeater rptCourses = e.Item.FindControl("rptCourses") as Repeater;


            var list = from item in BusinessFacade.Instance.GetCourseTypes()
                       select new CourseOrMealType(item.CourseTypeId, item.CourseTypeName);
            CourseOrMealType[] courseTypesArray = list.ToArray();

            if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal)
            {
                CourseOrMealType tempCourseType = courseTypesArray[0];
                //for (int i = 0; i < (courseTypesArray.Length - 1); i++)
                //{
                //    courseTypesArray[i] = courseTypesArray[i + 1];
                //}
                //courseTypesArray[courseTypesArray.Length - 1] = tempCourseType;
                rptCourses.DataSource = courseTypesArray;
            }
            else
            {
                rptCourses.DataSource = courseTypesArray.Where(cmt => cmt.CourseOrMealTypeId == 0);
            }

            rptCourses.DataBind();
        }
        if (menu.MenuTypeId == (int)MenuTypeEnum.Weekly)
        {
            MealDayOfWeek mdow = e.Item.DataItem as MealDayOfWeek;
            if (mdow != null)
            {
                System.Web.UI.WebControls.Image tableTopImag = e.Item.FindControl("imgTableTop") as System.Web.UI.WebControls.Image;
                tableTopImag.ImageUrl = "~/Images/bgr_Table" + mdow.DayId + ".jpg";
            }

            Repeater rptCourses = e.Item.FindControl("rptCourses") as Repeater;

            var list = from item in BusinessFacade.Instance.GetMealTypes()
                       select new CourseOrMealType(item.MealTypeId, item.MealTypeName);

            CourseOrMealType[] mealTypesArray = list.ToArray();
            rptCourses.DataSource = mealTypesArray;
            rptCourses.DataBind();
        }
    }

    protected void rptCourses_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        Menu menu = this.currentMenu;

        Meal currentMeal = null;
        MealDayOfWeek currentWeekDay = null;
        int currentWeekDayId = 0;

        int currentCourseOrMenuTypeId = ((CourseOrMealType)e.Item.DataItem).CourseOrMealTypeId;

        RepeaterItem parentItem = e.Item.Parent.Parent as RepeaterItem;
        if (parentItem != null)
        {
            currentWeekDay = parentItem.DataItem as MealDayOfWeek;
        }
        if (currentWeekDay != null)
        {
            currentWeekDayId = currentWeekDay.DayId;
        }

        if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal || menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
        {
            foreach (Meal m in this.currentMenu.Meals.ToArray())
            {
                if (m.CourseTypeId == currentCourseOrMenuTypeId)
                    currentMeal = m;

                if (currentCourseOrMenuTypeId == 6)
                {
                    Label txtComments = e.Item.FindControl("LabelComments") as Label;
                    if (txtComments != null)
                    {
                        if (m.Comments != null)
                            txtComments.Text = m.Comments.Replace("\r\n", "<br />");
                        txtComments.Visible = true;
                    }
                }
            }
        }
        if (menu.MenuTypeId == (int)MenuTypeEnum.Weekly)
        {
            foreach (Meal m in this.currentMenu.Meals.ToArray())
            {
                if (m.MealTypeId == currentCourseOrMenuTypeId && m.DayIndex == currentWeekDayId)
                    currentMeal = m;

                if (currentCourseOrMenuTypeId == 5 && m.DayIndex == currentWeekDayId)
                {
                    Label txtComments = e.Item.FindControl("LabelComments") as Label;
                    if (m.Comments != null) txtComments.Text = m.Comments.Replace("\r\n", "<br />");
                    txtComments.Visible = true;
                }

            }
        }

        if (currentMeal == null)
        {
            return;
        }

        CurrentMeal = currentMeal;

        Repeater rpt = e.Item.FindControl("rptRecipes") as Repeater;

        if (rpt != null)
        {
            rpt.DataSource = currentMeal.MealRecipes.ToArray();
            rpt.DataBind();
        }
    }

    protected void rptRecipes_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        Menu menu = this.currentMenu;

        Meal meal = this.CurrentMeal;

        HyperLink lnk = e.Item.FindControl("lblRecipeName") as HyperLink;
        if (lnk != null)
        {
            lnk.NavigateUrl = "~/RecipeDetails.aspx?RecipeId=" + ((MealRecipe)e.Item.DataItem).RecipeId.ToString();
        }

        if (this.currentMenu.MenuTypeId == (int)MenuTypeEnum.Weekly)
        {            
            HtmlTableCell tdDinerNum = e.Item.FindControl("tdDinersNum") as HtmlTableCell;
            if (tdDinerNum != null)
            {
                tdDinerNum.Style["width"] = "33px";                
                Label lblDinersNum = new Label();
                lblDinersNum.Text = meal.Diners.ToString();
                tdDinerNum.Controls.Add(lblDinersNum);
            }
        }
    }

    protected void btnAddMenuToFavorites_Click(object sender, EventArgs e)
    {
        int favMenusNum = 0;
        if (this.CurrentMenuId != null && BusinessFacade.Instance.AddMenuToUserFavorites(((BasePage)this.Page).UserId, this.CurrentMenuId.Value, out favMenusNum))
        {
            this.btnAddMenuToFavorites.Visible = false;
            this.btnAddMenuToFavorites_bottom.Visible = false;
            this.btnRemoveMenuFromFavorites.Visible = true;
            this.btnRemoveMenuFromFavorites_bottom.Visible = true;
            this.upActionsTop.Update();
            this.upActionsBottom.Update();
            this.RefreshTopTags();
            this.upTopTags.Update();

            List<int> favMenusList = Utils.FavoriteMenusAdded;
            favMenusList.Add(this.CurrentMenuId.Value);
            Utils.FavoriteMenusAdded = favMenusList;

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

    protected void btnRemoveMenuFromFavorites_Click(object sender, EventArgs e)
    {
        int favMenusNum = 0;
        if (this.CurrentMenuId != null && BusinessFacade.Instance.RemoveMenuFromUserFavorites(((BasePage)this.Page).UserId, this.CurrentMenuId.Value, out favMenusNum))
        {
            this.btnAddMenuToFavorites.Visible = true;
            this.btnAddMenuToFavorites_bottom.Visible = true;
            this.btnRemoveMenuFromFavorites.Visible = false;
            this.btnRemoveMenuFromFavorites_bottom.Visible = false;
            this.upActionsTop.Update();
            this.upActionsBottom.Update();
            this.RefreshTopTags();
            this.upTopTags.Update();

            List<int> favMenusList = Utils.FavoriteMenusAdded;
            if (favMenusList.Contains(this.CurrentMenuId.Value))
            {
                favMenusList.Remove(this.CurrentMenuId.Value);
            }
            Utils.FavoriteMenusAdded = favMenusList;

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

    protected void ucSendMailToFriend_EmailSent(object sender, SendToFriendEventArgs e)
    {
        ((BasePage)this.Page).DisplayMessage = "אימייל נשלח ל- " + e.recipentName;
    }

    //protected void btnSendMail_Click(object sender, EventArgs e)
    //{
        //Doesn't work - when method is called currentMenu is null? why?

        //StringWriter html = new StringWriter();
        //Server.Execute("~/PrintMenu.aspx?menuId=" + this.currentMenu.MenuId.ToString(), html);
        //string to = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId).Email;
        //try
        //{
        //    ProperServices.Common.Mail.Mailer.SendMail(to, ProperControls.General.Utils.FromEmail, BusinessFacade.Instance.GetMenu(this.currentMenu.MenuId).MenuName, html.ToString(), true);

        //    this.lblResult.Visible = true;
        //    this.lblResult.Text = "התפריט נשלח ל-" + to;
        //}
        //catch
        //{
        //    this.lblResult.Visible = true;
        //    this.lblResult.Text = "בעיה בשליחה";
        //}       
    //}

    protected void btnDeleteMenu_Click(object sender, EventArgs e)
    {
        try
        {
            if (CurrentMenuId.HasValue && BusinessFacade.Instance.DeleteMenu(CurrentMenuId.Value))
            {
                Response.Redirect("~/Menus.aspx");
            }
        }
        catch (Exception exception)
        {
            PopupMessage(exception.Message);
        }
    }

    protected string GetCategoriesString(MenuCategory[] categories)
    {
        string str = "";
        foreach (MenuCategory cat in categories)
        {
            str += cat.MCategory.MCategoryName + ", ";
        }

        if (str.Length > 2)
        {
            str = str.Remove(str.Length - 2);
        }

        return str;
    }

    protected string AdjustVideo(string embeddedVideoString)
    {
        string str = embeddedVideoString;
        int embedIndex = str.IndexOf("embed");
        int heightIndex = str.IndexOf("height=", embedIndex);
        string heightValue = str.Substring(heightIndex + "height=".Length + 1, 3);
        int widthIndex = str.IndexOf("width=", embedIndex);
        string widthValue = str.Substring(widthIndex + "width=".Length + 1, 3);
        str = str.Replace(widthValue, "330");
        str = str.Replace(heightValue, "236");
        return str;
    }

    protected void SaveImage(object sender, EventArgs e)
    {
        if (!CurrentMenuId.HasValue) return;

        Menu menu = BusinessFacade.Instance.GetMenu(CurrentMenuId.Value);
        string category = "MenusScreenShots";

        string savePath = Server.MapPath(string.Format(@"~\Images\{0}\", category));

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        string t_strLargeImage = string.Format(@"{0}{1}.jpg", savePath, menu.MenuName);

        // Create instance
        ThumbMakerClass t_xThumbMaker = new ThumbMakerClass();

        t_xThumbMaker.SetURL(string.Format("http://{0}{1}/ScreenShotMenu.aspx?menuId={2}&action=screenshot", Request.Url.Host,
                                           Request.ApplicationPath, CurrentMenuId));
        t_xThumbMaker.SetRegInfo("KRMAXARQW-XTABNYBXW-KMQXRWMKB-BNTQABQTE");
        t_xThumbMaker.StartSnap();

        // Save the image with full size in C#
        bool saveImage = t_xThumbMaker.SaveImage(t_strLargeImage.Replace(@"""", "``"));

        if (saveImage)
        {
            Response.Redirect("ScreenShotMenu.aspx?menuId=" + CurrentMenuId);
        }
    }

}
