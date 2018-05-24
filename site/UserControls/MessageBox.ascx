<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageBox.ascx.cs" Inherits="CommonControls_Messages_MessageBox" %>
<asp:LinkButton ID="LinkButton1" runat="server" Style="display: none" meta:resourcekey="LinkButton1Resource1" />
<asp:Panel ID="Panel1" runat="server" Style="display: none; width: 430px" CssClass="modalConfirm"
    meta:resourcekey="Panel1Resource1">
    <asp:Panel ID="pnlTitle" runat="server" Style="cursor: move; background-color: #DDDDDD;
        border: solid 1px Gray; color: Black" meta:resourcekey="pnlTitleResource1">
        <div>
            <p>
                <asp:Label ID="lblTitle" runat="server" meta:resourcekey="lblTitleResource1" /></p>
        </div>
    </asp:Panel>
    <div>
        <p style="overflow: auto;max-height: 500px">
            <asp:Label ID="lblMessage" runat="server" />
        </p>
        <p style="text-align: center;">
            <asp:Button ID="OkButton" runat="server" Text="OK" meta:resourcekey="OkButtonResource1" />
            <asp:Button ID="CancelButton" runat="server" Text="Cancel" meta:resourcekey="CancelButtonResource1" />
        </p>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="LinkButton1"
    PopupControlID="Panel1" BackgroundCssClass="modalBackground" OkControlID="OkButton"
    BehaviorID="msgbox" OnOkScript="_onOk();" OnCancelScript="_onCancelScript();"
    CancelControlID="CancelButton" DropShadow="True" PopupDragHandleControlID="pnlTitle"
    DynamicServicePath="" Enabled="True" />

<script type="text/javascript">
var _okScript;
var _cancelScript;

function msgbox(message, title, displayCancel, okScript, cancelScript){
    _okScript = okScript;
    _cancelScript = cancelScript;
    var pnlTitle = document.getElementById("<%= this.pnlTitle.ClientID %>");
    if(title == null){
        pnlTitle.style.display = 'none';
    } else {
        document.getElementById("<%= this.lblTitle.ClientID %>").innerHTML = title;
        pnlTitle.style.display = 'block';
    }
    
    var lbl = document.getElementById("<%= this.lblMessage.ClientID %>");
    lbl.innerHTML = message;
    
    var cancel = document.getElementById("<%= this.CancelButton.ClientID %>");
    cancel.style.display = displayCancel ? 'inline':'none';
    
    var modal = $find("msgbox");
    modal.show();
}

function _onOk(){
    if(_okScript != null)
        eval(_okScript);
}

function _onCancelScript(){
    if(_cancelScript != null)
        eval(_cancelScript);
}
</script>

