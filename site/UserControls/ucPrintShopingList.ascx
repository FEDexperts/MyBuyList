<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPrintShopingList.ascx.cs"
    Inherits="UserControls_ucPrintShopingList" %>
<table>
    <tr>
        <td style="font-family: Arial">
            <b>מצרכים: </b>
        </td>
    </tr>
    <tr>
        <td style='padding-right: 30px;'>
            <asp:Repeater ID="rpDep" runat="server" OnItemDataBound="Dep_DataBound">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblDepName" runat="server" Font-Bold="true" Font-Underline="true"
                                    ForeColor="#C51015" Font-Names="Arial" Text='<%#DataBinder.Eval(Container.DataItem ,"ShopDepartmentName")%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style='padding-right: 10px;'>
                                <table>
                                    <asp:Repeater ID="rptFoods" runat="server" OnItemDataBound="rptFoods_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 15px; height: 15px; border-style: solid; border-width: 1px;
                                                                    border-color: Black;">
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblFood" runat="server" Font-Names="Arial"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="2">
                                                                <asp:Image ID="imgFood" runat="server" Style="max-width: 100px; max-height: 140px"
                                                                    Visible="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr>
        <td>
            <br />
            <b>מוצרים נוספים:</b>
        </td>
    </tr>
    <tr>
        <td style='padding-right: 40px;'>
            <table>
                <asp:Repeater ID="rpAdditionalsItems" runat="server" OnItemDataBound="AdditionalsItem_DataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblItemName" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </td>
    </tr>
    <tr style="height: 20px;">
        <td>
            &nbsp;<br />
        </td>
    </tr>
    <tr>
        <td>
            <b>מתכונים: </b>
        </td>
    </tr>
    <tr>
        <td style='padding-right: 40px'>
            <table>
                <asp:Repeater ID="rpRecipes" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblRecipeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem ,"RecipeName")%>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </td>
    </tr>
</table>
