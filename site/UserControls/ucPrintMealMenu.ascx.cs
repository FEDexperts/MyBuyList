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

public partial class UserControls_ucPrintMenu : System.Web.UI.UserControl
{
    int MenuId
    {
        get { return ViewState["MenuId"] == null ? 0 : (int)ViewState["MenuId"]; }
        set { ViewState["MenuId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Bind(int menuId)
    {
        this.MenuId = menuId;
        int menuTypeId = BusinessFacade.Instance.GetMenuType(this.MenuId).MenuTypeId;

        if (menuTypeId == (int)MenuTypeEnum.QuickMenu)
        {
            this.rpCourses.DataSource = BusinessFacade.Instance.GetCourseTypes().Where(ct => ct.CourseTypeId == 0);
        }
        else
        {
            this.rpCourses.DataSource = BusinessFacade.Instance.GetCourseTypes().Where(ct => ct.CourseTypeId != 0);
        }
        this.rpCourses.DataBind();
    }

    protected void Course_DataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem rptItem = e.Item as RepeaterItem;
        CourseType courseType = (rptItem.DataItem as CourseType);

        Meal[] Meal = (BusinessFacade.Instance.GetMealsList(this.MenuId)).Where(m => m.CourseTypeId == courseType.CourseTypeId).ToArray<Meal>();

        if (Meal.Count() == 1)
        {
            Label lblDiners = rptItem.FindControl("lblDiners") as Label;
            lblDiners.Text = Meal[0].Diners.ToString();

            Repeater rpRecipes = rptItem.FindControl("rpMealRecipes") as Repeater;
            rpRecipes.DataSource = Meal[0].MealRecipes;
            rpRecipes.DataBind();
        }
    }
}
