using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;
using MyBuyList.BusinessLayer.Managers;

using ProperControls.Pages;

using Resources;
using Menu = MyBuyList.Shared.Entities.Menu;

public partial class PrintWeeklyMenu : BasePage
{
    private MyBuyList.Shared.Entities.Menu currMenu;
    private Meal currMeal;

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
        if (!string.IsNullOrEmpty(Request["action"]))
            DoScreenShot();
        else
            SaveScreenShot();
    }

    private void SaveScreenShot()
    {
        if (string.IsNullOrEmpty(Request["menuId"])) return;

        int menuId = Convert.ToInt32(Request["menuId"]);
        Menu menu = BusinessFacade.Instance.GetMenu(menuId);
        string category = "MenusScreenShots";

        byte[] data;
        string url;
        using (WebClient client = new WebClient())
        {
            url = string.Format("http://{0}{1}/Images/{2}/{3}.jpg", Request.Url.Host, Request.ApplicationPath,
                                       category, menu.MenuName.Replace(@"""","``"));
            data = client.DownloadData(url);
        }
        try
        {

            string fileName = HttpUtility.UrlPathEncode(string.Format("{0}.jpg", menu.MenuName));

            Response.Clear();
            Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            Response.Buffer = true;
            Response.AddHeader("Content-Length", data.Length.ToString());
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
            Response.AddHeader("Expires", "0");
            Response.AddHeader("Pragma", "cache");
            Response.AddHeader("Cache-Control", "no-cache");
            Response.ContentType = string.Format("image/JPEG");
            Response.AddHeader("Accept-Ranges", "bytes");
            Response.BinaryWrite(data);
            Response.Flush();
            Response.End();
        }
        catch (Exception)
        {
        }
    }

    private void DoScreenShot()
    {
        if (!string.IsNullOrEmpty(Request["menuId"]))
        {
            int menuId = int.Parse(Request["menuId"]);

            MyBuyList.Shared.Entities.Menu menu = BusinessFacade.Instance.GetMenuEx(menuId);

            if (menu != null && menu.IsDeleted == false && (menu.IsPublic == true || menu.UserId == ((BasePage)this.Page).UserId || ((BasePage)this.Page).UserType == 1))
            {
                Page.Title = "הדרך המטעימה לארגון הרשימה - MyBuyList";

                this.currentMenu = menu;
                this.BindMenuDetails(menu);

                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Key", "javascript:window.print();", true);
            }
            else
            {
                AppEnv.MoveToDefaultPage();
            }
        }
        else
        {
            AppEnv.MoveToDefaultPage();
        }
    }

    private void BindMenuDetails(MyBuyList.Shared.Entities.Menu menu)
    {
        this.lblTitle2.Text = menu.MenuName;
        //this.lblCategories.Text = this.GetCategoriesString(menu.MenuCategories.ToArray());
        //this.lnkPublisher.Text = menu.User.DisplayName;
        //this.lblPublishDate.Text = menu.ModifiedDate.ToString("dd/MM/yyyy");
        this.lblDescription.Text = menu.Description;
        //this.lblMenuTags.Text = menu.Tags;

        if (menu.Picture != null)
        {
            this.imgMenuPicture.ImageUrl = "~/ShowPicture.ashx?MenuId=" + menu.MenuId;
        }        
        else
        {
            this.imgMenuPicture.ImageUrl = "~/Images/Img_Default.jpg";  
        }

        bool isInMyFavorites = (menu.UserFavoriteMenus.SingleOrDefault(ufm => ufm.UserId == ((BasePage)this.Page).UserId) != null);

        //if (isInMyFavorites)
        //{
        //    this.myFavoritesTopTag.Visible = true;
        //}
        //else
        //{
        //    this.myFavoritesTopTag.Visible = false;
        //}

        //this.lblAllFavorites.Text = menu.UserFavoriteMenus.Count.ToString();

        if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal || menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
        {
            this.MenuDinersNum.Visible = true;

            if (menu.Meals.Count > 0 && menu.Meals.ToArray()[0].Diners != null)
            {
                this.lblNoDiner.Text = menu.Meals.ToArray()[0].Diners.ToString();
            }

            MealDayOfWeek[] fakeDaysOfWeek = new MealDayOfWeek[1] { new MealDayOfWeek(1, MyGlobalResources.Sunday) };
            this.rptDays.DataSource = fakeDaysOfWeek;
            this.rptDays.DataBind();
        }

        if (menu.MenuTypeId == (int)MenuTypeEnum.Weekly)
        {
            this.rptDays.DataSource = this.MealDayOfWeek;
            this.rptDays.DataBind();
        }
    }

    protected void rptDays_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        MyBuyList.Shared.Entities.Menu menu = this.currentMenu;

        if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal || menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
        {
            System.Web.UI.WebControls.Image tableTopImag = e.Item.FindControl("imgTableTop") as System.Web.UI.WebControls.Image;
            tableTopImag.ImageUrl = "~/Images/bgr_TableMenu_toPrint.jpg";

            Repeater rptCourses = e.Item.FindControl("rptCourses") as Repeater;


            var list = from item in BusinessFacade.Instance.GetCourseTypes()
                       select new CourseOrMealType(item.CourseTypeId, item.CourseTypeName);
            CourseOrMealType[] courseTypesArray = list.ToArray();

            if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal)
            {
                CourseOrMealType tempCourseType = courseTypesArray[0];
                for (int i = 0; i < (courseTypesArray.Length - 1); i++)
                {
                    courseTypesArray[i] = courseTypesArray[i + 1];
                }
                courseTypesArray[courseTypesArray.Length - 1] = tempCourseType;
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
                tableTopImag.ImageUrl = "~/Images/bgr_Table" + mdow.DayId + "_toPrint.jpg";
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
        MyBuyList.Shared.Entities.Menu menu = this.currentMenu;

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
            }
        }
        if (menu.MenuTypeId == (int)MenuTypeEnum.Weekly)
        {
            foreach (Meal m in this.currentMenu.Meals.ToArray())
            {
                if (m.MealTypeId == currentCourseOrMenuTypeId && m.DayIndex == currentWeekDayId)
                    currentMeal = m;
            }
        }

        if (currentMeal == null)
        {
            return;
        }

        this.CurrentMeal = currentMeal;

        Repeater rpt = e.Item.FindControl("rptRecipes") as Repeater;

        if (rpt != null)
        {
            rpt.DataSource = currentMeal.MealRecipes.ToArray();
            rpt.DataBind();
        }
    }

    protected void rptRecipes_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        MyBuyList.Shared.Entities.Menu menu = this.currentMenu;

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
}
