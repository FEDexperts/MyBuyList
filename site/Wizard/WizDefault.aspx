<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="WizDefault.aspx.cs" Inherits="Wizard.WizDefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style type="text/css">
        .stepcarousel
        {
            position: relative; /*leave this value alone*/
            border: none;
            overflow: scroll; /*leave this value alone*/
            width: 443px; /*Width of Carousel Viewer itself*/
            height: 332px; /*Height should enough to fit largest content's height*/
        }
        .stepcarousel .belt
        {
            position: absolute; /*leave this value alone*/
            left: 0;
            top: 0;
        }
        .stepcarousel .panel
        {
            float: left; /*leave this value alone*/
            overflow: hidden; /*clip content that go outside dimensions of holding panel DIV*/
            margin: 0px; /*margin around each panel*/
            width: 443px; /*Width of each panel holding each content. If removed, widths should be individually defined on each content DIV then. */
        }
        .wizard-small-box{width: 140px; height: 140px; display: block; float: right; background-color: #f2a20f; font-size: 20px; text-align: center; }
        .wizard-big-box{width: 140px; height: 230px; display: block; float: right; background-color: #f2a20f; font-size: 20px; text-align: center; margin: 40px 0px 20px 0px; }
        .wizard-small-box-text{ margin: 40px; text-align: center; }
        .wizard-plus-icon{font-size: 50px; float: right; margin: 10px; margin-top: 40px; }
        .wizard-big-box-space{float: right; width: 50px; height: 230px; display: block; }
    </style>

    <script type="text/javascript">

        function directToRecentItem(pic) {
            var url = pic.parentNode.parentNode.href;
            document.location = url;
        }
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManagerProxy ID="smp" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/stepcarousel.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <script type="text/javascript">
        /***********************************************
        * Step Carousel Viewer script- (c) Dynamic Drive DHTML code library (www.dynamicdrive.com)
        * Visit http://www.dynamicDrive.com for hundreds of DHTML scripts
        * This notice must stay intact for legal use
        ***********************************************/

        stepcarousel.setup({
            galleryid: 'mygallery', //id of carousel DIV
            beltclass: 'belt', //class of inner "belt" DIV containing all the panel DIVs
            panelclass: 'panel', //class of panel DIVs each holding content
            autostep: { enable: true, moveby: 1, pause: 6000 },  //original pause time: 3000 ms
            panelbehavior: { speed: 500, wraparound: false, persist: true },
            defaultbuttons: { enable: false, moveby: 1, leftnav: ['http://i34.tinypic.com/317e0s5.gif', -5, 80], rightnav: ['http://i38.tinypic.com/33o7di8.gif', -20, 80] },
            statusvars: ['statusA', 'statusB', 'statusC'], //register 3 variables that contain current panel (start), current panel (last), and total panels
            contenttype: ['inline'] //content setting ['inline'] or ['external', 'path_to_external_file']
        })

       
    </script>
    <div id="wizard" style="height: 300px; width: 850px; display: block;">
        <div style="margin-top: 50px; margin-right: 120px; height: 200px;">
            <div class="wizard-small-box ui-corner-all"><div class="wizard-small-box-text">רשימה קבועה</div></div>
            <div class="wizard-plus-icon">+</div>
            <div class="wizard-small-box ui-corner-all"><div class="wizard-small-box-text">רשימת חוסרים</div></div>
            <div class="wizard-plus-icon">+</div>
            <div class="wizard-small-box ui-corner-all"><div class="wizard-small-box-text">מתכונים ותפריטים</div></div>
            <div class="wizard-plus-icon">=</div>
            <div class="wizard-small-box ui-corner-all"><div class="wizard-small-box-text">רשימה בטוחה</div></div>
        </div>
        <div style="margin-right: 150px;">
            <div style="float: right">מה ברצונך לעשות היום?</div>
            <div style="float: right">
                <div style="cursor:pointer;" onclick="CheckLogin('WizShortageList.aspx', 'WizDefault.aspx');">התחל</div>
            </div>
        </div>
    </div>
    <div id="slideshow" style="display: none">
        <div id="slideshow_text">
            <asp:Label ID="lblTitle" runat="server" Text="תכנון ארוחות וארגון רשימות קניות" CssClass="title"></asp:Label><br />
            <br />
            <asp:Label ID="lblSmallTitle" runat="server" Text="הינן פעולות חשובות בדרך לחיסכון בזמן, כסף וויכוחים. השתמשו במנוע ארגון הרשימות של My Buy List לתכנון שבועי ומבוקר של הכלכלה המשפחתית"
                CssClass="small_title"></asp:Label><br />
            <br />
            <asp:HyperLink ID="lnkLearnMore" runat="server" NavigateUrl="~/HowToUse.aspx" Text="איך זה עובד?"
                CssClass="title"></asp:HyperLink>
        </div>
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
    <%--<div id="forum">
        <div id="new_in_forum">
            <asp:Image ID="imgNewInForum" runat="server" CssClass="header_new_in_forum" ImageUrl="~/Images/Header_Forum.png" />
            <div id="forum_top">
            </div>
            <div id="forum_container">
                <div style="padding: 12px 32px;">
                    <iframe src="http://www.mybuylistforum.dreamsite.co.il/default.aspx?msgs=10&level=1&hdr=n&cmnt=n"
                        id="forum_frame" scrolling="no" frameborder="0"></iframe>
                </div>
            </div>
            <div id="forum_bottom">
            </div>
        </div>
    </div>--%>
</asp:Content>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="cphLeftPane" runat="Server">
    <!-- Login -->
    <div id="login_box" class="SideTable">
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                <asp:Panel ID="pnlLoginbox" class="login_box_loggedOut" runat="server" Font-Size="11px"
                    Style="margin-right: 0px">
                    <asp:Login ID="MasterLogin" runat="server" Height="131px" LoginButtonText="כניסה"
                        PasswordLabelText="סיסמה:" RememberMeText="זכור אותי" TitleText="" PasswordRecoveryText="שכחת סיסמא?"
                        PasswordRecoveryUrl="PasswordRecovery.aspx" CreateUserText="הרשמה" CreateUserUrl="~/User.aspx"
                        UserNameRequiredErrorMessage="*" PasswordRequiredErrorMessage="*" FailureText="שם משתמש או סיסמה שגויים"
                        UserNameLabelText="שם:" OnLoggedIn="Login1_LoggedIn">
                        <LayoutTemplate>
                            <div style="text-align: left; vertical-align: top; padding-left: 7px;">
                                <asp:HyperLink ID="CreateUserLink" runat="server" NavigateUrl="~/Register.aspx" Font-Underline="true">משתמש חדש?</asp:HyperLink>
                            </div>
                            <div style="text-align: right; vertical-align: top; margin-top: 10px;">
                                <asp:Label ID="lblUserName" runat="server" Text="שם משתמש" ForeColor="#656565" Font-Bold="true"
                                    Font-Size="12px" />
                                <asp:TextBox ID="UserName" runat="server" Width="172px" Height="14px" Font-Size="12px"
                                    BackColor="#EEEEEE" BorderColor="#656565" BorderStyle="Solid" BorderWidth="1px"
                                    ForeColor="#656565" Style="margin-top: 6px;" />
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    Display="Dynamic" ErrorMessage="יש להכניס שם משתמש" ToolTip="יש להכניס שם משתמש"
                                    ValidationGroup="Login1" Text="נא להכניס שם משתמש" />
                            </div>
                            <div style="text-align: right; vertical-align: top; margin-top: 10px;">
                                <asp:Label ID="lblPassword" runat="server" Text="סיסמה" ForeColor="#656565" Font-Bold="true"
                                    Font-Size="12px" />
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="172px" Height="14px"
                                    Font-Size="12px" BackColor="#EEEEEE" BorderColor="#656565" BorderStyle="Solid"
                                    BorderWidth="1px" ForeColor="#656565" Style="margin-top: 6px;" />
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                    Display="Dynamic" ErrorMessage="יש להכניס סיסמא" ToolTip="יש להכניס סיסמא" ValidationGroup="Login1"
                                    Text="נא להכניס סיסמה" />
                            </div>
                            <div style="text-align: right; vertical-align: top; margin-top: 10px;">
                                <asp:HyperLink ID="PasswordRecoveryLink" runat="server" NavigateUrl="PasswordRecovery.aspx"
                                    Font-Underline="true">שכחת סיסמא?</asp:HyperLink>
                            </div>
                            <div style="text-align: right; vertical-align: top; margin-top: 18px; clear: both;
                                height: 30px;">
                                <div style="float: right; padding-top: 8px;">
                                    <asp:CheckBox ID="RememberMe" runat="server" Text="זכור אותי" ForeColor="#656565"
                                        CssClass="chxBox_aligned_small" />
                                </div>
                                <div style="float: left;">
                                    <asp:ImageButton ID="LoginButton" runat="server" CommandName="Login" ValidationGroup="Login1"
                                        ImageUrl="~/Images/btn_Enter_up.png" onmouseover='this.src="Images/btn_Enter_over.png";'
                                        onmouseout='this.src="Images/btn_Enter_up.png";' onmousedown='this.src="Images/btn_Enter_Down.png";'
                                        onmouseup='this.src="Images/btn_Enter_up.png";' />
                                </div>
                            </div>
                            <div style="height: 15px; margin-top: 5px; text-align: center; vertical-align: top;
                                color: Red; font-size: 12px; font-weight: bold;">
                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                            </div>
                        </LayoutTemplate>
                    </asp:Login>
                </asp:Panel>
            </AnonymousTemplate>
            <LoggedInTemplate>
                <asp:Panel ID="pnlConnected" class="login_box_loggedIn" runat="server" Style="margin-right: 0px">
                    <div>
                        <asp:Label ID="lblWelcome" runat="server" Text="" Font-Bold="true"></asp:Label>
                    </div>
                    <div style="margin-top: 10px;">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Register.aspx" Style="margin-top: 10px;">עדכן פרטים אישיים</asp:HyperLink>
                    </div>
                    <div id="rowAdmin" runat="server" style="margin-top: 20px;" visible="false">
                        <asp:LinkButton ID="btnAdmin" runat="server" Font-Bold="true" Text='<%$ Resources:MyGlobalResources, SystemManagement %>'
                            PostBackUrl="~/Admin/Admin.aspx"></asp:LinkButton>
                    </div>
                    <div style="margin-top: 20px;">
                        הוספת למועדפים:
                        <br />
                        <asp:HyperLink ID="lnkFavRecipes" runat="server" Text="מתכונים" NavigateUrl="TBD" />
                        <asp:Label ID="lblFavRecipesNum" runat="server" Text="(0)" Font-Bold="true" />
                        ,
                        <asp:HyperLink ID="lnkFavMenus" runat="server" Text="תפריטים" NavigateUrl="TBD" />
                        <asp:Label ID="lblFavMenusNum" runat="server" Text="(0)" Font-Bold="true" />
                    </div>
                    <div style="margin-top: 20px;">
                        <asp:HyperLink ID="Label1" runat="server" Text="לרשימת הקניות שלך" NavigateUrl="~/SelectedRecipesList.aspx"
                            onclick='return allowLeave()' />
                        נוספו מצרכים
                        <br />
                        מ
                        <asp:Label ID="lblSelectedRecipesNum" runat="server" Font-Bold="true" Text="(0)" />
                        מתכונים / תפריטים שבחרת &nbsp;
                    </div>
                    <div style="margin-top: 20px;">
                        <asp:LoginStatus ID="MasterLoginStatus" runat="server" LoginText="כניסה למערכת" LogoutText="יציאה מהמערכת"
                            OnLoggedOut="MasterLoginStatus_LoggedOut" />
                    </div>
                </asp:Panel>
            </LoggedInTemplate>
        </asp:LoginView>
    </div>
    <!-- Login -->

    <!-- Facebook -->
    <div id="facebook_box"> 
        <script src="http://connect.facebook.net/he_IL/all.js#xfbml=1"></script><fb:like href="www.mybuylist.com" show_faces="false" width="225" font="arial"></fb:like>
        <script src="http://connect.facebook.net/he_IL/all.js#xfbml=1"></script><fb:facepile href="www.mybuylist.com" width="200" max_rows="1"></fb:facepile>
    </div>
    <!-- Facebook -->

    <!-- Left Banner -->
    <div>
        <object><param name='movie' value='{0}'><embed src='Images/180x710_v3.swf' width='180' height='450'></embed></object>
    </div>
    <!-- Left Banner -->
    
</asp:Content>
--%>

<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
    <p style="display: none;">
        <b>Current Panel:</b> <span id="statusA"></span><b style="margin-left: 30px">Total Panels:</b> <span id="statusC"></span><br />
        <a href="javascript:stepcarousel.stepBy('mygallery', -1)">Back 1 Panel</a> <a href="javascript:stepcarousel.stepBy('mygallery',
    1)" style="margin-left: 80px">Forward 1 Panel</a> <br />
        <a href="javascript:stepcarousel.stepTo('mygallery',
    1)">To 1st Panel</a> <a href="javascript:stepcarousel.stepBy('mygallery', 2)" style="margin-left: 80px">Forward 2 Panels</a> </p></asp:Content>