<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="PersonalArea.aspx.cs" Inherits="PagePersonalArea" Title="<%$ Resources:MyGlobalResources, MyPersonalAreaPageTitle %>" %>

<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<%@ Register TagPrefix="uc1" TagName="Menus" Src="~/UserControls/ucMenusList.ascx" %>
<%@ Register TagPrefix="uc2" TagName="MyRecipes" Src="~/UserControls/ucMyRecipesList.ascx" %>
<%@ Register TagPrefix="uc3" TagName="FavoritesRecipes" Src="~/UserControls/ucFavoritesRecipes.ascx" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">

    <script type="text/javascript">

        
        function _onCategoriesLoaded() {
            var modalPopupBehavior = $find('child2');
            modalPopupBehavior.show();
            modalPopupBehavior._backgroundElement.style.zIndex = 300001;
            modalPopupBehavior._foregroundElement.style.zIndex = 300002;
        }

        function _onSelectPictureLoaded() {
            var iframe1 = document.getElementById('iframe1');
            iframe1.src = "../SelectRecipePicture.aspx";
            var modalPopupBehavior = $find('child3');
            modalPopupBehavior.show();
            modalPopupBehavior._backgroundElement.style.zIndex = 300001;
            modalPopupBehavior._foregroundElement.style.zIndex = 300002;
        }

        // hide select picture modal
        function HideSelectPicture() {
            var modal = $find('child3');
            modal.hide();
        }
        
    </script>

    <script type="text/javascript">
        
function changed(recipeId){
    PageMethods.AllowRecipe(recipeId);
}
        
function EditItem_MouseOver(element)
{
     element.style.color = "White";
     element.style.backgroundColor = "LightSteelBlue";
}
function EditItem_MouseOut(element)
{
     element.style.color = "Black";
     element.style.backgroundColor = "Transparent";
}
function  HideRecipeEdition()
{
    var modal = $find('editModal');
    modal.hide();
}
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 40px">
                    </td>
                    <td>
                        <asp:LinkButton ID="btnMenusList" runat="server" ForeColor="Crimson" Font-Bold="true"
                            Font-Underline="false" Text='<%$ Resources:MyGlobalResources, MyMenusList %>'
                            OnClick="btnMenusList_Click"></asp:LinkButton>
                        <asp:Label ID="lblSeparator1" runat="server" ForeColor="Crimson">&nbsp;|&nbsp;</asp:Label>
                        <asp:LinkButton ID="btnMyRecipesList" runat="server" ForeColor="Crimson" Font-Bold="true"
                            Font-Underline="false" Text='<%$ Resources:MyGlobalResources, MyRecipesList %>'
                            OnClick="btnMyRecipesList_Click"></asp:LinkButton>
                        <asp:Label ID="lblSeparator2" runat="server" ForeColor="Crimson">&nbsp;|&nbsp;</asp:Label>
                        <asp:LinkButton ID="btnMyFavoritesRecipes" runat="server" ForeColor="Crimson" Font-Bold="true"
                            Font-Underline="false" Text='<%$ Resources:MyGlobalResources, MyFavoritesRecipes %>'
                            OnClick="btnMyFavoritesRecipes_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10px">
                        <td>
                            <table style="width: 95%">
                                <tr>
                                    <td style="width: 40%" valign="top">
                                        <uc1:Menus ID="ucMenusList" runat="server" />
                                        <uc2:MyRecipes ID="ucMyRecipesList" runat="server" />
                                        <uc3:FavoritesRecipes ID="ucFavoritesRecipes" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
