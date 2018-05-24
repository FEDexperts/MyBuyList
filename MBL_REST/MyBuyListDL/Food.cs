namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Food")]
    public partial class Food
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Food()
        {
            NutValues = new HashSet<NutValues>();
            Ingredients = new HashSet<Ingredients>();
            MeasurementUnitsConverts = new HashSet<MeasurementUnitsConverts>();
            MissingListDetails = new HashSet<MissingListDetails>();
            SavedListDetails = new HashSet<SavedListDetails>();
        }

        public int FoodId { get; set; }

        [Required]
        [StringLength(200)]
        public string FoodName { get; set; }

        public int FoodCategoryId { get; set; }

        public int CalculateUnitId { get; set; }

        public int? MissingListUnitId { get; set; }

        public bool IsTemporary { get; set; }

        public string Remarks { get; set; }

        public int SortOrder { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool PrintPicture { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        public virtual Users Users { get; set; }

        public virtual FoodCategories FoodCategories { get; set; }

        public virtual Users Users1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NutValues> NutValues { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ingredients> Ingredients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeasurementUnitsConverts> MeasurementUnitsConverts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MissingListDetails> MissingListDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SavedListDetails> SavedListDetails { get; set; }
    }
}
