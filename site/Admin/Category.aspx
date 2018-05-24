<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="Category.aspx.cs" Inherits="PageCategory" Title="<%$ Resources:MyGlobalResources, CategoryEdition%>" %>

<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <table width="100%">
        <tr>
            <td style="width: 200px; height: 26px;" valign="top">
                <asp:Label ID="lblCategoryName" runat="server" Font-Bold="true" Text='<%$ Resources:MyGlobalResources, RecipeCategory%>'></asp:Label>
            </td>
            <td style="width: 239px; height: 26px;" valign="top">
                <asp:TextBox ID="txtCategoryName" runat="server" MaxLength="100" Width="225px"></asp:TextBox>
            </td>
            <td style="width: 400px; height: 26px;" valign="top">
                <asp:RequiredFieldValidator ID="reqValidCategoryName" runat="server" ValidationGroup="general"
                    ControlToValidate="txtCategoryName" 
                    ErrorMessage='<%$ Resources:ValidationResources, CategoryNameIsRequired%>' 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="custValidCategoryName" runat="server" ValidationGroup="general"
                    ErrorMessage='<%$ Resources:ValidationResources, CategoryNameDuplicate %>' 
                    OnServerValidate="custValidCategoryName_ServerValidate" Display="Dynamic"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 190px; height: 49px;" valign="top">
                <asp:Label ID="lblParentCategory" runat="server" Font-Bold="true" Text='<%$ Resources:MyGlobalResources, ParentCategory%>'></asp:Label>
                &nbsp;&nbsp;
                
                <%--<asp:Label ID="txtParentCategory" runat="server" Text="" Style="display: block; padding: 2px 30px 2px 30px;"Width="200px" />--%>
                &nbsp;&nbsp;
                <asp:Panel ID="pnlCategories" runat="server" CssClass="ContextMenuPanel" ScrollBars="Auto"
                    Height="300px" Width="180px" Style="display: none; visibility: hidden;">
                    <asp:TreeView ID="tvCategories" runat="server" ForeColor="Black" HoverNodeStyle-Font-Underline="false"
                        HoverNodeStyle-BackColor="LightSteelBlue" HoverNodeStyle-ForeColor="White" HoverNodeStyle-Font-Bold="true"
                        SelectedNodeStyle-BackColor="Blue" SelectedNodeStyle-ForeColor="White" OnSelectedNodeChanged="tvCategories_SelectedNodeChanged">
                    </asp:TreeView>
                </asp:Panel>
            </td>
            <td style="width: 239px; height: 49px;" valign="top">
                <asp:TextBox ID="txtParentCategory" runat="server"  Text="בחר" Width="181px" Style="text-align:center;"></asp:TextBox>
                
                &nbsp;&nbsp;<asp:LinkButton ID="btnClearParentCategory" runat="server" Text='<%$ Resources:MyGlobalResources, Clear%>' OnClick="btnClearParentCategory_Click" />
                <ajaxToolkit:DropDownExtender runat="server" ID="DDE" TargetControlID="txtParentCategory"
                    DropDownControlID="pnlCategories" />
            </td>
            <td style="width: 400px; height: 49px;" valign="top">
                </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnOK" runat="server" Text='<%$ Resources:MyGlobalResources, OK%>'
                    OnClick="btnOK_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text='<%$ Resources:MyGlobalResources, Cancel%>'
                    CausesValidation="false" PostBackUrl="~/Admin/CategoriesList.aspx" />&nbsp;&nbsp;
               <asp:Button ID="btnDelete" runat="server" Text='<%$ Resources:MyGlobalResources, Delete%>'
                    CausesValidation="false" OnClientClick='<%$ Resources:ValidationResources, ConfirmCategoryDelete %>' OnClick="btnDelete_Click"/>
            </td>
        </tr>
    </table>
</asp:Content>
