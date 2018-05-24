<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPrintIngredients.ascx.cs"
    Inherits="UserControls_PrintIngredients" %>
<table dir='rtl'>
    <tr>
        <td>
            <asp:Label ID="lblIngridTitle" runat="server" Text="" Font-Bold="true" Font-Size="Larger"
                ForeColor="Black"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="padding-right: 20px;">
            <table>
                <asp:Repeater ID="rptIngridList" runat="server" OnItemDataBound="IngridItem_DataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblIngrValue" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblUnitName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem ,"MeasurementUnit.UnitName")%>'>&nbsp;</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblIngridName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem ,"Food.FoodName")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem ,"Remarks")%>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </td>
    </tr>
 
</table>
