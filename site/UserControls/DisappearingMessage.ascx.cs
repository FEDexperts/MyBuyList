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
[Themeable(true)]
public partial class CommonControls_Messages_DisappearingMessage : System.Web.UI.UserControl
{
    public string Text { get; set; }
    public int DisappearAfter { get { return 8000; } }
    public bool Displayed { get; set; }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        RegisterScript();
    }

    private void RegisterScript()
    {
        if (Displayed && !string.IsNullOrEmpty(Text))
        {
            string script = string.Format("showDisappearingMessage('{0}');", Text);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showDisappearingMessage", script, true);
        }
    }
}
