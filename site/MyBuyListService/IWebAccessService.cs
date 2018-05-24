using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MyBuyListService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWebAccessService" in both code and config file together.
    [ServiceContract]
    public interface IWebAccessService
    {
        [OperationContract]
        [WebGet]
        string HelloWorld();

        [OperationContract]
        [WebInvoke(Method = "POST")]
        string CheckCredencials(string userName, string password);
    }
}
