<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="Admin.aspx.cs" Inherits="PageAdmin" Title="<%$ Resources:MyGlobalResources, SystemManagement %>" %>

<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblAdmin" runat="server" Font-Bold="true" ForeColor="Crimson" Text="<%$ Resources:MyGlobalResources, SystemManagement %>"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="color: SteelBlue">
                            ●&nbsp;<asp:LinkButton ID="btnCategories" runat="server" Font-Bold="true" Font-Underline="false"
                                ForeColor="SteelBlue" Text="<%$ Resources:MyGlobalResources, Categories %>" PostBackUrl="~/Admin/CategoriesList.aspx"
                                onmouseover="this.style.textDecoration='underline';" onmouseout="this.style.textDecoration='none'"></asp:LinkButton>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="color: SteelBlue">
                            ●&nbsp;<asp:LinkButton ID="btnMenuCategories" runat="server" Font-Bold="true" Font-Underline="false"
                                ForeColor="SteelBlue" Text="<%$ Resources:MyGlobalResources, MenuCategories %>" PostBackUrl="~/Admin/MenuCategoriesList.aspx"
                                onmouseover="this.style.textDecoration='underline';" onmouseout="this.style.textDecoration='none'"></asp:LinkButton>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="color: SteelBlue">
                            ●&nbsp;
                            <asp:LinkButton ID="btnShopDepartments" runat="server" Font-Bold="true" Font-Underline="false"
                                ForeColor="SteelBlue" Text="<%$ Resources:MyGlobalResources, FoodProductsDeparments %>"
                                PostBackUrl="~/Admin/ShopDepartmentsList.aspx" onmouseover="this.style.textDecoration='underline';"
                                onmouseout="this.style.textDecoration='none'"></asp:LinkButton>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="color: SteelBlue">
                            ●&nbsp;
                            <asp:LinkButton ID="btnFood" runat="server" Font-Bold="true" Font-Underline="false"
                                ForeColor="SteelBlue" Text="<%$ Resources:MyGlobalResources, Foods %>" PostBackUrl="~/Admin/FoodsList.aspx"
                                onmouseover="this.style.textDecoration='underline';" onmouseout="this.style.textDecoration='none'"></asp:LinkButton>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="color: SteelBlue">
                            ●&nbsp;
                            <asp:LinkButton ID="btnMeasurementUnits" runat="server" Font-Bold="true" Font-Underline="false"
                                ForeColor="SteelBlue" Text="<%$ Resources:MyGlobalResources, MeasurementUnits %>"
                                PostBackUrl="~/Admin/MeausurementUnitsList.aspx" onmouseover="this.style.textDecoration='underline';"
                                onmouseout="this.style.textDecoration='none'"></asp:LinkButton>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="color: SteelBlue">
                            ●&nbsp;
                            <asp:LinkButton ID="btnMeasurementUnitsConverts" runat="server" Font-Bold="true"
                                Font-Underline="false" ForeColor="SteelBlue" Text="<%$ Resources:MyGlobalResources, MeasurementUnitsConverts %>"
                                PostBackUrl="~/Admin/MeasurementUnitsConvertList.aspx" onmouseover="this.style.textDecoration='underline';"
                                onmouseout="this.style.textDecoration='none'"></asp:LinkButton>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="color: SteelBlue">
                            ●&nbsp;
                            <asp:LinkButton ID="lbtnGeneralItems" runat="server" Font-Bold="true" Font-Underline="false"
                                ForeColor="SteelBlue" Text="<%$ Resources:MyGlobalResources, GeneralItems %>"
                                PostBackUrl="~/Admin/GeneralItemsList.aspx" onmouseover="this.style.textDecoration='underline';"
                            onmouseout="this.style.textDecoration='none'"></asp:LinkButton>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="color: SteelBlue">
                            ●&nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="true" Font-Underline="false"
                                ForeColor="SteelBlue" Text="<%$ Resources:MyGlobalResources, EditWhatsNew %>"
                                PostBackUrl="~/Admin/EditWhatsNew.aspx" onmouseover="this.style.textDecoration='underline';"
                                onmouseout="this.style.textDecoration='none'"></asp:LinkButton>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="color: SteelBlue">
                            ●&nbsp;
                            <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="true" Font-Underline="false"
                                ForeColor="SteelBlue" Text="<%$ Resources:MyGlobalResources, EditArticles %>"
                                PostBackUrl="~/Admin/EditArticles.aspx" onmouseover="this.style.textDecoration='underline';"
                                onmouseout="this.style.textDecoration='none'"></asp:LinkButton>&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Crimson" Text="סטטיסטיקות"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="color: SteelBlue">
                            ●&nbsp;<asp:Label ID="lblUsers" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="color: SteelBlue">
                            ●&nbsp;<asp:Label ID="lblFoods" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="color: SteelBlue">
                            ●&nbsp;<asp:Label ID="lblRecipes" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="color: SteelBlue">
                            ●&nbsp;<asp:Label ID="lblMenus" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
