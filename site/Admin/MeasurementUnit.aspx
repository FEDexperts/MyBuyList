<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="MeasurementUnit.aspx.cs" Inherits="PageMeasurementUnit" Title="<%$ Resources:MyGlobalResources, MeasurementUnitEdition%>" %>

<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <table width="100%">
        <tr style="height: 70px">
            <td>
                <asp:Label ID="lblUnitName" runat="server" Font-Bold="true" Text='<%$ Resources:MyGlobalResources, UnitType%>'></asp:Label>&nbsp;&nbsp;
                <asp:TextBox ID="txtUnitName" runat="server" Width="225px" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqValidUnitName" runat="server" ValidationGroup="general"
                    ControlToValidate="txtUnitName" ErrorMessage='<%$ Resources:ValidationResources, MeasurementUnitIsRequired%>'></asp:RequiredFieldValidator>
                <br />
                <asp:CustomValidator ID="custValidUnitName" runat="server" ValidationGroup="general"
                    ErrorMessage='<%$ Resources:ValidationResources, MeasurementUnitNameDuplicate %>'
                    OnServerValidate="custUnitName_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnOK" runat="server" Text='<%$ Resources:MyGlobalResources, OK%>'
                    OnClick="btnOK_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text='<%$ Resources:MyGlobalResources, Cancel%>'
                    CausesValidation="false" PostBackUrl="~/Admin/MeausurementUnitsList.aspx" />
            </td>
        </tr>
    </table>
</asp:Content>
