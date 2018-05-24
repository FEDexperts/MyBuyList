<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScreenShotMenu.aspx.cs" Inherits="PrintWeeklyMenu" %>

<%@ Register TagPrefix="uc1" TagName="WeeklyMenu" Src="~/UserControls/ucPrintWeeklyMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MealMenu" Src="~/UserControls/ucPrintMealMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>הדפסת תפריט</title>
</head>
<body>
    <script type="text/javascript">

        function adjustCellAppearance() {
            var mealCellsCollection = $("td[id='mealCell']");
            var mealTypeId = "<%=this.currentMenu.MenuTypeId %>";
            var i = 0;
            if (mealTypeId == "1" || mealTypeId == "4") {
                for (i = 0; i < mealCellsCollection.length; i++) {
                    mealCellsCollection[i].className = "mealDetails_meals dinerMenuMealCell";
                }
            }
            else {
                for (i = 0; i < mealCellsCollection.length; i++) {
                    mealCellsCollection[i].className = "mealDetails_meals weeklyMenuMealCell";
                }
            }
        }
        
                    
    
    </script>
    <form id="form1" runat="server">
    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"
        EnablePageMethods="true" EnableScriptGlobalization="true" ScriptMode="Release"
        EnableScriptLocalization="true">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/jquery-1.2.6.pack.js" />
        </Scripts>
    </ajaxToolkit:ToolkitScriptManager>--%>
    <div dir="rtl" id="printPage_main">
        <div id="printPage_content">
            <asp:Label ID="lblTitle1" runat="server" Text="שם התפריט:" Font-Bold="true" Font-Size="22px" />
            &nbsp;
            <asp:Label ID="lblTitle2" runat="server" Font-Bold="true" Font-Size="22px" />
            <div style="clear: both; min-height: 250px; padding-top: 20px;">
                <div style="float: right; width: 500px; font-size: 16px;">
                    <%--<div id="menu_publisher_box" style="margin-bottom: 20px;">
                        <asp:Label ID="lblPublishedBy" runat="server" Text='פורסם ע"י: '></asp:Label>
                        <asp:HyperLink ID="lnkPublisher" runat="server" NavigateUrl=""></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblPublishedOn" runat="server" Text='בתאריך: '></asp:Label>
                        <asp:Label ID="lblPublishDate" runat="server"></asp:Label>
                    </div>--%>
                    <%--<div id="menu_categories" style="clear: both; font-weight: bold; color: Black; font-size: 16px;">
                        <div style="float: right; width: 70px;">
                            קטגוריות:
                        </div>
                        <div style="float: right; width: 230px;">
                            <asp:Label ID="lblCategories" runat="server" />
                        </div>
                        <div style="clear: both; height: 1px;">
                        </div>
                    </div>--%>
                    <%--<div id="menu_tags" style="clear: both; font-weight: bold; margin-top: 11px;">
                        <div style="float: right; width: 50px;">
                            תגיות:
                        </div>
                        <div style="float: right; width: 250px;">
                            <asp:Label ID="lblMenuTags" runat="server" />
                        </div>
                        <div style="clear: both; height: 1px;">
                        </div>
                    </div>--%>
                    <%--<div style="margin-top: 20px; font-style: italic;">
                        <div id="topTags" style="clear: both; height: 43px; padding-right: 8px;">
                            <div id="myFavoritesTopTag" runat="server" title="קיים במועדפים שלי">
                                (קיים במועדפים שלי)
                            </div>
                            <div>
                                <asp:Label ID="lblAllFavorites" runat="server" Font-Bold="true" />
                                <asp:Label ID="Label3" runat="server" Text="גולשים הוסיפו מתכון זה" />
                            </div>
                        </div>
                    </div>--%>
                    <div id="MenuDinersNum" runat="server" style="margin-top: 15px;" visible="false">
                        <asp:Label ID="Label1" runat="server" Text="מספר סועדים:" />
                        <asp:Label ID="lblNoDiner" runat="server" Font-Bold="true" />
                    </div>
                    <div id="menu_description1" style="margin-top: 20px; margin-bottom: 8px;">
                        <asp:Label ID="lblSubtitle1" runat="server" Text="תיאור קצר:" Font-Size="18px" Font-Bold="true" />
                    </div>
                    <div>
                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                    </div>
                </div>
                <div id="menu_picture" style="float: left; width: 300px; height: 231px; text-align: center;">
                    <div style="position: relative; top: 80px; left: 140px">
                        <img src='Images/mybuylist.png' alt="logo" width="100px" />
                    </div>
                    <div>
                        <asp:Image ID="imgMenuPicture" runat="server" BorderColor="Black" BorderWidth="1px"
                            BorderStyle="Solid" />
                    </div>
                    <%--<div id="printPage_top">
                        <asp:Label ID="lblSiteUrl" runat="server" Font-Bold="true" Font-Size="30px" Text="www.mybuylist.com" />
                    </div>--%>
                </div>
            </div>
            <div style="height: 165px;">
            </div>
            <div style="clear: both; height: 1px;">
            </div>
            <div id="menu_details" style="clear: both; min-height: 200px;">
                <asp:Repeater ID="rptDays" runat="server" OnItemDataBound="rptDays_ItemDataBound">
                    <ItemTemplate>
                        <div style="margin-top: 28px;">
                            <div style="width: 650px; height: 28px;">
                                <asp:Image ID="imgTableTop" runat="server" ImageUrl="" Width="650px" Height="28px" />
                            </div>
                            <table width="650px" cellspacing="0px">
                                <asp:Repeater ID="rptCourses" runat="server" OnItemDataBound="rptCourses_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td id="mealCell" class="mealDetails_meals dinerMenuMealCell" style="border-color: Black;">
                                                <table cellpadding="0px" cellspacing="0px">
                                                    <tr>
                                                        <td class="mealDetails_mealNames" style="color: Black;">
                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("CourseOrMealTypeName") %>' />
                                                        </td>
                                                        <td>
                                                            <table cellpadding="0px" cellspacing="0px">
                                                                <asp:Repeater ID="rptRecipes" runat="server" OnItemDataBound="rptRecipes_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td id="tdDinersNum" runat="server" style="font-size: 12px; text-align: center;">
                                                                            </td>
                                                                            <td align="center" style="width: 33px; font-size: 12px; padding-right: 1px;">
                                                                                <asp:Label ID="lblServings" runat="server" Text='<%# Eval("Servings") %>' />
                                                                            </td>
                                                                            <td id="tdRecipes">
                                                                                <asp:HyperLink ID="lblRecipeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Recipe.RecipeName") %>'
                                                                                    Target="recipeDetails" />
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
        </ItemTemplate> </asp:Repeater> </div>
        <script type="text/javascript">
            adjustCellAppearance();
        </script>
        </div>
    </div>
    </form>
</body>
</html>
