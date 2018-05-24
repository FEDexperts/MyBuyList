using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;

public partial class Testers_AjaxTester : System.Web.UI.Page
{
    [WebMethod]
    public static object GetDateTime(string name)
    {
        return new { Date = DateTime.Now, Name = string.Format("Your name is: {0}", name) };
    }

    [WebMethod]
    public static void GenerateError()
    {
        throw new ApplicationException(string.Format("Error generated at: {0}", DateTime.Now));
    }
}
