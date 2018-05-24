<%@ Application Language="C#" %>

<script runat="server">
    void Application_Start(object sender, EventArgs e)
    {
        ProperControls.General.Utils.HandleApplicationStarted();
    }

    void Application_End(object sender, EventArgs e)
    {
        ProperControls.General.Utils.HandleApplicationEnd();
    }

    void Application_Error(object sender, EventArgs e)
    {
        ProperControls.General.Utils.HandleApplicationError((HttpApplication)sender);

        HttpApplication app = (HttpApplication)sender;
        if (app != null && app.Context != null && app.Server != null && app.Server.GetLastError() != null)
        {
            Exception ex = app.Server.GetLastError().GetBaseException();
            app.Context.Items["exception"] = ex;
            app.Context.Server.Transfer("~/ErrorPage.aspx");
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }
</script>
