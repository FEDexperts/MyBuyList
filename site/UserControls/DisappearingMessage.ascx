<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DisappearingMessage.ascx.cs"
    Inherits="CommonControls_Messages_DisappearingMessage" %>
<asp:Panel runat="server" ID="message" Style='display: none; position: fixed; right:450px; width: 100%; top: 29px;z-index: 10000'>
    <asp:Panel ID="Panel1" runat="server" SkinID="DisappearingMessageContainer" style='margin:auto; width: 400px; text-align: center'>
        <asp:Panel ID="Panel2" runat="server" SkinID="DisappearingMessageText" style='text-align:center;' >
            <asp:Label ID="msg" runat="server" text="aaa" Font-Size="14px" ForeColor="#656565"/>
        </asp:Panel>
    </asp:Panel>
</asp:Panel>
<ajaxToolkit:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" BehaviorID="RoundedCornersBehavior1"
    TargetControlID="Panel1" Radius="8" Corners="All" />

<script type="text/javascript">
    function showDisappearingMessage(text) {        
        var msg = document.getElementById('<%= msg.ClientID %>');
        msg.innerHTML = text;

        var div = document.getElementById('<%= message.ClientID %>');
        div.style.display = 'block';

        window.setTimeout("hideDisappearingMessage();", <%= DisappearAfter %>);
    }
    
    function hideDisappearingMessage(){
        var div = document.getElementById('<%= message.ClientID %>');
        div.style.display = 'none';
    }
</script>

