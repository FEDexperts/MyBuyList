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
using System.Web.Mail;
using MyBuyList.BusinessLayer.Managers;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using System.IO;
using System.Text;

public partial class PageRecipeView : System.Web.UI.Page
{
    int RecipeId
    {
        get { return ViewState["RecipeId"] == null ? 0 : (int)ViewState["RecipeId"]; }
        set { ViewState["RecipeId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.Request["recipeId"]))
            {
                this.RecipeId = int.Parse(this.Request["recipeId"]);

                Recipe recipe = BusinessFacade.Instance.GetRecipe(this.RecipeId);
                if (recipe != null)
                {
                    this.lblRecipeName.Text = recipe.RecipeName;

                    List<SRL_Ingredient> ingredients = new List<SRL_Ingredient>();
                    foreach (Ingredient ing in recipe.Ingredients)
                    {
                        ingredients.Add(new SRL_Ingredient(ing));
                    }

                    this.dlistIngredients.DataSource = ingredients;
                    this.dlistIngredients.DataBind();

                    this.txtPreparationMethod.Text = recipe.PreparationMethod;
                    this.lblDiners.Text = recipe.Servings.ToString();

                    User recipeUser = BusinessFacade.Instance.GetUser(recipe.UserId);
                    if (recipeUser != null)
                    {
                        this.lblUserEntered.Text = recipeUser.DisplayName;
                    }
                }
                else
                {
                    this.tblRecipeDetail.Visible = false;
                }
            }
            else
            {
                this.tblRecipeDetail.Visible = false;
            }
        }
    }
}
