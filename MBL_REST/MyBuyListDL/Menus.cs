namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Menus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menus()
        {
            Meals = new HashSet<Meals>();
            ShoppingListAdditionalItems = new HashSet<ShoppingListAdditionalItems>();
            MCategories = new HashSet<MCategories>();
            Recipes = new HashSet<Recipes>();
            Users1 = new HashSet<Users>();
            Users2 = new HashSet<Users>();
        }

        [Key]
        public int MenuId { get; set; }

        [Required]
        [StringLength(200)]
        public string MenuName { get; set; }

        public int MenuTypeId { get; set; }

        public string Description { get; set; }

        public int SortOrder { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public int? TempUserId { get; set; }

        public bool IsPublic { get; set; }

        [Column(TypeName = "ntext")]
        public string Tags { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        public string EmbeddedVideo { get; set; }

        [StringLength(300)]
        public string Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Meals> Meals { get; set; }

        public virtual MenuTypes MenuTypes { get; set; }

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingListAdditionalItems> ShoppingListAdditionalItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MCategories> MCategories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipes> Recipes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users2 { get; set; }
    }
}
