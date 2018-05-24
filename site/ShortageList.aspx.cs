using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using ProperControls.Pages;

public partial class ShortageList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SRL_User user = (SRL_User)Session[AppConstants.SITE_USER];
        if (user == null)
            return;

        if (!IsPostBack)
        {
            int listId;
            //List<SRL_Ingredient> shortageList = BusinessFacade.Instance.GetShortageList(user.UserId, out listId);
            //ucIngridians1.Ingredients = shortageList;
            ucIngridians1.AddItem += AfterAddItem;
        }
    }

    private void AfterAddItem(SRL_Ingredient ingredient)
    {
        ucSummeryList1.AddIngridiant(ingredient);
    }

    protected void SaveShortageList(object sender, ImageClickEventArgs e)
    {
        SRL_User user = (SRL_User)Session[AppConstants.SITE_USER];
        if (user == null)
            return;

        List<SRL_Ingredient> currentIngredientList = ucIngridians1.Ingredients;
        int listId;
        //List<SRL_Ingredient> savedIngrediantList = BusinessFacade.Instance.GetShortageList(user.UserId, out listId);

        int summeryId = BusinessFacade.Instance.GetSummeryList(user.UserId);
        List<SRL_Ingredient> summeryList = BusinessFacade.Instance.GetSummeryListDetails(summeryId);

        //if (savedIngrediantList != null)
        //{
        //    foreach (SRL_Ingredient ingredient in savedIngrediantList)
        //    {
        //        string foodName = ingredient.FoodName;
        //        SRL_Ingredient find = currentIngredientList.ToList().Find(f => f.FoodName == foodName);
        //        if (find == null)
        //        {
        //            BusinessFacade.Instance.DeleteShortageListItem(ingredient.IngredientId);

        //            SRL_Ingredient ingredient1 = ingredient;
        //            SRL_Ingredient summeryIngrediant = summeryList.Find(f => f.FoodId == ingredient1.FoodId);
        //            BusinessFacade.Instance.DeleteSummeryListItem(summeryId, listId, summeryIngrediant);
        //        }
        //    }
        //}

        //listId = BusinessFacade.Instance.AddShortageList(user.UserId);
        int summeryListId = BusinessFacade.Instance.AddSummeryList(user.UserId);

        foreach (SRL_Ingredient ingredient in currentIngredientList)
        {
            //bool result = BusinessFacade.Instance.AddShortageListItem(ingredient, listId);
            //bool addSummeryListItem = BusinessFacade.Instance.AddSummeryListItem(ingredient, summeryListId, listId);
        }

        ucSummeryList1.RefreshList();
    }
}
