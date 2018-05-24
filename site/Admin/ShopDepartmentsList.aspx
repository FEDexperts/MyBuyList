<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    EnableEventValidation="true" Theme="Standard" CodeFile="ShopDepartmentsList.aspx.cs"
    Inherits="PageShopDepartmentsList" Title="<%$ Resources:MyGlobalResources, FoodProductsDeparments %>" %>

<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td style="width: 440px">
                                    <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Text="<%$ Resources:MyGlobalResources, FoodProductsDeparments %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnBack" runat="server" Text="<%$ Resources:MyGlobalResources, Back %>"
                                        PostBackUrl="~/Admin/Admin.aspx"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Vertical" BorderColor="Gray" BorderWidth="1px"
                            Height="365px" Width="470px">
                            <table width="95%" cellspacing="0">
                                <tr style="background-color: #dedede; height: 25px">
                                    <th style="width: 75%; text-align: right">
                                        &nbsp;&nbsp;<asp:Literal ID="thDepartmentName" runat="server" Text='<%$ Resources:MyGlobalResources, DepartmentName%>' />
                                    </th>
                                    <th style="width: 20%">
                                    </th>
                                </tr>
                            </table>
                            <ajaxToolkit:ReorderList ID="rolShopDepartments" runat="server" PostBackOnReorder="false"
                                CssClass="reorderList" DragHandleAlignment="Left" DataKeyField="ShopDepartmentID"
                                SortOrderField="SortOrder" OnItemReorder="rolShopDepartments_ItemReorder" OnItemDataBound="rolShopDepartments_ItemDataBound"
                                Width="95%">
                                <ItemTemplate>
                                    <table width="95%">
                                        <td style="width: 200px">
                                            &nbsp;<asp:Label ID="lblDepartmentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ShopDepartmentName")%>' />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnUpdate" runat="server" Text="<%$ Resources:MyGlobalResources, Update %>" />&nbsp;&nbsp;
                                            <asp:LinkButton ID="btnDelete" runat="server" depId='<%#DataBinder.Eval(Container.DataItem ,"ShopDepartmentId")%>'
                                                Text="<%$ Resources:MyGlobalResources, Delete %>" OnClientClick='<%$ Resources:ValidationResources, ConfirmDepartmentDelete %>'
                                                OnClick="btnDelete_Click"></asp:LinkButton>
                                        </td>
                                    </table>
                                </ItemTemplate>
                                <ReorderTemplate>
                                    <asp:Panel ID="Panel2" runat="server" CssClass="reorderCue" />
                                </ReorderTemplate>
                                <DragHandleTemplate>
                                    <div class="dragHandle">
                                    </div>
                                </DragHandleTemplate>
                            </ajaxToolkit:ReorderList>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:MyGlobalResources, Add %>"
                            PostBackUrl="~/Admin/ShopDepartment.aspx" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
