<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master"
    AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="Article" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div style="clear: both; height: 20px; text-align: left; padding-left: 20px;">
        <asp:HyperLink ID="btnAdminEdit" runat="server" Text="ערוך מאמר" NavigateUrl="~/Admin/EditArticles.aspx"
            Visible="false" Font-Underline="true" Font-Size="16px" Font-Bold="true" />
    </div>
    <div id="article_wrapper">
        <div id="title" style="margin-top: 60px; text-align: center;">
            <asp:Label ID="lblTitle" runat="server" ForeColor="#EF1E3D" Font-Size="25px" Font-Bold="true" />
        </div>
        <div id="author" style="margin-top: 20px; text-align: center;">
            <asp:Label ID="lblAuthor1" runat="server" Font-Size="18px" Font-Bold="true" Text='פורסם ע"י' />&nbsp;
            <asp:Label ID="lblAuthor2" runat="server" Font-Size="18px" ForeColor="#EF1E3D" Font-Bold="true" />
        </div>
        <div id="abstract" style="margin-top: 40px;">
            <asp:Label ID="lblAbstract" runat="server" Font-Bold="true" />
        </div>
        <div id="articleBody" style="margin-top: 20px;" runat="server">
        </div>
        <div id="date" style="margin-top: 20px; font-style: italic; text-align: left;">
            <asp:Label ID="lblDate" runat="server" Font-Size="14px" Font-Bold="true" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
