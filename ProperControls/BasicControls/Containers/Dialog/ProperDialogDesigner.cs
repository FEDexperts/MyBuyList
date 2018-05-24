using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Drawing;

namespace ProperControls.BasicControls.Containers.Dialog
{
    public class ProperDialogDesigner : ContainerControlDesigner
    {
        private Style style = null;

        // Define the caption text for the frame in the design surface.
        public override string FrameCaption { get { return "Proper Dialog"; } }

        // Define the style of the frame around the control in the design surface.
        public override Style FrameStyle
        {
            get
            {
                if (this.style == null)
                {
                    this.style = new Style();
                    this.style.Font.Name = "Verdana";
                    this.style.Font.Size = new FontUnit("XSmall");
                    this.style.BackColor = Color.LavenderBlush;
                    this.style.ForeColor = Color.DarkBlue;
                }

                return style;
            }
        }
    }
}
