<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPopupLogin.ascx.cs" Inherits="UserControls_ucPopupLogin" %>

<script src="Scripts/Login.js"></script>

<!-- Login -->
<div id="popuplogin-wrapper">
    <div id="popuplogin">
        <img alt="" src="Images/mybuylist.png" />
        <div>
            <asp:Literal ID="LiteralUserName" runat="server"></asp:Literal>
        </div>
        <div>
            <input id="TextUserName" type="text" />
        </div>
        <div>
            <asp:Literal ID="LiteralPassword" runat="server"></asp:Literal>
        </div>
        <div>
            <input id="TextPassword" type="password" />
        </div>
    </div>
</div>
<!-- Login -->