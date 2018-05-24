using System;
using System.Data.Linq;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    partial class Category
    {       
        public EntitySet<Category> ParentCategories
        {
            get
            {
                return this._Categories;
            }
        }

        private int _recipesCount;
        public int RecipesCount
        {
            get { return _recipesCount; }
            set { _recipesCount = value; }
        }

        private bool _allowDelete;
        public bool AllowDelete
        {
            get { return _allowDelete; }
            internal set { _allowDelete = value; }
        }
    }
}