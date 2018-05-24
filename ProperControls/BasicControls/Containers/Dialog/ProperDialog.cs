using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

[assembly: WebResource("ProperControls.BasicControls.Containers.Dialog.ProperDialogBehavior.js", "text/javascript")]
[assembly: WebResource("ProperControls.BasicControls.Containers.Dialog.ProperDialogStyles.css", "text/css")]
namespace ProperControls.BasicControls.Containers.Dialog
{
    [PersistChildren(true)]
    [ParseChildren(false)]
    [Designer(typeof(ProperDialogDesigner))]
    [ToolboxData("<{0}:ProperDialog runat=server></{0}:ProperDialog>")]
    public class ProperDialog : CompositeControl
    {
        public ProperDialog()
        {
            ModalPopupCssClass = "modalConfirm";
            BackgroundCssClass = "modalBackground";

            this.container = new Control();
            this.container.ID = "container";
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.ClientScript.RegisterClientScriptResource(typeof(ProperDialog), "ProperControls.BasicControls.Containers.Dialog.ProperDialogBehavior.js");
        }

        private void CreateCssReference()
        {
            string css = Page.ClientScript.GetWebResourceUrl(typeof(ProperDialog), "ProperControls.BasicControls.Containers.Dialog.ProperDialogStyles.css");
            if (!string.IsNullOrEmpty(css))
            {
                HtmlGenericControl link = new HtmlGenericControl("LINK");
                link.Attributes.Add("href", css);
                link.Attributes.Add("type", "text/css");
                link.Attributes.Add("rel", "stylesheet");
                Page.Header.Controls.Add(link);
            }
        }

        private Control container;
        private Panel background;
        private Panel wrapper;
        private Panel cell;
        protected override void CreateChildControls()
        {
            CreateCssReference();

            Control parent = this;
            background = CreateDiv(parent, "width: 100%; height: 100%; position: fixed; top: 0; left: 0;display:none", "background");
            //if (zIndex == 0) zIndex = 1;
            background.Style.Add("z-index", zIndex.ToString());
            background.CssClass = BackgroundCssClass;

            wrapper = CreateDiv(parent, "display:none;", "wrapper");

            Panel table = CreateDiv(wrapper, "width: 100%; height: 100%; position: fixed; top: 0; left: 0; display: table", "table");
            table.Style.Add("z-index", zIndex.ToString());
            Panel row = CreateDiv(table, "#position: fixed; #top: 50%; display: table-cell; vertical-align: middle;", "row");
            cell = CreateDiv(row, "position: relative; margin: auto; #top: -50%;", "cell");
            cell.CssClass = ModalPopupCssClass;

            cell.Controls.Add(this.container);
        }

        protected override void OnPreRender(EventArgs e)
        {
            ApplyStyle(this.Style.Value, cell);

            if (!string.IsNullOrEmpty(CancelButtonID))
            {
                Button cancel = (Button)this.FindControl(CancelButtonID);
                if (string.IsNullOrEmpty(CancelScript))
                    cancel.OnClientClick = string.Format("{0}; return false;", GenerateHideCommand());
                else
                    cancel.OnClientClick = string.Format("{0}; {1}; return false;", GenerateHideCommand(), CancelScript);
            }

            if (!string.IsNullOrEmpty(OKButtonID))
            {
                Button OK = (Button)this.FindControl(OKButtonID);
                if (string.IsNullOrEmpty(OKScript))
                    OK.OnClientClick = string.Format("{0}; return false;", GenerateHideCommand());
                else
                    OK.OnClientClick = string.Format("{0}; {1}; return false;", GenerateHideCommand(), OKScript);
            }


            base.OnPreRender(e);
        }

        public int zIndex { get; set; }
        public string ModalPopupCssClass { get; set; }
        public string BackgroundCssClass { get; set; }
        public string OKButtonID { get; set; }
        public string OKScript { get; set; }
        public string CancelScript { get; set; }
        public string CancelButtonID { get; set; }

        protected override void AddParsedSubObject(object obj)
        {
            Control child = obj as Control;
            if (child != null)
            {
                this.container.Controls.Add(child);
            }
        }

        private Panel CreateDiv(Control parent, string style, string id)
        {
            Panel panel = new Panel();
            panel.ID = id;
            ApplyStyle(style, panel);

            parent.Controls.Add(panel);
            return panel;
        }

        private static void ApplyStyle(string style, WebControl control)
        {
            if (!string.IsNullOrEmpty(style))
            {
                string[] parts = style.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    string[] styles = part.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    control.Style.Add(styles[0], styles[1]);
                }
            }
        }

        public string GenerateShowCommand() { return GenerateToggleCommand(true); }
        public string GenerateHideCommand() { return GenerateToggleCommand(false); }

        private string GenerateToggleCommand(bool show)
        {
            EnsureChildControls();

            string state;
            if (show)
                state = "block";
            else
                state = "none";

            return string.Format("toggleDialog('{0}','{1}','{2}');", this.wrapper.ClientID, this.background.ClientID, state);
        }

        public void Show()
        {
            string script = GenerateShowCommand();
            RegisterShowHideMethod(script, this.ClientID);
        }

        public void Hide()
        {
            string script = GenerateHideCommand();
            RegisterShowHideMethod(script, this.ClientID);
        }

        private void RegisterShowHideMethod(string script, string key)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this.Page);
            if (sm != null && sm.IsInAsyncPostBack)
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), key, script, true);
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), key, script, true);
        }
    }
}
