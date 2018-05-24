<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master"
    AutoEventWireup="true" CodeFile="Menus.aspx.cs" Inherits="Menus" Theme="Standard" %>

<%@ Register Src="~/UserControls/ucSendMailToFriend.ascx" TagPrefix="uc1" TagName="SendToFriend" %>
<%@ Register Src="UserControls/MenusSearchControl.ascx" TagName="MenusSearchControl" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/ucShoppingList.ascx" TagName="ucShoppingList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript">
        function changeSort(ddl) {
            //            var sort = ddl.options[ddl.selectedIndex].value;
            //            var recipesPage = '<%= ResolveUrl("~/Menus.aspx") %>';
            //            document.location = recipesPage + "?orderby=" + sort;

            var sort = ddl.options[ddl.selectedIndex].value;

            switch (sort) {
                case 'LastUpdate':
                    document.location = '<%= this.OrderByLastUpdateUrl %>';
                    break;
                case 'Name':
                    document.location = '<%= this.OrderByNameUrl %>';
                        break;
                    case 'Publisher':
                        document.location = '<%= this.OrderByPublisherUrl %>';
                        break;
                }
            }

            function activateCategoriesPopupPanelOk() {
                var categoriesPopupOK = document.getElementById('<%= categoriesPopupOK.ClientID %>');
            categoriesPopupOK.click();
        }

        function clickHiddenButton2() {
            var hiddenBtn2 = document.getElementById('<%= hiddenBtn2.ClientID %>');
            hiddenBtn2.click();
        }

        function passParametersToSendToFriend(sender) {
            var recipeId = sender.getAttribute('recipeId');
            var recipeName = sender.getAttribute('recipeName');
            setParameters(recipeId, recipeName);
            showSendMailToFriendBox();
        }

    </script>
    <div id="menus-wrapper">
        <div class="header">
            <asp:Image runat="server" ImageUrl="~/Images/Header_Menus.png" />
            <div class="top">
                <div>
                    <p style="margin-top: 18px; margin-bottom: 18px; padding: 0px;">
                        דף זה מכיל תפריטים מסוגים שונים: תפריט ש-ב-ו-ע-י, תפריט לארוחה משפחתית עם מאכלים
                        מוכרים ואהובים, תפריט 
                        <span style="color: red;">לארוחה רומנטית</span>, תפריט לארוחה
                        מחוץ לבית, תפריט ליום קר, תפריט ליום חם, תפריט לארוחת חברים ,
                        <span style="color: #FBAB14;">תפריט כתום</span>, <span style="color: #A4CB3A;">תפריט ירוק</span> ועוד מגוון
                        רחב של תפריטים. <b>את התפריטים ניתן לערוך ולקבל רשימת קניות אחת !</b>
                        <br />
                        <b>ניתן ליצור תפריטים ע"י בחירה מוקדמת של מתכונים בדף המתכונים</b>. לבחירת מתכונים
                        לשיבוץ בתפריט יש ללחוץ "הוסף לרשימת קניות/לתפריט" בכל מתכון רצוי.
                    </p>
                </div>
                <div class="hide" style="text-align: center; padding-bottom: 10px">
                    <object width="480" height="360">
                        <param name="movie" value="http://www.youtube.com/v/P8R_A5okork&amp;hl=en_US&amp;fs=1"></param>
                        <param name="allowFullScreen" value="true"></param>
                        <param name="allowscriptaccess" value="always"></param>
                        <embed src="http://www.youtube.com/v/P8R_A5okork&amp;hl=en_US&amp;fs=1" type="application/x-shockwave-flash"
                            allowscriptaccess="always" allowfullscreen="true" width="480" height="360"></embed></object>
                </div>
            </div>
            <div class="filters">
                <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div id="menus_filter">
                            <uc2:MenusSearchControl ID="MenusSearchControl1" runat="server" />
                            <%--<div>
                        <asp:LinkButton ID="lnkAllMenus" runat="server" Text="כל התפריטים" OnClick="lnkAllMenus_Click" />
                        <span style="margin-left: 40px"></span>
                        <asp:LinkButton ID="lnkMyMenus" runat="server" Text="התפריטים שלי" OnClick="lnkMyMenus_Click" />
                        <span style="margin-left: 40px"></span>
                        <asp:LinkButton ID="lnkFavMenus" runat="server" Text="המועדפים שלי" OnClick="lnkFavMenus_Click" />
                    </div>
                    <div style="margin-top: 20px;">
                        <asp:LinkButton ID="lnkSearch" runat="server" Text="חיפוש" OnClick="lnkSearch_Click" />
                        <span style="margin-left: 30px"></span>
                        <asp:LinkButton ID="lnkAdvancedSearch" runat="server" Text="חיפוש מתקדם" OnClick="lnkAdvancedSearch_Click" />
                        <span style="margin-left: 30px"></span>
                        <asp:LinkButton ID="lnkCategories" runat="server" Text="חיפוש לפי קטגוריות" OnClick="lnkCategories_Click" />
                    </div>
                    <div id="simpleSearch" runat="server" visible="false">
                        <asp:Label ID="lblText" runat="server" Text="טקסט חופשי" />
                        <asp:TextBox ID="txtSearchTerm" runat="server" Width="200px" Style="margin-top: 10px;" />
                    </div>--%>
                            <div id="advancedSearch" runat="server" visible="false">
                                <asp:Label ID="Label1" runat="server" Text="קטגוריות" />
                                <asp:TextBox ID="txtCategory" runat="server" Width="200px" Style="margin-top: 10px;" />
                                <asp:HiddenField ID="hdnCategorieIDs" runat="server" />
                                <asp:Button ID="btnChooseCategory" runat="server" OnClientClick='clickHiddenButton2()'
                                    Text="בחר" Style="margin-top: 10px;" />&nbsp;&nbsp;
                        <asp:Label ID="Label2" runat="server" Text="מס' סועדים" />
                                <asp:TextBox ID="txtDiners" runat="server" Width="50px" Style="margin-top: 10px;" />
                            </div>
                            <%--<asp:Button ID="btnSearch" runat="server" Text="חפש" OnCommand="btnSearch_Command"
                        Style="margin-top: 10px;" Visible="false" />--%>
                            <div id="categories" runat="server" visible="false" style="margin-top: 10px;">
                                <div id="pathLinks" runat="server" style="margin-bottom: 10px;">
                                </div>
                                <asp:Panel ID="pnlCategories" runat="server" Width="300px" Height="170px" BorderWidth="1px"
                                    BorderColor="#656565" ScrollBars="Vertical" Style="margin: 0px auto;">
                                    <table style="width: 90%">
                                        <asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="right">&nbsp;
                                                <%-- <asp:LinkButton ID="btnCategory" runat="server" categoryId='<%#DataBinder.Eval(Container.DataItem ,"CategoryId")%>'
                                                    Text='<%#DataBinder.Eval(Container.DataItem ,"CategoryName")%>' ForeColor="Navy"
                                                    OnCommand="btnCategory_Command" CommandArgument='<%#DataBinder.Eval(Container.DataItem ,"CategoryId")%>' />--%>
                                                        <asp:HyperLink ID="lnkCategory" runat="server" ForeColor="#656565"></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                        <div id="numResults">
                            <asp:Label ID="lblNumMenus" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
                        <asp:LinkButton ID="btnCatOK" runat="server">
                            <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/btn_Save_up.png" onmouseover='this.src="Images/btn_Save_over.png";'
                                onmouseout='this.src="Images/btn_Save_up.png";' onmousedown='this.src="Images/btn_Save_down.png";'
                                onmouseup='this.src="Images/btn_Save_up.png";' />
                        </asp:LinkButton>
                        &nbsp;
                    <asp:LinkButton ID="btnCatCancel" runat="server" CausesValidation="false">
                        <asp:Image runat="server" ImageUrl="~/Images/btn_Cancel_up.png"
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
            <div id="recipes_sort_by_box">
                <asp:DropDownList ID="ddlSortBy" runat="server" Width="129px" Height="18px" BackColor="#fee0a8"
                    Font-Bold="true" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #FBAB14; padding-right: 5px;"
                    onchange="return changeSort(this);">
                    <asp:ListItem Text="מיין לפי" Value="0"></asp:ListItem>
                    <asp:ListItem Text="תאריך" Value="LastUpdate"></asp:ListItem>
                    <asp:ListItem Text="שם" Value="Name"></asp:ListItem>
                    <asp:ListItem Text="מחבר" Value="Publisher"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <%--<div id="recipes_add_new_box">
            <asp:HyperLink ID="lnkNewRecipe" runat="server" NavigateUrl="~/MenuEdit.aspx">
                <asp:Image runat="server" ImageUrl="~/Images/btn_AddNewMenu_up.png" onmouseover='this.src="Images/btn_AddNewMenu_over.png";' onmouseout='this.src="Images/btn_AddNewMenu_up.png";'
                    onmousedown='this.src="Images/btn_AddNewMenu_Down.png";' onmouseup='this.src="Images/btn_AddNewMenu_up.png";' />
            </asp:HyperLink>
        </div>--%>
        </div>
        <div class="body">
            <div class="ShoppingList">
                <asp:UpdatePanel ID="upShoppingList" runat="server">
                    <ContentTemplate>
                        <uc1:ucShoppingList ID="ucShoppingList1" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="menu-list">
                <asp:UpdatePanel ID="upMenus" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:Button ID="categoriesPopupOK" runat="server" OnClick="btnCatOK_Click" Style="display: none" />
                        <asp:Repeater ID="rptRecipes" runat="server">
                            <HeaderTemplate>
                                <!-- pager -->
                                <div id="topPager" class="menu_pager" style="width: 540px; text-align: right; direction: ltr; margin-bottom: 18px; padding-right: 5px;">
                                    <asp:PlaceHolder ID="phPager" runat="server"></asp:PlaceHolder>
                                </div>
                            </HeaderTemplate>
                            <FooterTemplate>
                                <!-- pager -->
                                <div id="bottomPager" class="menu_pager" style="width: 540px; text-align: right; direction: ltr; margin-top: 18px; padding-right: 5px;">
                                    <asp:PlaceHolder ID="phPager" runat="server"></asp:PlaceHolder>
                                </div>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:HiddenField ID="hfMenuId" runat="server" Value='<%# Eval("MenuId") %>' />
                                <div class="menu_box">
                                    <div style="clear: both; height: 18px;">
                                        <div class="menu_main_category">
                                            <asp:Label ID="lblMainCategory" runat="server" Text='<%# Eval("MenuTypeNameShort") %>'></asp:Label>
                                        </div>
                                    </div>
                                    <div style="clear: both; height: 215px; padding-right: 12px; padding-left: 12px; padding-top: 20px;">
                                        <div class="menu_inner_box">
                                            <div class="menu_title">
                                                <asp:HyperLink ID="lnkRecipe" runat="server" Text='<%# Eval("MenuTitle") %>' NavigateUrl=""></asp:HyperLink>
                                            </div>
                                            <div class="recipe_actions_box">
                                                <asp:LinkButton ID="btnAddRemoveFromFavorites" runat="server" CssClass="recipe_action"
                                                    OnCommand="btnAddRemoveFromFavorites_Command" />
                                                <%--<asp:Literal ID="seperator" runat="server">&nbsp;|&nbsp;</asp:Literal>--%>
                                                <%--<asp:LinkButton ID="btnSendToFriend" runat="server" CssClass="recipe_action gray_action"
                                            Text='שלח לחבר' OnClientClick="passParametersToSendToFriend(this)" recipeId='<%# Eval("MenuId") %>'
                                            recipeName='<%# Eval("MenuTitle") %>'></asp:LinkButton>--%>
                                            </div>
                                            <div>
                                                <!-- AddThis Button BEGIN -->
                                                <%--<script type="text/javascript">
                                            var addthis_config = { "data_track_clickback": true };
                                        </script>
                                        <div class="addthis_toolbox addthis_default_style">
                                            <a href="http://www.addthis.com/bookmark.php?v=250&amp;username=dalit" class="addthis_button_compact" style="color: #fcab14; font-weight: bold">
                                                שתף</a> <span class="addthis_separator">|</span> <a class="addthis_button_facebook">
                                                </a><a class="addthis_button_myspace"></a><a class="addthis_button_google"></a>
                                            <a class="addthis_button_twitter"></a>
                                        </div>
                                        <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=dalit"></script>--%>
                                                <!-- AddThis Button END -->
                                            </div>
                                            <div id="infoTags" style="clear: both; min-height: 30px; padding-top: 10px;">
                                                <div id="myFavoritesInfoTag" runat="server" class="myFavoritesInfoTag" title="קיים במועדפים שלי">
                                                </div>
                                                <div id="allFavoritesInfoTag" title="גולשים אוהבים מתכון זה">
                                                    <div style="width: 15px; text-align: center;">
                                                        <asp:Label ID="lblAllFavorites" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="menu_tags">
                                                <asp:Label ID="lblRecipeTags" runat="server" Text='<%# Eval("MenuTags") %>'></asp:Label>
                                            </div>
                                            <div class="recipe_description">
                                                <asp:Label ID="lblRecipeDescription" runat="server" Text='<%# Eval("MenuDescription") %>'></asp:Label>
                                            </div>
                                            <div class="menuMealDetails">
                                                <div style="float: right; width: 127px; margin-left: 56px;">
                                                    <asp:Label ID="lblMealTitle1" runat="server" Font-Bold="true" />
                                                    <div style="clear: both; margin-top: 5px;">
                                                        <asp:DataList ID="dlRecipes1" runat="server" OnItemDataBound="dlRecipes_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRecipeArrow" runat="server" Text="&gt;" ForeColor="#FDAC14" />
                                                                <asp:HyperLink ID="hlkRecipeDetails" runat="server" NavigateUrl="~/RecipeDetails.aspx?RecipeId=" />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                                <div style="float: right; width: 127px; margin-left: 56px;">
                                                    <asp:Label ID="lblMealTitle2" runat="server" Font-Bold="true" />
                                                    <div style="clear: both; margin-top: 5px;">
                                                        <asp:DataList ID="dlRecipes2" runat="server" OnItemDataBound="dlRecipes_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRecipeArrow" runat="server" Text="&gt;" ForeColor="#FDAC14" />
                                                                <asp:HyperLink ID="hlkRecipeDetails" runat="server" NavigateUrl="~/RecipeDetails.aspx?RecipeId=" />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                                <div style="float: right; width: 127px;">
                                                    <asp:Label ID="lblMealTitle3" runat="server" Font-Bold="true" />
                                                    <div style="clear: both; margin-top: 5px;">
                                                        <asp:DataList ID="dlRecipes3" runat="server" OnItemDataBound="dlRecipes_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRecipeArrow" runat="server" Text="&gt;" ForeColor="#FDAC14" />
                                                                <asp:HyperLink ID="hlkRecipeDetails" runat="server" NavigateUrl="~/RecipeDetails.aspx?RecipeId=" />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="publisher_box" style="clear: both; margin-top: 10px;">
                                                <asp:Label ID="lblPublishedBy" runat="server" Text='פורסם ע"י'></asp:Label>
                                                <asp:HyperLink ID="lnkPublisher" runat="server" NavigateUrl="" CssClass="menu_published_value"
                                                    Text='<%# Eval("PublisherName") %>'></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblPublishedOn" runat="server" Text='בתאריך'></asp:Label>
                                                <asp:Label ID="lblPublishDate" runat="server" Text='<%# Eval("PublishDate") %>' CssClass="menu_published_value"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="recipe_thumbnail_box">
                                            <div class="recipe_thumbnail" style="width: 123px; height: 94px;">
                                                <asp:Image ID="imgThumbnail" runat="server" ImageUrl='<%# Eval("MenuThumbnail") %>'
                                                    Style="max-height: 94px; max-width: 123px;" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc1:SendToFriend ID="ucSendMailToFriend" runat="server" OnEmailSent="ucSendMailToFriend_EmailSent" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
