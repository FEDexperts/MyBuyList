<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRecipe.ascx.cs" Inherits="ucRecipe"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/UserControls/Ingridiants.ascx" TagPrefix="MBL" TagName="Ingridiants" %>

<style type="text/css">
    .style1
    {
        width: 430px;
    }
    .newRecipe_txtBox_short
    {
        width: 299px;
        height: 15px;
        background-color: #DDEBB6;
        border: 1px solid #A4CB3A;
        margin-top: 6px;
    }
</style>
<asp:Panel ID="Panel1" runat="server" BorderStyle="None" BackColor="White" Width="700px" CssClass="recipeModalPopup">    
    <div style="padding-right: 8px;">
        <asp:Image ID="imgTitle" runat="server" ImageUrl="~/Images/Header_AddNewRecipe.png" />
    </div>
    <div style="padding: 17px 3px 24px 6px;">
        <div id="newRecipe_top">
        </div>
        <div id="newRecipe_middle">
            <p style="color: Black;">
                שדות החובה מסומנים בכוכבית, אך מומלץ למלא את כל השדות. ככל שהמתכון יהיה מסווג ומפורט
                יותר, כך יהיה קל יותר לחברי קהילת האתר למצוא אותו ולהשתמש בו בעת הצורך.
            </p>
            <div style="min-height: 340px; margin-top: 38px;">
                <div style="float: right; width: 302px; vertical-align: top; margin-left: 42px;">
                    <div style="clear: both; text-align: right;">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/SubHeader_Details.png" />
                    </div>
                    <div style="clear: both; margin-top: 20px;">
                        <asp:Label ID="lblRecipeName" runat="server" Text="שם המתכון" Font-Bold="true" />
                        <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#EF1839" />
                        <asp:TextBox ID="txtRecipeName" runat="server" MaxLength="200" AutoCompleteType="Disabled"
                            Width="299px" Height="14px" BackColor="#ddecb6" BorderWidth="1px" BorderStyle="Solid"
                            BorderColor="#A4CB3A" Font-Size="12px" ForeColor="#656565" Style="margin-top: 6px;"
                            />
                        <asp:RequiredFieldValidator ID="reqValidRecipeName" runat="server" ValidationGroup="general"
                            Display="Dynamic" ControlToValidate="txtRecipeName" ErrorMessage='<%$ Resources:ValidationResources, RecipeNameIsRequired%>'></asp:RequiredFieldValidator>
                    </div>
                    <div style="clear: both; margin-top: 10px;">
                        <asp:Label ID="lblRecipeDesc" runat="server" Text="תיאור קצר" Font-Bold="true" />
                        <asp:TextBox ID="txtRecipeDesc" runat="server" TextMode="MultiLine" Width="299px"
                            BackColor="#ddecb6" BorderColor="#A4CB3A" BorderStyle="Solid" BorderWidth="1px"
                            Height="53px" Font-Size="12px" ForeColor="#656565" AutoCompleteType="Disabled"
                            Style="margin-top: 6px; margin-bottom: 0px;" />
                    </div>
                    <div style="clear: both; margin-top: 10px;">
                        <asp:UpdatePanel ID="updateCategories" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
                            RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Label ID="lblCategories" runat="server" Text="קטגוריה" Font-Bold="true" />
                                <asp:Label ID="Label3" runat="server" Text="*" ForeColor="#EF1839" />
                                <asp:TextBox ID="txtCategories" runat="server" MaxLength="200" ReadOnly="true" BackColor="#ddecb6"
                                    BorderColor="#A4CB3A" BorderStyle="Solid" BorderWidth="1px" Font-Size="12px"
                                    ForeColor="#656565" Height="14px" Width="277px" Style="margin-top: 6px;  padding-left: 0px; padding-right: 0px;"  />
                                <asp:ImageButton ID="btnSelectCategories" runat="server" OnClientClick='clickHiddenButton()'
                                    ImageUrl="~/Images/btn_DropDown_3.gif" Style="vertical-align: bottom;" />
                                <asp:RequiredFieldValidator ID="reqValidCategories" runat="server" ValidationGroup="general"
                                    Display="Dynamic" ControlToValidate="txtCategories" ErrorMessage='<%$ Resources:ValidationResources, RecipeCategoryIsRequired%>'></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="clear: both; margin-top: 10px;">
                        <asp:Label ID="lblTags" runat="server" Text="תגיות" Font-Bold="true" />
                        <asp:TextBox ID="txtTags" runat="server" Width="299px" BackColor="#ddecb6" BorderColor="#A4CB3A"
                            BorderStyle="Solid" BorderWidth="1px" Height="14px" Font-Size="12px" ForeColor="#656565"
                            AutoCompleteType="Disabled" Style="margin-top: 6px;" />
                    </div>
                    <div style="clear: both; margin-top: 10px;">                        
                        <asp:Label ID="lblRecipeImage" runat="server" Text="תמונה לתצוגה של המתכון" Font-Bold="true" />
                        <asp:FileUpload ID="fuRecipeImage" runat="server" Width="300px" size="33" BackColor="#ddecb6"
                            BorderColor="#A4CB3A" BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565"
                            Style="margin-top: 6px;" />                        
                        <p style="font-size: 11px;">
                            (הגודל לתמונה 600 פיקסלים לרוחב על 462 פיקסלים לגובה)
                        </p>
                    </div>
                </div>
                <div style="float: right; width: 302px; vertical-align: top;">
                    <div style="clear: both; height: 14px;">
                        <div id="divOptions" runat="server" style="margin: 0px; padding: 0px; float: left;">
                            <asp:CheckBox ID="chkPublic" runat="server" Checked="true" Text='<%$ Resources:MyGlobalResources, PublicRecipe %>'
                                CssClass="chxBox_aligned" />
                        </div>
                    </div>
                    <div style="clear: both; margin-top: 20px;">
                        <asp:Label ID="lblPrepTime" runat="server" Text="זמן הכנה (חיתוך, ערבוב)" Font-Bold="true"
                            Style="display: block;" />
                        <div style="margin-top: 6px;">
                            <asp:TextBox ID="txtPrepTime" runat="server" Width="214px" BackColor="#ddecb6" BorderColor="#A4CB3A"
                                BorderStyle="Solid" BorderWidth="1px" Height="14px" Font-Size="12px" ForeColor="#656565"
                                AutoCompleteType="Disabled" Style="margin-left: 1px; vertical-align: middle; padding-left: 0px; padding-right: 0px;"
                                 />
                            <ajaxToolkit:FilteredTextBoxExtender ID="filtertxtPrepTime" runat="server" TargetControlID="txtPrepTime"
                                ValidChars="0123456789." />
                            <asp:DropDownList ID="ddlPrepTimeUnits" runat="server" Width="80px" BackColor="#ddecb6"
                                Font-Bold="true" Font-Size="12px" Height="18px" ForeColor="#656565" Style="border: 1px solid #A4CB3A;
                                vertical-align: middle;">
                                <asp:ListItem Value="1" Text="דקות" Selected="True" />
                                <asp:ListItem Value="2" Text="שעות" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div style="clear: both; margin-top: 10px;">
                        <asp:Label ID="lblCookTime" runat="server" Text="זמן בישול / אפייה / טיגון / אידוי / צלייה"
                            Font-Bold="true" />
                        <div style="margin-top: 6px;">
                            <asp:TextBox ID="txtCookTime" runat="server" Width="214px" BackColor="#ddecb6" BorderColor="#A4CB3A"
                                BorderStyle="Solid" BorderWidth="1px" Height="14px" Font-Size="12px" AutoCompleteType="Disabled"
                                Style="margin-left: 1px; vertical-align: middle; padding-left: 0px; padding-right: 0px;" ForeColor="#656565" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="filtertxtCookTime" runat="server" TargetControlID="txtCookTime"
                                ValidChars="0123456789." />
                            <asp:DropDownList ID="ddlCookTimeUnits" runat="server" Width="80px" BackColor="#ddecb6"
                                Font-Bold="true" Font-Size="12px" Height="18px" ForeColor="#656565" Style="border: 1px solid #A4CB3A;
                                vertical-align: middle;">
                                <asp:ListItem Value="1" Text="דקות" Selected="True" />
                                <asp:ListItem Value="2" Text="שעות" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div style="clear: both; margin-top: 10px;">
                        <asp:Label ID="lblServings" runat="server" Text="מספר מנות" Font-Bold="true" />
                        <asp:TextBox ID="txtServings" runat="server" Width="299px" BackColor="#ddecb6" BorderColor="#A4CB3A"
                            BorderStyle="Solid" BorderWidth="1px" Font-Size="12px" ForeColor="#656565" Height="14px"
                            MaxLength="3" Style="margin-top: 6px;" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="txtServings_ftb" runat="server" TargetControlID="txtServings"
                            FilterType="Numbers" />
                    </div>
                    <div style="clear: both; margin-top: 10px;">
                        <asp:Label ID="Label4" runat="server" Text="דרגת קושי (קל, בינוני, קשה)" Font-Bold="true" />
                        <asp:DropDownList ID="ddlDifficulty" runat="server" Width="301px" BackColor="#ddecb6"
                            Font-Bold="true" Height="18px" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #A4CB3A;
                            margin-top: 6px;">
                            <asp:ListItem Text=" בחר דרגת קושי" Value="0" Selected="True" />
                            <asp:ListItem Text=" קל" Value="1" />
                            <asp:ListItem Text=" בינוני" Value="2" />
                            <asp:ListItem Text=" קשה" Value="3" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div style="clear: both; margin-top: 10px; margin-bottom: 32px;">
                <asp:Label ID="Label5" runat="server" Text=" קישור לסרטון או embedded" Font-Bold="true" />
                <asp:TextBox ID="txtEmbeddedLink" runat="server" TextMode="MultiLine" Width="643px"
                    MaxLength="100" BackColor="#ddecb6" BorderColor="#A4CB3A" BorderStyle="Solid"
                    BorderWidth="1px" Height="53px" Font-Size="12px" ForeColor="#656565" Style="margin-top: 6px;"
                     />
            </div>
            <div class="DividerAddNewRecipe">
            </div>
            <div style="margin-top: 24px;">
                <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/SubHeader_Products_AddNewRecipe.png" />
            </div>

            <MBL:Ingridiants runat="server" id="Ingridiants" />

            <div class="DividerAddNewRecipe">
            </div>
            <div style="margin-top: 24px; margin-bottom: 20px;">
                <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/subHeader_ToolsAddNewRecipe.png" />
            </div>
            <asp:TextBox ID="txtTools" runat="server" TextMode="MultiLine" Width="643" MaxLength="300"
                BackColor="#ddecb6" BorderColor="#A4CB3A" BorderStyle="Solid" BorderWidth="1px"
                Height="42px" Font-Size="12px" ForeColor="#656565" style="margin: 0px;"
                 />
            <div class="DividerAddNewRecipe" style="margin-top: 32px;">
            </div>
            <div style="margin-top: 24px; margin-bottom: 20px;">
                <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/SubHeader_Preparation_AddNewRecipe.png" />
                <asp:Label ID="Label1" runat="server" Text="*" ForeColor="#EF1839" style="vertical-align: top;" />
            </div>
            <asp:TextBox ID="txtPreparationMethod" runat="server" TextMode="MultiLine" Width="643"
                MaxLength="300" BackColor="#ddecb6" BorderColor="#A4CB3A" BorderStyle="Solid"
                BorderWidth="1px" Height="118px" Font-Size="12px" ForeColor="#656565" style="margin: 0px;"
                 />
            <asp:RequiredFieldValidator ID="reqValidPreparationMethod" runat="server" ValidationGroup="general"
                ControlToValidate="txtPreparationMethod" ErrorMessage='<%$ Resources:ValidationResources, PreparationMethodIsRequired%>'
                Display="Dynamic"></asp:RequiredFieldValidator>
            <div class="DividerAddNewRecipe" style="margin-top: 32px;">
            </div>
            <div style="margin-top: 24px; margin-bottom: 20px;">
                <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/SubHeader_Remark_AddNewRecipe.png" />
            </div>
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="643" BackColor="#ddecb6"
                BorderColor="#A4CB3A" BorderStyle="Solid" BorderWidth="1px" Height="42px" MaxLength="150"
                Font-Size="12px" ForeColor="#656565" style="margin: 0px;" />
            <div style="text-align: left;">
                <asp:PlaceHolder runat="server" ID="btnsReadOnly" Visible="false">
                    <asp:Button ID="btnClose" runat="server" Text="<%$ Resources:MyGlobalResources, CloseWindow %>"
                        OnClientClick="hide(); return false;" />&nbsp;
                    <asp:HyperLink ID="btnPrint" runat="server" Target="print" ForeColor="Black" Font-Bold="true"
                        Font-Underline="false" Text='<%$ Resources:MyGlobalResources, Print %>' />
                </asp:PlaceHolder>
            </div>          
        </div>
        <div class="wrapper_bottom_tab">
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td class="wrapper_bottom_tab_1t">
                                </td>
                            </tr>
                            <tr>
                                <td class="wrapper_bottom_tab_1b">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td class="wrapper_bottom_tab_2t">
                                </td>
                            </tr>
                            <tr>
                                <td class="wrapper_bottom_tab_2b">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td class="wrapper_bottom_tab_3t">
                                    <div id="btnsEditable" runat="server">
                                        <asp:LinkButton ID="btnSave" runat="server" OnClick ="btnSave_Click" CausesValidation="true"
                                            ValidationGroup="general" OnClientClick='clearDirty()'>
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/btn_Save_up.png" onmouseover='this.src="Images/btn_Save_over.png";'
                                                onmouseout='this.src="Images/btn_Save_up.png";' onmousedown='this.src="Images/btn_Save_down.png";'
                                                onmouseup='this.src="Images/btn_Save_up.png";' />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" CausesValidation="false"
                                            OnClientClick='return allowLeave()'>
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/btn_Cancel_up.png" onmouseover='this.src="Images/btn_Cancel_over.png";'
                                                onmouseout='this.src="Images/btn_Cancel_up.png";' onmousedown='this.src="Images/btn_Cancel_down.png";'
                                                onmouseup='this.src="Images/btn_Cancel_up.png";' />
                                        </asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="wrapper_bottom_tab_3b">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td class="wrapper_bottom_tab_4t">
                                </td>
                            </tr>
                            <tr>
                                <td class="wrapper_bottom_tab_4b">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <script type="text/javascript">

        function show() {
            var modalPopupBehavior = $find('recipeEditModal');
            modalPopupBehavior.show();
        }
        function hide() {
            var modalPopupBehavior = $find('recipeEditModal');
            modalPopupBehavior.hide();
        }
    </script>

</asp:Panel>
