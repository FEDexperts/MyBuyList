namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Recipes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Recipes()
        {
            Ingredients = new HashSet<Ingredients>();
            MealRecipes = new HashSet<MealRecipes>();
            RecipesInShoppingList = new HashSet<RecipesInShoppingList>();
            Menus = new HashSet<Menus>();
            Categories = new HashSet<Categories>();
            Users1 = new HashSet<Users>();
            ReciepList = new HashSet<ReciepList>();
        }

        [Key]
        public int RecipeId { get; set; }

        [Required]
        [StringLength(200)]
        public string RecipeName { get; set; }

        public bool IsPublic { get; set; }

        public int Servings { get; set; }

        [Required]
        public string PreparationMethod { get; set; }

        public string Remarks { get; set; }

        public string Source { get; set; }

        public string MediaLink { get; set; }

        public int SortOrder { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Tools { get; set; }

        public bool IsApproved { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        public string VideoLink { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Tags { get; set; }

        public int? DifficultyLevel { get; set; }

        public int? PreperationTime { get; set; }

        public int? CookingTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ingredients> Ingredients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MealRecipes> MealRecipes { get; set; }

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecipesInShoppingList> RecipesInShoppingList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menus> Menus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Categories> Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReciepList> ReciepList { get; set; }
    }
}
