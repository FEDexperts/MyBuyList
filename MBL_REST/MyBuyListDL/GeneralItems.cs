namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GeneralItems
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralItems()
        {
            ShoppingListAdditionalItems = new HashSet<ShoppingListAdditionalItems>();
        }

        [Key]
        public int GeneralItemId { get; set; }

        [Required]
        [StringLength(100)]
        public string GeneralItemName { get; set; }

        public int SortOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingListAdditionalItems> ShoppingListAdditionalItems { get; set; }
    }
}
