<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="MenuMeals.aspx.cs" Inherits="PageMenuMeals" Theme="Standard" Title="<%$ Resources:MyGlobalResources, PlanningMenu %>" %>

<%@ Register Src="~/UserControls/ucMealMenu.ascx" TagPrefix="uc1" TagName="MealMenu" %>
<%@ Register Src="~/UserControls/ucWeeklyMenu.ascx" TagPrefix="uc2" TagName="WeeklyMenu" %>
<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <div id="main">
        &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="גרור את המתכון מהרשימה בצד שמאל למקום המתאים בתפריט"
            Font-Bold="True"></asp:Label>
        <uc1:MealMenu ID="ucMealMenu" runat="server" Visible="false" />
        <uc2:WeeklyMenu ID="ucWeeklyMenu" runat="server" Visible="false" />
        <table>
            <tr>
                <td style="width: 94%; height: 32px">
                    &nbsp;
                    <%--<asp:LinkButton ID="btnPrint" runat="server" ForeColor="Black" Font-Bold="true" Font-Underline="false"
                        Text='<%$ Resources:MyGlobalResources, Print %>' OnClick="btnPrint_Click" />--%>
                    <asp:HyperLink ID="btnPrintMenuMeals" runat="server" Target="print" ForeColor="Black"
                        Font-Bold="true" Font-Underline="false" Text='<%$ Resources:MyGlobalResources, Print %>' />
                    <asp:Label ID="lblSeparator1" runat="server" ForeColor="Black">&nbsp;|&nbsp;</asp:Label>
                    <asp:LinkButton ID="btnSendMail" runat="server" ForeColor="Black" Font-Bold="true"
                        Font-Underline="false" Text='<%$ Resources:MyGlobalResources, SendMail %>' OnClick="btnSendMail_Click" />
                    <asp:Label ID="lblSeparator2" runat="server" ForeColor="Black">&nbsp;|&nbsp;</asp:Label>
                    <asp:LinkButton ID="btnShoppingList" runat="server" ForeColor="Black" Font-Bold="true"
                        Font-Underline="false" Text='<%$ Resources:MyGlobalResources, ShoppingList %>'
                        OnClick="btnShoppingList_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblResult" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="LeftContent" ContentPlaceHolderID="LeftContentPlaceHolder" runat="Server">
    <div>
        <table style="height: 100%;">
            <tr>
                <td>
                    <asp:Label ID="lblMenuRecipes" runat="server" Font-Bold="true" Text='<%$ Resources:MyGlobalResources, MenuRecipes %>'
                        Width="120px" Height="25px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlMenuRecipes" runat="server" Width="204px" Height="285px" ScrollBars="Auto">
                        <asp:Repeater ID="rptMenuRecipes" runat="server">
                            <ItemTemplate>
                                <asp:Panel ID="pnlItem" runat="server" CssClass="item" recipeId='<%# Eval("RecipeID") %>'>
                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Recipe.RecipeName") %>' />
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;<asp:Button ID="btnPrev" runat="server" Width="50px" Text='<%$ Resources:MyGlobalResources, Previous %>'
                        OnClick="btnPrev_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnNext" runat="server" Width="50px" Text='<%$ Resources:MyGlobalResources, Next %>'
                        OnClick="btnNext_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
