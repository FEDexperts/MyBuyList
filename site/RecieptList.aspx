<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="RecieptList.aspx.cs" Inherits="Wizard.WizReciept" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <script type="text/javascript">
        jQuery(document).ready(function () {

            CheckLogin();

            jQuery("#servings").dialog({
                autoOpen: false,
                title: "מספר סועדים",
                width: 350,
                height: 180,
                resizable: false,
                modal: true,
                buttons: { "אישור": AddToList, "ביטול": Close }
            });

            jQuery(".recipe_action").click(function () {

                jQuery("#servings").dialog("open");

                var recipeId = jQuery(this).attr("id").replace("addToList", "");
                jQuery("#HiddenSelectedReciep").val(recipeId);

                jQuery.ajax({
                    type: "POST",
                    url: "RecieptList.aspx/GetServings",
                    data: "{'recipeId':" + recipeId + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var servings = msg.d;
                        jQuery("#serving1").html(servings);
                        jQuery("#serving2").html(servings * 2);
                        jQuery("#serving3").html(servings * 4);
                        jQuery("#serving4").html(servings * 6);
                        
                    }
                });
            });

            jQuery(".serving").click(function () {
                var servingVal = jQuery(this).html();
                jQuery("#HiddenServingValue").val(servingVal);
            });
        })

        function AddToList() {
            var recipeId = jQuery("#HiddenSelectedReciep").val();
            var servings = jQuery("#HiddenServingValue").val();

            jQuery.ajax({
                type: "POST",
                url: "RecieptList.aspx/AddRecipeToList",
                data: "{'recipeId':" + recipeId + ", 'servings':" + servings + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    jQuery("#servings").dialog("close");
                    if (msg.d == true) {
                        jQuery("#message").html("המתכון הוסף בהצלחה לרשימת הקניות");
                        jQuery("#message").dialog("open");
                    }
                    else {
                        jQuery("#message").html("המתכון לא הוסף לרשימת הקניות! אנא נסה שוב.");
                        jQuery("#message").dialog("open");
                    }
                }
            });

        }

        function Close() {
            jQuery("#servings").dialog("close");
        }


        var selectedServingItem;

        function ServingSelected(recipeId, servingItemId, panel) {
        }

        function ServingMouseOut(servingsItem) {
            if (selectedServingItem == servingsItem) return;

            servingsItem.style.backgroundColor = 'white'
        }

        function ServingMouseOver(servingsItem) {
            if (servingsItem.style.backgroundColor == '#8DAC34') {
                selectedServingItem = servingsItem
            }
            servingsItem.style.backgroundColor = '#8DAC34'
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <input id="HiddenSelectedReciep" type="hidden" />
    <input id="HiddenServingValue" type="hidden" />
    <div id="servings" style="display: none">
        <div id="serving1" class="serving" style="border: 2px solid #8DAC34; float: right; width: 40px; height: 40px; text-align: center; font-size: 18pt; margin: 5px; cursor: pointer" onmouseover="ServingMouseOver(this)" onmouseout="ServingMouseOut(this)" onclick="ServingSelected({0},{1},this)"></div>
        <div id="serving2" class="serving" style="border: 2px solid #8DAC34; float: right; width: 40px; height: 40px; text-align: center; font-size: 18pt; margin: 5px; cursor: pointer" onmouseover="ServingMouseOver(this)" onmouseout="ServingMouseOut(this)" onclick="ServingSelected({0},{1},this)"></div>
        <div id="serving3" class="serving" style="border: 2px solid #8DAC34; float: right; width: 40px; height: 40px; text-align: center; font-size: 18pt; margin: 5px; cursor: pointer" onmouseover="ServingMouseOver(this)" onmouseout="ServingMouseOut(this)" onclick="ServingSelected({0},{1},this)"></div>
        <div id="serving4" class="serving" style="border: 2px solid #8DAC34; float: right; width: 40px; height: 40px; text-align: center; font-size: 18pt; margin: 5px; cursor: pointer" onmouseover="ServingMouseOver(this)" onmouseout="ServingMouseOut(this)" onclick="ServingSelected({0},{1},this)"></div>
    </div>
    <asp:UpdatePanel ID="upRecipes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div id="recipe_list">
                <asp:Repeater ID="rptRecipes" runat="server" OnItemDataBound="rptRecipes_ItemDataBound">
                    <HeaderTemplate>
                        <!-- pager -->
                        <div id="topPager" class="pager" style="width: 540px; text-align: right; direction: ltr;
                            margin-bottom: 18px; padding-right: 5px;">
                            <asp:PlaceHolder ID="phPager" runat="server"></asp:PlaceHolder>
                        </div>
                    </HeaderTemplate>
                    <FooterTemplate>
                        <!-- pager -->
                        <div id="bottomPager" class="pager" style="width: 540px; text-align: right; direction: ltr;
                            margin-top: 18px; padding-right: 5px;">
                            <asp:PlaceHolder ID="phPager" runat="server"></asp:PlaceHolder>
                        </div>
                    </FooterTemplate>
                    <ItemTemplate>
                        <div class="recipe_box">
                            <div style="clear: both; height: 18px;">
                                <div class="recipe_main_category">
                                    <asp:Label ID="lblMainCategory" runat="server" Text='<%# Eval("MainCategoryName") %>'></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both; height: 133px; padding-right: 12px; padding-left: 12px;
                                padding-top: 20px;">
                                <div class="recipe_inner_box">
                                    <div class="recipe_title">
                                        <asp:HyperLink ID="lnkRecipe" runat="server" Text='<%# Eval("RecipeTitle") %>' NavigateUrl=""></asp:HyperLink>
                                    </div>
                                    <div class="recipe_actions_box">
                                        <a id="addToList<%# Eval("RecipeId") %>" class="recipe_action" style="cursor: pointer;">הוסף לרשימת הקניות</a>
                                    </div>
                                    <div id="infoTags" style="clear: both; min-height: 30px; padding-top: 10px;">
                                        <div id="myFavoritesInfoTag" runat="server" class="myFavoritesInfoTag" title="קיים במועדפים שלי">
                                        </div>
                                        <div id="allFavoritesInfoTag" title="גולשים אוהבים מתכון זה">
                                            <div style="width: 15px; text-align: center;">
                                                <asp:Label ID="lblAllFavorites" runat="server" />
                                            </div>
                                        </div>
                                        <div id="allMenusInfoTag" title="תפריטים כוללים מתכון זה">
                                            <div style="width: 16px; text-align: center;">
                                                <asp:Label ID="lblAllMenus" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="recipe_tags">
                                        <asp:Label ID="lblRecipeTags" runat="server" Text='<%# Eval("RecipeTags") %>'></asp:Label>
                                    </div>
                                    <div class="recipe_description">
                                        <asp:Label ID="lblRecipeDescription" runat="server" Text='<%# Eval("RecipeDescription") %>'></asp:Label>
                                    </div>
                                    <div class="publisher_box">
                                        <asp:Label ID="lblPublishedBy" runat="server" Text='פורסם ע"י'></asp:Label>
                                        <asp:HyperLink ID="lnkPublisher" runat="server" NavigateUrl="" CssClass="published_value"
                                            Text='<%# Eval("PublisherName") %>'></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblPublishedOn" runat="server" Text='בתאריך'></asp:Label>
                                        <asp:Label ID="lblPublishDate" runat="server" Text='<%# Eval("PublishDate") %>' CssClass="published_value"></asp:Label>
                                    </div>
                                </div>
                                <div class="recipe_thumbnail_box">
                                    <div class="recipe_thumbnail" style="width: 123px; height: 94px;">
                                        <asp:Image ID="imgThumbnail" runat="server" ImageUrl='<%# Eval("RecipeThumbnail") %>'
                                            Style="max-height: 94px; max-width: 123px;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftPane" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
