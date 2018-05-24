<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AjaxIngridiants.ascx.cs" Inherits="UserControls_AjaxIngridiants" %>

<script src="Scripts/AjaxIngridiants.js"></script>

<script>
    var quantityClientId = '<%=txtQuantity.ClientID %>';
    var fractionClientId = '<%=ddlFractions.ClientID %>';
    var unitClientId = '<%=ddlMeasurementUnits.ClientID %>';
    var foodNameClientId = '<%=txtFoodName.ClientID %>';
    var ingredientsClientId = '<%=pnlIngredients.ClientID %>';
    var foodRemarkClientId = '<%=txtFoodRemark.ClientID %>';
    var hfSelectedIngridiantClientId = '<%=hfSelectedIngridiant.ClientID %>';
    var hfIngridiantsClientId = '<%=hfIngridiants.ClientID %>';
    var hfFoodIdClientId = '<%=hfFoodId.ClientID %>';
    var decimalSeperator = '<%=DecimalSeperator%>';
</script>

<asp:HiddenField ID="hfFoodId" runat="server" />
<asp:HiddenField ID="hfSelectedIngridiant" runat="server" />
<asp:HiddenField ID="hfIngridiants" runat="server" />

<style type="text/css">
    a.action {
        width: 103px;
        height: 33px;
        display: block;
        cursor: pointer;
    }
    #addIngridiant {
        background-image: url('Images/btn_AddProduct_up.png');
    }
    #ajaxIngridiants input.text {
        width: 48px;
        height: 14px;
        background-color: #ddecb6;
        border: 1px solid #A4CB3A;
        margin-top: 6px;
        color: #656565;
        font-size: 12px;
    }
    #updateIngridiant {
        background-image: url('Images/btn_EditProduct_up.png');
    }
    .floatRight {
        float: right;
    }
    .col {
        margin-left: 5px;
    }
    .row {
        font-size: 12pt;
        height: 20px;
    }
    .listBtn {
        float: right;
        cursor: pointer;
        font-family: Arial;
    }
    .updateBtn{
        color: blue;
    }
    .deleteBtn{
        color: red;
    }
</style>

<div id="ajaxIngridiants">
    <div style="margin-top: 20px;">
        <div style="float: right; height: 42px; width: 50px; margin-left: 10px;">
            <asp:Label ID="lblQuantity" runat="server" Text="<%$ Resources:MyGlobalResources, Quantity %>"
                Font-Bold="true" />
            <asp:TextBox ID="txtQuantity" runat="server" CssClass="quantity" />
            <ajaxToolkit:FilteredTextBoxExtender ID="txtQuantity_ftb" runat="server" TargetControlID="txtQuantity"
                ValidChars="0123456789." />
        </div>
        <div style="float: right; height: 42px; width: 50px; margin-left: 10px;">
            <asp:Label ID="lblAnd" runat="server" Text="<%$ Resources:MyGlobalResources, And %>"
                Font-Bold="true" /><br />
            <asp:DropDownList ID="ddlFractions" runat="server" Width="50px" BackColor="#ddecb6"
                Height="18px" Font-Bold="true" Font-Size="16px" ForeColor="#656565" Style="border: 1px solid #A4CB3A; margin-top: 6px;">
            </asp:DropDownList>
            <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
        </div>
        <div style="float: right; height: 42px; width: 76px; margin-left: 10px;">
            <asp:Label ID="lblMeasurementUnits" runat="server" Text="<%$ Resources:MyGlobalResources, MeasurementUnit %>"
                Font-Bold="true" /><br />
            <asp:DropDownList ID="ddlMeasurementUnits" runat="server" Width="76px" BackColor="#ddecb6"
                Height="18px" Font-Bold="true" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #A4CB3A; margin-top: 6px;">
            </asp:DropDownList>
        </div>
    </div>
    <div style="float: right; height: 42px; width: 159px; margin-left: 10px;">
        <asp:Label ID="lblFoodName" runat="server" Text="שם המצרך" Font-Bold="true" /><br />
        <asp:TextBox ID="txtFoodName" runat="server" Width="157px" BackColor="#ddecb6" BorderColor="#A4CB3A"
            BorderStyle="Solid" BorderWidth="1px" Font-Size="12px" Height="14px" ForeColor="#656565"
            AutoCompleteType="Disabled" Style="margin-top: 6px;" />
        <ajaxToolkit:AutoCompleteExtender runat="server" ID="txtFoodName_ac" TargetControlID="txtFoodName"
            ServiceMethod="GetCompletionFoodList" ServicePath="~/WebServices/AutoComplete.asmx"
            MinimumPrefixLength="2" CompletionInterval="200" EnableCaching="true" CompletionSetCount="12" 
            OnClientShowing="OnClientShowing" OnClientItemSelected="OnClientItemSelected" />
    </div>
    <div style="float: right; height: 42px; width: 159px; margin-left: 10px;">
        <asp:Label ID="lblFoodRemark" runat="server" Text="אופן חיתוך, עיבוד וייעוד" Font-Bold="true" /><br />
        <asp:TextBox ID="txtFoodRemark" runat="server" Width="157px" BackColor="#ddecb6"
            ForeColor="#656565" BorderColor="#A4CB3A" BorderStyle="Solid" BorderWidth="1px"
            Font-Size="12px" Height="14px" Style="margin-top: 6px;" />
    </div>
    <div style="float: right; height: 25px; padding-top: 15px;">

        <a id="addIngridiant" class="action"></a>

        <a id="updateIngridiant" class="action"></a>
    </div>
    <%--<div style="clear: both; margin-left: 5px; margin-right: 5px; height: 20px;">
        <asp:CustomValidator ID="custValidIngredients" runat="server" ValidationGroup="ingredients"
            OnServerValidate="custValidIngredients_ServerValidate" Display="Static"></asp:CustomValidator>
    </div>--%>
    <div id="divIngredients" runat="server" style="clear: both; margin-top: 4px; margin-bottom: 20px;">
        <p style="font-weight: bold;">
            מצרכים שהוספת
        </p>
    </div>
    <asp:Panel ID="pnlIngredients" runat="server" BorderWidth="1px" BorderColor="#656565"
        BorderStyle="Solid" Width="98%" Height="140px" Font-Bold="true" Font-Size="12px"
        ScrollBars="Vertical" Style="margin-bottom: 32px;" CssClass="ingredients ingredientsEditable">
    </asp:Panel>
</div>
