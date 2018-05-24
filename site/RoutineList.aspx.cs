using System;
using System.Collections.Generic;
using System.Web.UI;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using ProperControls.Pages;

    public partial class RoutineList : BasePage
    {
        private SRL_User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (SRL_User)Session[AppConstants.SITE_USER];

            if (!IsPostBack)
            {
                LoadRoutineLists();
            }
        }

        private void LoadRoutineLists()
        {
            if (user != null)
            {
                //Dictionary<int, string> list = BusinessFacade.Instance.GetRoutineList(user.UserId);
                //ddlNames.DataSource = list;
            }
            ddlNames.DataBind();
        }

        protected void SaveRoutineList(object sender, ImageClickEventArgs e)
        {
            user = (SRL_User)Session[AppConstants.SITE_USER];

            int summeryId = BusinessFacade.Instance.GetSummeryList(user.UserId);
            List<SRL_Ingredient> summeryList = BusinessFacade.Instance.GetSummeryListDetails(summeryId);

            List<SRL_Ingredient> currentIngredients = ucIngridians1.Ingredients;

            int listId = 0;
            //if (rbNewList.Checked)
            //    listId = BusinessFacade.Instance.AddRoutineList(user.UserId, txtName.Text);

            if (rbFromList.Checked)
                int.TryParse(ddlNames.SelectedValue, out listId);

            if (listId == 0) return;

            //List<SRL_Ingredient> routineList = BusinessFacade.Instance.GetRoutineListDetails(listId);
            //if (routineList != null)
            //    foreach (SRL_Ingredient ingredient in routineList)
            //    {
            //        int foodId = ingredient.FoodId;
            //        SRL_Ingredient find = currentIngredients.Find(f => f.FoodId == foodId);
            //        if (find == null)
            //        {
            //            BusinessFacade.Instance.DeleteRoutineListItem(ingredient.IngredientId);

            //            SRL_Ingredient summeryIngrediant = summeryList.Find(f => f.FoodId == foodId);
            //            BusinessFacade.Instance.DeleteSummeryListItem(summeryId, listId, summeryIngrediant);
            //        }
            //    }

            int summeryListId = BusinessFacade.Instance.AddSummeryList(user.UserId);

            foreach (SRL_Ingredient ingredient in currentIngredients)
            {
                //bool result = BusinessFacade.Instance.AddRoutineListItem(ingredient, listId);
                bool addSummeryListItem = BusinessFacade.Instance.AddSummeryListItem(ingredient, summeryListId, listId);
            }

            txtName.Text = string.Empty;

            ucSummeryList1.RefreshList();
        }

        protected void ListSelected(object sender, EventArgs e)
        {
            LoadListDetails();
        }

        private void LoadListDetails()
        {
            int listId = Convert.ToInt32(ddlNames.SelectedValue);
            //List<SRL_Ingredient> routineList = BusinessFacade.Instance.GetRoutineListDetails(listId);
            //ucIngridians1.Ingredients = routineList;
        }

        protected void ListsBound(object sender, EventArgs e)
        {
            if (ddlNames.Items.Count != 0)
            {
                LoadListDetails();
            }
        }

        protected void NewListChecked(object sender, EventArgs e)
        {
            ucIngridians1.Ingredients = null;
        }

        protected void ListChecked(object sender, EventArgs e)
        {
            LoadListDetails();
        }

        protected void DeleteList(object sender, EventArgs e)
        {
            int listId = Convert.ToInt32(ddlNames.SelectedValue);
            //BusinessFacade.Instance.DeleteRoutineList(listId);
            LoadRoutineLists();
        }
    }
