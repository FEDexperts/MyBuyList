<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="RoutineList.aspx.cs" Inherits="RoutineList" %>

<%@ Register Src="~/UserControls/ucIngridians.ascx" TagName="ucIngridians" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/ucSummeryList.ascx" TagName="ucSummeryList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <script type="text/javascript">
        onload = function () {
            CheckLogin();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div style="float: right; margin-right: 60px; margin-top: 10px;">
        <div>
            <div style="color: #F1A211; font-size: 22pt; font-weight: bold; font-family: Arial;
                text-align: center;">
                <asp:Label ID="LabelTile" runat="server" Text="<%$ Resources:MyGlobalResources, RoutineListTitle %>"></asp:Label>
            </div>
            <asp:RadioButton ID="rbFromList" runat="server" Checked="true" GroupName="1" AutoPostBack="true"
                OnCheckedChanged="ListChecked" />
            <asp:Label ID="Label1" runat="server" Text="רשימות שמורות" Font-Bold="true" />
            <asp:DropDownList ID="ddlNames" runat="server" DataTextField="Value" DataValueField="Key"
                Width="299px" Height="20px" BackColor="#ddecb6" BorderWidth="1px" BorderStyle="Solid"
                BorderColor="#A4CB3A" Font-Size="12px" ForeColor="#656565" Style="margin-top: 6px;"
                OnSelectedIndexChanged="ListSelected" OnDataBound="ListsBound" AutoPostBack="true">
            </asp:DropDownList>
            <asp:LinkButton ID="LinkButtonDeleteList" runat="server" OnClick="DeleteList" Font-Bold="true">מחק</asp:LinkButton>
        </div>
        <div style="clear: both;">
            <asp:RadioButton ID="rbNewList" runat="server" GroupName="1" AutoPostBack="true"
                OnCheckedChanged="NewListChecked" />
            <asp:Label ID="lblName" runat="server" Text="שם הרשימה" Font-Bold="true" />
            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#EF1839" />
            <asp:TextBox ID="txtName" runat="server" MaxLength="200" AutoCompleteType="Disabled"
                Width="296px" Height="14px" BackColor="#ddecb6" BorderWidth="1px" BorderStyle="Solid"
                BorderColor="#A4CB3A" Font-Size="12px" ForeColor="#656565" Style="margin-top: 6px;
                margin-right: 12px;" onkeydown="setDirty()" />
        </div>
        <uc1:ucIngridians ID="ucIngridians1" runat="server" />
        <div style="float: right">
            <asp:ImageButton ID="ImageButtonSaveRoutineList" runat="server" ImageUrl="~/Images/btn_Save_up.png"
                onmouseover="this.src='../Images/btn_Save_over.png'" onmouseout="this.src='../Images/btn_Save_up.png'"
                OnClick="SaveRoutineList" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftPane" runat="Server">
    <div style="float: right; margin-top: 10px;">
        <uc2:ucSummeryList ID="ucSummeryList1" runat="server" Height="330" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
