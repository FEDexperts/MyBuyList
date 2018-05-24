<%@ Page Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="User.aspx.cs" Inherits="PageUser" Title="<%$ Resources:MyGlobalResources, UserPageTitle %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td style="width: 109px">
                <asp:Label ID="lblUserName" runat="server" Text="שם משתמש:"></asp:Label>
                <br />
                <asp:Label ID="Label1" runat="server" Text="(אותיות באנגלית)"></asp:Label>
            </td>
            <td style="width: 129px">
                <asp:TextBox ID="txtUserName" runat="server" ValidationGroup="1"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvtxtUserName" runat="server" ControlToValidate="txtUserName"
                    ErrorMessage="חובה להזין שם משתמש" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="שם משתמש שהוכנס אינו תקין"
                    ValidationExpression="\w+([-+.']\w+)*" ControlToValidate="txtUserName"
                    ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr id="trPassword1" runat="server">
            <td style="width: 109px">
                <asp:Label ID="lblUserPassword" runat="server" Text="סיסמה:"></asp:Label>
            </td>
            <td style="width: 129px; direction: ltr;">
                <asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvtxtUserPassword" runat="server" ControlToValidate="txtUserPassword"
                    ErrorMessage="חובה להזין סיסמה" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="הסיסמה שהוכנסה אינה תקינה"
                    ValidationExpression="\w+([-+.']\w+)*" ControlToValidate="txtUserPassword"
                    ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr id="trPassword2" runat="server">
            <td style="width: 109px">
                <asp:Label ID="lblUserPassword2" runat="server" Text="  חזרה על סיסמה:"></asp:Label>
            </td>
            <td style="width: 129px; direction: ltr;">
                <asp:TextBox ID="txtUserPassword2" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvtxtUserPassword2" runat="server" ControlToValidate="txtUserPassword2"
                    Display="Dynamic" ErrorMessage="יש לחזור על הסיסמה" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvPasswords" runat="server" ControlToCompare="txtUserPassword"
                    ControlToValidate="txtUserPassword2" Display="Dynamic" ErrorMessage="הוזנו סיסמאות לא זהות"
                    ValidationGroup="1"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 109px">
                <asp:Label ID="lblFirstName" runat="server" Text="שם פרטי:"></asp:Label>
            </td>
            <td style="width: 129px">
                <asp:TextBox ID="txtFirtstName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvtxtFirstName" runat="server" ControlToValidate="txtFirtstName"
                    ErrorMessage="חובה להזין שם פרטי" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 109px">
                <asp:Label ID="lblLastName" runat="server" Text="שם משפחה:"></asp:Label>
            </td>
            <td style="width: 129px">
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 109px">
                <asp:Label ID="lbDisplayName" runat="server" Text="שם להצגה:"></asp:Label>
            </td>
            <td style="width: 129px">
                <asp:TextBox ID="txtDisplayName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvtxtDisplayName" runat="server" ControlToValidate="txtDisplayName"
                    ErrorMessage="חובה להזין שם להצגה"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 109px">
                <asp:Label ID="lblEmail" runat="server" Text="אימייל:"></asp:Label>
            </td>
            <td style="width: 129px; direction: ltr;">
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" ControlToValidate="txtEmail"
                    Display="Dynamic" ErrorMessage="חובה להזין אימייל" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="האימייל שהוכנס אינו תקין"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"
                    ValidationGroup="1"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 109px">
                &nbsp;
            </td>
            <td style="width: 129px">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 109px">
                <asp:Button ID="btnOk" runat="server" Text="אישור" OnClick="btnOk_Click" ValidationGroup="1" />
            </td>
            <td style="width: 129px" align="center">
                <asp:Button ID="btnCancel" runat="server" Text="ביטול" CausesValidation="False" PostBackUrl="~/Default.aspx" />
            </td>
            <td align="right" dir="rtl">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="hlSignIn" runat="server" NavigateUrl="~/Login.aspx" Visible="false">התחברות</asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
