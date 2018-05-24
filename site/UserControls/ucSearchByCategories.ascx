<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSearchByCategories.ascx.cs"
    Inherits="ucSearchByCategories " %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td id="cellCategoriesPath" runat="server">
                    <asp:LinkButton ID="btnSelectCategory" runat="server" Style="display: none" Text='<%$ Resources:MyGlobalResources, All %>'
                        OnClick="btnSelectCategory_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlCategories" runat="server" Width="460px" Height="104px" BorderWidth="1px"
                        BorderColor="SteelBlue" ScrollBars="Vertical">
                        <table style="width: 90%">
                            <asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td align="right">
                                            &nbsp;
                                            <asp:LinkButton ID="btnCategory" runat="server" categoryId='<%#DataBinder.Eval(Container.DataItem ,"CategoryId")%>'
                                                Text='<%#DataBinder.Eval(Container.DataItem ,"CategoryName")%>' ForeColor="Navy"
                                                OnClick="btnSelectCategory_Click" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblResultsCount" runat="server" Font-Bold="true" ForeColor="Crimson"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblComment" runat="server" Font-Bold="true" Font-Size="12px" Text="<%$ Resources:MyGlobalResources, SearchRecipeComment %>"
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gridRecipesList" runat="server" AllowPaging="true" PageSize="7"
                        ShowHeader="false" OnRowDataBound="gridRecipesList_RowDataBound" OnPageIndexChanging="gridRecipesList_PageIndexChanging" Width="460px">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="5px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnAddRecipe" runat="server" ImageAlign="Middle" ImageUrl="~/Images/arrow-previous.gif"
                                        recipeId='<%#DataBinder.Eval(Container.DataItem ,"RecipeId")%>' OnClick="btnAddRecipeToMenu_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbRecipeName" runat="server" Font-Underline="false" ForeColor="Black"
                                        Text='<%#DataBinder.Eval(Container.DataItem ,"RecipeName")%>' OnClick="lbRecipeName_Clicked"
                                        recipeId='<%#DataBinder.Eval(Container.DataItem ,"RecipeId")%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblUser" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
