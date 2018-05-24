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

public partial class ucMealMenu : System.Web.UI.UserControl
{
    private int MenuId
    {
        get { return ViewState["MenuId"] == null ? 0 : (int)ViewState["MenuId"]; }
        set { ViewState["MenuId"] = value; }
    }

    public int MenuTypeId
    {
        get { return ViewState["MenuTypeId"] == null ? 0 : (int)ViewState["MenuTypeId"]; }
        set { ViewState["MenuTypeId"] = value; }
    }

    [Serializable]
    public class SRL_CourseType
    {
        public int CourseTypeId { get; set; }
        public string CourseTypeName { get; set; }

        public SRL_CourseType(int id, string name)
        {
            this.CourseTypeId = id;
            this.CourseTypeName = name;
        }
    }
    private SRL_CourseType[] CourseTypes
    {
        get
        {
            if (ViewState["CourseTypes"] == null)
            {
                var list = from item in BusinessFacade.Instance.GetCourseTypes()
                           select new SRL_CourseType(item.CourseTypeId, item.CourseTypeName);

                ViewState["CourseTypes"] = list.ToArray();

            }
            return (SRL_CourseType[])ViewState["CourseTypes"];
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.Request["menuId"]))
            {
                this.MenuId = int.Parse(this.Request["menuId"]);
                this.MenuTypeId = BusinessFacade.Instance.GetMenuType(this.MenuId).MenuTypeId;
                
                if (this.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
                {
                    this.dayNameCell.Style["background-color"] = "#DAE58E";
                }
                else
                {                   
                    this.dayNameCell.Style["background-image"] = "url('Images/MealMenuBackground.jpg')";
                }

                this.Rebind();
            }
        }
    }

    private void Rebind()
    {
        var list = from item in BusinessFacade.Instance.GetMealsList(this.MenuId)
                   select new SRL_Meal(item);
        this.MealsList = list.ToArray();

        if (this.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
        {
            this.rptCourseTypes.DataSource = this.CourseTypes.Where(ct => ct.CourseTypeId == 0);
        }
        else
        {
            this.rptCourseTypes.DataSource = this.CourseTypes.Where(ct => ct.CourseTypeId != 0);
        }
        this.rptCourseTypes.DataBind();
    }

    protected void btnClearAll_Click(object sender, EventArgs e)
    {
        if (BusinessFacade.Instance.ClearAllMeals(this.MenuId))
        {
            Rebind();
        }
    }

    protected void rptCourseTypes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem rptItem = e.Item as RepeaterItem;
        HtmlTableCell cell = rptItem.FindControl("mealTypeCell") as HtmlTableCell;
        HtmlTableCell parentCell = rptItem.Parent.Parent as HtmlTableCell;

        object courseTypeItem = rptItem.DataItem;
        int courseTypeId = (rptItem.DataItem as SRL_CourseType).CourseTypeId;

        SRL_Meal meal = this.MealsList.SingleOrDefault(ml => ml.CourseTypeId == courseTypeId);
        if (meal != null)
        {
            Repeater rpt = cell.FindControl("rptMealRecipes") as Repeater;
            if (rpt != null)
            {
                rpt.DataSource = meal.MealRecipes;
                rpt.DataBind();
            }
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
}


