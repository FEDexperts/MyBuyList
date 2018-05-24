<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/AdminMasterPage.master" AutoEventWireup="true" CodeFile="LoggerReview.aspx.cs" Inherits="Administration_Logger_LoggerReview" %>

<%@ Register src="LoggerView.ascx" tagname="LoggerView" tagprefix="proper" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <proper:LoggerView ID="LoggerView1" runat="server" />

</asp:Content>

