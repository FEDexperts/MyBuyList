namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ingredients
    {
        [Key]
        public int IngredientId { get; set; }

        public int RecipeId { get; set; }

        public int FoodId { get; set; }

        public int MeasurementUnitId { get; set; }

        public decimal Quantity { get; set; }

        [StringLength(2000)]
        public string Remarks { get; set; }

        public int SortOrder { get; set; }

        public virtual Food Food { get; set; }

        public virtual MeasurementUnits MeasurementUnits { get; set; }

        public virtual Recipes Recipes { get; set; }
    }
}
