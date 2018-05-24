<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master"
    AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<%@ MasterType VirtualPath="~/MasterPages/MBL.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="page_header">
        <asp:Image ID="imgHeader" runat="server" ImageUrl="~/Images/Header_FormRegistration.png" />
    </div>
    <div id="register_right_pane" style="width: 350px; padding-right: 8px; color: #656565;
        margin-top: 14px; float: right;">
        <p style="line-height: 170%;">
            מערכת MyBuyList פותחה ככלי מסייע בתכנון ארוחות וקניות המאפשר שימוש במבחר רב של מתכונים
            ממקורות מגוונים.
            <br />
            שימוש במערכת מסייע בניהול זמן וארגון ומאפשר מימוש של תהליכים רבים. שימוש במערכת
            MyBuyList משפרת את החוויה הקולניארית והופכת את חוויות הקניות לא רק לטיול בסופר אלא
            גם לדרך חיים.
        </p>
        <div style="margin-top: 25px;">
            <asp:Image runat="server" ImageUrl="~/Images/SubHeader_RegisterDetails.png" />
        </div>
        <div style="margin-top: 15px;">
            <asp:Label ID="lblUserName" runat="server" Text="שם משתמש (אותיות באנגלית)" Font-Bold="true" />
            <asp:Label ID="Label5" runat="server" Text="*" ForeColor="#EF1839" />
            <asp:TextBox ID="txtUserName" Width="344px" Height="14px" BackColor="#eeeeee" BorderColor="#656565"
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565" Font-Size="12px" runat="server"
                ValidationGroup="1" Style="margin-top: 6px;" />
            <asp:RequiredFieldValidator ID="rfvtxtUserName" runat="server" ControlToValidate="txtUserName"
                ErrorMessage="חובה להזין שם משתמש" ValidationGroup="1" Display="Dynamic" />
            <asp:RegularExpressionValidator ID="revtxtUserName" runat="server" ErrorMessage="שם משתמש שהוכנס אינו תקין"
                ValidationExpression="\w+([-+.']\w+)*" ControlToValidate="txtUserName" ValidationGroup="1"
                Display="Dynamic" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblEmail" runat="server" Text='דוא"ל' Font-Bold="true" />
            <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#EF1839" />
            <asp:TextBox ID="txtEmail" Width="344px" Height="14px" BackColor="#eeeeee" BorderColor="#656565"
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565" Font-Size="12px" runat="server"
                Style="margin-top: 6px;" />
            <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" ControlToValidate="txtEmail"
                Display="Dynamic" ErrorMessage="חובה להזין אימייל" ValidationGroup="1"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="האימייל שהוכנס אינו תקין"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"
                ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>
        <div id="trPassword1" runat="server" style="margin-top: 10px;">
            <asp:Label ID="lblPassword1" runat="server" Text="סיסמה" Font-Bold="true" />
            <asp:Label ID="Label3" runat="server" Text="*" ForeColor="#EF1839" />
            <asp:TextBox ID="txtUserPassword" Width="344px" Height="14px" BackColor="#eeeeee"
                BorderColor="#656565" BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565"
                Font-Size="12px" runat="server" TextMode="Password" AutoCompleteType="Disabled"
                Style="margin-top: 6px;" />
            <asp:RequiredFieldValidator ID="rfvtxtUserPassword" runat="server" ControlToValidate="txtUserPassword"
                ErrorMessage="חובה להזין סיסמה" ValidationGroup="1" Display="Dynamic" />
            <asp:RegularExpressionValidator ID="revtxtUserPassword" runat="server" ErrorMessage="הסיסמה שהוכנסה אינה תקינה"
                ValidationExpression="\w+([-+.']\w+)*" ControlToValidate="txtUserPassword" ValidationGroup="1"
                Display="Dynamic" />
        </div>
        <div id="trPassword2" runat="server" style="margin-top: 10px;">
            <asp:Label ID="lblPassword2" runat="server" Text="אישור סיסמה" Font-Bold="true" />
            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#EF1839" />
            <asp:TextBox ID="txtUserPassword2" Width="344px" Height="14px" BackColor="#eeeeee"
                BorderColor="#656565" BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565"
                Font-Size="12px" runat="server" TextMode="Password" Style="margin-top: 6px;" />
            <asp:RequiredFieldValidator ID="rfvtxtUserPassword2" runat="server" ControlToValidate="txtUserPassword2"
                Display="Dynamic" ErrorMessage="יש לחזור על הסיסמה" ValidationGroup="1" />
            <asp:CompareValidator ID="cvPasswords" runat="server" ControlToCompare="txtUserPassword"
                ControlToValidate="txtUserPassword2" Display="Dynamic" ErrorMessage="הוזנו סיסמאות לא זהות"
                ValidationGroup="1" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblDisplayName" runat="server" Text="שם לתצוגה באתר (כינוי)" Font-Bold="true" />
            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="#EF1839" />
            <asp:TextBox ID="txtDisplayName" Width="344px" Height="14px" BackColor="#eeeeee"
                BorderColor="#656565" BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565"
                Font-Size="12px" runat="server" Style="margin-top: 6px;" />
            <asp:RequiredFieldValidator ID="rfvtxtDisplayName" runat="server" ControlToValidate="txtDisplayName"
                Display="Dynamic" ErrorMessage="חובה להזין שם להצגה" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblFirstName" runat="server" Text="שם פרטי" Font-Bold="true" />
            <asp:Label runat="server" Text="*" ForeColor="#EF1839" />
            <asp:TextBox ID="txtFirtstName" Width="344px" Height="14px" BackColor="#eeeeee" BorderColor="#656565"
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565" Font-Size="12px" runat="server"
                Style="margin-top: 6px;" />
            <asp:RequiredFieldValidator ID="rfvtxtFirstName" runat="server" ControlToValidate="txtFirtstName"
                Display="Dynamic" ErrorMessage="חובה להזין שם פרטי" ValidationGroup="1" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblLastName" runat="server" Text="שם משפחה" Font-Bold="true" />
            <asp:TextBox ID="txtLastName" Width="344px" Height="14px" BackColor="#eeeeee" BorderColor="#656565"
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565" Font-Size="12px" runat="server"
                Style="margin-top: 6px;" />
        </div>
        <div style="margin-top: 22px;">
            <asp:CheckBox ID="cbxEmail" runat="server" Text="אני מסכימ/ה לקבל דיוור מהאתר" Checked="true"
                CssClass="chxBox_aligned_small chxAlignedRight" Font-Size="12px" />
        </div>
        <div style="clear: both; height: 40px; margin-top: 20px; vertical-align: top;">
            <a href="Terms.aspx" id="linkTerms" style="padding-top: 5px;">תנאי שימוש &gt;</a>
            <asp:LinkButton ID="btnRegister" runat="server" OnClick="btnOk_Click" ValidationGroup="1"
                Style="float: left;">
                <asp:Image ID="imgBtnRegister" runat="server" ImageUrl="~/Images/btn_Registration_up.png"
                    onmouseover='this.src="Images/btn_Registration_over.png";' onmouseout='this.src="Images/btn_Registration_up.png";'
                    onmousedown='this.src="Images/btn_Registration_down.png";' onmouseup='this.src="Images/btn_Registration_up.png";' />
                <asp:Image ID="imgBtnSend" runat="server" ImageUrl="~/Images/btn_Send_up.png" onmouseover='this.src="Images/btn_Send_over.png";'
                    onmouseout='this.src="Images/btn_Send_up.png";' onmousedown='this.src="Images/btn_Send_down.png";'
                    onmouseup='this.src="Images/btn_Send_up.png.png";' Visible="false" />
            </asp:LinkButton>
        </div>
        <%--<div class="field_row">
        <div style="width: 109px">
            &nbsp;
        </div>
        <div style="width: 129px">
            &nbsp;
        </div>
        <div>
            &nbsp;
        </div>
    </div>
    <div class="field_row">        
        
        
        <div align="right" dir="rtl">
            &nbsp;
        </div>
    </div> --%>
        <div class="field_row">
            <div>
                <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="hlSignIn" runat="server" NavigateUrl="~/Login.aspx" Visible="false">התחברות</asp:HyperLink>
            </div>
        </div>
    </div>
    <%--Image should be replaces with the same picture, without numbers at the bottom--%>
    <div id="register_left_pane" style="width: 340px; margin-top: 14px; float: left;
        text-align: left; padding-left: 9px;">
        <%--<asp:Image runat="server" ImageUrl="~/Images/Img01.jpg" Width="300px" />--%>
        <div style="width: 300px; margin: 0px auto; height: 250px; border: 1px solid #656565;">
            <!-- BEGIN STANDARD TAG - 300 x 250 - www.mybuylist.com: Run-of-site - DO NOT MODIFY -->

            <script type="text/javascript" src="http://ad.adserverplus.com/st?ad_type=ad&ad_size=300x250&section=815787&ban_flash=1"></script>

            <!-- END TAG -->
        </div>
    </div>
    <div style="float: right">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
