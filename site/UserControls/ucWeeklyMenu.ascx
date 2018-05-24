<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucWeeklyMenu.ascx.cs"
    Inherits="ucWeeklyMenu" %>
<asp:Panel ID="pnlNavigator" runat="server" Width="485px" Height="20px">
    &nbsp;
    <asp:LinkButton ID="btnClearAll" runat="server" Text='<%$ Resources:MyGlobalResources, ClearAll %>'
        OnClick="btnClearAll_Click" OnClientClick='<%$ Resources:ValidationResources, ConfirmClearAllMealRecipes %>'></asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="btnPrevWeek" runat="server" Text='<%$ Resources:MyGlobalResources, PrevWeek %>'
        OnClick="btnPrevWeek_Click"></asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="btnNextWeek" runat="server" Text='<%$ Resources:MyGlobalResources, NextWeek %>'
        OnClick="btnNextWeek_Click"></asp:LinkButton>&nbsp;&nbsp;
    <asp:Label ID="lblWeekNumber" runat="server" Text=" "/>
</asp:Panel>
<table>
    <tr>
        <td style="width: 230px;" rowspan="2">
            &nbsp;
        </td>
        
        <td>
            <asp:Label ID="Label3" runat="server" Text="מנות"></asp:Label>
        </td>
    </tr>
</table>
<asp:Panel ID="pnlMeals" runat="server" Width="98%">
    <table style="width: 95%; border-color: Black;" border="1" cellspacing="0">
        <tr>
            <td style="width: 100%" valign="top">
                <asp:Repeater ID="rptDayWeekList" runat="server" OnItemDataBound="rptDayWeekList_ItemDataBound">
                    <ItemTemplate>
                        <table style="width: 100%; border-color: White;" border="1" cellspacing="0">
                            <tr>
                                <td id="dayNameCell" runat="server" style="border-color: Black; border-width: 1px;
                                    width: 18%">
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblDayName" runat="server" Width="60px" Font-Bold="true" Text='<%#DataBinder.Eval(Container.DataItem ,"DayName")%>'></asp:Label>
                                </td>
                                <td id="dayWeekCell" runat="server" style="border-color: Black; border-width: 1px;
                                    width: 82%">
                                    <asp:HiddenField ID="hidDayIndex" runat="server" Value='<%#DataBinder.Eval(Container.DataItem ,"DayId")%>' />
                                    <table style="width: 100%;" border="1" cellspacing="0">
                                        <asp:Repeater ID="rptMealTypes" runat="server" OnItemDataBound="rptMealTypes_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td id="dinersCell" runat="server" style="width: 40%; background-color: #e8e8e8">
                                                        <%--<asp:TextBox ID="txtDiners" runat="server" MaxLength="3" Width="25px"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeDiners" runat="server" TargetControlID="txtDiners"
                                                            FilterType="Numbers" />--%>
                                                        <%#DataBinder.Eval(Container.DataItem, "MealTypeName")%>
                                                    </td>
                                                    <td id="mealTypeCell" runat="server" mealtypeid='<%#DataBinder.Eval(Container.DataItem, "MealTypeId")%>'
                                                        style="width: 60%">
                                                        <asp:Repeater ID="rptMealRecipes" runat="server" OnItemDataBound="rptMealRecipes_ItemDataBound">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtServings" runat="server" MaxLength="3" Width="25px" Text='<%#DataBinder.Eval(Container.DataItem ,"Servings")%>'></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender
                                                                                    ID="ftbeServings" runat="server" TargetControlID="txtServings" FilterType="Numbers" />
                                                                            </td>
                                                                            <td>
                                                                                <%#DataBinder.Eval(Container.DataItem, "RecipeName")%>
                                                                            </td>
                                                                            <td>
                                                                                <img id="btnRemoveRecipe" runat="server" align="Middle" src="~/Images/x_normal.gif"
                                                                                    style="cursor: pointer" mealid='<%#DataBinder.Eval(Container.DataItem ,"MealId")%>'
                                                                                    recipeid='<%#DataBinder.Eval(Container.DataItem ,"RecipeId")%>' onclick="DeleteItem(event);" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
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
    </table>
</asp:Panel>
