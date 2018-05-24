using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Serialization;
using System.IO;
using ProperServices.Common.Mail.Data;

public partial class Testers_MailTester : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ProperServices.Common.Mail.Mailer.SendMail(new string[] { "master.oren@gmail.com" }, "support@mybuylist.com", "test", "test", true);
    }
}
