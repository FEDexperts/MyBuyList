namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserShoppingList")]
    public partial class UserShoppingList
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FOOD_ID { get; set; }

        [StringLength(200)]
        public string FOOD_NAME { get; set; }

        public int? MEASURMENT_ID { get; set; }

        [StringLength(100)]
        public string MEASUREMENT_NAME { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? QUANTITY { get; set; }

        public int? CATEGORY_ID { get; set; }

        [StringLength(100)]
        public string CATEGORY_NAME { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int USER_ID { get; set; }

        public bool? ACTIVE { get; set; }

        public bool? CAN_DELETE { get; set; }

        [Column(TypeName = "image")]
        public byte[] IMAGE { get; set; }
    }
}
