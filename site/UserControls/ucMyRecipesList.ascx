<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMyRecipesList.ascx.cs"
    Inherits="ucMyRecipesList" %>
<%@ Register Src="~/UserControls/ucRecipe.ascx" TagPrefix="uc1" TagName="Recipe" %>
<%@ Register Src="~/UserControls/ucRecipeCategories.ascx" TagPrefix="uc2" TagName="RecipeCategories" %>
<%@ Register Src="~/UserControls/ucRecipePicture.ascx" TagPrefix="uc3" TagName="RecipePicture" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="Panel4" runat="server">
    <table style="width: 99%">
        <tr>
            <td colspan="4">
                <asp:LinkButton ID="btnNewRecipe" runat="server" ForeColor="#990099" Font-Bold="true"
                    Font-Underline="false" Text='<%$ Resources:MyGlobalResources, NewRecipe %>' OnClick="btnNewRecipe_Click"></asp:LinkButton>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" Width="470px" Height="380px" ScrollBars="Auto">
                    <table cellspacing="0" cellpadding="2">
                        <asp:UpdatePanel ID="upListUpdate" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ucRecipe" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Repeater ID="rptRecipesList" runat="server" OnItemDataBound="Recipe_DataBound">
                                    <ItemTemplate>
                                        <tr onmouseover="EditItem_MouseOver(this);" onmouseout="EditItem_MouseOut(this);">
                                            <td>
                                                <asp:CheckBox ID="cbIsApproved" runat="server" Visible="false" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnDeleteRecipe" runat="server" ImageUrl="~/Images/x_normal.gif"
                                                    recipeId='<%#DataBinder.Eval(Container.DataItem ,"RecipeId")%>' AlternateText='<%$ Resources:MyGlobalResources, Delete %>'
                                                    OnClientClick='<%$ Resources:ValidationResources, ConfirmRecipeDelete %>' OnClick="btnDeleteRecipe_Click" />&nbsp;&nbsp;
                                                <asp:LinkButton ID="btnViewRecipe" runat="server" ForeColor="Black" Font-Underline="false"
                                                    recipeId='<%#DataBinder.Eval(Container.DataItem ,"RecipeId")%>' Text='<%#DataBinder.Eval(Container.DataItem ,"RecipeName")%>'
                                                    OnClick="btnViewRecipe_Click"></asp:LinkButton>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem ,"User.DisplayName")%>'
                                                    Visible="false"></asp:Label>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr id="result" runat="server" visible="false">
            <td>
                <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel>
<uc1:Recipe ID="ucRecipe" runat="server" OnShowCategoriesClick="ShowCategories_Click"
    OnSelectPictureClick="SelectPicture_Click" />
<asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ucRecipe" />
    </Triggers>
    <ContentTemplate>
    </ContentTemplate>
</asp:UpdatePanel>
<uc2:RecipeCategories ID="ucRecipeCats" runat="server" OnRefreshData="RecipeCategories_RefreshData" />
<uc3:RecipePicture ID="ucRecipePicture" runat="server" />
