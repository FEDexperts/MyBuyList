<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMealMenu.ascx.cs" Inherits="ucMealMenu" %>
<asp:Panel ID="pnlClear" runat="server" Width="480px" Height="20px">
    &nbsp;<asp:LinkButton ID="btnClearAll" runat="server" Text='<%$ Resources:MyGlobalResources, ClearAll %>'
        OnClick="btnClearAll_Click" OnClientClick='<%$ Resources:ValidationResources, ConfirmClearAllMealRecipes %>'></asp:LinkButton>
</asp:Panel>
<table>
    <tr>
        <td style="width: 235px;">
            &nbsp;
        </td>
        <td>
            <asp:Label ID="Label3" runat="server" Text="מנות"></asp:Label>
        </td>
    </tr>
</table>
<asp:Panel ID="pnlMeals" runat="server" Width="100%" Style="min-height: 345px">
    <table style="width: 95%; border-color: Black;" border="1" cellspacing="0">
        <tr>
            <td id="dayNameCell" runat="server" style="width: 18%">
                &nbsp;&nbsp;
                <asp:Label ID="lblDayName" runat="server" Width="60px" Font-Bold="true" Text=""></asp:Label>
            </td>
            <td>
                <table style="width: 100%;" border="1" cellspacing="0">
                    <asp:Repeater ID="rptCourseTypes" runat="server" OnItemDataBound="rptCourseTypes_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td id="dinersCell" runat="server" style="width: 40%; background-color: #e8e8e8">
                                    <%--<asp:TextBox ID="txtDiners" runat="server" MaxLength="3" Width="25px"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeDiners" runat="server" TargetControlID="txtDiners"
                                        FilterType="Numbers" />--%>
                                    <%#DataBinder.Eval(Container.DataItem, "CourseTypeName")%>
                                </td>
                                <td id="mealTypeCell" runat="server" coursetypeid='<%#DataBinder.Eval(Container.DataItem, "CourseTypeId")%>'
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
</asp:Panel>
