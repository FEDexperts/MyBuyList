<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true" EnableEventValidation="true" Theme="Standard"
CodeFile="MeausurementUnitsList.aspx.cs" Inherits="PageMeausurementUnitsList"  Title="<%$ Resources:MyGlobalResources, MeasurementUnits %>"%>

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
                                    <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Text="<%$ Resources:MyGlobalResources, MeasurementUnits %>"></asp:Label>
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
                                        &nbsp;&nbsp;<asp:Literal ID="thUnitName" runat="server" Text='<%$ Resources:MyGlobalResources, UnitType%>' />
                                    </th>
                                    <th style="width: 20%">
                                    </th>
                                </tr>
                            </table>
                            <ajaxToolkit:ReorderList ID="rolMeasurementUnits" runat="server" PostBackOnReorder="false"
                                CssClass="reorderList" DragHandleAlignment="Left" DataKeyField="UnitID"
                                SortOrderField="SortOrder" OnItemReorder="rolMeasurementUnits_ItemReorder" OnItemDataBound="rolMeasurementUnits_ItemDataBound"
                                Width="95%">
                                <ItemTemplate>
                                    <table width="95%">
                                        <td style="width: 200px">
                                            &nbsp;<asp:Label ID="lblUnitName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UnitName")%>' />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnUpdate" runat="server" Text="<%$ Resources:MyGlobalResources, Update %>" />&nbsp;&nbsp;
                                            <asp:LinkButton ID="btnDelete" runat="server" unitId='<%#DataBinder.Eval(Container.DataItem ,"UnitId")%>'
                                                Text="<%$ Resources:MyGlobalResources, Delete %>" OnClientClick='<%$ Resources:ValidationResources, ConfirmMeasurementUnitDelete %>'
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
                    <td valign="middle">
                        <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:MyGlobalResources, Add %>"
                            PostBackUrl="~/Admin/MeasurementUnit.aspx" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





