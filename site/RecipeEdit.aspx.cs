using System;

using ProperServices.Common.Log;

public partial class RecipeEdit : BasePage
{
    #region Properties
    protected int? RecipeId
    {
        get
        {
            int recipeId;

            if (string.IsNullOrEmpty(Request["recipeId"]) || !int.TryParse(Request["recipeId"], out recipeId))
                return null;
            else
                return recipeId;
        }
    }

    protected bool ShouldCopy
    {
        get
        {
            return !string.IsNullOrEmpty(Request["docopy"]) && Request["docopy"].Equals("1");
        }
    } 
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (RecipeId.HasValue)
                {
                    if (ShouldCopy)
                        ucRecipe.CopyRecipe(RecipeId.Value);
                    else
                        ucRecipe.EditRecipe(RecipeId.Value);
                }
                else
                {
                    ucRecipe.NewRecipe();
                }
            }
            catch (Exception ex)
            {
                Logger.Write("RecipesEdit -> Page Load", ex, Logger.Level.Error);
            }
        }
    }

    protected void RecipeCategories_RefreshData(SRL_RecipeCategory[] arr)
    {
        ucRecipe.RefreshCategories(arr);
    }

}
