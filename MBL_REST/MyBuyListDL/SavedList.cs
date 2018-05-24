namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SavedList")]
    public partial class SavedList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SavedList()
        {
            SavedListDetails = new HashSet<SavedListDetails>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string NAME { get; set; }

        public bool? ACTIVE { get; set; }

        public bool? SHOPPING_LIST { get; set; }

        public int? CREATED_BY { get; set; }

        public DateTime? CREATE_DATE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SavedListDetails> SavedListDetails { get; set; }
    }
}
