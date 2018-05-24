using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;

namespace ProperControls.General
{
    public static class ProperConfig
    {
        /// <summary>
        /// Returns the default Cache time in seconds.
        /// </summary>
        public static int DefaultCacheSeconds
        {
            get
            {
                string temp = WebConfigurationManager.AppSettings["DefaultCacheSeconds"];
                if (string.IsNullOrEmpty(temp))
                    return 0;

                // the value must be an integer
                return int.Parse(temp);
            }
        }
    }
    
}