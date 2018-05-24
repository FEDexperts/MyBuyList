<%@ Page Title="<%$ Resources:MyGlobalResources, MenuDetailsPageTitle %>" Language="C#"
    MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="MenuDetails.aspx.cs" Inherits="MenuDetails" %>

<%@ Register Src="~/UserControls/ucSendMailToFriend.ascx" TagPrefix="uc1" TagName="SendToFriend" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript">

        function adjustCellAppearance() {
            var mealCellsCollection = $("td[id='mealCell']");
            var mealTypeId = "<%=this.MenuTypeId%>";
            var i = 0;
            if (mealTypeId == "1" || mealTypeId == "4") {
                for (i = 0; i < mealCellsCollection.length; i++) {
                    mealCellsCollection[i].className = "mealDetails_meals dinerMenuMealCell";
                }
            }
            else {
                for (i = 0; i < mealCellsCollection.length; i++) {
                    mealCellsCollection[i].className = "mealDetails_meals weeklyMenuMealCell";
                }
            }
        }
        
                    
    </script>
    <div style="padding-top: 50px; padding-left: 9px;">
        <div style="clear: both; text-align: right; vertical-align: bottom; min-height: 30px;
            padding: 0px 8px 0px 0px;">
            <div style="float: right; margin-bottom: 22px;">
                <asp:Image ID="titleImg" runat="server" ImageUrl="Images/Header_MenuName.png" Style="vertical-align: bottom;" />
                <asp:Label ID="lblMenuName" runat="server" Style="margin-right: 11px; vertical-align: bottom;"
                    ForeColor="#FBAB14" Font-Size="18px" Font-Bold="true" />
            </div>
            <%--<div style="float: left;">
                <asp:HyperLink ID="btnNewMenu" runat="server" NavigateUrl="~/MenuEdit.aspx">
                    <asp:Image ID="btnImg" runat="server" ImageUrl="Images/btn_AddNewMenu_up.png" Style="vertical-align: bottom;"
                        onmouseover='this.src="Images/btn_AddNewMenu_over.png";' onmouseout='this.src="Images/btn_AddNewMenu_up.png";'
                        onmousedown='this.src="Images/btn_AddNewMenu_Down.png";' onmouseup='this.src="Images/btn_AddNewMenu_up.png";' />
                </asp:HyperLink>
            </div>--%>
        </div>
        <asp:UpdatePanel ID="upTopTags" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div id="topTags" style="clear: both; height: 43px; padding-right: 8px;">
                    <div id="myFavoritesTopTag" runat="server" class="myFavoritesTopTag" title="קיים במועדפים שלי">
                    </div>
                    <div id="allFavoritesTopTag">
                        <div style="width: 20px; text-align: center;">
                            <asp:Label ID="lblAllFavorites" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="menu_wrapper">
            <div class="right-side">
                <div id="menu_wrapper_top"></div>
                <div id="menu_wrapper_middle">
                    <div class="social-plugins">
                        <div style="float: left; margin-top: -3px;">
                            <div class="fb-like" data-send="false" data-layout="button_count" data-width="80"
                                data-show-faces="true" data-href="<%=FBUrl %>">
                            </div>
                        </div>
                        <div style="float: left; margin-left: 20px;">
                            <!-- AddThis Button BEGIN -->
                            <!-- Go to www.addthis.com/dashboard to customize your tools -->
                            <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=dalit"></script>
                            <!-- Go to www.addthis.com/dashboard to customize your tools -->
                            <div class="addthis_button_facebook"></div>

                            <%--<script type="text/javascript">
                                var addthis_config = { "data_track_clickback": true };
                            </script>
                            <div class="addthis_toolbox addthis_default_style">
                                <a href="http://www.addthis.com/bookmark.php?v=250&amp;username=dalit" class="addthis_button_compact"
                                    style="color: #fcab14; font-weight: bold">שתף</a> <span class="addthis_separator">|</span>
                                <a class="addthis_button_facebook"></a><a class="addthis_button_myspace"></a><a class="addthis_button_google"></a><a class="addthis_button_twitter"></a>
                            </div>
                            <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=dalit"></script>--%>
                            <!-- AddThis Button END -->
                        </div>
                    </div>
                    <div style="clear: both; padding: 0px 22px 0px 24px;">
                        <div id="menu_right_pane">
                            <div id="menu_categories" style="clear: both; font-weight: bold; color: #F1A30F; font-size: 13px;">
                                <div style="float: right; width: 60px;">
                                    קטגוריות:
                                </div>
                                <div style="float: right; width: 230px;">
                                    <asp:Label ID="lblCategories" runat="server" />
                                </div>
                                <div style="clear: both; height: 1px;">
                                </div>
                            </div>
                            <div id="menu_tags" style="clear: both; font-weight: bold; margin-top: 11px;">
                                <div style="float: right; width: 42px;">
                                    תגיות:
                                </div>
                                <div style="float: right; width: 250px;">
                                    <asp:Label ID="lblMenuTags" runat="server" />
                                </div>
                                <div style="clear: both; height: 1px;">
                                </div>
                            </div>
                            <div id="menu_publisher_box" style="margin-top: 26px;">
                                <asp:Label ID="lblPublishedBy" runat="server" Text='פורסם ע"י: '></asp:Label>
                                <asp:HyperLink ID="lnkPublisher" runat="server" NavigateUrl="" CssClass="menu_published_value"></asp:HyperLink>
                                &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblPublishedOn" runat="server" Text='בתאריך: '></asp:Label>
                                <asp:Label ID="lblPublishDate" runat="server" CssClass="menu_published_value"></asp:Label>
                            </div>
                            <div id="MenuDinersNum" runat="server" class="MenuDinersNum" visible="false">
                                <asp:Label ID="lblNoDiner" runat="server" />
                            </div>
                            <div id="menu_description1" style="margin-top: 20px; margin-bottom: 8px;">
                                <asp:Image ID="Image1" runat="server" ImageUrl="Images/SubHeader_ShortDescription_Menu.png"
                                    Style="vertical-align: bottom;" />
                            </div>
                            <div>
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div id="menu_left_pane">
                            <div style="height: 5px">
                            </div>
                            <asp:UpdatePanel ID="upActionsTop" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="menu-actions">
                                        <asp:HyperLink ID="hlShowShoppingList" runat="server" Text="הצג רשימת קניות" Font-Bold="true"
                                            ForeColor="#fcab14" />
                                        <asp:Label ID="lblShowShoppingListSeperator" runat="server">|</asp:Label>
                                        <asp:LinkButton ID="btnAddMenuToFavorites" runat="server" Text="הוסף למועדפים שלי"
                                            OnClick="btnAddMenuToFavorites_Click" />
                                        <asp:LinkButton ID="btnRemoveMenuFromFavorites" runat="server" ForeColor="Red" Text="הסר ממועדפים שלי"
                                            OnClick="btnRemoveMenuFromFavorites_Click" />
                                        <asp:Label ID="lblAddToFavoritesSeparator" runat="server">|</asp:Label>
                                        <asp:HyperLink ID="hlPrintMenu" runat="server" Target="print" Text='הדפס' />
                                        <asp:Label ID="Label1" runat="server">|</asp:Label>
                                        <asp:LinkButton ID="btnSaveAs" runat="server" OnClick="SaveImage">שמור כתמונה</asp:LinkButton>
                                        <%--<asp:Label ID="lblPrintMenuSeperator" runat="server">|</asp:Label>
                                    <asp:LinkButton ID="btnSendMail" runat="server" Text='<%$ Resources:MyGlobalResources, SendToFriend %>'
                                        OnClientClick="showSendMailToFriendBox()" />--%>
                                        <asp:Label ID="lblSendMailSeparator" runat="server">|</asp:Label>
                                        <asp:HyperLink ID="hlEditMenu" runat="server" Text='<%$ Resources:MyGlobalResources, Edit %>'
                                            NavigateUrl="~/MenuEdit.aspx?menuId=" />
                                        <asp:Label ID="lblEditMenuSeperator" runat="server">|</asp:Label>
                                        <asp:LinkButton ID="btnDeleteMenu" runat="server" Text='<%$ Resources:MyGlobalResources, Delete %>'
                                            OnClick="btnDeleteMenu_Click" />
                                        <ajaxToolkit:ConfirmButtonExtender ID="cbeDeleteMenu" runat="server" TargetControlID="btnDeleteMenu"
                                            ConfirmText="האם אתה בטוח שברצונך למחוק את התפריט?" />
                                        <%-- <asp:Label ID="lblResult" runat="server" Text="Text"></asp:Label>--%>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div id="menu_picture">
                                <asp:Image ID="imgMenuPicture" runat="server" BorderColor="#656565" BorderWidth="1px"
                                    BorderStyle="Solid" />
                            </div>
                            <div id="menu_video" runat="server" style="text-align: left; margin-top: 50px;">
                            </div>
                        </div>
                        <div style="clear: both; height: 2px;">
                        </div>
                    </div>
                    <div id="menu_details" style="clear: both; min-height: 200px; padding: 0px 22px 44px 24px;">
                        <asp:Repeater ID="rptDays" runat="server" OnItemDataBound="rptDays_ItemDataBound">
                            <ItemTemplate>
                                <div style="margin-top: 28px;">
                                    <div style="width: 650px; height: 28px;">
                                        <asp:Image ID="imgTableTop" runat="server" ImageUrl="" Width="650px" Height="28px" />
                                    </div>
                                    <table width="650px" cellspacing="0px">
                                        <asp:Repeater ID="rptCourses" runat="server" OnItemDataBound="rptCourses_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td id="mealCell" class="mealDetails_meals dinerMenuMealCell">
                                                        <table cellpadding="0px" cellspacing="0px">
                                                            <tr>
                                                                <td class="mealDetails_mealNames">
                                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("CourseOrMealTypeName") %>' />
                                                                </td>
                                                                <td>
                                                                    <table cellpadding="0px" cellspacing="0px">
                                                                        <asp:Repeater ID="rptRecipes" runat="server" OnItemDataBound="rptRecipes_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td id="tdDinersNum" runat="server" style="font-size: 12px; text-align: center;"></td>
                                                                                    <td align="center" style="width: 33px; font-size: 12px; padding-right: 1px;">
                                                                                        <asp:Label ID="lblServings" runat="server" Text='<%# Eval("Servings") %>' />
                                                                                    </td>
                                                                                    <td id="tdRecipes">
                                                                                        <asp:HyperLink ID="lblRecipeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Recipe.RecipeName") %>'
                                                                                            Target="recipeDetails" />
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LabelComments" runat="server" Text="" Width="500px" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                                <div style="color: #f4a612">
                                    *שמירת הערות מותנית בשיבוץ מתכון אחד לפחות
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <script type="text/javascript">
                        adjustCellAppearance();
                    </script>
                    <div class="social-plugins">
                        <div style="float: left; margin-top: -19px;">
                            <div class="fb-like" data-send="false" data-layout="button_count" data-width="80"
                                data-show-faces="true" data-href="<%=FBUrl %>">
                            </div>
                        </div>
                        <%--<div style="float: left; margin-left: 20px;">
                            <!-- AddThis Button BEGIN -->
                            <script type="text/javascript">
                                var addthis_config = { "data_track_clickback": true };
                            </script>
                            <div class="addthis_toolbox addthis_default_style">
                                <a href="http://www.addthis.com/bookmark.php?v=250&amp;username=dalit" class="addthis_button_compact"
                                    style="color: #fcab14; font-weight: bold">שתף</a> <span class="addthis_separator">|</span>
                                <a class="addthis_button_facebook"></a><a class="addthis_button_myspace"></a><a class="addthis_button_google"></a><a class="addthis_button_twitter"></a>
                            </div>
                            <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=dalit"></script>
                            <!-- AddThis Button END -->
                        </div>--%>
                    </div>
                    <div style="height: 5px">
                    </div>
                </div>
                <div class="wrapper_bottom_tab2">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="wrapper_bottom_tab2_1">&nbsp;
                            </td>
                            <td style="height: 30px; width: 20px;">
                                <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/bgr_TabIndexReciepesRight.png" />
                            </td>
                            <td class="wrapper_bottom_tab2_3" nowrap="nowrap">
                                <asp:UpdatePanel ID="upActionsBottom" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div id="menu_actions_bottom">
                                            <asp:HyperLink ID="hlShowShoppingList_bottom" runat="server" Text="הצג רשימת קניות"
                                                Font-Bold="true" ForeColor="#fcab14" />
                                            <asp:Label ID="lblShowShoppingListSeperator_bottom" runat="server">|</asp:Label>
                                            <asp:LinkButton ID="btnAddMenuToFavorites_bottom" runat="server" Text="הוסף למועדפים שלי"
                                                OnClick="btnAddMenuToFavorites_Click" />
                                            <asp:LinkButton ID="btnRemoveMenuFromFavorites_bottom" runat="server" ForeColor="Red"
                                                Text="הסר ממועדפים שלי" OnClick="btnRemoveMenuFromFavorites_Click" />
                                            <asp:Label ID="lblAddToFavoritesSeparator_bottom" runat="server">|</asp:Label>
                                            <asp:HyperLink ID="hlPrintMenu_bottom" runat="server" Target="print" Text='הדפס' />
                                            <asp:Label ID="Label2" runat="server">|</asp:Label>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="SaveImage">שמור כתמונה</asp:LinkButton>
                                            <%--<asp:Label ID="lblPrintMenuSeperator_bottom" runat="server">|</asp:Label>
                                        <asp:LinkButton ID="btnSendMail_bottom" runat="server" Text='<%$ Resources:MyGlobalResources, SendToFriend %>'
                                            OnClientClick="showSendMailToFriendBox()" />--%>
                                            <asp:Label ID="lblSendMailSeparator_bottom" runat="server">|</asp:Label>
                                            <asp:HyperLink ID="hlEditMenu_bottom" runat="server" Text='<%$ Resources:MyGlobalResources, Edit %>'
                                                NavigateUrl="~/MenuEdit.aspx?menuId=" />
                                            <asp:Label ID="lblEditMenuSeperator_bottom" runat="server">|</asp:Label>
                                            <asp:LinkButton ID="btnDeleteMenu_bottom" runat="server" Text='<%$ Resources:MyGlobalResources, Delete %>'
                                                OnClick="btnDeleteMenu_Click" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="cbeDeleteMenu_bottom" runat="server" TargetControlID="btnDeleteMenu_bottom"
                                                ConfirmText="האם אתה בטוח שברצונך למחוק את התפריט?" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="height: 30px; width: 20px;">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/bgr_TabIndexReciepesLeft.png" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="fb-comments" data-href="<%=Request.Url.AbsoluteUri.ToString() %>" data-numposts="10" data-width="100%" data-colorscheme="light"></div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <uc1:SendToFriend ID="ucSendMailToFriend" runat="server" OnEmailSent="ucSendMailToFriend_EmailSent" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
