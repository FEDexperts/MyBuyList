<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="MeasurementUnitsConvert.aspx.cs" Inherits="PageMeasurementUnitsConvert" %>

<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <table width="100%">
        <tr valign="top">
            <td style="width: 73px">
                <asp:Label ID="lblName" runat="server" Font-Bold="True" Text='<%$ Resources:MyGlobalResources, Ingredient %>'></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="txtIngredientName" runat="server" Width="225px"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender runat="server" ID="aceFoodName" TargetControlID="txtIngredientName"
                    ServiceMethod="GetCompletionFoodList" ServicePath="~/WebServices/AutoComplete.asmx"
                    MinimumPrefixLength="2" CompletionInterval="200" EnableCaching="true" CompletionSetCount="12" />
                <asp:RequiredFieldValidator ID="reqValidIngredientName" runat="server" ValidationGroup="general"
                    ControlToValidate="txtIngredientName" 
                    ErrorMessage='<%$ Resources:ValidationResources, IngredientNameIsRequired%>' 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <br />
                <asp:CustomValidator ID="custValidIngredientName" runat="server" ValidationGroup="general"
                    
                    ErrorMessage='<%$ Resources:ValidationResources, IngredientNameNotValid %>' 
                    OnServerValidate="custValidIngredientName_ServerValidate" Display="Dynamic"></asp:CustomValidator>
            </td>
        </tr>
        <tr>

            <td style="width: 73px">
                <asp:Label ID="lblFromQuantity" runat="server" Font-Bold="True" Text="כמות מ-"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFromQuantity" runat="server" Width="50px">1</asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeFromQty" runat="server" TargetControlID="txtFromQuantity"
                    ValidChars="0123456789." />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorFromQuantity" runat="server"
                    ValidationGroup="general" ControlToValidate="txtFromQuantity" ErrorMessage='<%$ Resources:ValidationResources, QuantityIsRequired %>' />
                <asp:CustomValidator ID="custValidatorFromQty" runat="server" ValidationGroup="general"
                    ControlToValidate="txtFromQuantity" ErrorMessage='<%$ Resources:ValidationResources, WrongQuantityFieldValue %>'
                    OnServerValidate="custValidatorQty_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 73px">
                <asp:Label ID="lblFromUnit" runat="server" Font-Bold="True" Text="יחידת מידה"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlFromMeasurementUnits" runat="server" Width="160px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 73px">
                <asp:Label ID="lblToQuantity" runat="server" Font-Bold="True" Text="כמות ל-"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtToQuantity" runat="server" Width="50px">1</asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeToQty" runat="server" TargetControlID="txtToQuantity"
                    ValidChars="0123456789." />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="general"
                    ControlToValidate="txtToQuantity" ErrorMessage='<%$ Resources:ValidationResources, QuantityIsRequired %>' />
                <asp:CustomValidator ID="custValidatorToQty" runat="server" ValidationGroup="general"
                    ControlToValidate="txtToQuantity" ErrorMessage='<%$ Resources:ValidationResources, WrongQuantityFieldValue %>'
                    OnServerValidate="custValidatorQty_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 73px">
                <asp:Label ID="lblToMeasurementUnits" runat="server" Font-Bold="True" Text="יחידת מידה"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlToMeasurementUnits" runat="server" Width="160px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnOK" runat="server" Text='<%$ Resources:MyGlobalResources, OK%>'
                    OnClick="btnOK_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text='<%$ Resources:MyGlobalResources, Cancel%>'
                    CausesValidation="false" PostBackUrl="~/Admin/MeasurementUnitsConvertList.aspx" />
            </td>
        </tr>
    </table>
</asp:Content>
