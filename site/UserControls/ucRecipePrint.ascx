<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRecipePrint.ascx.cs"
    Inherits="UserControls_RecipePrint" %>
<%@ Register TagPrefix="uc1" TagName="Ingredients" Src="~/UserControls/ucPrintIngredients.ascx" %>
<asp:Panel ID="pnlRecipe" runat="server">
    <table dir='rtl'>
        <tr>
            <td>
                <asp:Label ID="lblRecipeName" runat="server" Text="" Font-Bold="true" Font-Size="Larger"
                    ForeColor="#C51015"></asp:Label>
            </td>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td align="center">
                                        <asp:Image ID="imgContainer1" runat="server" Style="max-width: 100px; max-height: 140px"  Visible="false"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblimgContainer1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td align="center">
                                        <asp:Image ID="imgContainer2" runat="server" Style="max-width: 100px; max-height: 140px" Visible="false"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblimgContainer2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td align="center">
                                        <asp:Image ID="imgContainer3" runat="server" Style="max-width: 100px; max-height: 140px" Visible="false"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblimgContainer3" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr dir='rtl'>
            <td colspan="2">
                <div>
                    <asp:ListView ID="lvServs" runat="server" OnItemDataBound="Servs_DataBound">
                        <LayoutTemplate>
                            <ul dir="rtl">
                                <div id="itemPlaceholder" runat="server">
                                </div>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div style="float: right;">
                                <table style="border-left-style: solid; border-left-width: thin;">
                                    <tr>
                                        <td>
                                            <uc1:Ingredients ID="printIngredients" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <div>
                            </div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
            </td>
        </tr>
        <tr style="height: 20px;">
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblTools" runat="server" Text="כלים:" Font-Bold="true" Font-Size="Larger"
                    Font-Names="Arial" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 20px;" colspan="2">
                <asp:Literal ID="ltrlTools" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr style="height: 20px;">
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblPrep" runat="server" Text="אופן ההכנה:" Font-Bold="true" Font-Size="Larger"
                    Font-Names="Arial" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblPrepCommon" runat="server" Font-Bold="true" Font-Names="Arial"
                    ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 20px;" colspan="2">
                <asp:Literal ID="ltrlPrep" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Panel>
