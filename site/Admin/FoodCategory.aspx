<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="FoodCategory.aspx.cs" Inherits="PageFoodCategory" Title="<%$ Resources:MyGlobalResources, FoodCategoryEdition%>" %>

<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <table width="100%">
        <tr>
            <td style="width: 79px">
                <asp:Label ID="lblFoodCategoryName" runat="server" Font-Bold="true" Text='<%$ Resources:MyGlobalResources, FoodCategory%>'></asp:Label>&nbsp;&nbsp;
           </td>
           <td>
                <asp:TextBox ID="txtFoodCategoryName" runat="server" MaxLength="100" Width="225px"></asp:TextBox>
           </td>
           <td>
                <asp:RequiredFieldValidator ID="reqValidFoodCategoryName" runat="server" ValidationGroup="general"
                    ControlToValidate="txtFoodCategoryName" ErrorMessage='<%$ Resources:ValidationResources, FoodCategoryNameIsRequired%>'></asp:RequiredFieldValidator>
                <br />
                <asp:CustomValidator ID="custValidFoodCategoryName" runat="server" ValidationGroup="general"
                    ErrorMessage='<%$ Resources:ValidationResources, FoodCategoryNameDuplicate %>' OnServerValidate="custValidFoodCategoryName_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 79px">
                <asp:Label ID="lblParentFoodCategory" runat="server" Font-Bold="true" Text='<%$ Resources:MyGlobalResources, ParentFoodCategory%>'></asp:Label>
               <%-- <asp:Label ID="txtParentFoodCategory" runat="server" Text="בחר" Style=" padding:0px 0px" Width="184px" />--%>
           </td>
           <td colspan="2">
                <asp:TextBox ID="txtParentFoodCategory" runat="server"  Text="בחר" Width="181px" Style="text-align:center;"></asp:TextBox>
                 <asp:Panel ID="pnlFoodCategories" runat="server" CssClass="ContextMenuPanel" ScrollBars="Auto"
                    Height="300px" Width="180px" Style="display: none; visibility: hidden;">
                    <asp:TreeView ID="tvFoodCategories" runat="server" ForeColor="Black" HoverNodeStyle-Font-Underline="true"
                        HoverNodeStyle-BackColor="LightSteelBlue" HoverNodeStyle-ForeColor="White" HoverNodeStyle-Font-Bold="true"
                        SelectedNodeStyle-BackColor="Blue" SelectedNodeStyle-ForeColor="White" OnSelectedNodeChanged="tvFoodCategories_SelectedNodeChanged">
                    </asp:TreeView>
                </asp:Panel>
                &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnClearParentFoodCategory" runat="server" Text='<%$ Resources:MyGlobalResources, Clear%>' OnClick="btnClearParentFoodCategory_Click" />
                <ajaxToolkit:DropDownExtender runat="server" ID="DDE" TargetControlID="txtParentFoodCategory" DropDownControlID="pnlFoodCategories" />
           </td>
        </tr>
        <tr>
        </tr>
        <tr>
            <td colspan="3" >
                <asp:Button ID="btnOK" runat="server" Text='<%$ Resources:MyGlobalResources, OK%>'
                    OnClick="btnOK_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:Button ID="btnCancel" runat="server" Text='<%$ Resources:MyGlobalResources, Cancel%>'
                    CausesValidation="false" PostBackUrl="~/Admin/FoodCategoriesList.aspx" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;         
               <asp:Button ID="btnDelete" runat="server" Text='<%$ Resources:MyGlobalResources, Delete%>'
                    CausesValidation="false" OnClientClick='<%$ Resources:ValidationResources, ConfirmFoodCategoryDelete %>' OnClick="btnDelete_Click"/>
           </td>
        </tr>
    </table>
</asp:Content>
