<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master"
    AutoEventWireup="true" CodeFile="Recipes.aspx.cs" Inherits="Recipes" Theme="Standard" %>

<%@ Register Src="~/UserControls/ucSendMailToFriend.ascx" TagPrefix="uc1" TagName="SendToFriend" %>
<%@ Register Src="~/UserControls/ucRecipeCategories.ascx" TagPrefix="uc2" TagName="RecipeCategories" %>
<%@ Register Src="UserControls/ucRecipesFilter.ascx" TagName="RecipesFilter" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/ucShoppingList.ascx" TagName="ucShoppingList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <!-- Recipes -->

    <script src="Scripts/Recipes.js"></script>

    <script>
        var ButtonRecipesRefreshClientID = '<%=ButtonRecipesRefresh.ClientID%>';
        var RecipeIdClientID = '<%=hfRecipeId.ClientID%>';
        var RecipeNameClientID = '<%=hfRecipeName.ClientID%>';
    </script>

    <div id="reciep-wrapper">
        <script type="text/javascript">
            function changeSort(ddl) {
                //            var sort = ddl.options[ddl.selectedIndex].value;
                //            var recipesPage = '<%= ResolveUrl("~/Recipes.aspx") %>';
                //            document.location = recipesPage + "?orderby=" + sort;

                var sort = ddl.options[ddl.selectedIndex].value;

                switch (sort) {
                    case 'LastUpdate':
                        document.location = '<%= OrderByLastUpdateUrl %>';
                    break;
                case 'Name':
                    document.location = '<%= OrderByNameUrl %>';
                    break;
                case 'Publisher':
                    document.location = '<%= OrderByPublisherUrl %>';
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
        <div class="header">
            <div class="top" style="display: none">
                <asp:Image runat="server" ImageUrl="~/Images/Header_Recipes.png" />
                <p class="hide">
                    דף זה מכיל מתכונים מגוונים של גולשים, חברי קהילת Mybuylist, מומחים מתחום התזונה
            ויצרני מזון. בלחיצה על לחצן "הוסף מתכון חדש" תוכלו גם אתם להוסיף מתכונים חדשים עבורכם ועבור קהילת המייבליסטים.  
            <br />
                    <b>להוספת מצרכי המתכון לרשימת הקניות</b> יש ללחוץ  "הוסף לרשימת קניות/לתפריט" בכל מתכון רצוי. הרשימה המצטברת מופיעה בראש הדף (צמוד ללוגו האתר).
                </p>
            </div>
            <div class="search">
                <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div id="recipes_filter">
                            <uc3:RecipesFilter ID="RecipesFilter1" runat="server" />
                            <div id="categories" runat="server" visible="true" style="margin-top: 10px;">
                                <div id="pathLinks" runat="server" style="margin-bottom: 10px;">
                                </div>
                                <div style="float: right">
                                    <asp:Panel ID="pnlCategories" runat="server" Width="300px" Height="170px" BorderWidth="1px"
                                        BorderColor="#656565" ScrollBars="Vertical" Style="margin: 0px auto;" Visible="false">
                                        <table style="width: 90%">
                                            <asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="right">&nbsp;
                                                            <asp:HyperLink ID="lnkCategory" runat="server" ForeColor="#656565"></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div id="numResults">
                            <asp:Label ID="lblNumRecipes" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <uc2:RecipeCategories ID="ucRecipeCats" runat="server" OnRefreshData="ucRecipeCats_RefreshData" />
        </div>
        <div class="body">
            <div class="ShoppingList">
                <asp:UpdatePanel ID="upShoppingList" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <uc1:ucShoppingList ID="ucShoppingList1" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="recipes">
                <asp:Button ID="ButtonRecipesRefresh" runat="server" Text="Button" OnClick="ButtonRecipesRefresh_Click" />
                <asp:UpdatePanel ID="upRecipes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ButtonRecipesRefresh" EventName="Click" /> 
                    </Triggers>
                    <ContentTemplate>
                        <div class="recipe_list">
                            <asp:Repeater ID="rptRecipes" runat="server">
                                <HeaderTemplate>
                                    <!-- pager -->
                                    <div id="topPager" class="pager">
                                        <asp:PlaceHolder ID="phPager" runat="server"></asp:PlaceHolder>
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="recipe_main_category">
                                        <div style="float: right; background-image: url(Images/bgr_Header_BoxIndex_Right.png); width: 19px; height: 30px; background-repeat:no-repeat;"></div>
                                        <div style="float: right; background-image: url(Images/bgr_Header_BoxIndex_Center.png); width: 220px; height: 30px; background-repeat:repeat-x;">
                                            <div style="height: 10px;"></div>
                                            <asp:Label ID="lblMainCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RecipeCategories[0].Category.CategoryName") %>'></asp:Label>
                                        </div>
                                        <div style="float: right; background-image: url(Images/bgr_Header_BoxIndex_Left.png); width: 19px; height: 30px; background-repeat:no-repeat;"></div>
                                    </div>
                                    <div class="recipe_box">
                                        <div class="body">
                                            <div class="recipe_inner_box">
                                                <div class="recipe_title">
                                                    <asp:HyperLink ID="lnkRecipe" runat="server" Text='<%# Eval("RecipeName") %>' NavigateUrl=""></asp:HyperLink>
                                                </div>
                                                <div class="recipe_actions_box">
                                                    <asp:LinkButton ID="ShoppingListAddRemove" runat="server"
                                                        CssClass="recipe_action" CausesValidation="True" OnClientClick='<%# String.Format("addRemoveRecipe(this, \"{0}\")",  Eval("RecipeID"))  %>' />
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
                                                    <asp:Label ID="lblRecipeTags" runat="server" Text='<%# Eval("Tags") %>'></asp:Label>
                                                </div>
                                                <div class="recipe_description">
                                                    <asp:Label ID="lblRecipeDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                </div>
                                                <div class="publisher_box">
                                                    <asp:Label ID="lblPublishedBy" runat="server" Text='פורסם ע"י'></asp:Label>
                                                    <asp:HyperLink ID="lnkPublisher" runat="server" NavigateUrl="" CssClass="published_value"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "User.DisplayName") %>'></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                                        <div class="published_date">
                                            <asp:Label ID="lblPublishedOn" runat="server" Text='בתאריך'></asp:Label>
                                            <asp:Label ID="lblPublishDate" runat="server" Text='<%# Eval("CreatedDate") %>' CssClass="published_value"></asp:Label>
                                        </div>
                                                </div>
                                            </div>
                                            <div class="recipe_thumbnail_box">
                                                <div class="recipe_thumbnail" style="width: 123px; height: 94px;">
                                                    <asp:Image ID="imgThumbnail" runat="server" ImageUrl='<%# Eval("Picture") %>'
                                                        Style="max-height: 94px; max-width: 123px;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <!-- pager -->
                                    <div id="bottomPager" class="pager">
                                        <asp:PlaceHolder ID="phPager" runat="server"></asp:PlaceHolder>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Recipes -->

    <asp:HiddenField ID="hfRecipeId" runat="server" />
    <asp:HiddenField ID="hfRecipeName" runat="server" />

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
