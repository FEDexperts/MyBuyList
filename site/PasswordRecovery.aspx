<%@ Page Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="PasswordRecovery.aspx.cs" Inherits="Login" Title="<%$ Resources:MyGlobalResources, ForgotPasswordPageTitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="page_header" style="padding-right: 0px;">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Header_ForgotPassword.png" />
    </div>
    <div style="width: 330px;text-align: left; margin-top:20px;" class="login_link_container">
        <asp:HyperLink ID="lnkLogin" runat="server" Text="כניסה למערכת" NavigateUrl="~/Login.aspx" />
    </div>
    <div style="width: 330px; margin-top: 10px; color: #656565;">
        <div style="height: 20px; text-align:center;">
            <asp:Label ID="lblResult" runat="server" Visible="False" Font-Bold="true" ForeColor="#EE1E3E" Font-Size="Larger" />
        </div>
        <div style="vertical-align: top; margin-top: 10px;">
            <asp:Label ID="lblUsername" runat="server" Text="שם משתמש" Font-Bold="true" />&nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="יש להכניס שם משתמש"
                ControlToValidate="txtUserName" ValidationGroup="1" Display="Static" />
            <asp:TextBox ID="txtUserName" runat="server" Width="327px" Height="14px" ForeColor="#656565" Font-Size="12px" BackColor="#EEEEEE" BorderColor="#656565"
                BorderStyle="Solid" BorderWidth="1px" Style="margin-top: 6px;" />
        </div>
        <div style="vertical-align: top; margin-top: 10px;">
            <div>
                <asp:Label ID="lblPassword" runat="server" Text='דוא"ל' Font-Bold="true" />&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="rfvMail" runat="server" ErrorMessage="יש להכניס אימייל"
                    ControlToValidate="txtMail" Display="Dynamic" ValidationGroup="1" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                    ErrorMessage="האימייל שהוכנס אינו תקין" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="txtMail" ValidationGroup="1" />
                <asp:TextBox ID="txtMail" runat="server" Width="327px" Height="14px" ForeColor="#656565" Font-Size="12px" BackColor="#EEEEEE" BorderColor="#656565"
                    BorderStyle="Solid" BorderWidth="1px" Style="margin-top: 6px;" />
            </div>
            <div style="padding-right: 10px;">
            </div>
        </div>
        <div style="text-align:left; margin-top: 30px; margin-bottom: 24px;">
            <asp:LinkButton ID="btnOK" runat="server" OnClick="btnOK_Click" ValidationGroup="1">
                <asp:Image runat="server" ImageUrl="~/Images/btn_Save_up.png" onmouseover='this.src="Images/btn_Save_over.png";'
                        onmouseout='this.src="Images/btn_Save_up.png";' onmousedown='this.src="Images/btn_Save_down.png";'
                        onmouseup='this.src="Images/btn_Save_up.png";' /> 
            </asp:LinkButton>
            &nbsp;
            <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" PostBackUrl="~/Default.aspx">
                <asp:Image runat="server" ImageUrl="~/Images/btn_Cancel_up.png" onmouseover='this.src="Images/btn_Cancel_over.png";' 
                    onmouseout='this.src="Images/btn_Cancel_up.png";' onmousedown='this.src="Images/btn_Cancel_Down.png";' 
                    nmouseup='this.src="Images/btn_Cancel_up.png";' />
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
