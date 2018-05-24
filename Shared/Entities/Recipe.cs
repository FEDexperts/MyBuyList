using System;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    partial class Recipe
    {
        private bool _showInFavorites;
        public bool ShowInFavorites
        {
            get { return _showInFavorites; }
            set { _showInFavorites = value; }
        }

        private int expectedServings;
        public int ExpectedServings
        {
            get { return expectedServings; }
            set { expectedServings = value; }
        }

        public Recipe(Recipe item)
            : this()
        {
            this.SetValues(item);
        }

        public void SetValues(Recipe item)
        {
            RecipeName = item.RecipeName;
            IsPublic = item.IsPublic;
            Description = item.Description;
            Tags = item.Tags;
            DifficultyLevel = item.DifficultyLevel;
            PreperationTime = item.PreperationTime;
            CookingTime = item.CookingTime;
            ShowInFavorites = item.ShowInFavorites;
            PreparationMethod = item.PreparationMethod;
            Remarks = item.Remarks;
            Servings = item.Servings;
            Source = item.Source;
            UserId = item.UserId;
            Tools = item.Tools;
            VideoLink = item.VideoLink;
            Picture = item.Picture;
            ExpectedServings = item.Servings;
        }
    }
}