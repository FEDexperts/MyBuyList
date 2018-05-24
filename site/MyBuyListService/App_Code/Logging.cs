using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace MyBuyListService
{
    public class Logging
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void InfoLog(string message)
        {
            log.Info(message);
        }

        public static void ErrorLog(string message)
        {
            log.Error(message);
        }
    }
}