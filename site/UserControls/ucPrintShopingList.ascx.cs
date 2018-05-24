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
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;

public partial class UserControls_ucPrintShopingList : System.Web.UI.UserControl
{
    int MenuId
    {
        get { return ViewState["MenuId"] == null ? 0 : (int)ViewState["MenuId"]; }
        set { ViewState["MenuId"] = value; }
    }
    
    private ShoppingFood[] foodsList;

    public void Bind(int menuId)
    {
        this.MenuId = menuId;

        this.foodsList = BusinessFacade.Instance.GetMenuShoppingList(this.MenuId);
        
        this.rpDep.DataSource = BusinessFacade.Instance.GetMenuShopDepartments(menuId);
        this.rpDep.DataBind();

        this.rpAdditionalsItems.DataSource = BusinessFacade.Instance.GetShoppingListAdditionalItems(menuId);
        this.rpAdditionalsItems.DataBind();

        this.rpRecipes.DataSource = BusinessFacade.Instance.GetRecipesInMenuMeals(menuId).Values;
        this.rpRecipes.DataBind();
    }

    protected void Dep_DataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem rptItem = e.Item as RepeaterItem;
        int shopDepId = (rptItem.DataItem as ShopDepartment).ShopDepartmentId;

        if (this.foodsList != null)
        {
            Repeater rptFoods = rptItem.FindControl("rptFoods") as Repeater;
            rptFoods.DataSource = this.foodsList.Where(food => food.ShopDepartmentId == shopDepId).ToArray();
            rptFoods.DataBind();
        }

    }

    protected void rptFoods_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem rptItem = e.Item as RepeaterItem;
        ShoppingFood foodItem = (ShoppingFood)rptItem.DataItem;
        if(foodItem == null) return;

        Label food = rptItem.FindControl("lblFood") as Label;

        if (foodItem.CalculateUnitId == (int)AppConstants.GRAMM_UNIT_ID)
        {
            food.Text = string.Format("{0} {1}", foodItem.Display, foodItem.FoodName);
        }
        else
        {
            string grams = BusinessFacade.Instance.GetIngredientInGrams(foodItem, foodItem.CalculateUnitId);

            if (grams != string.Empty)
            {
                food.Text = string.Format("{0} / {1} {2}", foodItem.Display, grams, foodItem.FoodName);
            }
            else
            {
                food.Text = string.Format("{0} {1}", foodItem.Display, foodItem.FoodName);
            }
        }

        if (foodItem.Picture != null && foodItem.PrintPicture)
        {
            Image foodImage = rptItem.FindControl("imgFood") as Image;
            foodImage.Visible = true;
            foodImage.ImageUrl = "http://www.mybuylist.com/ShowPicture.ashx?FoodId=" + foodItem.FoodId;
        }
    }

    protected void AdditionalsItem_DataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem rptItem = e.Item as RepeaterItem;
        ShoppingListAdditionalItem shopAdditional = (rptItem.DataItem as ShoppingListAdditionalItem);

        Label name = rptItem.FindControl("lblItemName") as Label;

        if (shopAdditional.GeneralItem != null)
        {
            name.Text = shopAdditional.GeneralItem.GeneralItemName;
        }
        else if (shopAdditional.ItemName != null)
        {
            name.Text = shopAdditional.ItemName;
        }
    }

    
}
