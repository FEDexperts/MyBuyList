<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenusSearchControl.ascx.cs"
    Inherits="UserControls_MenusSearchControl" %>
<div style="width: 100%">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="ImageButton1">
    <div>
        הצג:
        <asp:LinkButton ID="lnkAllMenus" Style="color: #FBAB14" runat="server" Text="כל התפריטים"
            OnClick="lnkAllMenus_Click" />
        <span style="margin-left: 5px">|</span>
        <asp:LinkButton ID="lnkMyMenus" Style="color: #FBAB14" runat="server" Text="התפריטים שלי"
            OnClick="lnkMyMenus_Click" />
        <span style="margin-left: 5px">|</span>
        <asp:LinkButton ID="lnkFavMenus" Style="color: #FBAB14" runat="server" Text="המועדפים שלי"
            OnClick="lnkFavMenus_Click" />
    </div>
    <div style="padding-top: 10px">
        <table style="width: 100%">
            <tr style="vertical-align: top">
                <td style="width: 50%; padding-top: 7px">
                    <asp:DropDownList ID="lstCategories" runat="server" Width="137px" Height="18px" BackColor="#fee0a8"
                        Font-Bold="true" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #FBAB14;
                        padding-right: 5px;" AutoPostBack="True" 
                        onselectedindexchanged="lstCategories_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 50%; text-align: left; padding-top: 7px">
                    <asp:TextBox ID="txtSearchTerm" runat="server" Width="140px" BackColor="#fee0a8"
                        Font-Bold="true" Font-Size="12px" ForeColor="#656565" Style="border: 1px solid #FBAB14;
                        padding-right: 5px; vertical-align: top;" Height="16px" />
                </td>
                <td>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/btn_SearchMenu_up.png"
                        onmouseover='this.src="Images/btn_SearchMenu_Over.png";' onmouseout='this.src="Images/btn_SearchMenu_up.png";'
                        onmousedown='this.src="Images/btn_SearchMenu_down.png";' nmouseup='this.src="Images/btn_SearchMenu_up.png";'
                        Style="vertical-align: top" onclick="ImageButton1_Click" />
                </td>
            </tr>
        </table>
    </div>
    </asp:Panel>
</div>
