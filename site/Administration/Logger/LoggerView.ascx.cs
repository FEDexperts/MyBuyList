using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using ProperServices.Common.Log;
using System.Data;
using System.Configuration;
using System.Data.Common;
using ProperControls.General;
using System.Drawing;

public partial class Administration_Logger_LoggerView : System.Web.UI.UserControl
{
    private Dictionary<string, string> Colors
    {
        get
        {
            if (Session["_LogColors"] == null)
            {
                Session["_LogColors"] = new Dictionary<string, string>();
                Colors.Add("ERROR", "coral");
            }

            return (Dictionary<string, string>)Session["_LogColors"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RebindColors();
            this.txtFilter.Text = string.Format("date > '{0:yyyy/MM/dd}'", DateTime.Now);

            //this.btnColors.OnClientClick = string.Format("{0}; return false;", this.ProperDialog1.GenerateShowCommand());
            this.ddlConnections.DataSource = WebConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>().Where(c => !c.Name.Equals("LocalSqlServer"));
            this.ddlConnections.DataBind();
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        LogTable = null;
        Rebind();
    }

    private DataTable LogTable
    {
        get
        {
            if (Session["_LogTable"] == null)
            {
                Logger.Flush();
                Session["_LogTable"] = GetData();
            }

            return (DataTable)Session["_LogTable"];
        }
        set
        {
            Session["_LogTable"] = value;
        }
    }

    private void Rebind()
    {
        int? pageSize = null;
        if (!string.IsNullOrEmpty(this.txtPageSize.Text))
            pageSize = int.Parse(this.txtPageSize.Text);

        this.grid.AllowPaging = pageSize.HasValue;
        if (pageSize.HasValue)
        {
            this.grid.PageSize = pageSize.Value;
        }

        this.grid.DataSource = LogTable;
        this.grid.DataBind();
        this.UpdatePanel1.Update();

        RebindColors();
    }

    private void RebindColors()
    {
        this.gridColors.DataSource = Colors;
        this.gridColors.DataBind();
        this.updColors.Update();
    }

    private DataTable GetData()
    {
        try
        {
            string connectionString;
            DbProviderFactory factory ;
            if (string.IsNullOrEmpty(this.txtCustomConnection.Text))
            {
                string name = this.ddlConnections.SelectedValue;
                ConnectionStringSettings settings = WebConfigurationManager.ConnectionStrings[name];
                connectionString = settings.ConnectionString;
                factory = DbProviderFactories.GetFactory(settings.ProviderName);
            }
            else
            {
                connectionString = this.txtCustomConnection.Text;
                factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            } 
            
            using (DbConnection cn = factory.CreateConnection())
            {
                cn.ConnectionString = connectionString;
                using (DbCommand command = factory.CreateCommand())
                {
                    command.Connection = cn;
                    string where = null;
                    if (!string.IsNullOrEmpty(this.txtFilter.Text))
                        where = string.Format("WHERE {0}", this.txtFilter.Text);

                    command.CommandText = string.Format("SELECT * FROM {0} {1} ORDER BY Date", this.txtLogTableName.Text, where);
                    command.CommandType = CommandType.Text;

                    using (DbDataAdapter adapter = factory.CreateDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        this.lblError.Text = null;
                        return table;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ex = ex.GetBaseException();
            this.lblError.Text = string.Format("{0}", ex.Message);
            return null;
        }
        finally
        {
            this.UpdatePanel1.Update();
        }
    }
    protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grid.PageIndex = e.NewPageIndex;
        Rebind();
    }
    protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView)e.Row.DataItem;
            string message = (string)row["Message"];
            string level = (string)row["Level"];
            KeyValuePair<string, string>? color = Colors.FirstOrDefault(c => message.IndexOf(c.Key, StringComparison.InvariantCultureIgnoreCase) > -1
                || level.IndexOf(c.Key, StringComparison.InvariantCultureIgnoreCase) > -1);
            if (color != null && !string.IsNullOrEmpty(color.Value.Value))
            {
                e.Row.BackColor = Color.FromName(color.Value.Value);
            }
        }
    }

    protected void btnAddColor_Click(object sender, EventArgs e)
    {
        string filter = this.txtMessage.Text;
        if (string.IsNullOrEmpty(filter) || string.IsNullOrEmpty(this.txtColor.Text))
        {
            this.lblInUse.Text = "Please enter filter and color";
            this.ProperDialog2.Show();
        }
        else if (Colors.ContainsKey(filter))
        {
            this.lblInUse.Text = "Already in use";
            this.ProperDialog2.Show();
        }
        else
        {
            Colors.Add(filter, this.txtColor.Text);
            Rebind();
        }
    }

    protected void gridColors_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            KeyValuePair<string, string> item = (KeyValuePair<string, string>)e.Row.DataItem;
            Label lblValue = (Label)e.Row.FindControl("lblValue");
            lblValue.BackColor = Color.FromName(item.Value);
        }
    }

    protected void gridColors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string key = (string)e.CommandArgument;
        if (Colors.ContainsKey(key))
            Colors.Remove(key);

        Rebind();
    }
}
