<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintRecipe.aspx.cs" Inherits="PrintRecipe" %>

<%@ Register TagPrefix="uc1" TagName="Recipe" Src="~/UserControls/ucRecipePrint.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="title" runat="server"></title>
    <meta name="Description" id="PageDescription" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div dir="rtl" id="printPage_main">
        <div id="printPage_top">
            <img src='Images/mybuylist.png' alt="logo" width="185px" />
            <div style="height: 165px;">
            </div>
            <asp:Label ID="lblSiteUrl" runat="server" Font-Bold="true" Font-Size="30px" Text="www.mybuylist.com" />
        </div>
        <div id="printPage_content">
            <asp:Label ID="lblTitle1" runat="server" Text="שם המתכון:" Font-Bold="true" Font-Size="22px"/>
            &nbsp;
            <asp:Label ID="lblTitle2" runat="server" Font-Bold="true" Font-Size="22px" />
            <div style="clear: both; min-height: 250px; padding-top: 20px;">
                <div style="float: right; width: 500px; font-size: 16px;">
                    <div class="publisher_box" style="margin-bottom: 20px;">
                        <asp:Label ID="lblPublishedBy" runat="server" Text='פורסם ע"י'></asp:Label>
                        <asp:HyperLink ID="lnkPublisher" runat="server" NavigateUrl="" ></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblPublishedOn" runat="server" Text='בתאריך'></asp:Label>
                        <asp:Label ID="lblPublishDate" runat="server" ></asp:Label>
                    </div>
                    <div class="recipe_sub_header" style="color: Black; font-size: 16px;" >
                        <div style="float: right;">
                            קטגוריות: &nbsp;
                        </div>
                        <div style="float: right; width: 280px;">
                            <asp:Label ID="lblRecipeCategories" runat="server" />
                        </div>
                        <div style="clear: both; height: 1px;">
                        </div>
                    </div>
                    <div id="recipe_tags" style="margin-top: 6px; font-size: 16px;">
                        <div style="float: right; width: 50px;">
                            תגיות:
                        </div>
                        <div style="float: right; width: 290px;">
                            <asp:Label ID="lblRecipeTags" runat="server" />
                        </div>
                        <div style="clear: both; height: 1px;">
                        </div>
                    </div>                    
                    <div style="margin-top: 20px; font-style: italic; ">
                        <div id="myFavoritesTopTag" runat="server">
                            (קיים במועדפים שלי)
                        </div>
                        <div>
                            <div>
                                <asp:Label ID="lblAllFavorites" runat="server" Font-Bold="true" />
                                <asp:Label ID="Label3" runat="server" Text="גולשים הוסיפו מתכון זה" />
                            </div>
                        </div>
                        <div>
                            <div>
                                <asp:Label ID="lblAllMenus" runat="server" Font-Bold="true" />
                                <asp:Label ID="Label4" runat="server" Text="תפריטים כוללים מתכון זה" />
                            </div>
                        </div>
                    </div>
                    <div style="margin-top: 15px;">
                        <div>
                            <asp:Label ID="Label1" runat="server" Text="מספר מנות:" Font-Bold="true" />
                            <asp:Label ID="lblServNumber" runat="server" />
                        </div>
                        <div>
                            <asp:Label ID="Label2" runat="server" Text="זמן הכנה:" Font-Bold="true" />
                            <asp:Label ID="lblPrepTime" runat="server" />
                        </div>
                        <div>
                            <asp:Label ID="Label5" runat="server" Text="זמן בישול / אפיה:" Font-Bold="true" />
                            <asp:Label ID="lblCookTime" runat="server" />
                        </div>
                    </div>
                    <div id="recipe_description">
                        <div style="margin-bottom: 8px;">
                            <asp:Label ID="lblSubtitle1" runat="server" Text="תיאור קצר:" Font-Size="18px" Font-Bold="true" />                            
                        </div>
                        <asp:Label ID="lblRecipeDescription" runat="server" ></asp:Label>
                    </div>
                    <div id="recipe_ingredients">
                        <div style="clear: both; margin-bottom: 8px; text-align: right;">
                            <asp:Label ID="Label6" runat="server" Text="החומרים הדרושים:" Font-Size="18px" Font-Bold="true" />
                        </div>
                        <asp:TextBox ID="txtData" runat="server" Visible="False"></asp:TextBox>
                        <asp:DataList ID="dlistIngredients" runat="server">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "DISPLAY_NAME")%>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div id="recipe_tools">
                        <div style="clear: both; text-align: right; margin-bottom: 8px;">
                            <asp:Label ID="Label7" runat="server" Text="כלים:" Font-Size="18px" Font-Bold="true" />
                        </div>
                        <asp:Label ID="txtTools" runat="server"></asp:Label>
                    </div>
                    <div id="recipe_difficulty">
                        <div style="clear: both; margin-bottom: 8px; text-align: right;">
                            <asp:Label ID="Label8" runat="server" Text="דרגת קושי:" Font-Size="18px" Font-Bold="true" />
                        </div>
                        <asp:Label ID="txtDifficulty" runat="server" Style="min-height: 5px;"></asp:Label>
                    </div>
                    <div id="recipe_instructions">
                        <div style="clear: both; margin-bottom: 8px; text-align: right;">
                            <asp:Label ID="Label9" runat="server" Text="אופן הכנה:" Font-Size="18px" Font-Bold="true" />
                        </div>
                        <asp:Label ID="txtPreparationMethod" runat="server"></asp:Label>
                    </div>
                    <div id="recipe_remarks" runat="server" class="recipe_remarks">
                        <div style="clear: both; margin-bottom: 8px; text-align: right;">
                            <asp:Label ID="Label10" runat="server" Text="הערות:" Font-Size="18px" Font-Bold="true" />
                        </div>
                        <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                    </div>
                </div>
                <div style="float: left; width: 300px; height: 231px; text-align: center;">
                    <asp:Image ID="imgRecipePicture" runat="server" BorderColor="Black" BorderWidth="1px"
                        BorderStyle="Solid" ImageUrl="~/Images/Img_Default.jpg" />
                </div>
            </div>
            <div style="clear:both; height: 1px;">
            </div>
        </div>
        <%--<table>
        
            <tr align="center" valign="middle">
                <td style="width:500px;"></td>
                <td>
                    <img src='http://www.mybuylist.com/Images/New/Logo.gif' />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <uc1:Recipe ID="ucRecipePrint" runat="server" />
                </td>
            </tr>
        </table>--%>
    </div>
    </form>
</body>
</html>
