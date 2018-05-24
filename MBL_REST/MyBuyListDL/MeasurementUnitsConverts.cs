namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MeasurementUnitsConverts
    {
        [Key]
        public int ConvertId { get; set; }

        public int FoodId { get; set; }

        public int FromUnitId { get; set; }

        public decimal FromQuantity { get; set; }

        public int ToUnitId { get; set; }

        public decimal ToQuantity { get; set; }

        public int SortOrder { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool? Active { get; set; }

        public virtual Food Food { get; set; }

        public virtual MeasurementUnits MeasurementUnits { get; set; }

        public virtual MeasurementUnits MeasurementUnits1 { get; set; }
    }
}
