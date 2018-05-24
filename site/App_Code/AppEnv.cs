using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;

public class AppEnv
{
    public const int USER_ADMIN = 1;    

    public static void MoveToDefaultPage()
    {
        HttpContext.Current.Response.Redirect("~/Default.aspx");
    }
}

public enum PersonalAreaViewEnum
{
    MyMenusList = 0,
    MyRecipesList = 1,
    FavoritesRecipes = 2,
    SimpleSearch = 3,
    ComplexSearch = 4,
    CategoriesSearch = 5,
    MyRecipesSearch = 6,
    MyFavoritesRecipesSearch = 7
}

[Serializable]
public class SRL_Recipe
{
    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public string Name { get; set; }

    public SRL_Recipe(Recipe item)
    {
        this.RecipeId = item.RecipeId;
        this.RecipeName = item.RecipeName;

        if (item.User != null)
        {
            if (!string.IsNullOrEmpty(item.User.DisplayName))
            {
                this.Name = item.User.DisplayName;
            }
            else
            {
                this.Name = item.User.Name;
            }
        }
    }
}

[Serializable]
public class SRL_MealRecipe
{
    public int MealId { get; set; }
    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public int Servings { get; set; }

    public SRL_MealRecipe(MealRecipe item)
    {
        this.MealId = item.MealId;
        this.RecipeId = item.RecipeId;
        this.RecipeName = item.Recipe.RecipeName;
        this.Servings = item.Servings;
    }
}

[Serializable]
public class SRL_Meal
{
    public int MealId { get; set; }
    public int MealTypeId { get; set; }
    public int? DayIndex { get; set; }
    public int? CourseTypeId { get; set; }
    public int? Diners { get; set; }
    public SRL_MealRecipe[] MealRecipes { get; set; }

    public SRL_Meal(Meal meal)
    {
        this.MealId = meal.MealId;
        this.MealTypeId = meal.MealTypeId;
        this.DayIndex = meal.DayIndex;
        this.CourseTypeId = meal.CourseTypeId;
        this.Diners = meal.Diners;

        if (meal.MealRecipes != null)
        {
            var list = from item in meal.MealRecipes
                       select new SRL_MealRecipe(item);
            this.MealRecipes = list.ToArray();
        }
    }
}

[Serializable]
public class SRL_RecipeCategory
{
    public int RecipeId { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    public SRL_RecipeCategory(int recipeId, int categoryId, string categoryName)
    {
        this.RecipeId = recipeId;
        this.CategoryId = categoryId;
        this.CategoryName = categoryName;
    }
}

[Serializable]
public class SRL_Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int? ParentCategoryId { get; set; }
    public int RecipesCount { get; set; }

    public SRL_Category(int categoryId, string categoryName, int? parentCategoryId)
    {
        this.CategoryId = categoryId;
        this.CategoryName = categoryName;
        this.ParentCategoryId = parentCategoryId;
    }

    public SRL_Category(int categoryId, string categoryName, int? parentCategoryId, int recipesCount) :
        this(categoryId, categoryName, parentCategoryId)
    {
        this.RecipesCount = recipesCount;
    }
}
