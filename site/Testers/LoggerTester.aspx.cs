using System;
using System.Data.SqlClient;
using log4net;
using log4net.Appender;
using ProperServices.Common.Log;

public partial class Testers_LoggerTester : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        foreach (IAppender appender in LogManager.GetRepository().GetAppenders())
        {
            AdoNetAppender ado = appender as AdoNetAppender;
            if (ado != null)
            {
                Response.Write(string.Format("reconnected on error: {0}<BR/>", ado.ReconnectOnError));

                try
                {
                    using (SqlConnection cn = new SqlConnection(ado.ConnectionString))
                    {
                        cn.Open();
                    }
                    Response.Write("connection successful");
                }
                catch (Exception ex)
                {
                    Response.Write(string.Format("connection failed: {0}", ex.Message));
                }
            }
        }

        Logger.Debug("test: {0}", DateTime.Now);
        Logger.Debug(new Exception("test"), "test: {0}", DateTime.Now);

        Logger.Debug("flush");
        Logger.Flush();
    }
}
