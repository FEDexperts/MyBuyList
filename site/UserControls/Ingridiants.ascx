<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Ingridiants.ascx.cs" Inherits="UserControls_Ingridiants" %>

<script src="Scripts/Ingridiants.js"></script>

<script>
    var quantityClientId = '<%=txtQuantity.ClientID %>';
    var fractionClientId = '<%=ddlFractions.ClientID %>';
    var unitClientId = '<%=ddlMeasurementUnits.ClientID %>';
    var foodRemarkClientId = '<%=txtFoodRemark.ClientID %>';
    var hfSelectedIngridiantClientId = '<%=hfSelectedIngridiant.ClientID %>';
    var hfFoodIdClientId = '<%=hfFoodId.ClientID %>';
    var decimalSeperator = '<%=DecimalSeperator%>';
</script>

<asp:HiddenField ID="hfFoodId" runat="server" />
<asp:HiddenField ID="hfSelectedIngridiant" runat="server" />
<asp:HiddenField ID="hfIngridiants" runat="server" ClientIDMode="Static" />

<style type="text/css">
    .uc-ingridiants {
    }

    .ingredients-container {
        border: 1px solid #656565;
        width: 98%;
        height: 140px;
        font-weight: bold;
        font-size: 12px;
        margin-bottom: 32px;
    }

    .input-section {
        display: flex;
        justify-content: flex-start;
        margin-top: 20px;
    }

    .input-section-item {
        height: 42px;
        width: 50px;
        margin-top: 6px;
        margin-left: 21px;
    }

        .input-section-item input.text {
            height: 14px;
            background-color: #ddecb6;
            border: 1px solid #A4CB3A;
            color: #656565;
            font-size: 12px;
        }

        .input-section-item #txtQuantity{
            width: 48px;
        }

    .ingridiant-prefix {
        background-color: #ddecb6;
        border: 1px solid #A4CB3A;
        width: 100%;
    }

    .ingridiant-list {
        background-color: #ddecb6;
        border: 1px solid #A4CB3A;
        width: 157px;
        overflow: auto;
        max-height: 100px;
        margin-top: 1px;
        display: none;
        color: #656565
    }

    .ingridiant-list-item {
        color: #656565;
        font-size: 12px;
        cursor: pointer;
    }

        .ingridiant-list-item:hover {
            background-color: #808080;
        }

    .drop-down-fractions {
        width: 50px;
        background-color: #ddecb6;
        height: 18px;
        font-weight: bold;
        font-size: 16px;
        color: #656565;
        border: 1px solid #A4CB3A;
        margin-top: 6px;
    }

    drop-down-measurement-units {
        width: 76px;
        background-color: #ddecb6;
        height: 18px;
        font-weight: bold;
        font-size: 12px;
        color: #656565;
        border: 1px solid #A4CB3A;
        margin-top: 6px;
    }

    .food-process {
        width: 157px;
        background-color: #ddecb6;
        color: #656565;
        border: 1px solid #A4CB3A;
        font-size: 12px;
        height: 14px;
        margin-top: 6px;
    }

    a.action {
        width: 103px;
        height: 33px;
        display: block;
        cursor: pointer;
    }

    #addIngridiant {
        background-image: url('Images/btn_AddProduct_up.png');
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

    .updateBtn {
        color: blue;
    }

    .deleteBtn {
        color: red;
    }
</style>

<div class="uc-ingridiants">
    <div class="input-section">
        <div class="input-section-item">
            <asp:Label ID="lblQuantity" runat="server" Text="<%$ Resources:MyGlobalResources, Quantity %>"
                Font-Bold="true" />
            <asp:TextBox ID="txtQuantity" ClientIDMode="Static" runat="server" CssClass="quantity" />
            <ajaxToolkit:FilteredTextBoxExtender ID="txtQuantity_ftb" runat="server" TargetControlID="txtQuantity"
                ValidChars="0123456789." />
        </div>
        <div class="input-section-item">
            <asp:Label ID="lblAnd" runat="server" Text="<%$ Resources:MyGlobalResources, And %>"
                Font-Bold="true" /><br />
            <asp:DropDownList ID="ddlFractions" runat="server" CssClass="drop-down-fractions"></asp:DropDownList>
            <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
        </div>
        <div class="input-section-item" style="width: 76px;">
            <asp:Label ID="lblMeasurementUnits" runat="server" Text="<%$ Resources:MyGlobalResources, MeasurementUnit %>"
                Font-Bold="true" /><br />
            <asp:DropDownList ID="ddlMeasurementUnits" class="drop-down-measurement-units" runat="server"></asp:DropDownList>
        </div>
        <div class="input-section-item" style="width: 135px;">
            <asp:Label ID="lblFoodName" runat="server" Text="<%$ Resources:MyGlobalResources, IngridiantName %>" Font-Bold="true" /><br />
            <input id="ingridiantName" type="text" class="ingridiant-prefix" />
            <input id="ingridiantId" type="hidden" />
            <div id="ingridiantsList" class="ingridiant-list"></div>
        </div>
        <div class="input-section-item" style="width: 135px;">
            <asp:Label ID="lblFoodRemark" runat="server" Text="<%$ Resources:MyGlobalResources, IngridiantProcess %>" Font-Bold="true" /><br />
            <asp:TextBox ID="txtFoodRemark" CssClass="food-process" runat="server" />
        </div>
        <div class="input-section-item" style="height: 25px; padding-top: 7px;">
            <a id="addIngridiant" class="action"></a>
            <a id="updateIngridiant" class="action"></a>
        </div>
    </div>
    <div id="divIngredients" runat="server" style="clear: both; margin-top: 4px; margin-bottom: 20px;">
        <p style="font-weight: bold;">
            מצרכים שהוספת
        </p>
    </div>
    <asp:Panel ID="ingredientsContainer" ClientIDMode="Static" runat="server"
        ScrollBars="Vertical" CssClass="ingredients-container ingredients ingredientsEditable">
    </asp:Panel>
</div>
