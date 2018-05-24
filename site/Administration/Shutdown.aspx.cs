using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Hosting;
using ProperServices.Common.Log;

public partial class Administration_Shutdown : System.Web.UI.Page
{
    private bool IsValid()
    {
        return this.txtPassword.Text.Equals("Sanjuro2");
    }

    protected void btnShutdown_Click(object sender, EventArgs e)
    {
        if (IsValid())
        {
            Logger.Warn("Shutdown requested");
            HostingEnvironment.InitiateShutdown();
            this.Response.Write("OK, Done.");
        }
    }
    protected void btnAppDomainUnload_Click(object sender, EventArgs e)
    {
        if (IsValid())
        {
            Logger.Warn("AppDomain unload requested");
            AppDomain.Unload(AppDomain.CurrentDomain);
            this.Response.Write("OK, Done.");
        }
    }
}
