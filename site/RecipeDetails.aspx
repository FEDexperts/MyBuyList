<%@ Page Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="RecipeDetails.aspx.cs" Inherits="PageRecipeDetails" Theme="Standard" %>

<%@ Register Src="~/UserControls/ucSendMailToFriend.ascx" TagPrefix="uc1" TagName="SendToFriend" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <meta name="Description" id="PageDescription" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript">
    function AddRecipeToFavorites(recipeId) {
        PageMethods.AddRecipeToUserFavorites(recipeId, OnAddSucceeded, OnFailed);
    }
    function RemoveRecipeFromFavorites(recipeId) {
        PageMethods.RemoveRecipeFromFavorites(recipeId, OnRemoveSucceeded, OnFailed);
    }
    function OnAddSucceeded(results) {
        $get('<%= btnAddRecipeToFavorites.ClientID %>').style.display = 'none';
            $get('<%= btnRemoveRecipeFromFavorites.ClientID %>').style.display = 'inline';
        }
        function OnRemoveSucceeded(results) {
            $get('<%= btnAddRecipeToFavorites.ClientID %>').style.display = 'inline';
            $get('<%= btnRemoveRecipeFromFavorites.ClientID %>').style.display = 'none';
        }


        function OnFailed(results) {
        }


        function _onCategoriesLoaded() {
            var modalPopupBehavior = $find('child2');
            modalPopupBehavior.show();
            modalPopupBehavior._backgroundElement.style.zIndex = 300001;
            modalPopupBehavior._foregroundElement.style.zIndex = 300002;
        }

        function _onSelectPictureLoaded() {
            var iframe1 = document.getElementById('iframe1');
            iframe1.src = "SelectRecipePicture.aspx";
            var modalPopupBehavior = $find('child3');
            modalPopupBehavior.show();
            modalPopupBehavior._backgroundElement.style.zIndex = 300001;
            modalPopupBehavior._foregroundElement.style.zIndex = 300002;
        }

        // hide select picture modal
        function HideSelectPicture() {
            var modal = $find('child3');
            modal.hide();
        }

        var hfRecipeId = '<%=hfRecipeId.ClientID%>';
    </script>

    <script src="Scripts/RecipeDetails.js"></script>
    <script>
        var lnkNewRecipe = '<%=lnkNewRecipe.ClientID%>';
    </script>

    <div style="clear: both; height: 1px;">
    </div>
    <div id="recipe-details">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="recipe-name-wrapper">
                    <div style="clear: both; min-height: 30px; width:784px;float:left;margin-left:10px;">
                        <div class="recipe_name">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Header_Recipe.png" Style="margin-left: 11px;" />
                            <div>
                                <asp:Label ID="lblRecipeName" runat="server" />
                            </div>
                        </div>
                        <div class="add-recipe">
                            <asp:HyperLink ID="lnkNewRecipe" runat="server">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/btn_AddNewRecipe_up.png"
                                    onmouseover='this.src="Images/btn_AddNewRecipe_over.png";' onmouseout='this.src="Images/btn_AddNewRecipe_up.png";'
                                    onmousedown='this.src="Images/btn_AddNewRecipe_Down.png";' nmouseup='this.src="Images/btn_AddNewRecipe_up.png";' />
                            </asp:HyperLink>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="upTopTags" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="top-tags-wrapper">
                            <div class="top-tags">
                                <div id="myFavoritesTopTag" runat="server" class="myFavoritesTopTag">
                                </div>
                                <div id="allFavoritesTopTag">
                                    <div style="width: 20px; text-align: center;">
                                        <asp:Label ID="lblAllFavorites" runat="server" />
                                    </div>
                                </div>
                                <div id="allMenusTopTag">
                                    <div style="width: 20px; text-align: center;">
                                        <asp:Label ID="lblAllMenus" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="height: 15px;"></div>
                <div id="recipe_wrapper">
                    <div id="recipe_wrapper_top">
                    </div>
                    <div id="recipe_wrapper_middle">
                        <div id="recipe_right_pane">
                            <div id="recipe_categories" class="recipe_sub_header">
                                <div style="float: right;">
                                    קטגוריות: &nbsp;
                                </div>
                                <div style="float: right; width: 280px;">
                                    <asp:Label ID="lblRecipeCategories" runat="server" />
                                </div>
                                <div style="clear: both; height: 1px;">
                                </div>
                            </div>
                            <div id="recipe_tags">
                                <div style="float: right; width: 38px;">
                                    תגיות:
                                </div>
                                <div style="float: right; width: 290px;">
                                    <asp:Label ID="lblRecipeTags" runat="server" />
                                </div>
                                <div style="clear: both; height: 1px;">
                                </div>
                            </div>
                            <div id="recipe_publisher_box" class="publisher_box">
                                <asp:Label ID="lblPublishedBy" runat="server" Text='פורסם ע"י'></asp:Label>
                                <asp:HyperLink ID="lnkPublisher" runat="server" NavigateUrl="" CssClass="published_value"></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblPublishedOn" runat="server" Text='בתאריך'></asp:Label>
                                <asp:Label ID="lblPublishDate" runat="server" CssClass="published_value"></asp:Label>
                            </div>
                            <div id="recipe_description">
                                <div>
                                    <asp:Image ID="Image3" runat="server" ImageUrl="Images/SubHeader_ShortDescription.png" />
                                </div>
                                <asp:Label ID="lblRecipeDescription" runat="server" Style="margin-top: 8px;"></asp:Label>
                            </div>
                            <div id="recipe_ingredients">
                                <div style="clear: both; margin-bottom: 8px; text-align: right;">
                                    <asp:Image ID="IngredientsTitleImg" runat="server" ImageUrl="Images/SubHeader_Products.png" />
                                </div>
                                <asp:TextBox ID="txtData" runat="server" Visible="False"></asp:TextBox>
                                <asp:DataList ID="dlistIngredients" runat="server">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "DISPLAY_NAME")%>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div id="recipe_tools">
                                <div style="clear: both; text-align: right; margin-bottom: 8px;">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="Images/SubHeader_Tools.png" />
                                </div>
                                <asp:Label ID="txtTools" runat="server"></asp:Label>
                            </div>
                            <div id="recipe_difficulty">
                                <div style="clear: both; margin-bottom: 8px; text-align: right;">
                                    <asp:Image ID="Image7" runat="server" ImageUrl="Images/SubHeader_Grade.png" />
                                </div>
                                <asp:Label ID="txtDifficulty" runat="server" Style="min-height: 5px;"></asp:Label>
                            </div>
                        </div>
                        <div id="recipe_left_pane">
                            <div class="social-plugins">
                                <div style="float: left; margin-top: -3px;">
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
                                        <a class="addthis_button_facebook"></a><a class="addthis_button_myspace"></a><a class="addthis_button_google">
                                        </a><a class="addthis_button_twitter"></a>
                                    </div>
                                    <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=dalit"></script>
                                    <!-- AddThis Button END -->
                                </div>--%>
                            </div>
                            <div style="height: 30px">
                            </div>
                            <div id="recipe_actions">
                                <asp:LinkButton ID="blkAddRemove" runat="server" OnClientClick="StartHeaderInterval();" OnClick="blkAddRemove_Click" Font-Bold="true"
                                    ForeColor="#a4cb3a" />
                                <asp:Label ID="lblAddRemoveSeperator" runat="server">|</asp:Label>
                                <asp:LinkButton ID="btnAddRecipeToFavorites" runat="server" Text='הוסף למועדפים שלי'
                                    OnClick="btnAddRecipeToFavorites_Click" />
                                <asp:LinkButton ID="btnRemoveRecipeFromFavorites" runat="server" ForeColor="Red"
                                    Text='הסר ממועדפים שלי' OnClick="btnRemoveRecipeFromFavorites_Click" />
                                <asp:Label ID="lblAddToFavoritesSeparator" runat="server">|</asp:Label>
                                <asp:HyperLink ID="btnRecipe" runat="server" Target="print" Text='הדפס' />
                                <asp:Label ID="Label1" runat="server">|</asp:Label>
                                <asp:LinkButton ID="btnSaveAs" runat="server" OnClick="SaveImage">שמור כתמונה</asp:LinkButton>
                                <%--<asp:Label ID="lblSeparator1" runat="server">|</asp:Label>
                                <asp:LinkButton ID="btnSendMail" runat="server" Text='שלח לחבר' OnClientClick="showSendMailToFriendBox()" />--%>
                                <asp:Label ID="lblEditRecipeSeparator" runat="server">|</asp:Label>
                                <%--<asp:LinkButton ID="btnEditRecipe" runat="server" Text='<%$ Resources:MyGlobalResources, Edit %>'
                                    OnClick="btnEditRecipe_Click" />--%>
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                                    <a href="RecipeEdit.aspx?recipeId=<%=RecipeId %>">
                                        <asp:Literal ID="Literal1" runat="server" Text='<%$ Resources:MyGlobalResources, Edit %>'></asp:Literal>
                                    </a>
                                </asp:PlaceHolder>
                                <asp:Label ID="lblEditRecipeDisabled" runat="server" Text='<%$ Resources:MyGlobalResources, Edit %>'
                                    Font-Underline="true" ForeColor="LightGray" Visible="false" />
                                <asp:Label ID="lblSeparator3" runat="server" Visible="false">|</asp:Label>
                                <asp:LinkButton ID="btnCopyRecipe" runat="server" Text='<%$ Resources:MyGlobalResources, CopyRecipe %>'
                                    OnClick="btnCopyRecipe_Click" Visible="false" />
                                <asp:Label ID="lblCopyRecipeSeperator" runat="server">|</asp:Label>
                                <asp:LinkButton ID="btnDeleteRecipe" runat="server" Text='<%$ Resources:MyGlobalResources, Delete %>'
                                    OnClick="btnDeleteRecipe_Click" />
                                <asp:Label ID="lblDeleteRecipeDisabled" runat="server" Text='<%$ Resources:MyGlobalResources, Delete %>'
                                    Font-Underline="true" ForeColor="LightGray" Visible="false" />
                                <ajaxToolkit:ConfirmButtonExtender ID="cbeDeleteRecipe" runat="server" TargetControlID="btnDeleteRecipe"
                                    ConfirmText="האם אתה בטוח שברצונך למחוק את המתכון?" />
                                <asp:Label ID="lblResult" runat="server" Text="" Visible="false"></asp:Label>
                            </div>
                            <div id="recipe_picture">
                                <div style="float: left; text-align: center; width: 300px; height: 231px;">
                                    <asp:Image ID="imgRecipePicture" runat="server" BorderColor="#656565" BorderWidth="1px"
                                        BorderStyle="Solid" ImageUrl="~/Images/Img_Default.jpg" />
                                </div>
                                <div style="float: right;">
                                    <div id="servingsInfoTag">
                                        <asp:Label ID="lblServNumber" runat="server" Style="float: left;" />
                                    </div>
                                    <div id="prepTimeInfoTag">
                                        <asp:Label ID="lblPrepTime" runat="server" Style="float: left;" />
                                    </div>
                                    <div id="cookTimeInfoTag">
                                        <asp:Label ID="lblCookTime" runat="server" Style="float: left;" />
                                    </div>
                                </div>
                            </div>
                            <div id="recipe_video" runat="server" class="recipe_video">
                            </div>
                        </div>
                        <div id="recipe_instructions" style="margin: 20px;">
                            <div style="clear: both; margin-bottom: 8px; text-align: right;">
                                <asp:Image ID="Image5" runat="server" ImageUrl="Images/SubHeader_Preparation.png" />
                            </div>
                            <asp:Label ID="txtPreparationMethod" runat="server"></asp:Label>
                        </div>
                        <div id="recipe_remarks" runat="server" class="recipe_remarks" style="margin: 20px;">
                            <div style="clear: both; margin-bottom: 8px; text-align: right;">
                                <asp:Image ID="Image6" runat="server" ImageUrl="Images/SubHeader_Remark.png" />
                            </div>
                            <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <div id="recipe_nutrients">
                            <asp:Image ID="nutValuesTitle" runat="server" ImageUrl="~/Images/SubHeader_Table_100gr.png"
                                Style="margin-bottom: 15px;" />
                            <div>
                                <asp:Label ID="txtNoDataForNutritionalValues" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                            <div id="divNutritionalValues" runat="server" style="width: 900px; background-color: #f3f3f3;">
                                <table style="width: 900px; height: 80px;" cellspacing="1px" cellpadding="2px">
                                    <tr>
                                        <asp:Repeater ID="rptNutritionalValues" runat="server" OnItemDataBound="NutValue_ItemDataBound">
                                            <ItemTemplate>
                                                <td id="tdRepeater" runat="server" style="text-align: center; vertical-align: top;
                                                    width: auto; padding: 8px 3px 3px 3px;">
                                                    <div style="vertical-align: top;">
                                                        <asp:Label ID="lblNutItemName1" runat="server" class="nutritional_value_name" Text='<%# Eval("NutItemName") %>' />
                                                    </div>
                                                    <div style="vertical-align: bottom; padding-top: 6px;">
                                                        <asp:Label ID="lblDisplayUnit1" CssClass="nutritional_value_unit" Style="color: white;"
                                                            runat="server" Text='<%# Eval("DisplayUnit") %>' />
                                                    </div>
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                    <tr>
                                        <asp:Repeater ID="rptNutritionalValues1" runat="server" OnItemDataBound="NutValue_ItemDataBound">
                                            <ItemTemplate>
                                                <td id="tdRepeater" runat="server" style="text-align: center; vertical-align: top;
                                                    padding-top: 7px; height: 34px;">
                                                    <asp:Label ID="lblDisplayTotalValue1" CssClass="nutritional_value_value" Style="color: white;"
                                                        runat="server" Text='<%# Eval("DisplayTotalValue") %>' />
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <!-- AddThis Button BEGIN -->
                        <script type="text/javascript">
    var addthis_config = { "data_track_clickback": true };
                        </script>
                        <div class="social-plugins" style="margin-left: 20px">
                            <div style="float: left; margin-top: -3px;">
                                <div class="fb-like" data-send="false" data-layout="button_count" data-width="80"
                                    data-show-faces="true" data-href="<%=FBUrl %>">
                                </div>
                            </div>
                            <%--<div style="float: left;">
                                <div class="addthis_toolbox addthis_default_style" style="padding-left: 20px">
                                    <a href="http://www.addthis.com/bookmark.php?v=250&amp;username=dalit" class="addthis_button_compact"
                                        style="color: #fcab14; font-weight: bold">שתף</a> <span class="addthis_separator">|</span>
                                    <a class="addthis_button_facebook"></a><a class="addthis_button_myspace"></a><a class="addthis_button_google">
                                    </a><a class="addthis_button_twitter"></a>
                                </div>
                            </div>--%>
                            <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=dalit"></script>
                            <!-- AddThis Button END -->
                            <div style="height: 30px">
                            </div>
                        </div>
                    </div>
                    <div class="wrapper_bottom_tab2">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="wrapper_bottom_tab2_1" style="">
                                    &nbsp;
                                </td>
                                <td style="width: 20px;">
                                    <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/bgr_TabIndexReciepesRight.png" />
                                </td>
                                <td class="wrapper_bottom_tab2_3" style="" nowrap="nowrap">
                                    <div id="recipe_actions_bottom">
                                        <asp:LinkButton ID="blkAddRemove_bottom" runat="server" OnClick="blkAddRemove_Click"
                                            Font-Bold="true" ForeColor="#a4cb3a" />
                                        <asp:Label ID="lblAddRemoveSeperator_bottom" runat="server">|</asp:Label>
                                        <asp:LinkButton ID="btnAddRecipeToFavorites_bottom" runat="server" Text='הוסף למועדפים שלי'
                                            OnClick="btnAddRecipeToFavorites_Click" />
                                        <asp:LinkButton ID="btnRemoveRecipeFromFavorites_bottom" runat="server" ForeColor="Red"
                                            Text='הסר ממועדפים שלי' OnClick="btnRemoveRecipeFromFavorites_Click" />
                                        <asp:Label ID="lblAddToFavoritesSeparator_bottom" runat="server">|</asp:Label>
                                        <asp:HyperLink ID="btnRecipe_bottom" runat="server" Target="print" Text='הדפס' />
                                        <asp:Label ID="Label2" runat="server">|</asp:Label>
                                        <%--<a class="btnSaveAs" style="cursor: pointer;">שמור</a>--%>
                                        <asp:LinkButton ID="btnSaveAsBottom" runat="server" OnClick="SaveImage">שמור כתמונה</asp:LinkButton>
                                        <%--<asp:Label ID="lblSeparator1_bottom" runat="server">|</asp:Label>
                                        <asp:LinkButton ID="btnSendMail_bottom" runat="server" Text='שלח לחבר' OnClientClick="showSendMailToFriendBox()" />--%>
                                        <asp:Label ID="lblEditRecipeSeparator_bottom" runat="server">|</asp:Label>
                                        <%--<asp:LinkButton ID="btnEditRecipe_bottom" runat="server" Text='<%$ Resources:MyGlobalResources, Edit %>'
                                            OnClick="btnEditRecipe_Click" />--%>
                                        <asp:PlaceHolder ID="PlaceHolder2" runat="server">
                                            <a href="RecipeEdit.aspx?recipeId=<%=RecipeId %>">
                                                <asp:Literal ID="Literal2" runat="server" Text='<%$ Resources:MyGlobalResources, Edit %>'></asp:Literal>
                                            </a>
                                        </asp:PlaceHolder>
                                        <asp:Label ID="lblEditRecipeDisabled_bottom" runat="server" Text='<%$ Resources:MyGlobalResources, Edit %>'
                                            Font-Underline="true" ForeColor="LightGray" Visible="false" />
                                        <asp:Label ID="lblSeparator3_bottom" runat="server" Visible="false">|</asp:Label>
                                        <asp:LinkButton ID="btnCopyRecipe_bottom" runat="server" Text='<%$ Resources:MyGlobalResources, CopyRecipe %>'
                                            OnClick="btnCopyRecipe_Click" Visible="false" />
                                        <asp:Label ID="lblCopyRecipeSeperator_bottom" runat="server">|</asp:Label>
                                        <asp:LinkButton ID="btnDeleteRecipe_bottom" runat="server" Text='<%$ Resources:MyGlobalResources, Delete %>'
                                            OnClick="btnDeleteRecipe_Click" />
                                        <asp:Label ID="lblDeleteRecipeDisabled_bottom" runat="server" Text='<%$ Resources:MyGlobalResources, Delete %>'
                                            Font-Underline="true" ForeColor="LightGray" Visible="false" />
                                        <ajaxToolkit:ConfirmButtonExtender ID="cbeDeleteRecipe_bottom" runat="server" TargetControlID="btnDeleteRecipe_bottom"
                                            ConfirmText="האם אתה בטוח שברצונך למחוק את המתכון?" />
                                        <asp:Label ID="lblResult_bottom" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                </td>
                                <td style="width: 20px;">
                                    <asp:Image runat="server" ImageUrl="~/Images/bgr_TabIndexReciepesLeft.png" />
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="fb-comments" data-href="<%=Request.Url.AbsoluteUri.ToString() %>" data-numposts="10" data-width="100%" data-colorscheme="light"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc1:SendToFriend ID="ucSendMailToFriend" runat="server" OnEmailSent="ucSendMailToFriend_EmailSent" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="margin-top: 10px;">
            <!--<div id="forum" style="margin-top: 0px; padding-right: 16px;">
                <div id="new_in_forum" style="float: right; margin-left: 15px;">
                    <asp:Image ID="imgNewInForum" runat="server" CssClass="header_new_in_forum" ImageUrl="~/Images/SubHeader_LastMessageRecipe.png" />
                    <div id="forum_top">
                    </div>
                    <div id="forum_container">
                        <div style="padding: 12px 32px;">
                            <iframe src="http://62.219.14.86/forum/default.aspx?fsid=matkon<%=this.RecipeId %>&srch=n&ttl=n"
                                id="forum_frame" scrolling="no" frameborder="0" style="height: 216px;"></iframe>
                            <%--<div style="clear: both; height: 15px; margin-top: 10px;">
                                <asp:HyperLink ID="lnkForum" runat="server" CssClass="recipe_forum_link" NavigateUrl="~/Forum.aspx"
                                    Text="כניסה לפורום >"></asp:HyperLink>
                            </div>--%>
                        </div>
                    </div>
                    <div id="forum_bottom">
                    </div>
                </div>
            </div>-->
            <%--<div id="recipe_bottom_left_corner" class="SideTable">
            </div>--%>
        </div>
    </div>

    <asp:HiddenField ID="hfRecipeId" runat="server" />

</asp:Content>
