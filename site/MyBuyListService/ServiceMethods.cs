using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace MyBuyListService
{
    internal class ServiceMethods
    {
        private static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        public static string CheckCredencials(string userName, string password)
        {
            Logging.InfoLog("CheckCredencials...");
            try
            {
                User user = BusinessFacade.Instance.GetUser(userName, password);
                string result = JsonSerializer<User>(user);
                return result;
            }
            catch (Exception ex)
            {
                Logging.ErrorLog("CheckCredencials failed -> " + ex.Message);
                ServiceResponse response = new ServiceResponse() { isSuccess = false, message = ex.Message };
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(response);
            }
        }
    }
}