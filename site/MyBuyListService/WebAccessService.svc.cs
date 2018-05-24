using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace MyBuyListService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WebAccessService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WebAccessService.svc or WebAccessService.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WebAccessService : IWebAccessService
    {
        public string HelloWorld()
        {
            return "Hello World";
        }

        public string CheckCredencials(string userName, string password)
        {
            return ServiceMethods.CheckCredencials(userName, password);
        }
    }
}
