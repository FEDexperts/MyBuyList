using System;
using System.Linq;
using System.Web.UI;

using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;
using ProperServices.Common.Log;


public partial class PrintRecipe : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(Request["recipeId"]))
            {
                int recipeId = int.Parse(Request["recipeId"]);
                Recipe currRecipe = BusinessFacade.Instance.GetRecipe(recipeId);
                Page.Title = "הדרך המטעימה לארגון הרשימה - MyBuyList";
                this.PageDescription.Attributes["content"] = string.Format("פרטי מתכון - {0} מאת {1}", currRecipe.RecipeName, currRecipe.User.DisplayName);
                //this.ucRecipePrint.Bind(recipeId, null);
                BindRecipeDetails(currRecipe);

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Key", "javascript:window.print();", true);
            }
        }
        catch(Exception ex)
        {
            Logger.Error(ex, "PrintRecipe Error", null);
        }
    }

    private void BindRecipeDetails(Recipe recipe)
    {
        this.lblTitle2.Text = recipe.RecipeName;

        if (recipe.Picture != null)
        {
            this.imgRecipePicture.ImageUrl = "~/ShowPicture.ashx?RecipeId=" + recipe.RecipeId;
        }
        else
        {
            this.imgRecipePicture.ImageUrl = "~/Images/Img_Default.jpg";  
        }

        this.lnkPublisher.Text = recipe.User.DisplayName;
        this.lblPublishDate.Text = recipe.ModifiedDate.ToString("dd/MM/yyyy");
        
        this.lblServNumber.Text = recipe.Servings.ToString();

        if (recipe.Tags != null)
        {
            this.lblRecipeTags.Text = recipe.Tags;
        }
        if (recipe.Description != null)
        {
            this.lblRecipeDescription.Text = recipe.Description;
        }

        if (recipe.DifficultyLevel != null)
        {
            this.txtDifficulty.Text = this.GetDifficultyLevelString(recipe.DifficultyLevel.Value);
        }

        if (recipe.PreperationTime != null)
        {
            string units;
            this.lblPrepTime.Text = this.GetTimeInCorrectUnits(recipe.PreperationTime.Value, out units).ToString() + " " + units;
        }

        if (recipe.CookingTime != null)
        {
            string units;
            this.lblCookTime.Text = this.GetTimeInCorrectUnits(recipe.CookingTime.Value, out units).ToString() + " " + units;
        }

        if (!string.IsNullOrEmpty(recipe.Remarks))
        {
            this.lblRemarks.Text = recipe.Remarks;
        }
        else
        {
            this.recipe_remarks.Visible = false;
        }

        if (!string.IsNullOrEmpty(recipe.PreparationMethod))
        {
            this.txtPreparationMethod.Text = recipe.PreparationMethod.Replace("\n", "<br />");
        }

        if (!string.IsNullOrEmpty(recipe.Tools))
        {
            this.txtTools.Text = recipe.Tools.Replace("\n", "<br />");
        }

        string str = "";
        foreach (RecipeCategory rc in recipe.RecipeCategories)
        {
            str += rc.Category.CategoryName + ", ";
        }
        str = str.Remove(str.Length - 2);
        this.lblRecipeCategories.Text = str;

        //List<SRL_Ingredient> ingredients = new List<SRL_Ingredient>();
        //foreach (Ingredient ing in recipe.Ingredients)
        //{
        //    ingredients.Add(new SRL_Ingredient(ing));
        //}
        //dlistIngredients.DataSource = ingredients;
        dlistIngredients.DataSource = recipe.Ingredients;
        this.dlistIngredients.DataBind();

        bool isInMyFavorites = (recipe.UserFavoriteRecipes.SingleOrDefault(ufr => ufr.UserId == ((BasePage)this.Page).UserId) != null);
        if (isInMyFavorites)
        {
            this.myFavoritesTopTag.Visible = true;
        }
        else
        {
            this.myFavoritesTopTag.Visible = false;
        }

        this.lblAllFavorites.Text = recipe.UserFavoriteRecipes.Count.ToString();

        this.lblAllMenus.Text = recipe.MenuRecipes.Count.ToString();
    }

    private string GetDifficultyLevelString(int level)
    {
        string str = "";

        switch (level)
        {
            case 1:
                str = "קל";
                break;
            case 2:
                str = "בינוני";
                break;
            case 3:
                str = "קשה";
                break;
        }

        return str;
    }

    private decimal GetTimeInCorrectUnits(int time, out string unitString)
    {
        decimal timeInCorrectUnits = 0;
        unitString = "דקות";

        if (time < 120)
        {
            timeInCorrectUnits = (decimal)time;
        }
        else if (time >= 120 && time < 600)
        {
            if (time % 30 == 0)
            {
                int temp = time / 30;
                timeInCorrectUnits = (decimal)temp / 2;
                unitString = "שעות";
            }
            else
            {
                timeInCorrectUnits = (decimal)time;
            }
        }
        else
        {
            int temp = time / 30;
            timeInCorrectUnits = (decimal)temp / 2;
            unitString = "שעות";
        }

        return timeInCorrectUnits;
    }
}
