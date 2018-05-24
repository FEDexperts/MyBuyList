namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FoodCategories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FoodCategories()
        {
            Food = new HashSet<Food>();
            FoodCategories1 = new HashSet<FoodCategories>();
        }

        [Key]
        public int FoodCategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string FoodCategoryName { get; set; }

        public int? ParentCategoryId { get; set; }

        public int ShopDepartmentId { get; set; }

        public int SortOrder { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Food> Food { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodCategories> FoodCategories1 { get; set; }

        public virtual FoodCategories FoodCategories2 { get; set; }

        public virtual ShopDepartments ShopDepartments { get; set; }
    }
}
