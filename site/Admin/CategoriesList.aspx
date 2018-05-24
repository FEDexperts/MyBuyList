<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="CategoriesList.aspx.cs" Inherits="PageCategoriesList" Title="<%$ Resources:MyGlobalResources, Categories %>" %>

<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td style="width: 440px">
                                    <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Text="<%$ Resources:MyGlobalResources, Categories %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnBack" runat="server" Text="<%$ Resources:MyGlobalResources, Back %>"
                                        PostBackUrl="~/Admin/Admin.aspx"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Vertical" BorderColor="LightGray"
                            BorderWidth="1px" Height="355px" Width="460px" Style="padding: 5px 5px 5px 5px">
                            <asp:TreeView ID="tvCategories" runat="server" ForeColor="Black" HoverNodeStyle-Font-Underline="true"
                                HoverNodeStyle-BackColor="LightSteelBlue" HoverNodeStyle-ForeColor="White" HoverNodeStyle-Font-Bold="true">
                            </asp:TreeView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:MyGlobalResources, Add %>"
                            PostBackUrl="~/Admin/Category.aspx" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
