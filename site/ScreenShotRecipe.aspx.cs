using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;

using ProperControls.Pages;
using Resources;

public partial class ScreenShotRecipe : BasePage
{
    protected int ReciepId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["action"]))
            DoScreenShot();
        else
            SaveScreenShot();
    }

    private void SaveScreenShot()
    {
        if (string.IsNullOrEmpty(Request["recipeId"])) return;

        int RecipeId = int.Parse(Request["recipeId"]);
        Recipe currRecipe = BusinessFacade.Instance.GetRecipe(RecipeId);
        string category = "ReciepsScreenShots";

        byte[] data;
        string url;
        using (WebClient client = new WebClient())
        {
            url = string.Format("http://{0}{1}/Images/{2}/{3}.jpg", Request.Url.Host, Request.ApplicationPath,
                                       category, currRecipe.RecipeName);
            data = client.DownloadData(url);
        }
        try
        {

            string fileName = HttpUtility.UrlPathEncode(string.Format("{0}.jpg", currRecipe.RecipeName));

            Response.Clear();
            Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            Response.Buffer = true;
            Response.AddHeader("Content-Length", data.Length.ToString());
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
            Response.AddHeader("Expires", "0");
            Response.AddHeader("Pragma", "cache");
            Response.AddHeader("Cache-Control", "no-cache");
            Response.ContentType = string.Format("image/JPEG");
            Response.AddHeader("Accept-Ranges", "bytes");
            Response.BinaryWrite(data);
            Response.Flush();
            Response.End();
        }
        catch (Exception)
        {
        }
    }

    private void DoScreenShot()
    {
        if (!string.IsNullOrEmpty(Request["recipeId"]))
        {
            int recipeId = int.Parse(Request["recipeId"]);
            ReciepId = recipeId;
            Recipe currRecipe = BusinessFacade.Instance.GetRecipe(recipeId);
            Page.Title = ValidationResources.SaveRecipeTitle;
            this.PageDescription.Attributes["content"] = string.Format("פרטי מתכון - {0} מאת {1}", currRecipe.RecipeName,
                                                                       currRecipe.User.DisplayName);
            BindRecipeDetails(currRecipe);
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

        //this.lnkPublisher.Text = recipe.User.DisplayName;
        //this.lblPublishDate.Text = recipe.ModifiedDate.ToString("dd/MM/yyyy");

        bool showServNumber = string.IsNullOrEmpty(recipe.Servings.ToString());
        if (showServNumber)
        {
            lblServNumber.Text = recipe.Servings.ToString();
        }
        Label1.Visible = showServNumber;
        lblServNumber.Visible = showServNumber;

        //if (recipe.Tags != null)
        //{
        //    this.lblRecipeTags.Text = recipe.Tags;
        //}
        if (recipe.Description != null)
        {
            this.lblRecipeDescription.Text = recipe.Description;
        }

        if (recipe.DifficultyLevel != null)
        {
            this.txtDifficulty.Text = this.GetDifficultyLevelString(recipe.DifficultyLevel.Value);
        }

        bool showPreperationTime = recipe.PreperationTime != null;
        if (showPreperationTime)
        {
            string units;
            this.lblPrepTime.Text = this.GetTimeInCorrectUnits(recipe.PreperationTime.Value, out units).ToString() + " " + units;
        }
        Label2.Visible = showPreperationTime;
        lblPrepTime.Visible = showPreperationTime;

        bool showCookingTime = recipe.CookingTime != null;
        if (showCookingTime)
        {
            string units;
            this.lblCookTime.Text = this.GetTimeInCorrectUnits(recipe.CookingTime.Value, out units).ToString() + " " + units;
        }
        Label5.Visible = showCookingTime;
        lblCookTime.Visible = showCookingTime;

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
        //this.lblRecipeCategories.Text = str;

        List<SRL_Ingredient> ingredients = new List<SRL_Ingredient>();
        foreach (Ingredient ing in recipe.Ingredients)
        {
            ingredients.Add(new SRL_Ingredient(ing));
        }
        this.dlistIngredients.DataSource = ingredients;
        this.dlistIngredients.DataBind();

        bool isInMyFavorites = (recipe.UserFavoriteRecipes.SingleOrDefault(ufr => ufr.UserId == ((BasePage)this.Page).UserId) != null);
        //if (isInMyFavorites)
        //{
        //    this.myFavoritesTopTag.Visible = true;
        //}
        //else
        //{
        //    this.myFavoritesTopTag.Visible = false;
        //}

        //this.lblAllFavorites.Text = recipe.UserFavoriteRecipes.Count.ToString();

        //this.lblAllMenus.Text = recipe.MenuRecipes.Count.ToString();
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
