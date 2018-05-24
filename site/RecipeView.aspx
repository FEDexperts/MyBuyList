<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecipeView.aspx.cs" Inherits="PageRecipeView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript"> 
 function CloseWindow()
 {
    window.parent.HideRecipeDetails();
 } 
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
</head>

<body dir="rtl">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Image ID="imgClose" runat="server" ImageUrl="~/Images/close1.bmp" AlternateText="<%$ Resources:MyGlobalResources, CloseWindow %>" Onclick="CloseWindow();" />&nbsp;&nbsp;<asp:Image
                ID="imgPrint" runat="server" ImageUrl="~/Images/print1.bmp" AlternateText="<%$ Resources:MyGlobalResources, PrintRecipe %>" onclick="window.print();" />
            <table id="tblRecipeDetail" runat="server" style="width: 100%">
                <tr>
                    <td valign="top">
                        <asp:Label ID="lblRecipeName" runat="server" ForeColor="Crimson" Font-Bold="true"
                            Height="25px" Font-Size="13pt" />
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:DataList ID="dlistIngredients" runat="server" GridLines="Both" HeaderStyle-Font-Bold="true"
                            HeaderStyle-BackColor="Crimson" HeaderStyle-ForeColor="White" Width="405px">
                            <HeaderTemplate>
                                &nbsp;&nbsp;<asp:Literal ID="thIngredients" runat="server" Text='<%$ Resources:MyGlobalResources, Ingredients%>' /></HeaderTemplate>
                            <ItemTemplate>
                                &nbsp;&nbsp;<%#DataBinder.Eval(Container.DataItem, "DisplayIngredient")%></ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Panel ID="pnlPreparationMethod" runat="server" Width="405px" BackColor="Crimson">
                            &nbsp;&nbsp;<asp:Label ID="lblPreparationMethod" runat="server" ForeColor="White"
                                Font-Bold="true" Height="18px" Text="<%$ Resources:MyGlobalResources, PreparationMethod %>"></asp:Label>
                        </asp:Panel>
                        <asp:TextBox ID="txtPreparationMethod" runat="server" TextMode="MultiLine" Width="400px"
                            Height="220px" BorderWidth="1px" BorderColor="LightGray" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>    
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="מספר מנות : " Font-Bold ="true"></asp:Label>
                        <asp:Label ID="lblDiners" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>    
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="כותב המתכון : " Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblUserEntered" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
