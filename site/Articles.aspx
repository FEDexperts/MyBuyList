<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master"
    AutoEventWireup="true" CodeFile="Articles.aspx.cs" Inherits="Articles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <script type="text/javascript">
        function changeSort(ddl) {
            var sort = ddl.options[ddl.selectedIndex].value;
            var articlesPage = '<%= ResolveUrl("~/Articles.aspx") %>';
            document.location = recipesPage + "?orderby=" + sort;
        }
    </script>

    <div id="articles_header">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Header_Articl.png" /><br />
        <br />
    </div>
    <div id="article_list">
        <asp:Repeater ID="rptArticles" runat="server" OnItemCreated="rptArticles_ItemCreated"
            OnItemDataBound="rptArticles_ItemDataBound">
            <%--<HeaderTemplate>
                <!-- pager -->
                <div id="topPager" class="menu_pager" style="width: 540px; text-align: right; direction: ltr;">
                    <asp:PlaceHolder ID="phPager" runat="server"></asp:PlaceHolder>
                </div>
            </HeaderTemplate>
            <FooterTemplate>
                <!-- pager -->
                <div id="bottomPager" class="menu_pager" style="width: 540px; text-align: right;
                    direction: ltr; margin-bottom: 40px;">
                    <asp:PlaceHolder ID="phPager" runat="server"></asp:PlaceHolder>
                </div>
            </FooterTemplate>--%>
            <ItemTemplate>
                <div class="articleIndex_box">
                    <div class="article_inner_box">
                        <div style="clear:both; height:30px;">
                            <div class="article_title">
                                <asp:HyperLink ID="lnkArticle" runat="server" Text='<%# Eval("ArticleTitle") %>'
                                    NavigateUrl='<%# Eval("ArticleLink") %>'></asp:HyperLink>
                            </div>                            
                        </div>
                        <div class="article_abstract">
                            <asp:Label ID="lblRecipeDescription" runat="server" Text='<%# Eval("ArticleAbstract") %>'></asp:Label>
                        </div>
                        <div class="publisher_box" style="clear: both; margin-top: 15px;">
                            <asp:Label ID="lblPublishedBy" runat="server" Text='פורסם ע"י'></asp:Label>
                            <asp:HyperLink ID="lnkPublisher" runat="server" NavigateUrl="" CssClass="article_published_value"
                                Text='<%# Eval("AuthorName") %>'></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblPublishedOn" runat="server" Text='בתאריך'></asp:Label>
                            <asp:Label ID="lblPublishDate" runat="server" Text='<%# Eval("PublishDate") %>' CssClass="article_published_value"></asp:Label>
                        </div>
                    </div>
                    
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
