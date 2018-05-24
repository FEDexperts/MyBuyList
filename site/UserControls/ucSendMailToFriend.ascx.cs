using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_ucSendMailToFriend : System.Web.UI.UserControl
{
    public string Email
    {
        get { return ViewState["email"].ToString(); }
        set { ViewState["email"] = value; }
    }

    public string ItemType
    {
        get { return ViewState["itemType"].ToString(); }
        set { ViewState["itemType"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void BindItemDetails(string itemType, int itemId, string itemName, string email)
    {
        this.hdnItemId.Value = itemId.ToString();
        this.lblItemName.Text = itemName;
        this.ItemType = itemType;
        this.Email = email;

        if (string.IsNullOrEmpty(email))
            this.userEmail.Visible = true;

        this.setColors(itemType);
    }

    protected void btnSend_Click(object sender, ImageClickEventArgs e)
    {
        //this.mpeSMTF.Hide();

        string title = string.Empty;
        if (string.IsNullOrEmpty(this.lblItemName.Text))
            title = this.hdnItemName.Value;
        else
            title = this.lblItemName.Text;
        
        string subject = string.Empty;
        if (this.ItemType == "Recipe")
            subject = this.txtSenderName.Text + " שלח/ה לך קישור למתכון מאתר MyBuyList: " + title;
        else if (this.ItemType == "Menu")
            subject = this.txtSenderName.Text + " שלח/ה לך קישור לתפריט מאתר MyBuyList: " + title;
    
        string link = string.Empty;
        if (this.ItemType == "Recipe")
            //link = ResolveUrl(string.Format("~/RecipeDetails.aspx?RecipeId={0}", this.hdnItemId.Value));
            link = "http://" + Request.Url.Host + string.Format("/RecipeDetails.aspx?RecipeId={0}", this.hdnItemId.Value);
        if (this.ItemType == "Menu")
            //link = ResolveUrl(string.Format("~/MenuDetails.aspx?menuId={0}", this.hdnItemId.Value));
            link = "http://" + Request.Url.Host + string.Format("/MenuDetails.aspx?menuId={0}", this.hdnItemId.Value);

        string str = string.Empty;
        if (this.ItemType == "Recipe")
            str = "למתכון";
        if (this.ItemType == "Menu")
            str = "לתפריט";

        string body = "<html>" +
                        "<body dir='rtl'>" +
                            "<table  > " +
                                "<tr> " +
                                    "<td colspan='2'> " +
                                        this.txtSenderName.Text + " שלח/ה לך קישור " + str + " באתר  " + 
                                        "<a href='" + "http://" + Request.Url.Host + "'>" + "MyBuyList:" + "</a>" +
                                        "<br /><br />" +
                                    "</td> " +
                                "</tr> " +
                                "<tr> " +
                                    "<td colspan='2'> " +
                                        this.txtMessage.Text.Replace("\n", "<br/>") + "<br /><br />" +
                                    "</td> " +
                                "</tr> " +
                                "<tr> " +
                                    "<td colspan='2' dir='ltr' align='right'> " +
                                        "לצפייה ב" + str.Substring(1) + " " +
                                        "<a href='" + link + "'>" + "לחץ כאן" + "</a>" +
                                    "</td> " +
                                "</tr> " +
                            "<table> " +
                        "</body>" +
                    "</html>";

        ProperServices.Common.Mail.Mailer.SendMail(this.txtFriendEmail.Text, ProperControls.General.Utils.FromEmail, subject, body, true);

        bool succeed = true;

        if (this.EmailSent != null)
        {
            this.EmailSent(this, new SendToFriendEventArgs(succeed, this.txtFriendName.Text));
        }
    }

    private void setColors(string itemType)
    {
        string borderColor = string.Empty;
        string backColor = string.Empty;
        string buttonImageNum;

        switch (itemType)
        {
            case "Recipe":
                borderColor = "#A4CB3A";
                backColor = "#DDECB5";
                buttonImageNum = "3";
                break;
            case "Menu":
                borderColor = "#FBAB14";
                backColor = "#FFE0A9";
                buttonImageNum = "2";
                break;
            default:
                borderColor = "#656565";
                backColor = "White";
                buttonImageNum = "";
                break;
        }

        this.pnlTitle.Style["background-color"] = borderColor;
        this.txtSenderName.Style["background-color"] = backColor;
        this.txtSenderName.Style["border-color"] = borderColor;
        this.txtFriendName.Style["background-color"] = backColor;
        this.txtFriendName.Style["border-color"] = borderColor;
        this.txtFriendEmail.Style["background-color"] = backColor;
        this.txtFriendEmail.Style["border-color"] = borderColor;
        this.txtMessage.Style["background-color"] = backColor;
        this.txtMessage.Style["border-color"] = borderColor;
        this.txtUserEmail.Style["background-color"] = backColor;
        this.txtUserEmail.Style["border-color"] = borderColor;
        this.lblItemName.Style["color"] = borderColor;
        this.btnSend.ImageUrl = string.Format("~/Images/btn_Send{0}_up.png", buttonImageNum);
        this.btnSend.Attributes["onmouseover"] = string.Format("this.src='Images/btn_Send{0}_over.png'", buttonImageNum);
        this.btnSend.Attributes["onmousedown"] = string.Format("this.src='Images/btn_Send{0}_down.png'", buttonImageNum);
        this.btnSend.Attributes["onmouseup"] = string.Format("this.src='Images/btn_Send{0}_up.png'", buttonImageNum);
        this.btnSend.Attributes["onmouseout"] = string.Format("this.src='Images/btn_Send{0}_up.png'", buttonImageNum);        
    }

    public delegate void SentHandler(object sender, SendToFriendEventArgs e);
    public event SentHandler EmailSent;
}    


public class SendToFriendEventArgs : EventArgs
{
    public bool SendSuccess { get; set; }
    public string recipentName { get; set; }

    public SendToFriendEventArgs(bool sendSuccess, string name)
    {
        this.SendSuccess = sendSuccess;
        this.recipentName = name;
    }
}
