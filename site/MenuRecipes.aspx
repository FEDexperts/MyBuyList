<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="MenuRecipes.aspx.cs" Inherits="PageMenuRecipes" Title="<%$ Resources:MyGlobalResources, ChoiceMenuRecipesPageTitle %>" %>

<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<%@ Register TagPrefix="uc1" TagName="SearchSimple" Src="~/UserControls/ucSearchSimple.ascx" %>
<%@ Register TagPrefix="uc2" TagName="SearchComplex" Src="~/UserControls/ucSearchComplex.ascx" %>
<%@ Register TagPrefix="uc3" TagName="SearchByCategories" Src="~/UserControls/ucSearchByCategories.ascx" %>
<%@ Register TagPrefix="uc4" TagName="MyRecipes" Src="~/UserControls/ucAllRecipesList.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">

    <script type="text/javascript">

        function CheckMenuRecipeBeforeDeletion(menuId, recipeId) {
            PageMethods.CheckMenuRecipeBeforeDeletion(menuId, recipeId, OnBeforeDeletionSucceeded, OnActionFailed);
        }
        function OnBeforeDeletionSucceeded(results) {
            var recipeExistsInMeals = results[0];
            var menuId = results[1];
            var recipeId = results[2];
            var confirmationMessage = results[3];
            if (recipeExistsInMeals) {
                if (confirm(confirmationMessage)) {
                    PageMethods.DeleteMenuRecipe(menuId, recipeId, OnDeleteSucceeded, OnActionFailed);
                }
            }
            else {
                PageMethods.DeleteMenuRecipe(menuId, recipeId, OnDeleteSucceeded, OnActionFailed);
            }
        }
        function OnDeleteSucceeded(rowIndex) {
            $get('<%= btnRefreshMenuRecipes.ClientID %>').click();
        }
        function OnActionFailed(results) {
        }
    </script>

    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr style="height: 15px">
            <td>
                <asp:Label runat="server" Font-Bold="true" Text="חיפוש מתכונים" />
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
                <asp:LinkButton ID="btnSearchByCategories" runat="server" ForeColor="Crimson" Font-Bold="true"
                    Font-Underline="false" Text='<%$ Resources:MyGlobalResources, SearchByCategories %>'
                    OnClick="btnSearchByCategories_Click"></asp:LinkButton>
                <asp:Label ID="lblSeparator1" runat="server" ForeColor="Crimson">&nbsp;|&nbsp;</asp:Label>
                <asp:LinkButton ID="btnSearchSimple" runat="server" ForeColor="Crimson" Font-Bold="true"
                    Font-Underline="false" Text='<%$ Resources:MyGlobalResources, SearchSimple %>'
                    OnClick="btnSearchSimple_Click"></asp:LinkButton>
                <asp:Label ID="lblSeparator2" runat="server" ForeColor="Crimson">&nbsp;|&nbsp;</asp:Label>
                <asp:LinkButton ID="btnSearchComplex" runat="server" ForeColor="Crimson" Font-Bold="true"
                    Font-Underline="false" Text='<%$ Resources:MyGlobalResources, SearchComplex %>'
                    OnClick="btnSearchComplex_Click"></asp:LinkButton>
                <asp:Label ID="lblSeparator3" runat="server" ForeColor="Crimson">&nbsp;|&nbsp;</asp:Label>
                <asp:LinkButton ID="btnMyRecipes" runat="server" ForeColor="Crimson" Font-Bold="true" Font-Underline="false"
                    Text="המתכונים שלי" OnClick="btnMyRecipes_Click"></asp:LinkButton>
                <asp:Label ID="lblSeparator4" runat="server" ForeColor="Crimson">&nbsp;|&nbsp;</asp:Label>
                <asp:LinkButton ID="btnMyFavoritesRecipes" runat="server" ForeColor="Crimson" Font-Bold="true"
                    Font-Underline="false" Text="המתכונים המועדפים שלי" OnClick="btnMyFavoritesRecipes_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Font-Bold="true" Text="לצפייה במתכון לחץ על שם המתכון. לבחירת המתכון לחץ על החץ הורוד. "></asp:Label>
            </td>
        </tr>
        <tr style="height: 5px">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ucSearchSimple" />
                    </Triggers>
                    <ContentTemplate>
                        <uc1:SearchSimple ID="ucSearchSimple" runat="server" OnAddRecipeToCallback="Search_AddRecipeToCallback" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <uc2:SearchComplex ID="ucSearchComplex" runat="server" OnAddRecipeToCallback="Search_AddRecipeToCallback" />
                <uc3:SearchByCategories ID="ucSearchByCategories" runat="server" OnAddRecipeToCallback="Search_AddRecipeToCallback" />
                <uc4:MyRecipes ID="ucMyRecipes" runat="server" OnAddRecipeToCallback="Search_AddRecipeToCallback" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="LeftContent" ContentPlaceHolderID="LeftContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="updpMenuRecipes" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr valign="top">
                    <td style="height: 100%;">
                        <asp:Label ID="lblMenuRecipes" runat="server" Font-Bold="true" Text="<%$ Resources:MyGlobalResources, MenuRecipes %>"></asp:Label><br />
                        <asp:Label ID="Label2" runat="server" Text="(לחיצה על החץ השחור תסיר את המתכון)"
                            Font-Bold="true" Font-Size="12px"></asp:Label><br />
                        <asp:Panel ID="pnlMenuRecipes" runat="server" Height="280px" Width="210px" ScrollBars="Auto">
                            <asp:Button ID="btnRefreshMenuRecipes" runat="server" OnClick="btnRefreshMenuRecipes_Click"
                                Style="display: none" />
                            <table id="tblMenuRecipes" style="width: 90%">
                                <asp:Repeater ID="rptMenuRecipes" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnRemoveRecipe" runat="server" ImageAlign="Middle" ImageUrl="~/Images/arrow-next.gif"
                                                    recipeId='<%#DataBinder.Eval(Container.DataItem ,"RecipeId")%>' OnClick="btnRemoveRecipeFromMenu_Click" />
                                            </td>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem ,"Recipe.RecipeName")%>
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
                        &nbsp;&nbsp;<asp:Button ID="btnPrev" runat="server" Width="50px" Text='<%$ Resources:MyGlobalResources, Previous %>'
                            OnClick="btnPrev_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnNext" runat="server" Width="50px" Text='<%$ Resources:MyGlobalResources, Next %>'
                            OnClick="btnNext_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
