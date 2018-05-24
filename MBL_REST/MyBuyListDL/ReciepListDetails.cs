namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ReciepListDetails
    {
        public int ID { get; set; }

        public int LIST_ID { get; set; }

        public int FOOD_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FOOD_NAME { get; set; }

        public int QUANTITY { get; set; }

        public int MEASUREMENT_UNIT_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string MEASUREMENT_UNIT { get; set; }

        public virtual ReciepList ReciepList { get; set; }
    }
}
