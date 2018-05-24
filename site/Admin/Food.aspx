<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    CodeFile="Food.aspx.cs" Inherits="PageFood" Title="<%$ Resources:MyGlobalResources, FoodsEdition%>" %>

<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">

    <script type="text/javascript">
        function cbDeletePicture_Changed(e) {
            if (e.checked) {
                $("span[id*='lblSelectPicture']")[0].disabled = true;
                $("input[id*='pictureFile']")[0].disabled = true;
                $("img[id*='imgContainer']").css({ 'display': 'none' });

            }
            else {
                $("span[id*='lblSelectPicture']")[0].disabled = false;
                $("input[id*='pictureFile']")[0].disabled = false;
                $("img[id*='imgContainer']").css({ 'display': 'inline' });
            }
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
                __doPostBack('ctl00$RightContentPlaceHolder$btnHidden', '');
            }
        }
    </script>

    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width: 105px;">
                            <asp:Label ID="lblFoodName" runat="server" Font-Bold="true" Text='<%$ Resources:MyGlobalResources, FoodName%>'
                                Width="80px"></asp:Label>
                        </td>
                        <td style="width: 105px;">
                            <asp:TextBox ID="txtFoodName" runat="server" MaxLength="200" Width="225px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqValidFoodName" runat="server" ValidationGroup="general"
                                Display="Dynamic" ControlToValidate="txtFoodName" ErrorMessage='<%$ Resources:ValidationResources, FoodNameIsRequired%>'></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="custValidFoodName" runat="server" ValidationGroup="general"
                                ErrorMessage='<%$ Resources:ValidationResources, FoodNameDuplicate %>' OnServerValidate="custValidFoodName_ServerValidate"
                                Display="Dynamic" ValidateEmptyText="false"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 105px;">
                            <asp:Label ID="lblCategory" runat="server" Font-Bold="True" Text='<%$ Resources:MyGlobalResources, FoodCategory %>'
                                Width="16px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCategory" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 105px;">
                            <asp:Label ID="lblCalculateUnit" runat="server" Font-Bold="True" Text='<%$ Resources:MyGlobalResources, MeasurementUnit %>'
                                Width="59px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCalculateUnit" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 105px;">
                            <asp:Label ID="lblRemarks" runat="server" Font-Bold="True" Text='<%$ Resources:MyGlobalResources, Remarks %>'
                                Width="34px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="225px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 105px;">
                            <asp:Label ID="lblSelectPicture" runat="server" Font-Bold="True" Text=" תמונה (עד 2MB) :"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="pictureFile" runat="server" Width="314px" OnChange="PictureFile_Changed(this)" />
                            <asp:LinkButton ID="btnHidden" runat="server" OnClick="btnHidden_Click" Style="display: none"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="cbDeletePicture" runat="server" Font-Bold="True" Text="מחק תמונה"
                                OnClick="cbDeletePicture_Changed(this);" />
                            <br />
                            <asp:CheckBox ID="cbPrint" runat="server" Font-Bold="True" Text="הדפס תמונה" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="middle" style="height: 60px;">
            <td colspan="2">
                <asp:Button ID="btnOK" runat="server" Text='<%$ Resources:MyGlobalResources, OK%>'
                    OnClick="btnOK_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text='<%$ Resources:MyGlobalResources, Cancel%>'
                    CausesValidation="false" PostBackUrl="~/Admin/FoodsList.aspx" />&nbsp;&nbsp;
            </td>
        </tr>
        <tr valign="middle">
            <td colspan="2">
                <asp:Label ID="lblRecipesCaption" runat="server" Text="מתכונים עם המצרך" Font-Bold="true"
                    Visible="false" />
            </td>
        </tr>
        <tr valign="middle">
            <td colspan="2">
                <asp:Panel ID="pnlRecipes" runat="server" ScrollBars="Vertical" Width="466px" Height="130px"
                    BorderColor="LightGray" BorderWidth="1" Visible="false">
                    <table cellpadding="0" cellspacing="0" style="width: 90%">
                        <asp:Repeater ID="rptRecipesLinks" runat="server" OnItemDataBound="rptRecipesLinks_ItemDataBound">
                            <ItemTemplate>
                                <tr style="height: 25px" onmouseover="this.style.backgroundColor='#dddddd';" onmouseout="this.style.backgroundColor='Transparent';">
                                    <td>
                                        &nbsp;&nbsp;<%#DataBinder.Eval(Container.DataItem ,"RecipeName")%>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="lnkShowRecipe" runat="server" Target="_blank" Text="הצג" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="position: fixed; top: 350px; left: 500px;">
                <asp:Image ID="imgProgress" runat="server" ImageUrl="~/Images/ajax-loader.gif" AlternateText="" />
            </div>
            </center>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
<asp:Content ID="LeftContent" ContentPlaceHolderID="LeftContentPlaceHolder" runat="Server">
    <table>
        <tr valign="top" align="center">
            <td>
                <asp:Image ID="imgContainer" runat="server" BorderColor="Gray" BorderWidth="1px"
                    Style="max-width: 200px; max-height: 200px; position: relative; top: -160px;"
                    Visible="false" />
            </td>
        </tr>
    </table>
</asp:Content>
