<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRecipesFilter.ascx.cs"
    Inherits="UserControls_ucRecipesFilter" %>

<script src="UserControls/scripts/ucRecipesFilter.js"></script>

<script>
    var lnkNewRecipeClientId = '<%=lnkNewRecipe.ClientID%>';
</script>

<style>
    .recipes-search-option {
        text-decoration: none !important;
        color: #A4CB3A !important;
    }
    
    .recipes-search-option:hover {
        text-decoration: underline !important;
    }
</style>

<div style="width: 80%; float: left;">
    <asp:Panel ID="Panel1" runat="server">

        <%--<span>
            הצג:
        </span>
        <span>
            <asp:HyperLink ID="lnkRecipes" runat="server" CssClass="recipes-search-option" data-href="Recipes.aspx">כל המתכונים</asp:HyperLink>
        </span>
        <span>|</span>
        <span>
            <asp:HyperLink ID="lnkMyRecipes" runat="server" CssClass="recipes-search-option" data-href="Recipes.aspx?page=1&orderby=LastUpdate&disp=MyRecipes">המתכונים שלי</asp:HyperLink>
        </span>
        <span>|</span>
        <span>
            <asp:HyperLink ID="lnkMyFavoriteRecipes" runat="server" CssClass="recipes-search-option" data-href="Recipes.aspx?page=1&orderby=LastUpdate&disp=MyFavoriteRecipes">המועדפים שלי</asp:HyperLink>
        </span>--%>

        <div>
            <table style="width: 100%">
                <tr style="vertical-align: top">
                    <td style="width: 15%; padding-top: 10px;">
                        <asp:DropDownList ID="lstCategories" runat="server" Width="137px" Height="18px" BackColor="#DDECB6"
                            Font-Bold="true" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #A4CB3A; padding-right: 5px;"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="lstCategories_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 15%; padding-top: 10px;">
                        <div class="sort">
                            <asp:DropDownList ID="ddlSortBy" runat="server" Width="129px" Height="18px" BackColor="#DDECB6"
                                Font-Bold="true" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #A4CB3A; padding-right: 5px;"
                                onchange="return changeSort(this);">
                                <asp:ListItem Text="מיין לפי" Value="0"></asp:ListItem>
                                <asp:ListItem Text="תאריך" Value="LastUpdate"></asp:ListItem>
                                <asp:ListItem Text="שם" Value="Name"></asp:ListItem>
                                <asp:ListItem Text="מחבר" Value="Publisher"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div class="add-recipe">
                            <asp:HyperLink ID="lnkNewRecipe" runat="server">
                                <asp:Image runat="server" ImageUrl="~/Images/btn_AddNewRecipe_up.png" onmouseover='this.src="Images/btn_AddNewRecipe_over.png";' 
                                    onmouseout='this.src="Images/btn_AddNewRecipe_up.png";' onmousedown='this.src="Images/btn_AddNewRecipe_Down.png";' 
                                    nmouseup='this.src="Images/btn_AddNewRecipe_up.png";' />
                            </asp:HyperLink>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</div>
