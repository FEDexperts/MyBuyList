namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ShoppingListAdditionalItems
    {
        [Key]
        public int ShoppingListItemId { get; set; }

        public int MenuId { get; set; }

        public int? GeneralItemId { get; set; }

        [StringLength(100)]
        public string ItemName { get; set; }

        public virtual GeneralItems GeneralItems { get; set; }

        public virtual Menus Menus { get; set; }
    }
}
