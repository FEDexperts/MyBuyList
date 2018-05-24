using System;
using System.Web;

public partial class ErrorPage : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Exception ex = (Exception)HttpContext.Current.Items["exception"];
        if (ex != null)
        {
            lblMessage.Text = ex.Message;
            lblStackTrace.Text = ex.StackTrace;
        }
    }
}
