<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CauseException.aspx.cs" Inherits="Testers_CauseException" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button runat="server" OnClick="btnCrash_Click" ID="btnCrash" Text="Crash Me" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
