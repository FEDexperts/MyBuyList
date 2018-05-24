<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master"
    AutoEventWireup="true" CodeFile="SelectedRecipesList.aspx.cs" Inherits="SelectedRecipesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">

    <script type="text/javascript">

        function showPopup() {
            var hidBtn = document.getElementById('<%=hiddenBtn2.ClientID %>');
            hidBtn.click();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div style="padding: 50px 8px 20px 9px;">
        <div style="margin-bottom: 20px;">
            <asp:Image ID="imgTitle" runat="server" ImageUrl="~/Images/Header_ShoppingCart.png" />
        </div>
        <div style="margin-bottom: 30px;">
            <p>
                בדף זה מוצגים המתכונים שבחרת.
            </p>
            <ul style="padding-right: 20px; margin-top: 5px;">
                <li>ניתן לשנות את <b>כמות המנות</b> בטבלה – רשימת הקניות תשתנה בהתאם.</li>
                <li>ניתן לארגן את המתכונים <b>במערך של תפריט</b> – התפריט יישמר במאגר האישי שלך. מכל
                    תפריט ניתן להפיק רשימת קניות.</li>
                <li>ניתן להפיק <b>רשימת קניות מהירה</b> ומיד לצאת לקניות ....</li>
            </ul>
        </div>
        <asp:UpdatePanel ID="upSelectedRecipes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div id="divRecipesList" runat="server" class="selectedRecipes">
                    <div style="width: 648px; height: 29px;">
                        <asp:Image ID="imgTableTop" runat="server" ImageUrl="~/Images/bgr_TableMenusToBuyList.jpg"
                            Width="648px" Height="29px" />
                    </div>
                    <table width="648px" cellspacing="0px" cellpadding="0">
                        <asp:Repeater ID="dlRecipes" runat="server" OnItemDataBound="dlRecipes_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td class="selectedRecipesRow">
                                        <table cellspacing="0px" cellpadding="0">
                                            <tr>
                                                <td style="width: 522px; height: 25px; padding-right: 15px;">
                                                    <asp:HyperLink ID="lnkRecipeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Value.RecipeName")%>'
                                                        CssClass="recipeName" Target="recipeWindow" />
                                                </td>
                                                <td style="width: 36px; height: 25px; text-align: center;">
                                                    <asp:TextBox ID="txtRecipeServings" runat="server" Width="24px" Height="15px" Font-Size="12px"
                                                        Style="text-align: center; border: 1px solid #E4E4E4;" Text='<%#DataBinder.Eval(Container.DataItem, "Value.Servings")%>'
                                                        Enabled="false" ForeColor="Black" BackColor="White" />
                                                </td>
                                                <td style="width: 36px; height: 25px; text-align: center;">
                                                    <asp:LinkButton ID="LinkButtonServingsUp" runat="server" OnCommand="ChangeServings" CommandName="Increase">
                                                        <asp:Image ID="Image6" runat="server" ImageUrl="Images/Arrow-up-icon.png" />
                                                    </asp:LinkButton>
                                                    <asp:TextBox ID="txtSelectServings" runat="server" Width="24px" Height="15px" Font-Size="12px"
                                                        Style="text-align: center; border: 1px solid #656565;" ReadOnly="True" Text='<%#DataBinder.Eval(Container.DataItem, "Value.ExpectedServings")%>' />
                                                    <asp:LinkButton ID="LinkButtonServingsDown" runat="server" OnCommand="ChangeServings" CommandName="Decrease">
                                                        <asp:Image ID="Image7" runat="server" ImageUrl="Images/Arrow-down-icon.png" />
                                                    </asp:LinkButton>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtSelectServings_ftb" runat="server" TargetControlID="txtSelectServings"
                                                        FilterType="Numbers" />
                                                </td>
                                                <td style="height: 25px; text-align: center; padding-right: 7px;">
                                                    <asp:LinkButton ID="lnkRemove" runat="server" Text="הסר" OnClientClick="StartHeaderInterval();" OnClick="lnkRemove_Click"
                                                        recipeId="" CausesValidation="True" CssClass="remove" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div class="selectedRecipes" style="text-align: center;">
                    <asp:Label ID="lblNoRecipesSelected" runat="server" Font-Bold="true" Font-Size="16px" />
                </div>
                <div class="addRecipesLink">
                    <%--<div style="float: right;">--%>
                    <asp:LinkButton ID="btnClearSelectedRecipes" runat="server" Text="נקה הכל" ForeColor="#EE1D3D" OnClientClick="StartHeaderInterval();"
                        OnClick="btnClearSelectedRecipes_Click" />
                    <%--</div>--%>
                    <%--<div style="float: left; ">
                <asp:HyperLink ID="lnkBacktoRecipes" runat="server" NavigateUrl="Recipes.aspx" Text="הוספת מתכונים" />
            </div>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="SelectedRecipesButtons">
            <asp:HyperLink ID="lnkBacktoRecipes" runat="server" NavigateUrl="Recipes.aspx">
                <asp:Image ID="imgBtnAddrecipes" runat="server" ImageUrl="~/Images/btn_AddRecipes_Up.png"
                    onmouseover='this.src="Images/btn_AddRecipes_Over.png";' onmouseout='this.src="Images/btn_AddRecipes_Up.png";'
                    onmousedown='this.src="Images/btn_AddRecipes_Down.png";' onmouseup='this.src="Images/btn_AddRecipes_Up.png";' />
            </asp:HyperLink>
            <%--<asp:ImageButton ID="btnAddToExistingMenu" runat="server" OnClick="btnAddToExistingMenu_Click"
                ImageUrl="~/Images/btn_AddToExistMenu_Up.png" onmouseover='this.src="Images/btn_AddToExistMenu_Over.png";'
                onmouseout='this.src="Images/btn_AddToExistMenu_Up.png";' onmousedown='this.src="Images/btn_AddToExistMenu_Down.png";'
                onmouseup='this.src="Images/btn_AddToExistMenu_Up.png";' Style="margin-right: 5px;" OnClientClick="showPopup();" />--%>
            <img id="btnAddToExistingMenu" runat="server" onclick="showPopup();" src="Images/btn_AddToExistMenu_Up.png"
                onmouseover='this.src="Images/btn_AddToExistMenu_Over.png";' onmouseout='this.src="Images/btn_AddToExistMenu_Up.png";'
                onmousedown='this.src="Images/btn_AddToExistMenu_Down.png";' onmouseup='this.src="Images/btn_AddToExistMenu_Up.png";'
                style="margin-right: 5px;" />
            <asp:ImageButton ID="btnAddToNewMenu" runat="server" OnClick="btnAddToNewMenu_Click"
                ImageUrl="~/Images/btn_AddToNewMenu_Up.png" onmouseover='this.src="Images/btn_AddToNewMenu_Over.png";'
                onmouseout='this.src="Images/btn_AddToNewMenu_Up.png";' onmousedown='this.src="Images/btn_AddToNewMenu_Down.png";'
                onmouseup='this.src="Images/btn_AddToNewMenu_Up.png";' Style="margin-right: 5px;" />
            <asp:ImageButton ID="btnQuickList" runat="server" OnClick="btnQuickList_Click" ImageUrl="~/Images/btn_BuyList_up.png"
                onmouseover='this.src="Images/btn_BuyList_over.png";' onmouseout='this.src="Images/btn_BuyList_up.png";'
                onmousedown='this.src="Images/btn_BuyList_down.png";' onmouseup='this.src="Images/btn_BuyList_up.png";'
                Style="margin-right: 5px;" />
        </div>
        <%--<div class="addToMenu">
            <div style="float: right;">
                <asp:Label ID="lblChooseMenu" runat="server" Text="בחר תפריט:" Style="margin-left: 20px;" />
            </div>
            <div style="float: right;">
                <asp:RadioButton ID="rbnNewMenu" runat="server" Text="תפריט חדש" Checked="true" GroupName="Menu"
                    class="radioButton_aligned" />
                <br />
                <br />
                <asp:RadioButton ID="rbnExistingMenu" runat="server" Text="תפריט קיים" GroupName="Menu"
                    class="radioButton_aligned" />&nbsp;&nbsp;--%>
        <%--</div>
            <div style="float: left; margin-right: 30px;">
                <asp:Button ID="btnAddToMenu" runat="server" Text="הוסף לתפריט" Height="40px" OnClick="btnAddToMenu_Click" />
            </div>
        </div>
        <div style="clear: both;">
            <asp:Button ID="btnQuickList" runat="server" Text="הפק רשימת קניות מהירה" Height="40px"
                OnClick="btnQuickList_Click" />
        </div>--%>
    </div>
    <%--<asp:DropDownList ID="ddlUserMenus" runat="server" />--%>
    <asp:Panel ID="pnlUserMenus" runat="server" CssClass="menuCategoriesModalPopup" Style="width: 300px;
        display: none;">
        <asp:Panel ID="pnlUserMenuTitle" runat="server" CssClass="pnlMenuCategoriesTitle">
            <div style="text-align: center;">
                <asp:Label ID="lblUserMenusTitle" runat="server" Font-Size="11pt" Font-Bold="true" style="float: right; padding-right: 50px;"
                    Text="<%$ Resources:MyGlobalResources, SelectUserMenus %>" />
                <div style="float: left; padding-top: 2px; padding-left: 10px;">
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/btn_X.gif" Style="cursor: pointer;" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCategoriesList" runat="server" ScrollBars="Vertical" CssClass="pnlCategoriesList"
            Style="width: 288px; height: 260px;">
            <asp:UpdatePanel ID="upTreeViewCategories" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <table style="width: 90%">
                        <asp:Repeater ID="rptUserMenus" runat="server" OnItemDataBound="rptUserMenus_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td align="right" class="userMenusRepeaterCell">
                                        &nbsp;
                                        <asp:HyperLink ID="lnkMenu" runat="server" ForeColor="#656565" Text='<%#Eval("MenuName") %>'></asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Button ID="hiddenBtn2" runat="server" Style="display: none;" />
    <ajaxToolkit:ModalPopupExtender BehaviorID="" ID="mpeUserMenus" runat="server" RepositionMode="RepositionOnWindowResizeAndScroll"
        TargetControlID="hiddenBtn2" PopupControlID="pnlUserMenus" BackgroundCssClass="modalBackground2"
        CancelControlID="btnCancel" DropShadow="true" PopupDragHandleControlID="pnlUserMenuTitle" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
