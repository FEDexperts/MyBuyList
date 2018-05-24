<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSearch.ascx.cs" Inherits="UserControls_ucSearch" %>

<style>
    .search-btn{
        float: left;
        margin-left: 15px;
    }   
    .search-value{
        border-width: 0px;
        height: 32px;
        font-size: 14pt;
        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
    }
</style>

<script type="text/javascript">
    var hfSearchedValueClientId = '<%=hfSearchValue.ClientID%>';
    var hfSearchCategoryIdClientId = '<%=hfSelectedCategoryId.ClientID%>';
    var hfSearchCategoryTextClientId = '<%=hfSelectedCategoryText.ClientID%>'
</script>

<script type="text/javascript" src="Scripts/Search.js"></script>

<div class="search-box">
    <asp:TextBox ID="TextBoxSearchValue" runat="server" CssClass="search-value"></asp:TextBox>
    <asp:DropDownList ID="DropDownListSearchType" runat="server" CssClass="search-category search-value" DataTextField="value" DataValueField="key">
        <asp:ListItem Value="1">מתכונים</asp:ListItem>
        <asp:ListItem Value="2" class="requireLogin">המתכונים שלי</asp:ListItem>
        <asp:ListItem Value="3" class="requireLogin">המתכונים המועדפים שלי</asp:ListItem>
        <asp:ListItem Value="4">תפריטים</asp:ListItem>
        <asp:ListItem Value="5" class="requireLogin">התפריטים שלי</asp:ListItem>
        <asp:ListItem Value="6" class="requireLogin">התפריטים המועדפים שלי</asp:ListItem>
    </asp:DropDownList>
    <div class="search-btn">
        <asp:ImageButton ID="ImageButtonSearch" runat="server" ImageUrl="~/Images/Very-Basic-Search-icon.png" CssClass="search-button" OnClientClick="return false;" />
    </div>
</div>

<asp:HiddenField ID="hfSelectedCategoryId" runat="server" />
<asp:HiddenField ID="hfSelectedCategoryText" runat="server" />
<asp:HiddenField ID="hfSearchValue" runat="server" />
