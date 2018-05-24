using System;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    partial class RecipeCategory
    {
        public RecipeCategory(int recipeId, int categoryId)
            : this()
        {
            this.RecipeId = recipeId;
            this.CategoryId = categoryId;
        }
    }
}