<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRecipePicture.ascx.cs"
    Inherits="ucRecipePicture" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Panel ID="Panel1" runat="server" Style="display: none" BorderWidth="1px" BorderColor="#aa0000"
    BackColor="White" Width="350px" CssClass="modalPopup">
    <asp:Panel ID="pnlTitle" runat="server" Style="cursor: move; background-color: #aa0000;
        border: solid 1px Gray; color: White; height: 24px;">
        <div style="text-align: center;">
            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="11pt" Font-Bold="true"
                Height="15px" Text="בחר תמונה"></asp:Label>
        </div>
    </asp:Panel>
    <iframe id="iframe1" src="" frameborder="0"
        width="350" height="320" scrolling="no"></iframe>
</asp:Panel>
<asp:LinkButton ID="hiddenLink1" runat="server" Style="display: none" />
<ajaxToolkit:ModalPopupExtender BehaviorID="child3" ID="mpeSelectPicture" runat="server"
    RepositionMode="RepositionOnWindowResizeAndScroll" TargetControlID="hiddenLink1"
    PopupControlID="Panel1" BackgroundCssClass="modalBackground2" DropShadow="true"
    PopupDragHandleControlID="pnlTitle" />
