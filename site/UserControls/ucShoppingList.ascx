<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucShoppingList.ascx.cs" Inherits="UserControls.UcShoppingList" %>

<script src="UserControls/scripts/ucShoppingList.js"></script>

<style>
    .hide{
        display: none;
    }

    #ShoppingList {
        width: 360px;
    }

        #ShoppingList div.category {
            color: #8dac34;
            font-size: 14pt;
            background-color: silver;
            text-align: center;
            font-weight: bold;
            border-radius: 7px;
        }

        #ShoppingList div.text {
            float: right;
            font-size: 10pt;
            margin-left: 3px;
            margin-top: 2px;
        }

        #ShoppingList div.recipes-list-title {
            background-color: gray;
            font-size: 13pt;
            color: white;
            padding: 10px;
            font-weight: bold;
        }

        #ShoppingList div.recipes-list {
            margin-bottom: 8px;
        }

            #ShoppingList div.recipes-list span.delete {
                margin-top: 10px;
            }

        #ShoppingList div.item {
            margin-top: 4px;
            height: 1%;
            overflow: auto;
            width: 100%;
        }

            #ShoppingList div.item span {
                margin-left: 5px;
            }

            #ShoppingList div.item img.remove {
                float: left;
                position: relative;
                top: -3px;
            }

    .additional {
        /*margin-top: 10px;*/
    }

        .additional input.quantity {
            width: 50px;
        }

        .additional input.name {
            width: 190px;
        }

        .additional select {
            height: 25px;
            width: 100px;
        }

        .additional div.data {
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            margin-top: 5px;
        }

        .additional div.row {
            height: 50px;
        }

        .additional img{
            width: 103px;
        }

    #tabs {
        border: none;
    }

        #tabs ul li {
            float: right;
        }

    .fr {
        float: right;
    }
</style>

<script>
    var hfSelectedTabClientID = '<%=hfSelectedTab.ClientID%>';
</script>

<asp:HiddenField ID="hfSelectedTab" runat="server" />

<asp:Panel ID="PanelShoppingList" runat="server">
    <div id="ShoppingList" class="hide">
        <div>
            <asp:Image ID="Image1" runat="server" ImageUrl="../Images/Header_lastBuyList.png" />
        </div>
        <div id="tabs">
            <ul>
                <li id="tabs1"><a href="#tabs-1">רשימת קניות</a></li>
                <li id="tabs2"><a href="#tabs-2">מתכונים (<span></span>)</a></li>
                <li id="tabs3"><a href="#tabs-3">תפריטים (<%=NumOfSelectedMenus %>)</a></li>
            </ul>
            <div id="tabs-1">
                <div class="categoriesList"></div>
                <div class="additional">
                    <div class="category">
                        <asp:Literal ID="ShoppingListCategory" runat="server" Text="מוצרים נוספים"></asp:Literal>
                    </div>
                    <div class="data">
                        <div class="row">
                            <div>כמות</div>
                            <input id="quantity" type="text" class="quantity" />
                        </div>
                        <div class="row">
                            <div>יחידת מידה</div>
                            <select id="measureUnits"></select>
                        </div>
                        <div class="row">
                            <div>שם</div>
                            <input id="foodName" type="text" class="name" />
                        </div>
                        <img id="addAdditional" src="Images/btn_AddProduct_up.png" />
                    </div>
                </div>
            </div>
            <div id="tabs-2">
                <div id="recipes"></div>
                <div>
                    <img id="btnAddToExistingMenu" src="Images/btn_AddToExistMenu_Up.png"
                        onmouseover='this.src="Images/btn_AddToExistMenu_Over.png";' onmouseout='this.src="Images/btn_AddToExistMenu_Up.png";'
                        onmousedown='this.src="Images/btn_AddToExistMenu_Down.png";' onmouseup='this.src="Images/btn_AddToExistMenu_Up.png";'
                        style="margin-right: 5px;" />
                    <asp:ImageButton ID="btnAddToNewMenu" runat="server" OnClick="btnAddToNewMenu_Click"
                        ImageUrl="~/Images/btn_AddToNewMenu_Up.png" onmouseover='this.src="Images/btn_AddToNewMenu_Over.png";'
                        onmouseout='this.src="Images/btn_AddToNewMenu_Up.png";' onmousedown='this.src="Images/btn_AddToNewMenu_Down.png";'
                        onmouseup='this.src="Images/btn_AddToNewMenu_Up.png";' Style="margin-right: 5px;" />
                </div>
            </div>
            <div id="tabs-3"></div>
        </div>
    </div>
</asp:Panel>

<div id="menusListPopUp">
    <asp:UpdatePanel ID="upTreeViewCategories" runat="server" UpdateMode="Conditional"
        ChildrenAsTriggers="false">
        <ContentTemplate>
            <table style="width: 90%">
                <asp:Repeater ID="rptUserMenus" runat="server" OnItemDataBound="rptUserMenus_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td align="right" class="userMenusRepeaterCell">&nbsp;
                                        <asp:HyperLink ID="lnkMenu" runat="server" ForeColor="#656565" Text='<%#Eval("MenuName") %>'></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
