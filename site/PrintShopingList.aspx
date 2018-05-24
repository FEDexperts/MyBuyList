<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintShopingList.aspx.cs"
    Inherits="PrintShopingList" Title="<%$ Resources:MyGlobalResources, ShoppingListPageTitle %>" %>

<%@ Register TagPrefix="uc1" TagName="ShopingList" Src="~/UserControls/ucPrintShopingList.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html;  charset=window-1255" />
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    <div id="printPage_main">
        <div id="printPage_top">
            <img src='Images/mybuylist.png' alt="logo" width="185px" />
            <div style="height: 165px;">
            </div>
            <asp:Label ID="lblSiteUrl" runat="server" Font-Bold="true" Font-Size="30px" Text="www.mybuylist.com" />
        </div>
        <div id="printPage_content" style="font-size: 16px;">       
            <div id="shoppingListTitle">
                <asp:Image ID="imgShoppingListTitle" runat="server" ImageUrl="~/Images/Header_BuyList.png" />
            </div>
            <div id="shoppingListRecipes">
                <asp:Label ID="lblMenuRecipes" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="18px">הרשימה מכילה מצרכים למתכונים:</asp:Label>
                <table style="width: 95%; margin-top: 5px; margin-right: 15px;">
                    <tr>
                        <td valign="top">
                            <asp:Repeater ID="rptMenuRecipes" runat="server">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td style="color: Black; font-size: smaller; height: 25px;">
                                                &gt;
                                            </td>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem ,"RecipeName")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="productsList">
                <asp:Repeater ID="rptMenuShopDepartments" runat="server" OnItemDataBound="rptMenuShopDepartments_ItemDataBound">
                    <ItemTemplate>
                        <div style="margin-top: 20px;">
                            <div id="printPage_departmentName">
                                <%#DataBinder.Eval(Container.DataItem ,"ShopDepartmentName")%>:
                            </div>
                            <div>
                                <table style="width: 100%">
                                    <asp:Repeater ID="rptShoppingFoods" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 30px;">
                                                    <table style="width: 15px; height: 15px; border-style: solid; border-width: 1px;
                                                        border-color: Black;">
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right" style="color: Black; height: 25px;">
                                                    <%#DataBinder.Eval(Container.DataItem, "Display")%>
                                                    <%#DataBinder.Eval(Container.DataItem, "FoodName")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div style="color: Black; font-weight: bold; margin-top: 20px; margin-bottom: 5px;
                font-size: 18px;">
                <asp:Label ID="lblAdditionalItems" runat="server" Text="מצרכים נוספים:" Visible="false" />
            </div>
            <div>
                <asp:Repeater ID="rptAdditionalItems" runat="server">
                    <ItemTemplate>
                        <div>
                            <table>
                                <tr>
                                    <td style="width: 30px;">
                                        <table style="width: 15px; height: 15px; border-style: solid; border-width: 1px;
                                            border-color: Black;">
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem ,"ItemName")%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <%--<table>
            <tr style='margin-top: 0px;'>
                <td colspan='2' align='center'>
                    <img src='http://www.mybuylist.com/Images/New/SpiralVertical.gif' />
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style='width: 400px;'>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align='center'>
                                <b style="font-family:Arial">הדרך המטעימה והנעימה שלך לרשימת קניות </b>
                            </td>
                            <td rowspan='2'>
                                <img src='http://www.mybuylist.com/Images/New/Logo.gif' />
                            </td>
                        </tr>
                        <tr>
                            <td align='center'>
                                <b style='font-size: larger; color: #C51015; font-family:Arial'>רשימת קניות</b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc1:ShopingList ID="ucShopingListPrint" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>--%>
    </div>
    </form>
</body>
</html>
