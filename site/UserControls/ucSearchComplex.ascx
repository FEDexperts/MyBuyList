<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSearchComplex.ascx.cs"
    Inherits="ucSearchComplex" %>
<%@ Register Src="~/UserControls/ucRecipeCategories.ascx" TagPrefix="uc4" TagName="RecipeCategories" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Label ID="lblCategories" runat="server" Font-Bold="true" Text="<%$ Resources:MyGlobalResources, Categories %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCategories" runat="server" MaxLength="200" ReadOnly="true" Width="270px"></asp:TextBox>&nbsp;<asp:Button
                        ID="btnCategories" runat="server" Text='<%$ Resources:MyGlobalResources, Select %>'
                        OnClick="btnCategories_Click" />
                    <uc4:RecipeCategories ID="ucRecipeCats" runat="server" OnRefreshData="RecipeCategories_Rebind" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Label ID="lblFreeText" runat="server" Font-Bold="true" Text="<%$ Resources:MyGlobalResources, FreeText %>"></asp:Label>
                </td>
                <td style="width: 210px">
                    <asp:TextBox ID="txtFreeText" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 63px">
                    <asp:Label ID="lblServingsNum" runat="server" Font-Bold="true" Text="<%$ Resources:MyGlobalResources, ServingsNum %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtServingsNum" runat="server" MaxLength="3" Width="25px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeServings" runat="server" TargetControlID="txtServingsNum"
                        FilterType="Numbers" />
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:MyGlobalResources, Search %>"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="lblResultsCount" runat="server" Font-Bold="true" ForeColor="Crimson"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblComment" runat="server" Font-Bold="true" Font-Size="12px" Text="<%$ Resources:MyGlobalResources, SearchRecipeComment %>"
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView ID="gridRecipesList" runat="server" AllowPaging="true" PageSize="10"
                        ShowHeader="false" OnRowDataBound="gridRecipesList_RowDataBound" OnPageIndexChanging="gridRecipesList_PageIndexChanging">
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
