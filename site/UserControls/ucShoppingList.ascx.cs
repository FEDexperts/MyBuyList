using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using ProperControls.General;
using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;

namespace UserControls
{
    public partial class UcShoppingList : System.Web.UI.UserControl
    {
        public int UserId;
        IEnumerable<UserShoppingList> shoppingListItems;

        public int NumOfSelectedRecipes
        {
            get;
            set;
        }

        public int NumOfSelectedMenus
        {
            get;
            set;
        }

        private string Quantity(string quantity)
        {
            string displayQuantity = "";
            string[] arr = quantity.Split(new string[] { ".", "," }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length > 0)
            {
                if (arr[0] != "0")
                    displayQuantity = arr[0];
            }
            if (arr.Length > 1 && arr[1] != "" && arr[1] != "00")
            {
                while (arr[1].EndsWith("0"))
                {
                    arr[1] = arr[1].Remove(arr[1].Length - 1, 1);
                }

                if (arr[1] == "25")
                {
                    displayQuantity = "¼" + displayQuantity;
                }
                else if (arr[1] == "3" || arr[1] == "33" || arr[1] == "34")
                {
                    displayQuantity = "⅓" + displayQuantity;
                }
                else if (arr[1] == "5")
                {
                    displayQuantity = "½" + displayQuantity;
                }
                else if (arr[1] == "6" || arr[1] == "66" || arr[1] == "67")
                {
                    displayQuantity = "⅔" + displayQuantity;
                }
                else if (arr[1] == "75")
                {
                    displayQuantity = "¾" + displayQuantity;
                }
                else
                {
                    displayQuantity = quantity;
                    while (displayQuantity.EndsWith("0"))
                    {
                        displayQuantity = displayQuantity.Remove(displayQuantity.Length - 1, 1);
                    }
                    if (displayQuantity.EndsWith("."))
                    {
                        displayQuantity = displayQuantity.Remove(displayQuantity.Length - 1, 1);
                    }
                }
            }
            return displayQuantity;
        }

        public void UpdateList()
        {
            BindShoppingList();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            //BindShoppingList();
            BindMenusList();
            //BindMeasureUnits();
        }

        //private void BindMeasureUnits()
        //{
        //    MeasurementUnit[] measurementUnit = BusinessFacade.Instance.GetMeasurementUnitsList(MeasurementUnitTarget.shoppingList);
        //    MeasureMentId.DataSource = measurementUnit;
        //    MeasureMentId.DataBind();
        //}

        private void BindMenusList()
        {
            int currentUserId = ((BasePage)Page).UserId;
            if (currentUserId != -1)
            {
                MyBuyList.Shared.Entities.Menu[] userMenus = BusinessFacade.Instance.GetMenusList(currentUserId);
                MyBuyList.Shared.Entities.Menu[] userMenus1 = userMenus.OrderBy(um => um.MenuName).ToArray();
                rptUserMenus.DataSource = userMenus1;
                rptUserMenus.DataBind();
            }
        }

        protected void rptUserMenus_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HyperLink hp = e.Item.FindControl("lnkMenu") as HyperLink;
            hp.NavigateUrl = string.Format("~/MenuEdit.aspx?menuId={0}", ((MyBuyList.Shared.Entities.Menu)e.Item.DataItem).MenuId);
        }

        private void BindShoppingList()
        {
            User user = ((BasePage)Page).CurrUser;
            if (user != null)
            {
                UserId = user.UserId;
                PanelShoppingList.Visible = UserId != -1;

                if (UserId != -1)
                {
                    Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;
                    foreach (KeyValuePair<int, Recipe> item in Utils.SelectedRecipes)
                    {
                        Recipe recipe = (Recipe)item.Value;
                        BusinessFacade.Instance.AddRecipeToShoppingList(UserId, recipe.RecipeId);
                    }
                }

                //BindRecipesList();
                //BindList();
            }
        }

        //private void BindRecipesList()
        //{
        //    IQueryable<RecipesInShoppingList> recipes = BusinessFacade.Instance.GetSelectedRecipes(UserId);
        //    RepeaterRecipes.ItemDataBound += RepeaterRecipes_ItemDataBound;
        //    RepeaterRecipes.DataSource = recipes;
        //    RepeaterRecipes.DataBind();

        //    NumOfSelectedRecipes = recipes.Count();
        //}

        private void RepeaterRecipes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RecipesInShoppingList recipe = (RecipesInShoppingList)e.Item.DataItem;
                HyperLink recipeName = (HyperLink)e.Item.FindControl("RecipeName");
                recipeName.Text = recipe.RECIPE_NAME.Substring(0, recipe.RECIPE_NAME.Length <= 40 ? recipe.RECIPE_NAME.Length : 30);
                recipeName.NavigateUrl = string.Format("../RecipeDetails.aspx?RecipeId={0}", recipe.RECIPE_ID);
                recipeName.ToolTip = recipe.RECIPE_NAME;
            }
        }
        
        private void BindList()
        {
            //shoppingListItems = BusinessFacade.Instance.GetShoppingList(UserId);
            //if (shoppingListItems != null)
            //{
            //    IEnumerable<IGrouping<int, UserShoppingList>> shoppingListCategories = shoppingListItems.GroupBy(p => p.CATEGORY_ID.HasValue ? p.CATEGORY_ID.Value : 0);
            //    RepeaterCategories.ItemDataBound += RepeaterCategories_ItemDataBound;
            //    RepeaterCategories.DataSource = shoppingListCategories;
            //    RepeaterCategories.DataBind();
            //}
        }

        void RepeaterCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header)
            {
                IGrouping<int, UserShoppingList> categoryItem = (IGrouping<int, UserShoppingList>)e.Item.DataItem;
                if (categoryItem != null)
                {
                    Literal shoppingListCategory = (Literal)e.Item.FindControl("ShoppingListCategory");
                    shoppingListCategory.Text = categoryItem.First().CATEGORY_NAME;

                    Repeater items = (Repeater)e.Item.FindControl("RepeaterItems");
                    items.ItemDataBound += items_ItemDataBound;
                    items.DataSource = categoryItem.ToList();
                    items.DataBind();
                }
            }
        }

        void items_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header)
            {
                UserShoppingList userShoppingList = (UserShoppingList)e.Item.DataItem;
                bool canDelete = userShoppingList.CAN_DELETE.HasValue ? userShoppingList.CAN_DELETE.Value : false;
                PlaceHolder canDeletePlaceHolder = (PlaceHolder)e.Item.FindControl("PlaceHolder1");
                canDeletePlaceHolder.Visible = canDelete;
            }
        }

        protected void btnAddToNewMenu_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("~/MenuEdit.aspx");
        }
    }
}