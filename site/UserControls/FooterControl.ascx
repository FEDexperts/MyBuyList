<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooterControl.ascx.cs" Inherits="UC_FooterControl" %>
<div id="footer">
    <div id="links_footer">
        <div id="footerContent" runat="server">
        </div>
    </div>
        <div id="bottom_links">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/" Text="עמוד הבית" onclick='return allowLeave()'></asp:HyperLink>&nbsp;|&nbsp;
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/About.aspx" Text="אודות" onclick='return allowLeave()'></asp:HyperLink>&nbsp;|&nbsp;
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/ContactUs.aspx" Text="פרסמו אצלנו" onclick='return allowLeave()'></asp:HyperLink>&nbsp;|&nbsp;
            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Terms.aspx" Text="תנאי שימוש" onclick='return allowLeave()'></asp:HyperLink>&nbsp;|&nbsp;            
            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/ContactUs.aspx" Text="צור קשר" onclick='return allowLeave()'></asp:HyperLink>
        </div>
    <%--<div id="credits">
        אפיון Daphna Ben Ezra | <a href="mailto:natalie.broyer@gmail.com">עיצוב</a> <a href="mailto:natalie.broyer@gmail.com"><asp:Image ImageUrl="~/Images/tn-multimedia.png" runat="server" /></a> | <a href="http://www.properdev.com">פיתוח תוכנה</a> <a href="http://www.properdev.com"><asp:Image ID="imgProperDevLogo" ImageUrl="~/Images/properdev.png" runat="server" /></a>
    </div>--%> 
    <div id="credits">
        <a href="mailto:natalie.broyer@gmail.com">עיצוב</a> <a href="mailto:natalie.broyer@gmail.com"><asp:Image ID="Image1" ImageUrl="~/Images/tn-multimedia.png" runat="server" /></a> | <a href="http://www.properdev.com">פיתוח תוכנה</a> <a href="http://www.properdev.com"><asp:Image ID="Image2" ImageUrl="~/Images/properdev.png" runat="server" /></a>
    </div>
    <div id="copyright">
        כל הזכויות באתר שמורות לחברת MyBuyList.com מפעילת האתר. אין להעתיק או לעשות שימוש בתכנים באתר, פרט לשימושים המותרים המפורטים בתנאי השימוש באתר ללא קבלת אישור מפורש ובכתב ממפעילת האתר.
    </div>
</div>