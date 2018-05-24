using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using MyBuyList.Shared.Enums;
using ProperControls.General;
using ProperControls.Pages;

namespace Wizard
{
    public partial class WizReciept : BasePage
    {
        protected class RecipeView
        {
            public int RecipeId { get; set; }
            public string RecipeTitle { get; set; }
            public string MainCategoryName { get; set; }
            public string RecipeTags { get; set; }
            public string RecipeDescription { get; set; }
            public int PublisherId { get; set; }
            public string PublisherName { get; set; }
            public string PublishDate { get; set; }
            public string RecipeThumbnail { get; set; }
            public bool InMyFavorites { get; set; }
            public int NumUsersFavorite { get; set; }
            public int NumMenusInclude { get; set; }
            public int Servings { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RebindRecipes();
        }

        private void RebindRecipes()
        {
            Recipe[] recipesList = BusinessFacade.Instance.GetRecipesList();

            List<RecipeView> recipes =
                (from r in recipesList
                 select
                     new RecipeView
                         {
                             //MainCategoryName = r.RecipeCategories[0].Category.CategoryName,
                             RecipeId = r.RecipeId,
                             RecipeTitle = r.RecipeName,
                             PublisherName = r.User.DisplayName,
                             PublishDate = r.ModifiedDate.ToString("dd/MM/yyyy"),
                             RecipeThumbnail = ResolveUrl(r.Picture != null ? string.Format("~/ShowPicture.ashx?RecipeId={0}", r.RecipeId) : "~/Images/Img_Default_small.jpg"),
                             Servings = r.Servings
                         }).ToList();

            rptRecipes.DataSource = recipes;
            rptRecipes.DataBind();

           
        }

        protected void rptRecipes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //HyperLink lnkRecipe = (HyperLink)e.Item.FindControl("lnkRecipe");
                //lnkRecipe.NavigateUrl = ResolveUrl(string.Format("~/RecipeDetails.aspx?RecipeId={0}", ((RecipeView)e.Item.DataItem).RecipeId));

                //LinkButton linkBtn = e.Item.FindControl("blkAddRemove") as LinkButton;
                //if (linkBtn != null)
                //{
                //    linkBtn.Attributes["recipeID"] = ((RecipeView)e.Item.DataItem).RecipeId.ToString();
                //}

                //linkBtn = e.Item.FindControl("btnSendToFriend") as LinkButton;
                //if (linkBtn != null)
                //{
                //    linkBtn.Attributes["recipeId"] = ((RecipeView)e.Item.DataItem).RecipeId.ToString();
                //}

                int recipeId = ((RecipeView) e.Item.DataItem).RecipeId;

                //HtmlGenericControl divServings = e.Item.FindControl("servingsDiv") as HtmlGenericControl;

                

                HtmlGenericControl divMyFavoritesInfoTag = e.Item.FindControl("myFavoritesInfoTag") as HtmlGenericControl;

                if (((RecipeView)e.Item.DataItem).InMyFavorites)
                {
                    divMyFavoritesInfoTag.Visible = true;
                }
                else
                {
                    divMyFavoritesInfoTag.Visible = false;
                }

                //Label lblAllFavorites = e.Item.FindControl("lblAllFavorites") as Label;
                //Label lblAllMenus = e.Item.FindControl("lblAllMenus") as Label;

                //lblAllFavorites.Text = ((RecipeView)e.Item.DataItem).NumUsersFavorite.ToString();
                //lblAllMenus.Text = ((RecipeView)e.Item.DataItem).NumMenusInclude.ToString();
            }
        }

        [WebMethod]
        public static int GetServings(int recipeId)
        {
            Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeId);
            return recipe.Servings;
        }



        [WebMethod(EnableSession = true)]
        public static bool AddRecipeToList(int recipeId, int servings)
        {
            SRL_User user = (SRL_User)HttpContext.Current.Session[AppConstants.SITE_USER];
            if (user == null)
                return false;

            Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeId);
            List<SRL_Ingredient> ingredients =
                (from p in recipe.Ingredients
                 select
                     new SRL_Ingredient
                         {
                             //Id = p.IngredientId,
                             FoodName = p.FoodName,
                             CompleteValue = p.Quantity.ToString(),
                             MeasurementUnitId = p.MeasurementUnitId,
                             MeasurementUnitName = p.MeasurementUnit.ToString()
                         }).ToList();

            int newListId = BusinessFacade.Instance.AddGeneralList(user.UserId, ListTypes.ShoppingList);

            foreach (SRL_Ingredient ingredient in ingredients)
            {
                bool addGeneralListItem = BusinessFacade.Instance.AddGeneralListItem(ingredient, newListId);
            }

            return true;
        }
    }
}