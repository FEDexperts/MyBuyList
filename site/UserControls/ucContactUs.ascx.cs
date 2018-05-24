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

using MyBuyList.BusinessLayer;
using MyBuyList.BusinessLayer.Managers;
using MyBuyList.Shared.Entities;

using Resources;
using ProperControls.Pages;

public partial class ucContactUs : System.Web.UI.UserControl
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        string mailBody = "<table><tr><td>שולח:</td><td>" + this.txtFirtstName.Text + "</td></tr>" +
                            "<tr><td>הודעה:</td><td>" + this.txtData.Text + "</td></tr></table>";

      
        ProperServices.Common.Mail.Mailer.SendMail(this.txtEmail.Text, ProperControls.General.Utils.FromEmail, this.txtSubject.Text, mailBody, true);
        this.lblResult.Text = "תודה";
    }
}
