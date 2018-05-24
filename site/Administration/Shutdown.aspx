<%@ Page Title="Shutdown" Language="C#" MasterPageFile="~/Administration/AdminMasterPage.master"
    AutoEventWireup="true" CodeFile="Shutdown.aspx.cs" Inherits="Administration_Shutdown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    Password:
    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
    <br />
    <asp:Button ID="btnShutdown" runat="server" Text="Shutdown" OnClick="btnShutdown_Click" />
    <ajaxToolkit:ConfirmButtonExtender ID="btnShutdown_ConfirmButtonExtender" 
        runat="server" ConfirmText="Are you sure?" Enabled="True" TargetControlID="btnShutdown" />
    <br />
    <asp:Button ID="btnAppDomainUnload" runat="server" OnClick="btnAppDomainUnload_Click"
        Text="Unload AppDomain" />
    <ajaxToolkit:ConfirmButtonExtender ID="btnAppDomainUnload_ConfirmButtonExtender" 
        runat="server" ConfirmText="Are you sure?" Enabled="True" 
        TargetControlID="btnAppDomainUnload" />
</asp:Content>
