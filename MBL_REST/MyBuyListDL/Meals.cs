namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Meals
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meals()
        {
            MealRecipes = new HashSet<MealRecipes>();
        }

        [Key]
        public int MealId { get; set; }

        public int MenuId { get; set; }

        public int MealTypeId { get; set; }

        public int? DayIndex { get; set; }

        public int? CourseTypeId { get; set; }

        public int? Diners { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [StringLength(1000)]
        public string Comments { get; set; }

        public virtual CourseTypes CourseTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MealRecipes> MealRecipes { get; set; }

        public virtual MealTypes MealTypes { get; set; }

        public virtual Menus Menus { get; set; }
    }
}
