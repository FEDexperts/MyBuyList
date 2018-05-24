<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMenusList.ascx.cs" Inherits="ucMenusList" %>
<asp:Panel ID="pnlList" runat="server" Width="470px" Height="380px" ScrollBars="Auto">
    <table style="width: 80%">
        <asp:Repeater ID="rptMenusList" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="width: 450px">
                        <asp:ImageButton ID="btnDeleteMenu" runat="server" ImageAlign="Middle" ImageUrl="~/Images/x_normal.gif"
                            menuId='<%#DataBinder.Eval(Container.DataItem ,"MenuId")%>' AlternateText='<%$ Resources:MyGlobalResources, Delete %>'
                            OnClientClick='<%$ Resources:ValidationResources, ConfirmMenuDelete %>' OnClick="btnDeleteMenu_Click" />&nbsp;
                        <asp:ImageButton ID="btnShoppingList" runat="server" ImageAlign="Middle" ImageUrl="~/Images/cart.gif"
                            menuId='<%#DataBinder.Eval(Container.DataItem ,"MenuId")%>' AlternateText='<%$ Resources:MyGlobalResources, ShoppingList %>'
                            OnClick="btnShoppingList_Click" />&nbsp;
                        <asp:LinkButton ID="btnMenuDetails" runat="server" Width="300px" ForeColor="Black"
                            Font-Underline="false" menuId='<%#DataBinder.Eval(Container.DataItem ,"MenuId")%>'
                            OnMouseOver="EditItem_MouseOver(this);" OnMouseOut="EditItem_MouseOut(this);"
                            Text='<%#DataBinder.Eval(Container.DataItem ,"MenuName")%>' OnClick="btnMenuDetails_Click"></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
<asp:LinkButton ID="btnQuickMenu" runat="server" ForeColor="#990099" Font-Bold="true"
    Font-Underline="false" Text='<%$ Resources:MyGlobalResources, QuickMenu %>' OnClick="btnQuickMenu_Click" />
<asp:Label ID="lblSeparator" runat="server" ForeColor="#990099">&nbsp;|&nbsp;</asp:Label>
<asp:LinkButton ID="btnMealMenu" runat="server" ForeColor="#990099" Font-Bold="true"
    Font-Underline="false" Text='<%$ Resources:MyGlobalResources, MealMenu %>' OnClick="btnMealMenu_Click" />
<asp:Label ID="lblSeparator2" runat="server" ForeColor="#990099">&nbsp;|&nbsp;</asp:Label>
<asp:LinkButton ID="btnWeeklyMenu" runat="server" ForeColor="#990099" Font-Bold="true"
    Font-Underline="false" Text='<%$ Resources:MyGlobalResources, WeeklyMenu %>'
    OnClick="btnWeeklyMenu_Click" />
<asp:Label ID="lblSeparator3" runat="server" ForeColor="#990099">&nbsp;|&nbsp;</asp:Label>
<asp:LinkButton ID="btnManyWeeksMenu" runat="server" ForeColor="#990099" Font-Bold="true"
    Font-Underline="false" Text='<%$ Resources:MyGlobalResources, ManyWeeksMenu %>'
    OnClick="btnManyWeeksMenu_Click" />
