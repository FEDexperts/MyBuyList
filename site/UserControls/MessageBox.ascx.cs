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

public partial class CommonControls_Messages_MessageBox : System.Web.UI.UserControl
{
    public void Show(string message)
    {
        Show(message, false);
    }

    public void Show(string message, bool displayCancel)
    {
        Show(message, displayCancel, null);
    }

    public void Show(string message, bool displayCancel, string title)
    {
        Show(message, displayCancel, title, null);
    }

    public void Show(string message, bool displayCancel, string title, string okScript)
    {
        Show(message, displayCancel, title, okScript, null);
    }

    public void Show(string message, bool displayCancel, string title, string okScript, string cancelScript)
    {
        Page page = this.Page;
        string script = string.Format("msgbox(\"{0}\", \"{1}\", {2}, \"{3}\", \"{4}\");",
            message,
            title,
            displayCancel.ToString().ToLowerInvariant(),
            okScript,
            cancelScript);

        if (ScriptManager.GetCurrent(page).IsInAsyncPostBack)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "msgbox", script, true);
        }
        else
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "msgbox", script, true);
        }
    }
}
