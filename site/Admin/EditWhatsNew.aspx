<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="EditWhatsNew.aspx.cs" Inherits="Admin_EditWhatsNew" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <div style="padding-left: 10px; font-size: 14px; font-weight: bold;">
        <p>
            הזנת שני מתכונים ושני תפריטים שיופיעו בעמוד הבית באיזור "מה חדש".
            <br />
            יש להזין את מספרי הזיהוי של המתכונים / תפריטים (מס' הזיהוי מופיע בסוף כתובת הדף,
            כאשר נמצאים בדף פרטי מתכון / תפריט).
        </p>
    </div>
    <br />
    <br />
    <asp:Label runat="server" Text="מתכונים חדשים:" Font-Bold="true" Font-Size="Larger" />
    <br />
    <br />
    <asp:TextBox ID="txtNewRecipe1" runat="server" Width="100px" />
    &nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtNewRecipe2" runat="server" Width="100px" />
    <br />
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="תפריטים חדשים:" Font-Bold="true" Font-Size="Larger" />
    <br />
    <br />
    <asp:TextBox ID="txtNewMenu1" runat="server" Width="100px" />
    &nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtNewMenu2" runat="server" Width="100px" />
    <br />
    <br />
    <br />
    <asp:Button ID="btnSave" runat="server" Text="שמירה" OnClick="btnSave_Click" />
    <br />
    <br />
    <asp:Label ID="lblStatus" runat="server" Visible="false" />
</asp:Content>
