<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAllRecipesList.ascx.cs"
    Inherits="ucAllRecipesList" %>
<%@ Register Src="~/UserControls/ucRecipe.ascx" TagPrefix="uc1" TagName="Recipe" %>
<%@ Register Src="~/UserControls/ucRecipeCategories.ascx" TagPrefix="uc2" TagName="RecipeCategories" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <asp:Panel ID="pnlList" runat="server" Width="460px" ScrollBars="Auto">
            <table style="width: 100%; padding: 0px;">
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblResultsCount" runat="server" Font-Bold="true" ForeColor="Crimson"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gridRecipesList" runat="server" ShowHeader="false" PageSize="11"
                            OnRowDataBound="gridRecipesList_RowDataBound" OnPageIndexChanging="gridRecipesList_PageIndexChanging">
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
                                        <asp:HyperLink ID="btnViewRecipe" runat="server" Width="200px" ForeColor="Black"
                                            Font-Underline="false" recipeId='<%#DataBinder.Eval(Container.DataItem ,"RecipeId")%>'
                                            Text='<%#DataBinder.Eval(Container.DataItem ,"RecipeName")%>'></asp:HyperLink>
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
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
