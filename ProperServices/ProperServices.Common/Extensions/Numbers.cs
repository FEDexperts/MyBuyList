using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProperServices.Common.Extensions
{
    public static class Numbers
    {
        public static bool IsBetween(this int i, int min, int max)
        {
            return (i >= min && i <= max);
        }
    }
}
