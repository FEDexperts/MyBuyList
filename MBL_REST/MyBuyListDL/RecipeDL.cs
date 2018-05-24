using System.Collections.Generic;
using System.Linq;

namespace MyBuyListDL
{
    public class RecipeIngrediant
    {
        public string name { get; set; }
        public decimal quantity { get; set; }
        public string unitName { get; set; }
    }

    public class RecipeModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public List<string> categories { get; set; }
        public List<RecipeIngrediant> ingredients { get; set; }
        public string instructions { get; set; }

        public RecipeModel()
        {
            ingredients = new List<RecipeIngrediant>();
        }
    }

    public static class RecipeDL
    {
        public static List<RecipeModel> GetRecipes()
        {
            using (Mbl_Model ctx = new Mbl_Model())
            {
                var recipes = ctx.Recipes.OrderByDescending(x => x.CreatedDate).Select(x => new RecipeModel
                {
                    id = x.RecipeId,
                    name = x.RecipeName,
                    displayName = x.Users.DisplayName,
                    categories = x.Categories.Select(cat => cat.CategoryName).ToList()
                });
                return recipes.ToList();
            }
        }

        public static List<RecipeModel> GetRecipesByName(string name)
        {
            using (Mbl_Model ctx = new Mbl_Model())
            {
                var recipes = ctx.Recipes.Where(a => a.RecipeName.Contains(name)).Take(100).Select(x => new RecipeModel
                {
                    id = x.RecipeId,
                    name = x.RecipeName
                });
                return recipes.ToList();
            }
        }

        public static RecipeModel GetRecipes(int id)
        {
            using (Mbl_Model ctx = new Mbl_Model())
            {
                var recipe = ctx.Recipes.SingleOrDefault(a => a.RecipeId == id);

                if (recipe != null) 
                {
                    var recipeModel = new RecipeModel
                    {
                        id = recipe.RecipeId,
                        name = recipe.RecipeName,
                        instructions = recipe.PreparationMethod
                    };

                    recipeModel.ingredients = (from p in ctx.Ingredients
                                               join p1 in ctx.MeasurementUnits on p.MeasurementUnitId equals p1.UnitId
                                                where p.RecipeId == id
                                               select new RecipeIngrediant {
                                                   name = p.Food.FoodName,
                                                   quantity = p.Quantity,
                                                   unitName = p1.UnitName
                                               }).ToList();

                    return recipeModel;
                };

                return null;
            }
        }
    }
}
