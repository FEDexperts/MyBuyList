using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProperControls.BasicControls.Containers
{
    [ParseChildren(true)]
    [ToolboxData("<{0}:ProperContainer runat=server></{0}:ProperContainer>")]
    public class ProperContainer : WebControl, INamingContainer
    {
        #region Members
        private static readonly Type type = typeof(ProperContainer);

        private HiddenField stateHidden;
        private Panel visible;
        private Panel hidden;
        private HyperLink link;
        private string moreText = "more";
        private string hideText = "hide";
        private bool isHidden;
        #endregion

        public Panel AlwaysShown
        {
            get { return this.visible; }
            set { this.visible = value; }
        }

        public Panel Hidden
        {
            get { return this.hidden; }
            set { this.hidden = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!bool.TryParse(this.stateHidden.Value, out this.isHidden))
                this.IsHidden = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GenerateScript();
        }

        /// <summary>
        /// Creates the child containers.
        /// </summary>
        protected override void CreateChildControls()
        {
            this.stateHidden = new HiddenField();
            this.Controls.Add(this.stateHidden);

            if (this.visible == null)
                this.visible = new Panel();
            this.visible.ID = "visible";

            if (this.hidden == null)
                this.hidden = new Panel();
            this.hidden.ID = "hidden";

            this.link = new HyperLink();
            this.link.ID = "more";
            link.SkinID = "ProperContainerToggleLink";
            this.link.Text = "...";

            this.Controls.Add(this.visible);
            this.Controls.Add(this.link);
            this.Controls.Add(this.hidden);
        }

        #region Properties
        /// <summary>
        /// Gets or sets the text of the more button.
        /// </summary>
        public string HideText
        {
            get { return this.hideText; }
            set { this.hideText = value; }
        }

        /// <summary>
        /// Gets or sets the text of the more button.
        /// </summary>
        public string MoreText
        {
            get { return this.moreText; }
            set { this.moreText = value; }
        }

        /// <summary>
        /// Gets or sets the hidden containers state.
        /// </summary>
        public bool IsHidden
        {
            get { return this.isHidden; }
            set
            {
                EnsureChildControls();

                this.isHidden = value;
                this.stateHidden.Value = this.isHidden.ToString();
            }
        }
        #endregion

        #region Add visible or hidden
        /// <summary>
        /// Adds the control to the visible panel.
        /// </summary>
        /// <param name="control"></param>
        public void AddVisible(Control control)
        {
            EnsureChildControls();
            this.visible.Controls.Add(control);
        }

        /// <summary>
        /// Adds the control to the hidden panel.
        /// </summary>
        /// <param name="control"></param>
        public void AddHidden(Control control)
        {
            EnsureChildControls();
            this.hidden.Controls.Add(control);
        }
        #endregion

        /// <summary>
        /// Renders the control.
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            DecorateHidden();

            base.RenderContents(writer);
        }

        /// <summary>
        /// Sets the display value for the hidden container.
        /// </summary>
        private void DecorateHidden()
        {
            string state;
            if (IsHidden)
            {
                this.link.Text = MoreText;
                state = "none";
            }
            else
            {
                this.link.Text = HideText;
                state = "block";
            }

            if (this.visible.Controls.Count == 0)
                this.visible.Visible = false;

            this.stateHidden.Value = IsHidden.ToString();

            if (this.hidden.Controls.Count == 0)
                this.hidden.Visible = false;
            else
                this.hidden.Style.Add("display", state);

            this.link.NavigateUrl = string.Format("javascript:ProperContainerToggleScript('{0}', '{1}', '{2}', '{3}', '{4}');", this.hidden.ClientID, this.link.ClientID, this.MoreText, this.HideText, this.stateHidden.ClientID);

            this.link.Visible = (this.hidden.Controls.Count != 0);
        }

        /// <summary>
        /// Generates the hidden toggle script.
        /// </summary>
        private void GenerateScript()
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered(ProperContainer.type, "ProperContainerToggleScript"))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("function ProperContainerToggleScript(id, linkId, moreText, hideText, hiddenId){\r\n");
                builder.Append("    var div = document.getElementById(id);\r\n");
                builder.Append("    var link = document.getElementById(linkId);\r\n");
                builder.Append("    var hidden = document.getElementById(hiddenId);\r\n");
                builder.Append("    if ( div.style.display == 'none' ) {\r\n");
                builder.Append("        div.style.display = 'block';\r\n");
                builder.Append("        link.innerHTML = hideText;\r\n");
                builder.Append("        hidden.value = 'False';\r\n");
                builder.Append("    } else {\r\n");
                builder.Append("        div.style.display = 'none';\r\n");
                builder.Append("        link.innerHTML = moreText;\r\n");
                builder.Append("        hidden.value = 'True';\r\n");
                builder.Append("    }\r\n");
                builder.Append("}\r\n");

                Page.ClientScript.RegisterClientScriptBlock(ProperContainer.type, "ProperContainerToggleScript", builder.ToString(), true);
            }
        }
    }
}
