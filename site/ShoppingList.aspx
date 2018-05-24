<%@ Page Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="ShoppingList.aspx.cs" Inherits="PageShoppingList" Title="<%$ Resources:MyGlobalResources, ShoppingListPageTitle %>" %>

<%@ MasterType VirtualPath="~/MasterPages/MBL.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="cphMain" runat="Server">
    <div style="height: 50px; clear: both;">
    </div>
    <div id="shoppingList">
        <asp:UpdatePanel ID="upResultLabelTop" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div style="clear: both; height: 30px;">
                    <div class="shoppingList_links">
                        <asp:HyperLink ID="btnPrintShoppingList_top" runat="server" Target="printShoppingList"
                            Text="הדפס" />
                        <asp:Label ID="Label1" runat="server" ForeColor="Black">&nbsp;|&nbsp;</asp:Label>
                        <asp:LinkButton ID="btnSendMail_top" runat="server" Text='שלח לדוא"ל' OnClick="btnSendMail_Click" />
                    </div>
                </div>
                <div style="min-height: 2px; font-size: larger; font-weight: bold; text-align: center;">
                    <asp:Label ID="lblResultTop" runat="server" Visible="false"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="shoppingListTitle">
            <asp:Image ID="imgShoppingListTitle" runat="server" ImageUrl="~/Images/Header_BuyList.png" />
        </div>
        <div id="shoppingListRecipes">
            <asp:Label ID="lblMenuRecipes" runat="server" Text="הרשימה מכילה מצרכים למתכונים:"
                ForeColor="#8AA733" Font-Bold="true" Font-Size="14px"></asp:Label>
            <table style="width: 95%; margin-top: 3px;">
                <tr>
                    <td valign="top">
                        <asp:Repeater ID="rptMenuRecipes" runat="server">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="color: #8AA733; font-size: smaller; height: 25px; "">
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
                        <div id="departmentName">
                            <%#DataBinder.Eval(Container.DataItem ,"ShopDepartmentName")%>:
                        </div>
                        <div>
                            <table style="width: 100%">
                                <asp:Repeater ID="rptShoppingFoods" runat="server">
                                    <ItemTemplate>
                                        <tr>
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
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            Visible="false">
            <ContentTemplate>
                <div id="additionalItems">
                    <div style="color: #EF1E3D; font-weight: bold;">
                        מצרכים נוספים:
                    </div>
                    <div style="margin-top: 10px;">
                        <asp:Repeater ID="rptAdditionalItems" runat="server">
                            <ItemTemplate>
                                <div style="padding-top: 10px;">
                                    <asp:ImageButton ID="btnDeleteAdditionalItem" runat="server" ImageUrl="~/Images/x_normal.gif"
                                        itemId='<%#DataBinder.Eval(Container.DataItem ,"ItemId")%>' AlternateText='<%$ Resources:MyGlobalResources, Delete %>'
                                        OnClick="btnDeleteAdditionalItem_Click" />
                                    &nbsp;&nbsp;<%#DataBinder.Eval(Container.DataItem ,"ItemName")%>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div style="margin-top: 15px;">
                        <asp:TextBox ID="txtAddShoppingListItem" runat="server" Width="420px" Height="14px"
                            BackColor="#FFD6DC" BorderColor="#FF8B9E" BorderWidth="1px" BorderStyle="Solid"
                            ForeColor="#656565" Font-Size="12px" Style="vertical-align: top; margin-top: 7px;" />
                        <asp:LinkButton ID="btnAddShoppingListItem" runat="server" OnClick="btnAddShoppingListItem_Click">
                            <asp:Image ID="imgAddBtn" runat="server" ImageUrl="~/Images/btn_AddProductBuyList_up.png"
                                onmouseover='this.src="Images/btn_AddProductBuyList_over.png";' onmouseout='this.src="Images/btn_AddProductBuyList_up.png";'
                                onmousedown='this.src="Images/btn_AddProductBuyList_down.png";' onmouseup='this.src="Images/btn_AddProductBuyList_up.png";' />
                        </asp:LinkButton>
                        <ajaxToolkit:AutoCompleteExtender runat="server" ID="aceAddShoppingListItem" TargetControlID="txtAddShoppingListItem"
                            ServiceMethod="GetCompletionGeneralItemsList" ServicePath="WebServices/AutoComplete.asmx"
                            MinimumPrefixLength="2" CompletionInterval="200" EnableCaching="true" CompletionSetCount="12" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upResultLabelBottom" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <div style="clear: both; min-height: 30px; margin-top: 20px;">
                    <div class="shoppingList_links">
                        <asp:HyperLink ID="btnPrintShoppingList_bottom" runat="server" Target="printShoppingList"
                            Text="הדפס" />
                        <asp:Label ID="lblSeparator1" runat="server" ForeColor="Black">&nbsp;|&nbsp;</asp:Label>
                        <asp:LinkButton ID="btnSendMail_bottom" runat="server" Text='שלח לדוא"ל' OnClick="btnSendMail_Click" />
                        <%--<asp:LinkButton ID="btnMenuMeals" runat="server" ForeColor="Black" Font-Bold="true"
                            Font-Underline="false" Text='<%$ Resources:MyGlobalResources, PlanningMenu %>'
                            OnClick="btnMenuMeals_Click" />--%>
                    </div>
                </div>
                <div style="min-height: 20px; font-size: larger; font-weight: bold; text-align: center;">
                    <asp:Label ID="lblResultBottom" runat="server" Visible="false"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
