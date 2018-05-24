<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="ShopDepartment.aspx.cs" Inherits="PageShopDepartment" Title="<%$ Resources:MyGlobalResources, DepartmentEdition%>" %>

<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <table width="100%">
        <tr style="height: 70px">
            <td>
                <asp:Label ID="lblDepName" runat="server" Font-Bold="true" Text='<%$ Resources:MyGlobalResources, DepartmentName%>'></asp:Label>&nbsp;&nbsp;
                <asp:TextBox ID="txtDepName" runat="server" Width="225px" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqValidDepartmentName" runat="server" ValidationGroup="general"
                    ControlToValidate="txtDepName" ErrorMessage='<%$ Resources:ValidationResources, DepartmentNameIsRequired%>'></asp:RequiredFieldValidator>
                <br />
                <asp:CustomValidator ID="custValidDepartmentName" runat="server" ValidationGroup="general"
                    ErrorMessage='<%$ Resources:ValidationResources, DepartmentNameDuplicate %>'
                    OnServerValidate="custValidDepartmentName_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnOK" runat="server" Text='<%$ Resources:MyGlobalResources, OK%>'
                    OnClick="btnOK_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text='<%$ Resources:MyGlobalResources, Cancel%>'
                    CausesValidation="false" PostBackUrl="~/Admin/ShopDepartmentsList.aspx" />
            </td>
        </tr>
    </table>
</asp:Content>
