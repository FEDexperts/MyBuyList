using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    public class RecipeTotalNutValues
    {
        public int NutCategoryId { get; set; }
        public int NutItemId { get; set; }
        public string NutItemName { get; set; }
        public string DisplayUnit { get; set; }
        public decimal? TotalValue { get; set; }
        public string DisplayTotalValue { get; set; }
        public bool ShowNullValueRemark { get; set; }
        public bool ShowNotExistValueRemark { get; set; }
    }
}
