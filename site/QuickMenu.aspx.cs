using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;
using MyBuyList.BusinessLayer.Managers;

using ProperControls.General;
using ProperControls.Pages;

public partial class QuickMenu : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                MyBuyList.Shared.Entities.Menu menu = new MyBuyList.Shared.Entities.Menu();

                Meal theMeal = new Meal();
                theMeal.MealTypeId = 4;
                theMeal.CourseTypeId = 0;
                theMeal.CreatedDate = DateTime.Now;
                theMeal.ModifiedDate = DateTime.Now;

                Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;
                Dictionary<int, int> selectedRecipesServings = Utils.SelectedRecipesServings;

                menu.MenuName = string.Format("רשימת קניות מהירה מתאריך {0}, {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
                menu.MenuTypeId = (int)MenuTypeEnum.QuickMenu;
                menu.UserId = ((BasePage)this.Page).UserId;
                menu.IsPublic = false;
                menu.Description = string.Format("רשימת קניות מהירה שהופקה בתאריך {0}, בשעה {1}. כוללת {2} מתכונים", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), (selectedRecipes != null) ? selectedRecipes.Count.ToString() : "0");

                if (selectedRecipes != null && selectedRecipes.Count != 0)
                {
                    foreach (KeyValuePair<int, Recipe> r in selectedRecipes)
                    {
                        MenuRecipe recipe = new MenuRecipe();
                        recipe.RecipeId = r.Key;
                        menu.MenuRecipes.Add(recipe);

                        MealRecipe mealRecipe = new MealRecipe();
                        mealRecipe.RecipeId = r.Key;
                        //mealRecipe.Servings = r.Value.ExpectedServings;
                        mealRecipe.ExpectedServings = r.Value.ExpectedServings;
                        mealRecipe.Servings = selectedRecipesServings.Keys.Contains(recipe.RecipeId) ? selectedRecipesServings[recipe.RecipeId] : r.Value.Servings;
                        theMeal.MealRecipes.Add(mealRecipe);
                    }
                }

                menu.Meals.Add(theMeal);

                int newMenuId;

                BusinessFacade.Instance.CreateOrUpdateMenu(menu, out newMenuId);

                if (newMenuId != 0)
                {
                    Utils.SelectedRecipes = null;
                    Utils.SelectedRecipesServings = null;
                    string url = string.Format("~/ShoppingList.aspx?menuId={0}", newMenuId);
                    this.Response.Redirect(url);
                }
            }
        }
        catch (Exception)
        {
        }
    }
}
