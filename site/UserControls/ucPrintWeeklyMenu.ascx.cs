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
public partial class UserControls_PrintWeeklyMenu : System.Web.UI.UserControl
{
    int MenuId
    {
        get { return ViewState["MenuId"] == null ? 0 : (int)ViewState["MenuId"]; }
        set { ViewState["MenuId"] = value; }
    }

    int Day
    {
        get { return ViewState["Day"] == null ? 0 : (int)ViewState["Day"]; }
        set { ViewState["Day"] = value; }
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
        
    }

    public void Bind(int menuId)
    {
        this.MenuId = menuId;

        int[] days = BusinessFacade.Instance.GetMenuDays(menuId);
        //int[] days = new int[]{1, 2, 3, 4, 5, 6, 7};

        this.rpDays.DataSource = days;
        this.rpDays.DataBind();
    }

    protected void Day_DataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem rptItem = e.Item as RepeaterItem;
        int day = (int)rptItem.DataItem;

        Label lblDay = rptItem.FindControl("lblDay") as Label;

        if (day <= 7)
        {
            lblDay.Text = this.MealDayOfWeek.Single(m => m.DayId == day).DayName;
        }
        else
        {
            int n = day%7;
            if (n != 0)
            {
                lblDay.Text = this.MealDayOfWeek.Single(m => m.DayId == n).DayName;
            }
            else
            {
                lblDay.Text = this.MealDayOfWeek.Single(m => m.DayId == 7).DayName;
            }
        }

        this.Day = day;
        Repeater rpDayMeals = rptItem.FindControl("rpDayMeals") as Repeater;
        rpDayMeals.DataSource = BusinessFacade.Instance.GetMealTypes();
        rpDayMeals.DataBind();

    }

    protected void DayMeals_DataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem rptItem = e.Item as RepeaterItem;
        MealType mealType = (rptItem.DataItem as MealType);

        Meal[] meals = (BusinessFacade.Instance.GetMealsList(this.MenuId)).Where(m => m.DayIndex == this.Day && m.MealTypeId == mealType.MealTypeId).ToArray<Meal>();
        if (meals.Count() == 1)
        {
            Label lblDiners = rptItem.FindControl("lblDiners") as Label;
            
            //lblDiners.Text = meals[0].Diners.Value.ToString() + " סועדים";
            lblDiners.Text = meals[0].Diners.ToString() + " סועדים";

            Repeater rpMealRecipes = rptItem.FindControl("rpMealRecipes") as Repeater;
            rpMealRecipes.DataSource = meals[0].MealRecipes;
            rpMealRecipes.DataBind();
        }

    }

    protected void Recipe_DataBound(object sender, RepeaterItemEventArgs e)
    {
      
        

    }
}
