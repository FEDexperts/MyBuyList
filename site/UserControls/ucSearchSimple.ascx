<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSearchSimple.ascx.cs"
    Inherits="ucSearchSimple" %>
<%@ Register Src="~/UserControls/ucRecipe.ascx" TagPrefix="uc1" TagName="Recipe" %>

<script type="text/javascript">

    var KEY_ENTER = 13;

    function FreeText_KeyDown(e) {
        if (e.keyCode == KEY_ENTER) {
            $get('<%= btnSearch.ClientID %>').click();
        }
    }
     
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <table>
            <tr>
                <td style="width: 80px">
                    <asp:Label ID="lblSearch" runat="server" Font-Bold="true" Text="<%$ Resources:MyGlobalResources, FreeText %>"></asp:Label>&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtFreeText" runat="server" Width="200px" OnKeyDown="FreeText_KeyDown(event);"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:MyGlobalResources, Search %>"
                        OnClick="btnSearch_Click" />
                </td>
                <td Width="100px"></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblResultsCount" runat="server" Font-Bold="true" ForeColor="Crimson"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblComment" runat="server" Font-Bold="true" Font-Size="12px" Text="<%$ Resources:MyGlobalResources, SearchRecipeComment %>"
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="gridRecipesList" runat="server" AllowPaging="true" PageSize="11"
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
        <uc1:Recipe ID="ucRecipe" runat="server" ReadOnly="true"/>
    </ContentTemplate>
</asp:UpdatePanel>
