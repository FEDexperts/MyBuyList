<%@ Page Language="C#" MasterPageFile="~/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="MenuDetails_old.aspx.cs" Inherits="PageMenuDetails_old" Title="<%$ Resources:MyGlobalResources, MenuDetailsPageTitle %>" %>

<%@ MasterType VirtualPath="~/ProperDevMasterPage.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <%--<tr style="height: 20px">
                </tr>--%>
                <tr>
                    <%--<td style="width: 35px">
                        &nbsp;
                    </td>--%>
                    <td>
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto" Style="height: 200px;">
                            <table id="tblMenuDetail" runat="server" style="width: 100%">
                                <tr>
                                    <td valign="top" colspan="2">
                                        <asp:Label ID="lblTitle" runat="server" ForeColor="Blue" Font-Bold="true" Height="25px"
                                            Font-Size="13pt" Text="<%$ Resources:MyGlobalResources, MenuDetails %>" />
                                    </td>
                                    <td align="center">
                                        <asp:LinkButton ID="btnBack" runat="server" Text="<%$ Resources:MyGlobalResources, Back %>"
                                            OnClick="btnBack_Click" />
                                    </td>
                                </tr>
                                <tr style="height: 25px">
                                    <td style="width: 70px" valign="middle">
                                        <asp:Label ID="lblMenuName" runat="server" Font-Bold="true" Text="<%$ Resources:MyGlobalResources, MenuName %>" />
                                    </td>
                                    <td style="width: 250px">
                                        <asp:TextBox ID="txtMenuName" runat="server" MaxLength="200" Width="250px" />
                                    </td>
                                    <td align="right">
                                        &nbsp;
                                        <asp:LinkButton ID="btnChangeName" runat="server" Text="<%$ Resources:MyGlobalResources, Change %>"
                                            OnClick="btnChangeName_Click" />
                                        <asp:LinkButton ID="btnSaveName" runat="server" Text="<%$ Resources:MyGlobalResources, Save %>"
                                            OnClick="btnSaveName_Click" />
                                        &nbsp;
                                        <asp:LinkButton ID="btnCancelName" runat="server" Text="<%$ Resources:MyGlobalResources, Cancel1 %>"
                                            OnClick="btnCancelName_Click" />
                                    </td>
                                </tr>
                                <tr style="height: 40px">
                                    <td style="width: 70px" valign="middle">
                                        <asp:Label ID="lblDescription" runat="server" Font-Bold="true" Text="<%$ Resources:MyGlobalResources, Description %>" />
                                    </td>
                                    <td style="width: 250px">
                                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="250px" />
                                    </td>
                                    <td align="right">
                                        &nbsp;
                                        <asp:LinkButton ID="btnChangeDescription" runat="server" Text="<%$ Resources:MyGlobalResources, Change %>"
                                            OnClick="btnChangeDescription_Click" />
                                        <asp:LinkButton ID="btnSaveDescription" runat="server" Text="<%$ Resources:MyGlobalResources, Save %>"
                                            OnClick="btnSaveDescription_Click" />
                                        &nbsp;
                                        <asp:LinkButton ID="btnCancelDescription" runat="server" Text="<%$ Resources:MyGlobalResources, Cancel1 %>"
                                            OnClick="btnCancelDescription_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:TextBox ID="txtData" runat="server" Visible="false"></asp:TextBox>
                        <table>
                            <tr style="height: 30px">
                                <td>
                                    <asp:Label ID="lblMenuRecipes" runat="server" Text='<%$ Resources:MyGlobalResources, MenuRecipes %>'
                                        ForeColor="Crimson" Font-Bold="true" />
                                </td>
                                <td>
                                    &nbsp;|&nbsp;
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnMenuRecipesEdit" runat="server" ForeColor="Black" Font-Bold="true"
                                        Font-Underline="false" Text='<%$ Resources:MyGlobalResources, Edit %>' OnClick="btnMenuRecipesEdit_Click" />
                                    <asp:Label ID="lblSeparator1" runat="server" ForeColor="Black">&nbsp;|&nbsp;</asp:Label>
                                    <%--<asp:LinkButton ID="btnPrint" runat="server" ForeColor="Black" Font-Bold="true" Font-Underline="false"
                                        Text='<%$ Resources:MyGlobalResources, Print %>'  OnClick="btnMenuRecipesPrint_Click"/>--%>
                                    <asp:HyperLink ID="btnPrintMenuRecipes" runat="server" Target="print" ForeColor="Black"
                                        Font-Bold="true" Font-Underline="false" Text='<%$ Resources:MyGlobalResources, Print %>' />
                                    <asp:Label ID="lblSeparator2" runat="server" ForeColor="Black">&nbsp;|&nbsp;</asp:Label>
                                    <asp:LinkButton ID="btnSendMail" runat="server" ForeColor="Black" Font-Bold="true"
                                        Font-Underline="false" Text='<%$ Resources:MyGlobalResources, SendMail %>' OnClick="btnMenuRecipesSendMail_Click" />
                                    <asp:Label ID="lblMenuRecipesResult" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text='<%$ Resources:MyGlobalResources, PlanningMenu %>'
                                        ForeColor="Crimson" Font-Bold="true" />
                                </td>
                                <td>
                                    &nbsp;|&nbsp;
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnMenuMealsEdit" runat="server" ForeColor="Black" Font-Bold="true"
                                        Font-Underline="false" Text='<%$ Resources:MyGlobalResources, Edit %>' OnClick="btnMenuMealsEdit_Click" />
                                    <asp:Label ID="Label2" runat="server" ForeColor="Black">&nbsp;|&nbsp;</asp:Label>
                                    <%--<asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Black" Font-Bold="true"
                                Font-Underline="false" Text='<%$ Resources:MyGlobalResources, Print %>' OnClick="btnMenuMealsPrint_Click" />--%>
                                    <asp:HyperLink ID="btnPrintMenuMeals" runat="server" Target="print" ForeColor="Black"
                                        Font-Bold="true" Font-Underline="false" Text='<%$ Resources:MyGlobalResources, Print %>' />
                                    <asp:Label ID="Label3" runat="server" ForeColor="Black">&nbsp;|&nbsp;</asp:Label>
                                    <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Black" Font-Bold="true"
                                        Font-Underline="false" Text='<%$ Resources:MyGlobalResources, SendMail %>' OnClick="btnMenuMealsSendMail_Click" />
                                    <asp:Label ID="lblMenuMealsResult" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text='<%$ Resources:MyGlobalResources, ShoppingList %>'
                                        ForeColor="Crimson" Font-Bold="true" />
                                </td>
                                <td>
                                    &nbsp;|&nbsp;
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnShoppingListEdit" runat="server" ForeColor="Black" Font-Bold="true"
                                        Font-Underline="false" Text='<%$ Resources:MyGlobalResources, Edit %>' OnClick="btnShoppingListEdit_Click" />
                                    <asp:Label ID="Label5" runat="server" ForeColor="Black">&nbsp;|&nbsp;</asp:Label>
                                    <asp:HyperLink ID="btnPrintShoppingList" runat="server" Target="print" ForeColor="Black"
                                        Font-Bold="true" Font-Underline="false" Text='<%$ Resources:MyGlobalResources, Print %>' />
                                    <asp:Label ID="Label6" runat="server" ForeColor="Black">&nbsp;|&nbsp;</asp:Label>
                                    <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="Black" Font-Bold="true"
                                        Font-Underline="false" Text='<%$ Resources:MyGlobalResources, SendMail %>' OnClick="btnShoppingListSendMail_Click" />
                                    <asp:Label ID="lblShoppingListResult" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblResult" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
