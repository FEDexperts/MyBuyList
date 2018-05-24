using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using Resources;

namespace UserControls
{
    public partial class UcSummeryList : System.Web.UI.UserControl
    {
        public List<SRL_Ingredient> Ingredients
        {
            get
            {
                if (ViewState["Ingredients"] == null)
                {
                    ViewState["Ingredients"] = new List<SRL_Ingredient>();
                }
                return (List<SRL_Ingredient>)ViewState["Ingredients"];
            }
            set
            {
                if (value == null)
                {
                    dlistIngredients.DataSource = null;
                }
                else
                {
                    dlistIngredients.DataSource = value.ToArray();
                }

                ViewState["Ingredients"] = value;
                dlistIngredients.DataBind();
            }
        }

        public int Height
        {
            set { pnlIngredients.Height = Unit.Pixel(value); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        protected void dlistIngredients_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            
        }

        protected void btnUpdateIngredient_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnRemoveIngredient_Click(object sender, EventArgs e)
        {
            
        }

        public void AddIngridiant(SRL_Ingredient ingredient)
        {
            List<SRL_Ingredient> list = Ingredients;

            string foodName = ingredient.FoodName;
            SRL_Ingredient tIngredient = list.Find(f => f.FoodName == foodName);
            if (tIngredient == null)
            {
                list.Add(ingredient);
            }

            Ingredients = list;

            dlistIngredients.DataSource = Ingredients.ToArray();
            dlistIngredients.DataBind();
        }

        public void RefreshList()
        {
            SRL_User user = (SRL_User)Session[AppConstants.SITE_USER];
            if (user == null)
                return;

            int listId = BusinessFacade.Instance.GetSummeryList(user.UserId);
            List<SRL_Ingredient> summeryListDetails = BusinessFacade.Instance.GetSummeryListDetails(listId);
            Ingredients = summeryListDetails;
        }
    }
}