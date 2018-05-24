<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="GeneralItem.aspx.cs" Inherits="PageGeneralItem" Title="<%$ Resources:MyGlobalResources, ItemEdition%>" %>

<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <table width="100%">
        <tr style="height: 70px">
            <td>
                <asp:Label ID="lblItemName" runat="server" Font-Bold="true" Text='<%$ Resources:MyGlobalResources, ItemName%>'></asp:Label>&nbsp;&nbsp;
                <asp:TextBox ID="txtItemName" runat="server" Width="225px" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqValidItemName" runat="server" ValidationGroup="general"
                    ControlToValidate="txtItemName" ErrorMessage='<%$ Resources:ValidationResources, ItemNameIsRequired%>'></asp:RequiredFieldValidator>
                <br />
                <asp:CustomValidator ID="custValidItemName" runat="server" ValidationGroup="general"
                    ErrorMessage='<%$ Resources:ValidationResources, ItemNameDuplicate %>'
                    OnServerValidate="custValidItemName_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnOK" runat="server" Text='<%$ Resources:MyGlobalResources, OK%>'
                    OnClick="btnOK_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text='<%$ Resources:MyGlobalResources, Cancel%>'
                    CausesValidation="false" PostBackUrl="~/Admin/GeneralItemsList.aspx" />
            </td>
        </tr>
    </table>
</asp:Content>
