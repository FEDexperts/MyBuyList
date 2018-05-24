<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="RecipesNew.aspx.cs" Inherits="RecipesNew" %>

<%@ Register Src="~/UserControls/ucSendMailToFriend.ascx" TagPrefix="uc1" TagName="SendToFriend" %>
<%@ Register Src="~/UserControls/ucRecipeCategories.ascx" TagPrefix="uc2" TagName="RecipeCategories" %>
<%@ Register Src="UserControls/ReciepesSearchControl.ascx" TagName="ReciepesSearchControl"
    TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/ucSummeryList.ascx" TagName="ucSummeryList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style type="text/css">
        .serving
        {
            border: 2px solid #8DAC34;
            float: right;
            width: 40px;
            height: 40px;
            text-align: center;
            font-size: 18pt;
            margin: 5px;
            cursor: pointer;
        }
        
        .serving-selected
        {
            border: 2px solid #8DAC34;
            float: right;
            width: 40px;
            height: 40px;
            text-align: center;
            font-size: 18pt;
            margin: 5px;
            cursor: pointer;
            background-color: #8DAC34;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript">
        var selectedServingItem;
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

            jQuery(".serving").mouseover(function () {
                jQuery(this).switchClass("serving", "serving-selected");
            });

            jQuery(".serving").mouseout(function () {
                if (jQuery(this).html() != selectedServingItem)
                    jQuery(this).switchClass("serving-selected", "serving");
            });

            jQuery(".serving").click(function () {
                jQuery(".serving").each(function () {
                    jQuery(this).removeClass("serving-selected");
                    jQuery(this).addClass("serving");
                });

                selectedServingItem = jQuery(this).html();
                jQuery(this).addClass("serving-selected");
            });

            jQuery(".recipe_action").click(function () {

                jQuery("#servings").dialog("open");

                var recipeId = jQuery(this).attr("id").replace("addToList", "");
                jQuery("#HiddenSelectedReciep").val(recipeId);

                jQuery.ajax({
                    type: "POST",
                    url: "RecipesNew.aspx/GetServings",
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
                url: "RecipesNew.aspx/AddRecipeToList",
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
    </script>
    <script type="text/javascript">
        function changeSort(ddl) {
            //            var sort = ddl.options[ddl.selectedIndex].value;
            //            var recipesPage = '<%= ResolveUrl("~/Recipes.aspx") %>';
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

        function passParametersToSendToFriend(sender) {
            var recipeId = sender.getAttribute('recipeId');
            var recipeName = sender.getAttribute('recipeName');
            setParameters(recipeId, recipeName);
            showSendMailToFriendBox();
        }
    </script>
    <input id="HiddenSelectedReciep" type="hidden" />
    <input id="HiddenServingValue" type="hidden" />
    <div id="servings" style="display: none">
        <div id="serving1" class="serving">
        </div>
        <div id="serving2" class="serving">
        </div>
        <div id="serving3" class="serving">
        </div>
        <div id="serving4" class="serving">
        </div>
    </div>
    <div id="recipes_header" style="display: none;">
        <asp:Image runat="server" ImageUrl="~/Images/Header_Recipes.png" />
        <p style="margin-top: 18px; margin-bottom: 18px; padding: 0px;">
            דף זה מכיל מתכונים מגוונים של גולשים, חברי קהילת Mybuylist, מומחים מתחום התזונה
            ויצרני מזון. בלחיצה על לחצן "הוסף מתכון חדש" תוכלו גם אתם להוסיף מתכונים חדשים עבורכם
            ועבור קהילת המייבליסטים.
            <br />
            <b>להוספת מצרכי המתכון לרשימת הקניות</b> יש ללחוץ "הוסף לרשימת קניות/לתפריט" בכל
            מתכון רצוי. הרשימה המצטברת מופיעה בראש הדף (צמוד ללוגו האתר).
        </p>
        <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div id="recipes_filter">
                    <uc3:ReciepesSearchControl ID="ReciepesSearchControl1" runat="server" />
                    <%--<div>
                        <asp:LinkButton ID="lnkAllRecipes" runat="server" Text="כל המתכונים" OnClick="lnkAllRecipes_Click" />
                        <span style="margin-left: 40px"></span>
                        <asp:LinkButton ID="lnkMyRecipes" runat="server" Text="המתכונים שלי" OnClick="lnkMyRecipes_Click" />
                        <span style="margin-left: 40px"></span>
                        <asp:LinkButton ID="lnkFavRecipes" runat="server" Text="המועדפים שלי" OnClick="lnkFavRecipes_Click" />
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
                        <asp:Button ID="btnChooseCategory" runat="server" OnClientClick='clickHiddenButton()'
                            Text="בחר" Style="margin-top: 10px;" />&nbsp;&nbsp;
                        <asp:Label ID="Label2" runat="server" Text="מס' מנות" />
                        <asp:TextBox ID="txtServings" runat="server" Width="50px" Style="margin-top: 10px;" />
                    </div>
                    <%--<asp:Button ID="btnSearch" runat="server" Text="חפש" OnCommand="btnSearch_Command"
                    Style="margin-top: 10px;" Visible="false" />--%>
                    <div id="categories" runat="server" visible="false" style="margin-top: 10px;">
                        <div id="pathLinks" runat="server" style="margin-bottom: 10px;">
                        </div>
                        <asp:Panel ID="pnlCategories" runat="server" Width="300px" Height="170px" BorderWidth="1px"
                            BorderColor="#656565" ScrollBars="Vertical" Style="margin: 0px auto;" Visible="false">
                            <table style="width: 90%">
                                <asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td align="right">
                                                &nbsp;
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
                    <asp:Label ID="lblNumRecipes" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <uc2:RecipeCategories ID="ucRecipeCats" runat="server" OnRefreshData="ucRecipeCats_RefreshData" />
        <div id="recipes_sort_by_box">
            <asp:DropDownList ID="ddlSortBy" runat="server" Width="129px" Height="18px" BackColor="#DDECB6"
                Font-Bold="true" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #A4CB3A;
                padding-right: 5px;" onchange="return changeSort(this);">
                <asp:ListItem Text="מיין לפי" Value="0"></asp:ListItem>
                <asp:ListItem Text="תאריך" Value="LastUpdate"></asp:ListItem>
                <asp:ListItem Text="שם" Value="Name"></asp:ListItem>
                <asp:ListItem Text="מחבר" Value="Publisher"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div id="recipes_add_new_box">
            <asp:HyperLink ID="lnkNewRecipe" runat="server" NavigateUrl="~/RecipeEdit.aspx">
                <asp:Image runat="server" ImageUrl="~/Images/btn_AddNewRecipe_up.png" onmouseover='this.src="Images/btn_AddNewRecipe_over.png";' 
                    onmouseout='this.src="Images/btn_AddNewRecipe_up.png";' onmousedown='this.src="Images/btn_AddNewRecipe_Down.png";' 
                    nmouseup='this.src="Images/btn_AddNewRecipe_up.png";' />
            </asp:HyperLink>
        </div>
    </div>
    <asp:UpdatePanel ID="upRecipes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div id="recipe_list">
                <asp:Repeater ID="rptRecipes" runat="server">
                    <HeaderTemplate>
                        <!-- pager -->
                        <div id="topPager" class="pager" style="width: 540px; text-align: left; direction: ltr;
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
                                <div class="recipe_inner_box" style="width: 70%">
                                    <div class="recipe_title">
                                        <asp:HyperLink ID="lnkRecipe" runat="server" Text='<%# Eval("RecipeTitle") %>' NavigateUrl=""></asp:HyperLink>
                                    </div>
                                    <div class="recipe_actions_box">
                                        <a id="addToList<%# Eval("RecipeId") %>" class="recipe_action" style="cursor: pointer;">
                                            הוסף לרשימת הקניות</a>
                                        <%--<asp:LinkButton ID="blkAddRemove" runat="server" OnClick="blkAddRemove_click" Text="הוסף לרשימת קניות"
                                            CssClass="recipe_action" CausesValidation="True" />&nbsp;|&nbsp;
                                        <asp:LinkButton ID="btnSendToFriend" runat="server" CssClass="recipe_action gray_action"
                                            Text='שלח לחבר' OnClientClick="passParametersToSendToFriend(this)" recipeId='<%# Eval("RecipeId") %>'
                                            recipeName='<%# Eval("RecipeTitle") %>' />--%>
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <uc1:SendToFriend ID="ucSendMailToFriend" runat="server" OnEmailSent="ucSendMailToFriend_EmailSent" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftPane" runat="Server">
    <div style="float: right; margin-top: 10px;">
        <uc2:ucSummeryList ID="ucSummeryList1" runat="server" Height="330" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
