<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<%@ Register Assembly="ProperControls" Namespace="ProperControls.BasicControls.Containers"
    TagPrefix="proper" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    
    <p style="text-align:right;margin-right:200px;margin-top:30px;">
    צר לנו, אך אירעה שגיאה בלתי צפויה במערכת.<br />נשמח אם תדווחו לנו על כך במייל הבא: <a href="mailto:support@mybuylist.com">support@mybuylist.com</a>.
    </p>
    <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"  ></asp:Label>
    <asp:Label ID="lblStackTrace" runat="server" Text="" Visible="false" ></asp:Label>
</asp:Content>
