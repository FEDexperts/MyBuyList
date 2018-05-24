<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucIngridians.ascx.cs"
    Inherits="UserControls.UcIngridians" %>

<style>
    .uc-ingridiants{
        width: 450px;
    }
</style>

<div class="uc-ingridiants">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div style="margin-top: 20px;">
                <div style="float: right; height: 42px; width: 50px; margin-left: 10px;">
                    <asp:Label ID="lblQuantity" runat="server" Text="<%$ Resources:MyGlobalResources, Quantity %>"
                        Font-Bold="true" />
                    <asp:TextBox ID="txtQuantity" runat="server" Width="48px" BackColor="#ddecb6" BorderColor="#A4CB3A"
                        BorderStyle="Solid" BorderWidth="1px" Height="14px" Style="margin-top: 6px;"
                        ForeColor="#656565" Font-Size="12px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtQuantity_ftb" runat="server" TargetControlID="txtQuantity"
                        ValidChars="0123456789." />
                </div>
                <div style="float: right; height: 42px; width: 76px; margin-left: 10px;">
                    <asp:Label ID="lblMeasurementUnits" runat="server" Text="<%$ Resources:MyGlobalResources, MeasurementUnit %>"
                        Font-Bold="true" /><br />
                    <asp:DropDownList ID="ddlMeasurementUnits" runat="server" Width="76px" BackColor="#ddecb6"
                        Height="18px" Font-Bold="true" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #A4CB3A;
                        margin-top: 6px;">
                    </asp:DropDownList>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div style="float: right; height: 42px; width: 159px; margin-left: 10px;">
                <asp:Label ID="lblFoodName" runat="server" Text="שם המצרך" Font-Bold="true" /><br />
                <asp:TextBox ID="txtFoodName" runat="server" Width="157px" BackColor="#ddecb6" BorderColor="#A4CB3A"
                    BorderStyle="Solid" BorderWidth="1px" Font-Size="12px" Height="14px" ForeColor="#656565"
                    AutoCompleteType="Disabled" Style="margin-top: 6px;" />
                <ajaxToolkit:AutoCompleteExtender runat="server" ID="txtFoodName_ac" TargetControlID="txtFoodName"
                    ServiceMethod="GetCompletionFoodList" ServicePath="~/WebServices/AutoComplete.asmx"
                    MinimumPrefixLength="2" CompletionInterval="200" EnableCaching="true" CompletionSetCount="12" />
            </div>
            <div style="float: right; height: 25px; padding-top: 15px;">
                <asp:LinkButton ID="btnAddIngerdient" runat="server" OnCommand="btnAddItemCommand"
                    CommandArgument="<%$ Resources:MyGlobalResources, Add %>" CausesValidation="false"
                    OnClientClick='setDirty(); SuspendLeaveConfirmation();'>
                    <asp:Image ID="imgAdd" runat="server" ImageUrl="~/Images/btn_AddProduct_up.png" onmouseover='this.src="../Images/btn_AddProduct_over.png";'
                        onmouseout='this.src="../Images/btn_AddProduct_up.png";' onmousedown='this.src="../Images/btn_AddProduct_down.png";'
                        onmouseup='this.src="../Images/btn_AddProduct_up.png";' />
                    <asp:Image ID="imgUpdate" runat="server" ImageUrl="~/Images/btn_EditProduct_up.png"
                        onmouseover='this.src="../Images/btn_EditProduct_over.png";' onmouseout='this.src="../Images/btn_EditProduct_up.png";'
                        onmousedown='this.src="../Images/btn_EditProduct_down.png";' onmouseup='this.src="../Images/btn_EditProduct_up.png";'
                        Visible="false" />
                </asp:LinkButton>
            </div>
            <div style="clear: both; margin-left: 5px; margin-right: 5px; height: 20px;">
                <asp:CustomValidator ID="custValidIngredients" runat="server" ValidationGroup="ingredients"
                    OnServerValidate="custValidIngredients_ServerValidate" Display="Static"></asp:CustomValidator>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlIngredients" runat="server" BorderWidth="1px" BorderColor="#656565"
        BorderStyle="Solid" Width="450px" Height="200px" Font-Bold="true" Font-Size="12px"
        ScrollBars="Vertical" Style="margin-bottom: 32px;" CssClass="ingredients ingredientsEditable">
        <div style="margin-left: 5px; margin-right: 5px;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:DataList ID="dlistIngredients" runat="server" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="#dedede"
                        Width="300px" OnItemDataBound="dlistIngredients_ItemDataBound">
                        <ItemTemplate>
                            <asp:PlaceHolder runat="server" ID="buttons">
                                <asp:LinkButton ID="btnUpdateIngredient" runat="server" Text="<%$ Resources:MyGlobalResources, Update %>"
                                    class="UpdateLink" OnClick="btnUpdateIngredient_Click" OnClientClick="SuspendLeaveConfirmation()"></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="btnRemoveIngredient" runat="server" Text="<%$ Resources:MyGlobalResources, Remove %>"
                                    class="DeleteLink" OnClick="btnRemoveIngredient_Click" OnClientClick="setDirty(); SuspendLeaveConfirmation()"></asp:LinkButton>&nbsp;</asp:PlaceHolder>
                            <asp:Literal ID="ltrDisplayIngredient" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DisplayIngredient")%>' />
                        </ItemTemplate>
                    </asp:DataList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
</div>
