using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProperServices.Common.Extensions
{
    public static class Texts
    {
        public static string TrimToMax(this string s, int maxChars)
        {
            if (s.Length > maxChars - 3)
            {
                return s.Substring(0, maxChars - 3) + "...";
            }
            else
                return s;
        }

        public static string TrimOrNull(this string s)
        {
            if (!string.IsNullOrEmpty(s))
                return s.Trim();
            else
                return null;
        }
    }
}
