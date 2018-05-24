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
using System.Threading;
using ProperControls.Pages;

public partial class Testers_CacheTester : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string msg = string.Format("Created at: {0}", DateTime.Now.ToString("dd/MM/yyyy HH:MM:ss"));
        this.Response.Write(msg);

        msg = string.Format("<BR/>Language is: {0}", Thread.CurrentThread.CurrentUICulture.Name);
        this.Response.Write(msg);
    }
}
