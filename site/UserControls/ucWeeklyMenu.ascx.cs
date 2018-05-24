using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using MyBuyList.BusinessLayer.Managers;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;

using Resources;

public partial class ucWeeklyMenu : System.Web.UI.UserControl
{
    int MenuId
    {
        get { return ViewState["MenuId"] == null ? 0 : (int)ViewState["MenuId"]; }
        set { ViewState["MenuId"] = value; }
    }

    public int MenuTypeId
    {
        get { return ViewState["MenuTypeId"] == null ? 0 : (int)ViewState["MenuTypeId"]; }
        set { ViewState["MenuTypeId"] = value; }
    }

    int CurrentWeek
    {
        get { return ViewState["CurrentWeek"] == null ? 0 : (int)ViewState["CurrentWeek"]; }
        set { ViewState["CurrentWeek"] = value; }
    }

    [Serializable]
    public class SRL_MealType
    {
        public int MealTypeId { get; set; }
        public string MealTypeName { get; set; }

        public SRL_MealType(int id, string name)
        {
            this.MealTypeId = id;
            this.MealTypeName = name;
        }
    }

    private SRL_MealType[] MealTypes
    {
        get
        {
            if (ViewState["MealTypes"] == null)
            {
                var list = from item in BusinessFacade.Instance.GetMealTypes()
                           select new SRL_MealType(item.MealTypeId, item.MealTypeName);

                ViewState["MealTypes"] = list.ToArray();

            }
            return (SRL_MealType[])ViewState["MealTypes"];
        }
    }

    private SRL_Meal[] MealsList
    {
        get
        {
            return (SRL_Meal[])ViewState["MealsList"];
        }
        set
        {
            ViewState["MealsList"] = value;
        }
    }

    MealDayOfWeek[] MealDayOfWeek
    {
        get
        {
            if (ViewState["MealDayOfWeek"] == null)
            {
                MealDayOfWeek[] array = new MealDayOfWeek[] {
                    new MealDayOfWeek(1, MyGlobalResources.Sunday),
                    new MealDayOfWeek(2, MyGlobalResources.Monday),
                    new MealDayOfWeek(3, MyGlobalResources.Tuesday),
                    new MealDayOfWeek(4, MyGlobalResources.Wednesday),
                    new MealDayOfWeek(5, MyGlobalResources.Thursday),
                    new MealDayOfWeek(6, MyGlobalResources.Friday),
                    new MealDayOfWeek(7, MyGlobalResources.Saturday)
                };

                ViewState["MealDayOfWeek"] = array;
                return array;
            }
            else
            {
                return (MealDayOfWeek[])ViewState["MealDayOfWeek"];
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.Request["menuId"]))
            {
                this.MenuId = int.Parse(this.Request["menuId"]);
                
                Rebind();

                if (this.MenuTypeId == (int)MenuTypeEnum.ManyWeeks)
                {
                    this.btnPrevWeek.Visible = (bool)(this.CurrentWeek > 0);
                    this.lblWeekNumber.Text = MyGlobalResources.WeekNo + " " + (this.CurrentWeek + 1);
                }
                else
                {
                    this.btnNextWeek.Visible = false;
                    this.btnPrevWeek.Visible = false;
                    this.lblWeekNumber.Visible = false;
                }
            }
        }
    }

    public void Rebind()
    {
        var list = from item in BusinessFacade.Instance.GetMealsList(this.MenuId)
                   select new SRL_Meal(item);
        this.MealsList = list.ToArray();

        rptDayWeekList.DataSource = this.MealDayOfWeek;
        rptDayWeekList.DataBind();
    }

    protected void rptDayWeekList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem rptItem = e.Item as RepeaterItem;
        HtmlTableCell cell = rptItem.FindControl("dayNameCell") as HtmlTableCell;

        if (this.MenuTypeId == (int)MenuTypeEnum.Weekly)
        {
            cell.Style["background-image"] = "url('Images/WeeklyBackground.jpg')";
        }
        else if (this.MenuTypeId == (int)MenuTypeEnum.ManyWeeks)
        {
            cell.Style["background-image"] = "url('Images/MonthlyBackground.jpg')";
        }

        cell = rptItem.FindControl("dayWeekCell") as HtmlTableCell;
        Repeater rptMealTypes = cell.FindControl("rptMealTypes") as Repeater;
        rptMealTypes.DataSource = this.MealTypes;
        rptMealTypes.DataBind();
    }

    protected void rptMealTypes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem rptItem = e.Item as RepeaterItem;
        HtmlTableCell cell = rptItem.FindControl("mealTypeCell") as HtmlTableCell;
        HtmlTableCell parentCell = rptItem.Parent.Parent as HtmlTableCell;

        HiddenField hidDayIndex = parentCell.FindControl("hidDayIndex") as HiddenField;

        object mealTypeItem = rptItem.DataItem;
        int mealTypeId = (rptItem.DataItem as SRL_MealType).MealTypeId;
        if (hidDayIndex.Value != "")
        {
            int dayIndex = int.Parse(hidDayIndex.Value) + 7 * CurrentWeek;
            cell.Attributes["dayIndex"] = dayIndex.ToString();

            SRL_Meal meal = this.MealsList.SingleOrDefault(ml => ml.DayIndex == dayIndex &&
                                                                 ml.MealTypeId == mealTypeId);
            if (meal != null)
            {
                Repeater rpt = cell.FindControl("rptMealRecipes") as Repeater;
                if (rpt != null)
                {
                    rpt.DataSource = meal.MealRecipes;
                    rpt.DataBind();
                }
            }

            //cell = rptItem.FindControl("dinersCell") as HtmlTableCell;
            //TextBox txtDiners = cell.FindControl("txtDiners") as TextBox;
            //if (meal != null)
            //{
            //    txtDiners.Text = meal.Diners.ToString();
            //}
            //txtDiners.Attributes["OnChange"] = string.Format("DinersChange(this,{0},{1},{2})", this.MenuId, dayIndex, mealTypeId);

        }
    }

    protected void rptMealRecipes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem rptItem = e.Item as RepeaterItem;
        SRL_MealRecipe data = rptItem.DataItem as SRL_MealRecipe;

        TextBox txtServings = rptItem.FindControl("txtServings") as TextBox;
        txtServings.Attributes["mealId"] = data.MealId.ToString();
        txtServings.Attributes["recipeId"] = data.RecipeId.ToString();
        txtServings.Attributes["OnChange"] = "Servings_Change(event);";
    }

    protected void btnPrevWeek_Click(object sender, EventArgs e)
    {
        if (this.CurrentWeek > 0)
        {
            this.CurrentWeek = this.CurrentWeek - 1;
            this.btnPrevWeek.Visible = (bool)(this.CurrentWeek > 0);
            this.lblWeekNumber.Text = MyGlobalResources.WeekNo + " " + (this.CurrentWeek + 1);

            Rebind();
        }
    }

    protected void btnNextWeek_Click(object sender, EventArgs e)
    {
        this.CurrentWeek = this.CurrentWeek + 1;
        this.btnPrevWeek.Visible = (bool)(this.CurrentWeek > 0);
        this.lblWeekNumber.Text = MyGlobalResources.WeekNo + " " + (this.CurrentWeek + 1);

        Rebind();
    }

    protected void btnClearAll_Click(object sender, EventArgs e)
    {
        this.CurrentWeek = 0;
        this.btnPrevWeek.Visible = false;
        this.lblWeekNumber.Text = MyGlobalResources.WeekNo + " " + 1;

        if (BusinessFacade.Instance.ClearAllMeals(this.MenuId))
        {
            Rebind();
        }
    }
}
