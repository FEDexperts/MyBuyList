<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPrintWeeklyMenu.ascx.cs"
    Inherits="UserControls_PrintWeeklyMenu" %>
<asp:Repeater ID="rpDays" runat="server" OnItemDataBound="Day_DataBound">
    <ItemTemplate>
        <table border="1" style="width: 100%; border-color: Gray;" cellspacing="0" cellpadding="0">
            <tr>
                <td style="width: 70px; padding-right: 20px; border-width: 0px;">
                    <asp:Label ID="lblDay" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Repeater ID="rpDayMeals" runat="server" OnItemDataBound="DayMeals_DataBound">
                        <ItemTemplate>
                            <table border="1" cellspacing="0" cellpadding="0" style="width: 100%; border-color:lightgray">
                                <tr>
                                    <td style="width: 150px; vertical-align: top; border-width:0px;">
                                        &nbsp;
                                        <asp:Label ID="lblMealTypeName" runat="server" Font-Bold="true" Text='<%#DataBinder.Eval(Container.DataItem ,"MealTypeName")%>'></asp:Label>
                                        -
                                        <asp:Label ID="lblDiners" runat="server"></asp:Label>
                                    </td>
                                    <td style="border-width: 0px; border-right: solid 1px gray">
                                        <asp:Repeater ID="rpMealRecipes" runat="server" OnItemDataBound="Recipe_DataBound">
                                            <ItemTemplate>
                                                <table border="0" cellspacing="0" cellpadding="1">
                                                    <tr>
                                                        <td>
                                                            &nbsp;<asp:Label ID="lblServs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem ,"Servings")%>'></asp:Label>
                                                            מנות -
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblbRecipeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem ,"Recipe.RecipeName")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:Repeater>
