<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintRecipes.aspx.cs" Inherits="PrintRecipes" %>

<%@ Register TagPrefix="uc1" TagName="Recipe" Src="~/UserControls/ucRecipePrint.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div dir="rtl">
        <table>
            <tr >
                <td align="center" style="text-align:center; width:400px;">
                    
                    <asp:Label ID="lblMenuName" runat="server" Font-Bold="true" ForeColor="#C51015" Font-Names="Arial"></asp:Label>
                </td>
               
                <td>
                    <img src='http://www.mybuylist.com/Images/New/Logo.gif' />
                </td>
            </tr>
            
            <tr>
                <td colspan="2">
                    <asp:Repeater ID="rptRecipes" runat="server" OnItemDataBound="rptRecipes_DataBound">
                        <ItemTemplate>
                            <uc1:Recipe ID="ucRecipePrint" runat="server" />
                            <br />
                            <hr />
                            <br />
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
