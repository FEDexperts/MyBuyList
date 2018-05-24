<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderControl.ascx.cs" Inherits="UC_HeaderControl" %>
<%@ Register Src="~/UserControls/ucSearch.ascx" TagPrefix="MBL" TagName="SearchBox" %>

<style type="text/css">
    .menuItem {
        width: 150px;
        float: right;
        font-size: 10pt;
    }
</style>

<div id="header">
    <div class="helloUser_and_links">
        <div class="helloUser">
            <asp:Label ID="lblHeaderUserName" runat="server" Font-Bold="true" CssClass="HelloUser" Text="שלום, אורח" />
            <a class="login" onclick="return allowLeave()">כניסה</a>
        </div>
        <div class="links">
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Recipes.aspx">מתכונים</asp:HyperLink>
            <span>|</span>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Menus.aspx">תפריטים</asp:HyperLink>
            <span>|</span>
            <asp:PlaceHolder ID="PlaceHolderJoin" runat="server">
                <asp:HyperLink ID="lnkReg1" NavigateUrl="~/Register.aspx" runat="server" Text="הצטרפות"
                    onclick='return allowLeave()'></asp:HyperLink>
                <span>|</span>
            </asp:PlaceHolder>
            <asp:HyperLink ID="lnkHowToUse" runat="server" Text="צור קשר" NavigateUrl="~/ContactUs.aspx"
                onclick='return allowLeave()'></asp:HyperLink>
        </div>
    </div>
    <div class="logo-and-search">
        <span class="logo">
            <asp:HyperLink ID="lnkLogo" runat="server" NavigateUrl="~/" onclick='return allowLeave()'>
                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/mybuylist.png" />
            </asp:HyperLink>
        </span>
        <span class="search">
            <MBL:SearchBox runat="server" ID="Search" />
        </span>
    </div>
</div>    
