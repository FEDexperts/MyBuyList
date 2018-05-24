<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style type="text/css">
        .stepcarousel {
            position: relative; /*leave this value alone*/
            border: none;
            overflow: scroll; /*leave this value alone*/
            width: 443px; /*Width of Carousel Viewer itself*/
            height: 332px; /*Height should enough to fit largest content's height*/
        }

            .stepcarousel .belt {
                position: absolute; /*leave this value alone*/
                left: 0;
                top: 0;
            }

            .stepcarousel .panel {
                float: left; /*leave this value alone*/
                overflow: hidden; /*clip content that go outside dimensions of holding panel DIV*/
                margin: 0px; /*margin around each panel*/
                width: 443px; /*Width of each panel holding each content. If removed, widths should be individually defined on each content DIV then. */
            }
    </style>
    <script type="text/javascript">

        function directToRecentItem( pic ) {
            var url = pic.parentNode.parentNode.href;
            document.location = url;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <script src="Scripts/stepcarousel.js"></script>

    <%--<asp:ScriptManagerProxy ID="smp" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/stepcarousel.js" />
        </Scripts>
    </asp:ScriptManagerProxy>--%>
    <script type="text/javascript">
        /***********************************************
        * Step Carousel Viewer script- (c) Dynamic Drive DHTML code library (www.dynamicdrive.com)
        * Visit http://www.dynamicDrive.com for hundreds of DHTML scripts
        * This notice must stay intact for legal use
        ***********************************************/

        stepcarousel.setup( {
            galleryid: 'mygallery', //id of carousel DIV
            beltclass: 'belt', //class of inner "belt" DIV containing all the panel DIVs
            panelclass: 'panel', //class of panel DIVs each holding content
            autostep: { enable: true, moveby: 1, pause: 6000 },  //original pause time: 3000 ms
            panelbehavior: { speed: 500, wraparound: false, persist: true },
            defaultbuttons: { enable: false, moveby: 1, leftnav: ['http://i34.tinypic.com/317e0s5.gif', -5, 80], rightnav: ['http://i38.tinypic.com/33o7di8.gif', -20, 80] },
            statusvars: ['statusA', 'statusB', 'statusC'], //register 3 variables that contain current panel (start), current panel (last), and total panels
            contenttype: ['inline'] //content setting ['inline'] or ['external', 'path_to_external_file']
        } )

    </script>
    <div id="slideshow_text">
        <asp:Label ID="lblTitle" runat="server" Text="תכנון ארוחות וארגון רשימות קניות" CssClass="title"></asp:Label><br />
        <br />
        <asp:Label ID="lblSmallTitle" runat="server" Text="הינן פעולות חשובות בדרך לחיסכון בזמן, כסף וויכוחים. השתמשו במנוע ארגון הרשימות של My Buy List לתכנון שבועי ומבוקר של הכלכלה המשפחתית"
            CssClass="small_title"></asp:Label><br />
        <br />
        <asp:HyperLink ID="lnkLearnMore" runat="server" NavigateUrl="~/HowToUse.aspx" Text="איך זה עובד?"
            CssClass="title"></asp:HyperLink>
    </div>
    <div id="slideshow">
        <asp:Repeater ID="rptSlides" runat="server">
            <HeaderTemplate>
                <div id="mygallery" class="stepcarousel slideshow_pics">
                    <div class="belt">
            </HeaderTemplate>
            <FooterTemplate>
                </div> </div>
            </FooterTemplate>
            <ItemTemplate>
                <div class="panel">
                    <asp:Image runat="server" ImageUrl='<%# Eval("Source") %>' />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div id="whats_new">
        <div id="new_recipes">
            <div class="recent_item_inner">
                <asp:Repeater ID="rptRecentRecipes" runat="server" OnItemDataBound="rpt_ItemDataBound">
                    <ItemTemplate>
                        <div class="recent_item">
                            <asp:HyperLink ID="lnkRecentRecipePic" runat="server" NavigateUrl="~/RecipeDetails.aspx?RecipeId=">
                                <div class="recent_item_image_box">
                                    <asp:Image ID="imgThumbnail" runat="server" onclick="if (isIE) { directToRecentItem(this); }" />
                                </div>
                            </asp:HyperLink>
                            <asp:HyperLink ID="lblRecipeName" runat="server" Font-Bold="true" ForeColor="#8BAC31"
                                Style="text-decoration: none;" NavigateUrl="~/RecipeDetails.aspx?RecipeId=" />
                            <br />
                            ע"י
                            <asp:Label ID="lblPublisherName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "User.DisplayName") %>' />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div id="new_menus">
            <div class="recent_item_inner">
                <asp:Repeater ID="rptRecentMenus" runat="server" OnItemDataBound="rpt_ItemDataBound">
                    <ItemTemplate>
                        <div class="recent_item">
                            <asp:HyperLink ID="lnkRecentMenuPic" runat="server" NavigateUrl="~/MenuDetails.aspx?menuId=">
                                <div class="recent_item_image_box">
                                    <asp:Image ID="imgThumbnail" runat="server" onclick="if (isIE) { directToRecentItem(this); }" />
                                </div>
                            </asp:HyperLink>
                            <asp:HyperLink ID="lblMenuName" runat="server" Font-Bold="true" ForeColor="#FBAB14"
                                Style="text-decoration: none;" NavigateUrl="~/MenuDetails.aspx?menuId=" />
                            <br />
                            ע"י
                            <asp:Label ID="lblPublisherName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "User.DisplayName") %>' />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div id="articles">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Article.aspx?articleId=3"
            CssClass="hyperlink_article_box">
            <div class="article_box food_article">
                <div class="article_box_link_container">
                    <asp:Label ID="lblTextArticle1" runat="server" Text="איך לדעת מה אנחנו באמת אוכלים?"
                        Font-Underline="false" />
                    <asp:Label ID="lblArrowsArticle1" runat="server" CssClass="InfoLinkHP"> &gt;&gt;</asp:Label>
                </div>
            </div>
        </asp:HyperLink><asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Article.aspx?articleId=4"
            CssClass="hyperlink_article_box">
            <div class="article_box family_article">
                <div class="article_box_link_container">
                    <asp:Label ID="Label1" runat="server" Text="מה היתרונות ?איך נממש בקלות?" Font-Underline="false" />
                    <asp:Label ID="Label3" runat="server" CssClass="CookingLinkHP"> &gt;&gt;</asp:Label>
                </div>
            </div>
        </asp:HyperLink><asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Article.aspx?articleId=5"
            CssClass="hyperlink_article_box">
            <div class="article_box time_article">
                <div class="article_box_link_container">
                    <asp:Label ID="Label2" runat="server" Text="איך נרחיב את הזמן המשפחתי?" Font-Underline="false" />
                    <asp:Label ID="Label4" runat="server" CssClass="TimeLinkHP"> &gt;&gt;</asp:Label>
                </div>
            </div>
        </asp:HyperLink><%--<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Article.aspx?articleId=6"
            CssClass="hyperlink_article_box">--%><div class="article_box environment_article">
                <div class="article_box_link_container">
                    <asp:Label ID="Label5" runat="server" Text="" Font-Underline="false" />
                    <%--<asp:Label ID="Label6" runat="server" CssClass="TimeLinkHP"> &gt;&gt;</asp:Label>--%>
                </div>
            </div>
        <%--</asp:HyperLink>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
    <p style="display: none;">
        <b>Current Panel:</b> <span id="statusA"></span><b style="margin-left: 30px">Total Panels:</b> <span id="statusC"></span>
        <br />
        <a href="javascript:stepcarousel.stepBy('mygallery', -1)">Back 1 Panel</a> <a href="javascript:stepcarousel.stepBy('mygallery',
    1)"
            style="margin-left: 80px">Forward 1 Panel</a>
        <br />
        <a href="javascript:stepcarousel.stepTo('mygallery',
    1)">To 1st Panel</a> <a href="javascript:stepcarousel.stepBy('mygallery', 2)" style="margin-left: 80px">Forward 2 Panels</a>
    </p>
</asp:Content>
