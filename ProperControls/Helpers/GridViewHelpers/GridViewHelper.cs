using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Reflection;

namespace ProperControls.Helpers.GridViewHelpers
{
    [Serializable]
    public class GridViewHelper
    {
        public void SortingHandler(GridView grid, GridViewSortEventArgs e)
        {
            if (!e.SortExpression.Equals(SortExpression))
            {
                SortExpression = e.SortExpression;
                SortDirection = SortDirection.Ascending;
            }
            else if (SortDirection == SortDirection.Ascending)
                SortDirection = SortDirection.Descending;
            else
                SortDirection = SortDirection.Ascending;

            grid.PageIndex = 0;
        }

        public string SortExpression { get; set; }
        public SortDirection SortDirection { get; set; }

        public IOrderedEnumerable<T> GetSortedList<T>(T[] items)
        {
            Type t = typeof(T);
            PropertyInfo property = t.GetProperty(SortExpression);
            IOrderedEnumerable<T> list;
            if (SortDirection == SortDirection.Ascending)
                list = items.OrderBy(c => property.GetValue(c, null));
            else
                list = items.OrderByDescending(c => property.GetValue(c, null));
            return list;
        }
    }
    
}