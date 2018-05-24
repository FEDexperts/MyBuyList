<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReciepesSearchControl.ascx.cs"
    Inherits="UserControls_ReciepesSearchControl" %>

<script>
    var lnkNewRecipeClientId = '<%=lnkNewRecipe.ClientID%>';
</script>

<div style="width: 100%">
    <asp:Panel ID="Panel1" runat="server">
        <div>
            הצג:
        <asp:LinkButton ID="lnkAllMenus" Style="color: #A4CB3A" runat="server" Text="כל המתכונים"
            OnClick="lnkAllMenus_Click" />
            <span style="margin-left: 5px">|</span>
            <asp:LinkButton ID="lnkMyMenus" Style="color: #A4CB3A" runat="server" Text="המתכונים שלי"
                OnClick="lnkMyMenus_Click" />
            <span style="margin-left: 5px">|</span>
            <asp:LinkButton ID="lnkFavMenus" Style="color: #A4CB3A" runat="server" Text="המועדפים שלי"
                OnClick="lnkFavMenus_Click" />
        </div>
        <div>
            <table style="padding-top: 10px; width: 100%">
                <tr style="vertical-align: top">
                    <td style="width: 30%; padding-top: 10px;">
                        <asp:DropDownList ID="lstCategories" runat="server" Width="137px" Height="18px" BackColor="#DDECB6"
                            Font-Bold="true" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #A4CB3A; padding-right: 5px;"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="lstCategories_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 30%; padding-top: 10px;">
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
