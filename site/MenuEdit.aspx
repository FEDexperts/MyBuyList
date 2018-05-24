<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="MenuEdit.aspx.cs" Inherits="MenuEdit" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    
    <script src="Scripts/MenuEditDnD.js"></script>
    <script src="Scripts/MenuEdit.js"></script>

    

    <div style="clear: both; padding-top: 50px; padding-right: 8px;">
        <asp:Image ID="titleImg" runat="server" ImageUrl="~/Images/Header_AddNewMenus.png" />
    </div>    
    <div style="float: right; width: 760px">
        <div id="menu_wrapper" style="margin: 0px; padding: 17px 3px 24px 6px;">
            <div id="menu_wrapper_top">
            </div>
            <div id="menu_wrapper_middle" style="color: #656565;">
                <div style="padding: 26px 22px 23px 22px;">
                    <p>
                        שדות החובה מסומנים בכוכבית, אך מומלץ למלא את כל השדות. ככל שהמתכון יהיה מסווג ומפורט
                    יותר, כך יהיה קל יותר לחברי קהילת האתר למצוא אותו ולהשתמש בו בעת הצורך.
                    </p>
                    <div style="clear: both; margin-top: 20px; margin-bottom: 20px; text-align: right;">
                        <asp:Image ID="CategoryTitleImg" runat="server" ImageUrl="~/Images/SubHeader_Category.png" />
                    </div>
                    <div>
                        <asp:RadioButton ID="rbnCategoryWeekly" runat="server" Text="תפריט שבועי" GroupName="category"
                            class="newMenu_radioBtn" OnCheckedChanged="rbnCategoryOneMeal_CheckedChanged"
                            AutoPostBack="true" onclick='clearDirty()' />
                        <asp:RadioButton ID="rbnCategoryOneMeal" runat="server" Text="תפריט ארוחה" GroupName="category"
                            Checked="true" class="newMenu_radioBtn" OnCheckedChanged="rbnCategoryOneMeal_CheckedChanged"
                            AutoPostBack="true" onclick='clearDirty()' />
                    </div>
                    <div style="clear: both; margin-top: 32px; text-align: right;">
                        <asp:Image ID="GeneralTitleImg" runat="server" ImageUrl="~/Images/SubHeader_DetailsAddNewMenu.png" />
                    </div>
                    <div style="width: 301px; text-align: left;">
                        <asp:CheckBox ID="chxPulicMenu" runat="server" Text="שיתוף התפריט באתר" onclick="setDirty()" />
                    </div>
                    <div style="width: 301px; margin-top: 10px;">
                        <asp:Label ID="lblMenuName" runat="server" Text="שם התפריט" Font-Bold="true" />
                        <asp:TextBox ID="txtMenuName" runat="server" BackColor="#fbdea8" BorderColor="#fbab14"
                            BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565" Font-Size="12px" Width="299px"
                            Height="14px" Style="margin-top: 6px;" onkeydown="setDirty()" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="שם מתכון ריק" ControlToValidate="txtMenuName"></asp:RequiredFieldValidator>
                    </div>
                    <div style="width: 301px; clear: both; margin-top: 10px;">
                        <asp:UpdatePanel ID="upCategories" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
                            RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Label ID="lblCategories" runat="server" Text="קטגוריה" Font-Bold="true" /><br />
                                <asp:TextBox ID="txtCategories" runat="server" MaxLength="200" ReadOnly="true" BackColor="#fbdea8"
                                    BorderColor="#fbab14" BorderStyle="Solid" BorderWidth="1px" Height="14px" Width="277px"
                                    ForeColor="#656565" Font-Size="12px" Style="margin-top: 6px; padding-left: 0px; padding-right: 0px;" />
                                <asp:ImageButton ID="btnSelectCategories" runat="server" OnClientClick='clickHiddenButton2()'
                                    ImageUrl="~/Images/btn_DropDown_2.gif" Style="vertical-align: bottom;" />
                                <%--<asp:RequiredFieldValidator ID="reqValidCategories" runat="server" ValidationGroup="general"
                                Display="Dynamic" ControlToValidate="txtCategories" ErrorMessage='<%$ Resources:ValidationResources, RecipeCategoryIsRequired%>'></asp:RequiredFieldValidator>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="width: 301px; margin-top: 10px;">
                        <asp:Label ID="lblMenuTags" runat="server" Text="תגיות" Font-Bold="true" />
                        <asp:TextBox ID="txtMenuTags" runat="server" BackColor="#fbdea8" BorderColor="#fbab14"
                            BorderStyle="Solid" BorderWidth="1px" Width="299px" Height="14px" ForeColor="#656565"
                            Font-Size="12px" Style="margin-top: 6px;" onkeydown="setDirty()" />
                    </div>
                    <asp:UpdatePanel ID="upNumDiners" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <div id="divNumDiners" runat="server" style="width: 301px; margin-top: 10px;">
                                <asp:Label ID="lblNumDiners" runat="server" Text="מספר סועדים" Font-Bold="true" Style="display: block;" />
                                <asp:TextBox ID="txtNumDiners" runat="server" BackColor="#fbdea8" BorderColor="#fbab14"
                                    BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565" Font-Size="12px" Width="100px"
                                    Height="14px" Style="margin-top: 6px;" onkeydown="setDirty()" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="txtNumDiners_ftb" runat="server" TargetControlID="txtNumDiners"
                                    FilterType="Numbers" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div style="width: 301px; margin-top: 10px;">
                        <asp:Label ID="lblDescription" runat="server" Text="תיאור קצר" Font-Bold="true" />
                        <asp:TextBox ID="txtMenuDescription" runat="server" TextMode="MultiLine" BackColor="#fbdea8"
                            BorderColor="#fbab14" BorderStyle="Solid" BorderWidth="1px" Width="299px" Height="53px"
                            ForeColor="#656565" Font-Size="12px" Style="margin-top: 6px; margin-bottom: 0px;"
                            onkeydown="setDirty()" />
                    </div>
                    <div style="width: 301px; margin-top: 10px;">
                        <asp:Label ID="lblPicture" runat="server" Text="תמונה לתצוגה של התפריט" Font-Bold="true" />
                        <asp:FileUpload ID="fuMenuPicture" runat="server" Width="300px" size="41" BackColor="#fbdea8"
                            BorderColor="#fbab14" BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565"
                            Font-Size="12px" Style="margin-top: 6px;" onclick="setDirty()" />
                        <p style="font-size: 12px;">
                            (הגודל לתמונה 600 פיקסלים לרוחב על 462 פיקסלים לגובה)
                        </p>
                    </div>
                    <div style="margin-top: 10px;">
                        <asp:Label ID="lblVideo" runat="server" Text="קישור לסרטון או embedded" Font-Bold="true" />
                        <asp:TextBox ID="txtEmbeddedVideo" runat="server" TextMode="MultiLine" BackColor="#fbdea8"
                            BorderColor="#fbab14" BorderStyle="Solid" BorderWidth="1px" Width="650px" Height="42px"
                            ForeColor="#656565" Font-Size="12px" Style="margin-top: 6px; margin-bottom: 0px;"
                            onkeydown="setDirty()" onmousedown="setDirty()" />
                    </div>
                    <asp:UpdatePanel ID="upMealsDetails" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div style="clear: both; margin-top: 20px; text-align: right;">
                                <asp:Image ID="TablesTitleImg" runat="server" ImageUrl="Images/SubHeader_MeatDetails.png" />
                            </div>
                            <div id="menu_details" style="clear: both; min-height: 200px; margin-top: 20px;">
                                <asp:Repeater ID="rptDays" runat="server" OnItemDataBound="rptDays_ItemDataBound" OnItemCreated="rptDays_ItemCreated">
                                    <ItemTemplate>
                                        <div style="margin-top: 20px;">
                                            <div style="width: 650px; height: 28px;">
                                                <asp:Image ID="imgTableTop" runat="server" ImageUrl="" Width="650px" Height="28px" />
                                            </div>
                                            <table width="650px;" cellspacing="0px" cellpadding="0px">
                                                <asp:Repeater ID="rptCourses" runat="server" OnItemDataBound="rptCourses_ItemDataBound" OnItemCreated="rptCourses_ItemCreated">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="mealDetails_meals" style="padding-bottom: 4px; padding-top: 4px;">
                                                                <table width="100%" cellspacing="0px" cellpadding="0px">
                                                                    <tr>
                                                                        <td class="mealDetails_mealNames" style="padding-top: 5px; padding-bottom: 5px;">
                                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("CourseOrMealTypeName") %>' />
                                                                        </td>
                                                                        <td id="tdDinersNum" runat="server" style="width: 1px; text-align: center;">
                                                                            <asp:TextBox ID="txtDinersNum" runat="server" Width="24px" Height="15px" Font-Size="12px"
                                                                                Style="text-align: center; border: 1px solid #656565;" OnTextChanged="txtDinersNum_TextChanged"
                                                                                mealSignature="" Visible="false" onkeydown="setDirty()" />
                                                                        </td>
                                                                        <td id="tdMealRecipes" runat="server" meal_signature="" style="padding-bottom: 5px;">
                                                                            <table width="100%" cellspacing="0px" cellpadding="0px">
                                                                                <asp:Repeater ID="rptRecipes" runat="server" OnItemDataBound="rptRecipes_ItemDataBound" OnItemCreated="rptRecipes_ItemCreated">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td style="width: 100px; padding-right: 1px; padding-top: 5px; text-align: center;">
                                                                                                <div style="float: right; margin-top: 2px;">
                                                                                                    <%--<a id="servingUp" recipeId='<%#Eval ("Recipe.RecipeId") %>' recipeServings='<%#Eval ("Recipe.Servings") %>'>
                                                                                                    <img alt="" src="Images/Arrow-up-icon.png" />
                                                                                                </a>--%>
                                                                                                    <asp:LinkButton ID="LinkButtonServingUp" runat="server" OnCommand="ChangeServings" CommandName="Increase" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Recipe.RecipeId") %>' CssClass="linkServingsUp" recipeId='<%#DataBinder.Eval(Container.DataItem, "Recipe.RecipeId") %>' recipeServings='<%#DataBinder.Eval(Container.DataItem, "Recipe.Servings") %>'>
                                                                                                        <asp:Image ID="Image6" runat="server" ImageUrl="Images/Arrow-up-icon.png" />
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                                <div style="float: right; margin-top: 4px;">
                                                                                                    <asp:TextBox ID="txtServings" runat="server" ReadOnly="True" Text='<%#DataBinder.Eval(Container.DataItem, "Recipe.ExpectedServings") %>' Width="24px"
                                                                                                        Height="15px" Font-Size="12px" Style="text-align: center; border: 1px solid #656565;"
                                                                                                        OnTextChanged="txtServings_TextChanged" recipeId='<%#DataBinder.Eval(Container.DataItem, "Recipe.RecipeId") %>' mealSignature="" onkeydown="setDirty()" />
                                                                                                </div>
                                                                                                <div style="float: right; margin-top: 3px;">
                                                                                                    <%--<a id="servingDown" recipeId='<%#Eval ("Recipe.RecipeId") %>' recipeServings='<%#Eval ("Recipe.Servings") %>'>
                                                                                                    <img alt="" src="Images/Arrow-down-icon.png" />
                                                                                                </a>--%>
                                                                                                    <asp:LinkButton ID="LinkButtonServingDown" runat="server" OnCommand="ChangeServings" CommandName="Decrease" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Recipe.RecipeId") %>' CssClass="linkServingsDown" recipeId='<%#DataBinder.Eval(Container.DataItem, "Recipe.RecipeId") %>' recipeServings='<%#DataBinder.Eval(Container.DataItem, "Recipe.Servings") %>'>
                                                                                                        <asp:Image ID="Image7" runat="server" ImageUrl="Images/Arrow-down-icon.png" />
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </td>
                                                                                            <td id="tdRecipes" style="padding-left: 14px;">
                                                                                                <asp:Label ID="lblRecipeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Recipe.RecipeName") %>'
                                                                                                    Style="float: right;" />
                                                                                                <asp:LinkButton ID="removeFromMeal" runat="server" Text="הסר" recipeId="" mealSignature=""
                                                                                                    OnClick="removeFromMeal_Click" ForeColor="#EE1D3D" Style="float: left;" OnClientClick="setDirty(); SuspendLeaveConfirmation();" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TextBoxComments" runat="server" Width="98%" BorderColor="#fbab14"
                                                                                BorderStyle="Solid" BorderWidth="1px" BackColor="#fbdea8" Visible="false" TextMode="MultiLine" Rows="4" OnTextChanged="CommentsTextChanged"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="upChooseCategories" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:Button ID="hiddenBtn" runat="server" Style="display: none;" CausesValidation="False" />
                            <asp:Button ID="btnRefresh" runat="server" OnClick="RefreshMenu" Style="display: none" />
                            <asp:Button ID="popupPanelOk" runat="server" OnClick="btnTmpOK_Click" Style="display: none" CausesValidation="False" />
                            <asp:Button ID="categoriesPopupOK" runat="server" OnClick="btnCatOK_Click" Style="display: none" />
                            <asp:Button ID="btnCancelHdn" runat="server" Style="display: none;" />
                            <asp:Panel ID="pnlPopupChooseMeal" runat="server" Width="245px" Height="250px" CssClass="MenuRecipeModalPopup"
                                Style="display: none;">
                                <div id="mealRecipeModalPopupTitle">
                                    <div style="float: right;">
                                        <asp:Label ID="modalPopupTitle" runat="server" Text="שייך/י מתכון" Style="vertical-align: middle;" />
                                    </div>
                                    <div style="float: left; padding-top: 2px; padding-left: 10px;">
                                        <asp:ImageButton runat="server" ImageUrl="Images/btn_X.gif" OnClientClick="return closeChooseMealDialogBox();" />
                                    </div>
                                </div>
                                <div style="padding: 10px 19px 10px 19px;">
                                    <asp:Label ID="lblChooseMealRecipeName" runat="server" CssClass="lblChooseMealRecipeName" />
                                    <asp:HiddenField ID="hfRecipeId" runat="server" />
                                    <asp:HiddenField ID="hfBaseServings" runat="server" />
                                    <asp:HiddenField ID="hfBaseExpectedServings" runat="server" />
                                    <asp:HiddenField ID="hfExpectedServings" runat="server" />
                                    <div id="selectMeal" style="margin-top: 20px;">
                                        <asp:DropDownList ID="ddlChooseDay" runat="server" BackColor="#FCDEA8" BorderColor="#F4A612"
                                            BorderStyle="Solid" BorderWidth="1px" Width="196px" Height="18px" Font-Bold="true"
                                            ForeColor="#656565" Font-Size="12px" Visible="false">
                                            <asp:ListItem Value="0" Text="בחר/י יום" Selected="True" />
                                            <asp:ListItem Value="1" Text="ראשון" />
                                            <asp:ListItem Value="2" Text="שני" />
                                            <asp:ListItem Value="3" Text="שלישי" />
                                            <asp:ListItem Value="4" Text="רביעי" />
                                            <asp:ListItem Value="5" Text="חמישי" />
                                            <asp:ListItem Value="6" Text="שישי" />
                                            <asp:ListItem Value="7" Text="שבת" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlChooseMeal" runat="server" Style="margin-top: 20px;" BackColor="#FCDEA8"
                                            BorderColor="#F4A612" BorderStyle="Solid" BorderWidth="1px" Width="196px" Height="18px"
                                            Font-Bold="true" ForeColor="#656565" Font-Size="12px" Visible="false">
                                            <asp:ListItem Value="0" Text="בחר/י ארוחה" Selected="True" />
                                            <asp:ListItem Value="1" Text="ארוחת בוקר" />
                                            <asp:ListItem Value="2" Text="ארוחת צהריים" />
                                            <asp:ListItem Value="3" Text="ארוחת ערב" />
                                            <asp:ListItem Value="4" Text="שונות" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlChooseCourse" runat="server" BackColor="#FCDEA8" BorderColor="#F4A612"
                                            BorderStyle="Solid" BorderWidth="1px" Width="196px" Height="18px" Font-Bold="true"
                                            ForeColor="#656565" Font-Size="12px">
                                            <asp:ListItem Value="-1" Text="בחר/י מנה" Selected="True" />
                                            <asp:ListItem Value="1" Text="מנה ראשונה" />
                                            <asp:ListItem Value="2" Text="מנה עיקרית" />
                                            <asp:ListItem Value="3" Text="תוספות" />
                                            <asp:ListItem Value="4" Text="קינוחים" />
                                            <asp:ListItem Value="5" Text="משקאות" />
                                            <asp:ListItem Value="0" Text="שונות" />
                                        </asp:DropDownList>
                                        <div style="margin-top: 10px;">
                                            <asp:Label runat="server" Text="מספר מנות" Font-Bold="true" />
                                        </div>
                                        <div style="margin-top: 6px; margin-top: 3px;">
                                            <div style="float: right">
                                                <a id="popUpServingUp">
                                                    <img alt="" src="Images/Arrow-up-icon.png" />
                                                </a>
                                            </div>
                                            <div style="float: right; margin-top: 4px;">
                                                <asp:TextBox ID="txtChooseServings" runat="server" ReadOnly="True" BackColor="#FCDEA8" BorderColor="#F4A612"
                                                    BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565" Font-Size="12px" Width="30px"
                                                    Height="14px" />
                                            </div>
                                            <div style="float: right; margin-top: 1px;">
                                                <a id="popUpServingDown">
                                                    <img alt="" src="Images/Arrow-down-icon.png" />
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="clear: both; margin-top: 17px;">
                                        <asp:LinkButton ID="btnTmpOK" runat="server" Style="float: right;">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/btn_Add_up.png" />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnTmpCancel" runat="server" OnClientClick="return closeChooseMealDialogBox();"
                                            Style="float: left;">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/btn_Cancel_up.png" />
                                        </asp:LinkButton>
                                    </div>
                                    <div style="clear: both; min-height: 1px;">
                                    </div>
                                </div>
                            </asp:Panel>
                            <ajaxToolkit:ModalPopupExtender BehaviorID="" ID="mpeChooseMeal" runat="server" RepositionMode="RepositionOnWindowResizeAndScroll"
                                TargetControlID="hiddenBtn" PopupControlID="pnlPopupChooseMeal" BackgroundCssClass="modalBackground2"
                                CancelControlID="btnCancelHdn" DropShadow="false" PopupDragHandleControlID=""
                                OkControlID="btnTmpOK" OnOkScript="activatePopupPanelOk()" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="wrapper_bottom_tab">
                <table width="693px" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td>
                            <table cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td class="wrapper_bottom_tab_1t"></td>
                                </tr>
                                <tr>
                                    <td class="wrapper_bottom_tab_1b"></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td class="wrapper_bottom_tab_2t"></td>
                                </tr>
                                <tr>
                                    <td class="wrapper_bottom_tab_2b"></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td class="wrapper_bottom_tab_3t" align="left" valign="bottom">
                                        <div>
                                            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick='clearDirty()'>
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/btn_Save_up.png" onmouseover='this.src="Images/btn_Save_over.png";'
                                                    onmouseout='this.src="Images/btn_Save_up.png";' onmousedown='this.src="Images/btn_Save_down.png";'
                                                    onmouseup='this.src="Images/btn_Save_up.png";' />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" OnClientClick='return allowLeave()'>
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/btn_Cancel_up.png" onmouseover='this.src="Images/btn_Cancel_over.png";'
                                                    onmouseout='this.src="Images/btn_Cancel_up.png";' onmousedown='this.src="Images/btn_Cancel_Down.png";'
                                                    onmouseup='this.src="Images/btn_Cancel_up.png";' />
                                            </asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="wrapper_bottom_tab_3b"></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td class="wrapper_bottom_tab_4t"></td>
                                </tr>
                                <tr>
                                    <td class="wrapper_bottom_tab_4b"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:Panel ID="pnlPopupCategories" runat="server" CssClass="menuCategoriesModalPopup"
            Style="display: none;">
            <asp:Panel ID="pnlMenuCategoriesTitle" runat="server" CssClass="pnlMenuCategoriesTitle">
                <div style="text-align: center;">
                    <asp:Label ID="lblCategoriesTitle" runat="server" Font-Size="11pt" Font-Bold="true"
                        Text="<%$ Resources:MyGlobalResources, SelectCategories %>" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlCategoriesList" runat="server" ScrollBars="Vertical" CssClass="pnlCategoriesList">
                <asp:UpdatePanel ID="upTreeViewCategories" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:TreeView ID="tvCategories" runat="server" ForeColor="#656565" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <div style="height: 7px">
            </div>
            <div style="clear: both; min-height: 27px;">
                <div style="float: left;">
                    <asp:LinkButton ID="btnCatOK" runat="server" OnClick="btnSave_Click">
                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/btn_Save_up.png" onmouseover='this.src="Images/btn_Save_over.png";'
                            onmouseout='this.src="Images/btn_Save_up.png";' onmousedown='this.src="Images/btn_Save_down.png";'
                            onmouseup='this.src="Images/btn_Save_up.png";' />
                    </asp:LinkButton>
                    &nbsp;
                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false">
                    <asp:Image ID="btnCatCancel" runat="server" ImageUrl="~/Images/btn_Cancel_up.png"
                        onmouseover='this.src="Images/btn_Cancel_over.png";' onmouseout='this.src="Images/btn_Cancel_up.png";'
                        onmousedown='this.src="Images/btn_Cancel_Down.png";' onmouseup='this.src="Images/btn_Cancel_up.png";' />
                </asp:LinkButton>
                </div>
            </div>
            <div style="height: 7px">
            </div>
        </asp:Panel>
        <asp:Button ID="hiddenBtn2" runat="server" Style="display: none;" />
        <ajaxToolkit:ModalPopupExtender BehaviorID="" ID="mpeCategories" runat="server" RepositionMode="RepositionOnWindowResizeAndScroll"
            TargetControlID="hiddenBtn2" PopupControlID="pnlPopupCategories" BackgroundCssClass="modalBackground2"
            CancelControlID="btnCatCancel" DropShadow="true" PopupDragHandleControlID="pnlMenuCategoriesTitle"
            OkControlID="btnCatOK" OnOkScript="activateCategoriesPopupPanelOk()" />
    </div>
    <div style="float: right;">
        <div class="menuRecipesList SideTable">
            <%--<div style="text-align: center; margin-top: 20px;">
            <asp:Label ID="lblRecipesTitle" runat="server" Text="מתכונים:" ForeColor="#8bab31"
                Font-Size="20px" Font-Bold="true" />
        </div>--%>
            <div id="Instructions" style="padding: 18px 18px 10px 18px;">
                <p style="font-weight: bold; color: #656565;">
                    שבץ את המתכון במקום המתאים ע"י לחיצה על מקש "שבץ" או ע"י גרירת המתכון
                </p>
            </div>
            <asp:Panel ID="pnlRecipesList" runat="server" class="pnlRecipesList">
                <asp:UpdatePanel ID="upSelectedRecipes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div id="menuRecipes" class="selectedMenuRecipes">
                            <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="#8bab31" Font-Size="14px"
                                Font-Bold="true" Style="padding-right: 30px;" Visible="false" />
                            <asp:DataList ID="dlRecipes" runat="server" OnItemDataBound="dlRecipes_ItemDataBound">
                                <ItemTemplate>
                                    <asp:Panel ID="pnlItem" runat="server" Width="180px" CssClass="item" recipeid="">
                                        <asp:HyperLink ID="lnkRecipeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Value.RecipeName")%>'
                                            Target="recipeDetails" CssClass="recipeName" />
                                    </asp:Panel>
                                    <asp:LinkButton ID="lnkRemove" runat="server" Text="הסר" OnClick="lnkRemove_Click"
                                        recipeId="" CausesValidation="False" CssClass="remove" OnClientClick="SuspendLeaveConfirmation();" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="cbeRemoveMenuRecipe" runat="server" TargetControlID="lnkRemove"
                                        ConfirmText="מחיקת המתכון מרשימת המתכונים לתפריט תמחק אותו מכל הארוחות בהן הוא משובץ. האם אתה בטוח שברצונך למחוק את המתכון?" />
                                    &nbsp;
                                <asp:LinkButton ID="lnkAddToMenu2" runat="server" CausesValidation="False" Text="שבץ" recipeId="" recipeName=""
                                    recipeServings='<%#DataBinder.Eval(Container.DataItem, "Value.Servings")%>'
                                    recipeExpectedServings='<%#DataBinder.Eval(Container.DataItem, "Value.ExpectedServings")%>' CssClass="add" OnClientClick="SuspendLeaveConfirmation(); showPopupChooseMeal(this);" />
                                    <div style="clear: both; height: 15px;">
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
