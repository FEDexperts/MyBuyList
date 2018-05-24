<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSendMailToFriend.ascx.cs"
    Inherits="UserControls_ucSendMailToFriend" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>

<asp:Panel ID="Panel1" runat="server" Style="display: none" BorderWidth="0px" BackColor="White"
    Width="250px" CssClass="modalPopup">
    <asp:Panel ID="pnlTitle" runat="server" Style="cursor: move; color: White; height: 30px;">
        <div style="clear: both; height: 20px; padding-top: 5px;">
            <div style="float: right; padding-right: 15px;">
                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="11pt" Font-Bold="true"
                    Height="15px" Text="<%$ Resources:MyGlobalResources, SendToFriend %>" />
            </div>
            <div style="float: left; padding-top: 2px; padding-left: 10px;">
                <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/btn_X.gif" style="cursor: pointer;" />
            </div>
        </div>
    </asp:Panel>
    <div style="height: 7px">
    </div>
    <div style="clear: both; min-height: 27px;">
        <div id="sendMailToFriend_txtBoxes">
            <div style="margin-bottom: 10px;">
                <asp:HiddenField ID="hdnItemId" runat="server" Value="" />
                <asp:HiddenField ID="hdnItemName" runat="server" Value="" />
                <asp:Label ID="lblItemName" runat="server" Font-Size="14px" Font-Bold="true" />
            </div>
            <div>
                <asp:Label ID="lblSenderName" runat="server" Text="שמך" /><br />
                <asp:TextBox ID="txtSenderName" runat="server" />
            </div>
            <div id="userEmail" runat="server" visible="false">
                <asp:Label ID="lblUserEmail" runat="server" Text="אימייל שלך" /><br />
                <asp:TextBox ID="txtUserEmail" runat="server" />
            </div>
            <div>
                <asp:Label ID="lblFriendName" runat="server" Text="שם החבר" /><br />
                <asp:TextBox ID="txtFriendName" runat="server" />
            </div>
            <div>
                <asp:Label ID="lblFriendEmail" runat="server" Text="אימייל של החבר" /><br />
                <asp:TextBox ID="txtFriendEmail" runat="server" />
            </div>
            <div>
                <asp:Label ID="lblMessage" runat="server" Text="הודעה" /><br />
                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Height="56px" Font-Bold="false"
                    Width="98%" Style="margin-top: 5px; border: 1px solid;" />
            </div>
        </div>
        <%--<asp:UpdatePanel ID="upBtnSend" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>--%>
                <div style="clear: both; height: 33px;">
                    <div style="float: left; padding-left: 13px;">
                        <asp:ImageButton ID="btnSend" runat="server" OnClick="btnSend_Click" />
                    </div>
                </div>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <div style="height: 4px">
    </div>
</asp:Panel>
<asp:Button ID="btnHdn" runat="server" Style="display: none;" />
<%--<ajaxToolkit:ModalPopupExtender BehaviorID="categories" ID="mpeSMTF" runat="server"
    RepositionMode="RepositionOnWindowResizeAndScroll" TargetControlID="btnHdn" PopupControlID="Panel1"
    BackgroundCssClass="modalBackground2" DropShadow="true" PopupDragHandleControlID="pnlTitle"
    CancelControlID="btnCancel" />--%>

<script type="text/javascript">

    function showSendMailToFriendBox() {
        var btnHdn = document.getElementById('<%=btnHdn.ClientID %>');
        btnHdn.click();
    }

    function setParameters(id, name) {
        document.getElementById('<%= hdnItemId.ClientID %>').value = id;
        document.getElementById('<%= lblItemName.ClientID %>').innerHTML = name;
        document.getElementById('<%= hdnItemName.ClientID %>').value = name;
    }     
    
</script>

