namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NutItems
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NutItems()
        {
            NutValues = new HashSet<NutValues>();
        }

        [Key]
        public int NutItemId { get; set; }

        [Required]
        [StringLength(100)]
        public string NutItemName { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayUnit { get; set; }

        public int NutCategoryId { get; set; }

        public virtual NutCategories NutCategories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NutValues> NutValues { get; set; }
    }
}
