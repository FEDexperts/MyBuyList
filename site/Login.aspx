<%@ Page Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" Title="<%$ Resources:MyGlobalResources, Login %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="page_header">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Header_LogIn.png" /><br />
        <br />
    </div>
    <table style="margin-top: 10px; color: #656565;">
        <tr>
            <td align="right" colspan="3">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Underline="True" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Login ID="Login1" runat="server" Height="161px" LoginButtonText="כניסה" PasswordLabelText="סיסמא:"
                    RememberMeText="זכור אותי" TitleText="" PasswordRecoveryText="שכחת סיסמא?" PasswordRecoveryUrl="PasswordRecovery.aspx"
                    CreateUserText="משתמש חדש?" CreateUserUrl="~/Register.aspx" UserNameRequiredErrorMessage="יש להכניס שם משתמש"
                    PasswordRequiredErrorMessage="יש להכניס סיסמא" FailureText="שם משתמש או סיסמא אינם נכונים"
                    UserNameLabelText="משתמש:" Width="330px" OnLoggedIn="Login1_LoggedIn" style="margin-top:10px;">
                    <LayoutTemplate>
                        <div class="login_link_container" style="text-align: left;">
                            <asp:HyperLink ID="CreateUserLink" runat="server" NavigateUrl="~/Register.aspx" >משתמש חדש?</asp:HyperLink>
                        </div>
                        <div style="margin-top: 10px;">
                            <asp:Label ID="lblUserName" runat="server" Text="שם משתמש" Font-Bold="true" />&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                Display="Static" ErrorMessage="יש להכניס שם משתמש" ToolTip="יש להכניס שם משתמש"
                                ValidationGroup="Login1" />
                            <asp:TextBox ID="UserName" runat="server" Width="329px" Height="14px" ForeColor="#656565" Font-Size="12px" BackColor="#EEEEEE" BorderColor="#656565"
                                BorderStyle="Solid" BorderWidth="1px" TabIndex="1" Style="margin-top: 6px;"></asp:TextBox>
                        </div>
                        <div style="margin-top: 10px;">
                            <asp:Label ID="lblPassword" runat="server" Text="סיסמה" Font-Bold="true" />&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                Display="Static" ErrorMessage="יש להכניס סיסמא" ToolTip="יש להכניס סיסמא" ValidationGroup="Login1" />
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="329px" Height="14px" ForeColor="#656565" Font-Size="12px" BackColor="#EEEEEE"
                                BorderColor="#656565" BorderStyle="Solid" BorderWidth="1px" TabIndex="2" Style="margin-top: 6px;"></asp:TextBox>
                        </div>                       
                        <div class="login_link_container" style="margin-top: 10px;">
                            <asp:HyperLink ID="PasswordRecoveryLink" runat="server" NavigateUrl="PasswordRecovery.aspx">שכחת סיסמא?</asp:HyperLink>
                        </div>                     
                        <div style="clear: both; height: 40px; margin-top: 20px; vertical-align: top">
                            <div style="float: right; padding-top: 6px;">
                                <asp:CheckBox ID="RememberMe" runat="server" Text="זכור אותי" CssClass="chxBox_aligned" />
                            </div>                           
                            <div style="float: left;">
                                <asp:ImageButton ID="LoginButton" runat="server" CommandName="Login" ValidationGroup="Login1"
                                    TabIndex="3" ImageUrl="~/Images/btn_EnterFromLogIn_up.png" onmouseover='this.src="Images/btn_EnterFromLogIn_over.png";'
                                    onmouseout='this.src="Images/btn_EnterFromLogIn_up.png";' onmousedown='this.src="Images/btn_EnterFromLogIn_down.png";'
                                    onmouseup='this.src="Images/btn_EnterFromLogIn_up.png";' />
                            </div>
                        </div>                       
                        <div>
                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                        </div>  
                    </LayoutTemplate>
                </asp:Login>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
