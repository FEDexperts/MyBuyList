<%@ Page Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="page_header">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Header_ContactUs.png" /><br />
        <br />
    </div>
    <div class="field_row" style="height: 40px; padding-right:8px; margin-top:20px; color:#656565;">
        <div>
            <b>ניתן לפנות אליינו באמצעות דואר אלקטרוני</b>
        </div>
    </div>
    <div class="field_row" style="height: 40px; padding-right:8px;color:#656565;">
        <div>
            לתמיכה טכנית&nbsp;-&nbsp;<a href="mailto:Support@mybuylist.com">Support@mybuylist.com</a>
        </div>
    </div>
    <div class="field_row" style="height: 40px;padding-right:8px;color:#656565;">
        <div>
            לפרסום באתר&nbsp;-&nbsp;<a href="mailto:Advertising@Mybuylist.com">Advertising@Mybuylist.com</a>
        </div>
    </div>
    <div class="field_row" style="height: 40px;padding-right:8px;color:#656565;">
        <div>
            למנהל קשרי לקוחות&nbsp;-&nbsp;<a href="mailto:Dalit@mybuylist.com">Dalit@mybuylist.com</a>&nbsp;או בנייד&nbsp;<span dir="ltr">052-6226525</span>
        </div>
    </div>
    <div class="field_row" style="height: 40px;padding-right:8px;color:#656565;">
        <div>
            צוות מערכת האתר מודה לכם על פנייתכם , פנייתכם תענה תוך 24 שעות.
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
