<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucContactUs.ascx.cs" Inherits="ucContactUs" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <table dir="rtl" style="width: 350px;" cellpadding="0px" cellspacing="0px">
            <tr>
                <td>
                    <div style="margin-top: 10px;">
                        <asp:Label ID="lblFirstName" runat="server" Text="שם:"></asp:Label>
                    </div>
                    <div style="margin-top: 6px;">
                        <asp:TextBox ID="txtFirtstName" Width="344px" Height="14px" BackColor="#eeeeee" BorderColor="#656565"
                            BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565" Font-Size="12px" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RequiredFieldValidator ID="rfvtxtFirstName" runat="server" ControlToValidate="txtFirtstName"
                        ErrorMessage="חובה להזין שם " ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>                
                <td>
                    <div style="margin-top: 10px;">
                        <asp:Label ID="lblEmail" runat="server" Text="אימייל:"></asp:Label>
                    </div>
                    <div style="margin-top: 6px;">
                        <asp:TextBox ID="txtEmail" Width="344px" Height="14px" BackColor="#eeeeee" BorderColor="#656565"
                            BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565" Font-Size="12px" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" ErrorMessage="חובה להזין אימייל" ValidationGroup="1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="האימייל שהוכנס אינו תקין"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"
                        ValidationGroup="1" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>                
                <td>
                    <div style="margin-top: 10px;">
                        <asp:Label ID="lbl2" runat="server" Text="נושא:"></asp:Label>
                    </div>
                    <div style="margin-top: 6px;">
                        <asp:TextBox ID="txtSubject" Width="344px" Height="14px" BackColor="#eeeeee" BorderColor="#656565"
                            BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565" Font-Size="12px" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RequiredFieldValidator ID="rfvtxtEmail0" runat="server" ControlToValidate="txtSubject"
                        Display="Dynamic" ErrorMessage="חובה להזין נושא להודעה" ValidationGroup="1"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>                
                <td>
                    <div style="margin-top: 10px;">
                        <asp:Label ID="Label1" runat="server" Text="הודעה:"></asp:Label>
                    </div>
                    <div style="margin-top: 6px;">
                        <asp:TextBox ID="txtData" Width="344px" Height="140px" TextMode="MultiLine" BackColor="#eeeeee" BorderColor="#656565"
                            BorderStyle="Solid" BorderWidth="1px" ForeColor="#656565" Font-Size="12px" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtData"
                        Display="Dynamic" ErrorMessage="חובה להזין הודעה" ValidationGroup="1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:ImageButton ID="btnOk" runat="server" OnClick="btnOk_Click" ValidationGroup="1" style="margin-top: 10px;"
                        ImageUrl="~/Images/btn_Send4_up.png" onmouseover='this.src="Images/btn_Send4_over.png";'
                        onmouseout='this.src="Images/btn_Send4_up.png";' onmousedown='this.src="Images/btn_Send4_down.png";'
                        onmouseup='this.src="Images/btn_Send4_up.png";' />
                    <%--&nbsp;<asp:Button ID="btnCancel" runat="server" Text="ביטול" CausesValidation="False"
                        PostBackUrl="~/Default.aspx" />
                    &nbsp;--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="hlSignIn" runat="server" NavigateUrl="~/Login.aspx" Visible="false">התחברות</asp:HyperLink>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
