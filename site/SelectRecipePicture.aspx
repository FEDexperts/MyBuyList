<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectRecipePicture.aspx.cs"
    Inherits="SelectRecipePicture" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body dir="rtl">

    <script type="text/javascript">
        function CloseWindow() {
            window.parent.HideSelectPicture();
        }

        function PictureFile_Changed(e) {
            var fileSize;
            if (document.layers) {
                var f = new java.io.File(e.value);
                netscape.security.PrivilegeManager.enablePrivilege('UniversalFileRead');
                fileSize = f.length()
            }
            else if (navigator.appName.indexOf('Microsoft') != -1) {
                try {
                    var fs = new ActiveXObject('Scripting.FileSystemObject');
                    var f = fs.GetFile(e.value);
                    fileSize = f.size;
                }
                catch (err) { }
            }
            if (fileSize > 2097152) {
                e.value = null;
                alert('2MB לא ניתן לבחור תמונה בגודל מעל');
                return;
            }
            else {
                __doPostBack('btnHidden', '');
            }
        }
    </script>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Panel ID="pnlImage" runat="server" BorderWidth="1px" Height="200px" Width="200px"
        BorderColor="Gray" Style="padding: 5px 5px 5px 5px;">
        <asp:Image ID="imgContainer" runat="server" Style="max-width: 200px; max-height: 200px"
            Visible="false" />
    </asp:Panel>
    <div>
        <asp:Label ID="lblSelectPicture" runat="server" Font-Bold="True" Text="בחר תמונה (עד 2MB) :"></asp:Label>
        <asp:FileUpload ID="pictureFile" runat="server" Width="314px" OnChange="PictureFile_Changed(this)" />
        <asp:LinkButton ID="btnHidden" runat="server" OnClick="btnHidden_Click" Style="display: none"></asp:LinkButton>
        <br />
        <br />
    </div>
    <div>
        <hr style="width: 100%" />
        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:MyGlobalResources, OK %>"
            OnClick="btnSave_Click" OnClientClick="CloseWindow();" />
        &nbsp;
        <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:MyGlobalResources, Clear %>"
            OnClick="btnClear_Click" />
        &nbsp;
        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="<%$ Resources:MyGlobalResources, Cancel %>"
            OnClientClick="CloseWindow();" />
    </div>
    <div style="height: 7px">
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="position: fixed; top: 150px; left: 200px;">
                <asp:Image ID="imgProgress" runat="server" ImageUrl="~/Images/ajax-loader.gif" AlternateText="" />
            </div>
            </center>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
