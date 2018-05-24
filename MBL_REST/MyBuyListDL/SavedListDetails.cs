namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SavedListDetails
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LIST_ID { get; set; }

        public int FOOD_ID { get; set; }

        public int MEASUREMENT_UNIT_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal QUANTITY { get; set; }

        public virtual Food Food { get; set; }

        public virtual MeasurementUnits MeasurementUnits { get; set; }

        public virtual SavedList SavedList { get; set; }
    }
}
