<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="ShortageList.aspx.cs" Inherits="ShortageList" %>

<%@ MasterType VirtualPath="MasterPages/MBL.master" %>
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
    <div style="float: right; margin-right: 40px;">
        <div style="margin-top: 10px; color: #F1A211; font-size: 22pt; font-weight: bold;
            font-family: Arial; text-align: center;">
            <asp:Label ID="LabelTile" runat="server" Text="<%$ Resources:MyGlobalResources, ShortageListTitle %>"></asp:Label>
        </div>
        <div style="margin-right: 20px;">
            <uc1:ucIngridians ID="ucIngridians1" runat="server" />
        </div>
        <div>
            <asp:ImageButton ID="ImageButtonSaveShortageList" runat="server" ImageUrl="~/Images/btn_Save_up.png"
                onmouseover="this.src='../Images/btn_Save_over.png'" onmouseout="this.src='../Images/btn_Save_up.png'"
                OnClick="SaveShortageList" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftPane" runat="Server">
    <div style="float: right; margin-top: 10px; height: 280px;">
        <uc2:ucSummeryList ID="ucSummeryList1" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
