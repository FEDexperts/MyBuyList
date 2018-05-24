using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Collections.Generic;
using ProperServices.Common.Mail.Data;
using System.Reflection;
using ProperServices.Common.Log;
using System.Diagnostics;
using MyBuyList.Shared.Entities;

namespace ProperControls.General
{
    public static class Utils
    {
        public static string Direction
        {
            get
            {
                if (Utils.IsLTR)
                    return "dir='LTR'";
                else
                    return "dir='RTL'";
            }
        }

        public static bool IsLTR
        {
            get
            {
                if (Thread.CurrentThread.CurrentUICulture.Name.Equals("he-IL"))
                    return false;

                return true;
            }
        }

        #region Application health
        /// <summary>
        /// Handles Application End event.
        /// </summary>
        public static void HandleApplicationStarted()
        {
            Logger.Info("Website started");

            ProperServices.Common.Mail.Data.WebsiteNotifications mailinfo = (ProperServices.Common.Mail.Data.WebsiteNotifications)ConfigurationManager.GetSection("Notifications/WebsiteNotifications");
            if (mailinfo != null && mailinfo.WebsiteStarted.Enabled && mailinfo.From != null && mailinfo.Recipients.Count > 0)
            {
                string subject = string.Format("{0} - {1}", Environment.MachineName, mailinfo.WebsiteStarted.Subject);
                ProperServices.Common.Mail.Mailer.SendMail(mailinfo.Recipients.GetReceipients(), mailinfo.From.Email, subject, mailinfo.WebsiteStarted.Body.Text, mailinfo.WebsiteStarted.Body.IsHtml);

                // send sms
                if (mailinfo.WebsiteStarted.SMSNotification)
                {
                    SMSNotify(mailinfo.WebsiteStarted.Subject, mailinfo);
                }
            }
        }

        private static long ToKiloBytes(long bytes)
        {
            return bytes / 1024;
        }

        /// <summary>
        /// Handles Application End event.
        /// </summary>
        public static void HandleApplicationEnd()
        {
            Logger.Info("Website stopped");

            HttpRuntime runtime = (HttpRuntime)typeof(System.Web.HttpRuntime).InvokeMember("_theRuntime", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField, null, null, null);
            if (runtime == null)
                return;

            string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);
            string shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);

            Logger.Info(string.Format("Website stopped (Message: {0}, Stacktrace: {1})", shutDownMessage, shutDownStack));

            WebsiteNotifications mailinfo = (WebsiteNotifications)ConfigurationManager.GetSection("Notifications/WebsiteNotifications");
            if (mailinfo != null && mailinfo.WebsiteStopped.Enabled && mailinfo.From != null && mailinfo.Recipients.Count > 0)
            {
                // memory stats
                Process process = Process.GetCurrentProcess();
                long memoryKB = ToKiloBytes(process.WorkingSet64);
                long virtualMemoryKB = ToKiloBytes(process.VirtualMemorySize64);

                string memory = memoryKB.ToString("N0");
                string virtualMemory = virtualMemoryKB.ToString("N0");

                // build message
                string subject = string.Format("{0} - {1}", Environment.MachineName, mailinfo.WebsiteStopped.Subject);
                string body = string.Format(mailinfo.WebsiteStopped.Body.Text, shutDownMessage.Replace("\r\n", "<BR/>"), shutDownStack, memory, virtualMemory);
                ProperServices.Common.Mail.Mailer.SendMail(mailinfo.Recipients.GetReceipients(), mailinfo.From.Email, subject, body, mailinfo.WebsiteStopped.Body.IsHtml);

                // send sms
                if (mailinfo.WebsiteStopped.SMSNotification)
                {
                    SMSNotify("Shutdown: " + shutDownMessage, mailinfo);
                }
            }
        }

        /// <summary>
        /// Handles Application End event.
        /// </summary>
        public static void HandleApplicationError(HttpApplication app)
        {
            if (app != null && app.Context != null && app.Server != null && app.Server.GetLastError() != null)
            {
                Exception ex = app.Server.GetLastError().GetBaseException();
                Logger.Error(ex, "Website errors: {0}", ex.Message);

                ProperServices.Common.Mail.Data.WebsiteNotifications mailinfo = (ProperServices.Common.Mail.Data.WebsiteNotifications)ConfigurationManager.GetSection("Notifications/WebsiteNotifications");
                if (mailinfo != null && mailinfo.WebsiteErrors.Enabled && mailinfo.From != null && mailinfo.Recipients.Count > 0)
                {
                    string subject = string.Format("{0} - {1}", Environment.MachineName, mailinfo.WebsiteErrors.Subject);
                    string body = string.Format(mailinfo.WebsiteErrors.Body.Text, ex.Message, ex.StackTrace);
                    ProperServices.Common.Mail.Mailer.SendMail(mailinfo.Recipients.GetReceipients(), mailinfo.From.Email, subject, body, mailinfo.WebsiteErrors.Body.IsHtml);
                }

                // send sms
                if (mailinfo.WebsiteErrors.SMSNotification)
                {
                    SMSNotify(mailinfo.WebsiteErrors.Subject, mailinfo);
                }
            }
        }

        private static void SMSNotify(string msg, WebsiteNotifications mailinfo)
        {
            return; // inactive

            if (msg.Length > 70)
                msg = msg.Substring(0, 70);

            List<string> list = new List<string>();
            foreach (AdminDetails details in mailinfo.Recipients)
            {
                list.Add(details.Phone);
            }

            // send SMS code
        }
        #endregion

        private static string fromEmail;
        
        public static string FromEmail
        {
            get
            {
                if (string.IsNullOrEmpty(fromEmail))
                {
                    fromEmail = ConfigurationManager.AppSettings["FromEmail"];

                    if (string.IsNullOrEmpty(fromEmail))
                    {
                        fromEmail = "support@mybuylist.com";
                    }
                }

                return fromEmail;
            }
        }

        private static string fromEmail_ShopingList;
        public static string FromEmail_ShopingList
        {
            get
            {
                if (string.IsNullOrEmpty(fromEmail_ShopingList))
                {

                    fromEmail_ShopingList = "Mylist@mybuylist.com";
                   // fromEmail_ShopingList = "shania@properdev.com";
                    
                }

                return fromEmail_ShopingList;
            }
        }

        public static Dictionary<int, Recipe> SelectedRecipes 
        { 
            get 
            {
                if (HttpContext.Current.Session["selectedRecipes"] != null)
                {
                    return HttpContext.Current.Session["selectedRecipes"] as Dictionary<int, Recipe>;
                }
                else
                {
                    return new Dictionary<int, Recipe>();
                }
            }
            set { HttpContext.Current.Session["selectedRecipes"] = value; }
        }

        public static Dictionary<int, int> SelectedRecipesServings
        {
            get
            {
                if (HttpContext.Current.Session["selectedRecipesServings"] != null)
                {
                    return HttpContext.Current.Session["selectedRecipesServings"] as Dictionary<int, int>;
                }
                else
                {
                    return new Dictionary<int, int>();
                }
            }
            set { HttpContext.Current.Session["selectedRecipesServings"] = value; }
        }

        public static List<int> FavoriteRecipesAdded
        {
            get
            {
                if (HttpContext.Current.Session["favRecipesAdded"] != null)
                {
                    return HttpContext.Current.Session["favRecipesAdded"] as List<int>;
                }
                else
                {
                    return new List<int>();
                }
            }
            set { HttpContext.Current.Session["favRecipesAdded"] = value; }
        }

        public static List<int> FavoriteMenusAdded
        {
            get
            {
                if (HttpContext.Current.Session["favMenusAdded"] != null)
                {
                    return HttpContext.Current.Session["favMenusAdded"] as List<int>;
                }
                else
                {
                    return new List<int>();
                }
            }
            set { HttpContext.Current.Session["favMenusAdded"] = value; }
        }
    }
}