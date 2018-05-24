<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
CodeFile="MeasurementUnitsConvertList.aspx.cs" Inherits="PageMeasurementUnitsConvertList"  Title="<%$ Resources:MyGlobalResources,  MeasurementUnitsConverts%>"%>

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
                                    <asp:Label ID="lblIngredientsList" runat="server" Font-Bold="true" 
                                        Text="<%$ Resources:MyGlobalResources,  MeasurementUnitsConverts%>"></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnBack" runat="server" PostBackUrl="~/Admin/Admin.aspx" 
                                        Text="<%$ Resources:MyGlobalResources, Back %>"></asp:LinkButton>
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
                                    <th style="width: 85%; text-align: right">
                                        &nbsp;&nbsp;<asp:Literal ID="thMeasurementUnitsConvert" runat="server" 
                                            Text='<%$ Resources:MyGlobalResources, MeasurementUnitsConvertText%>' />
                                    </th>
                                    <th style="width: 15%">
                                    </th>
                                </tr>
                            </table>
                            <ajaxToolkit:ReorderList ID="rolMeasurementUnitsConverts" runat="server" PostBackOnReorder="false"
                                CssClass="reorderList" DragHandleAlignment="Left"
                                SortOrderField="SortOrder"  OnItemDataBound="rolMeasurementUnitsConverts_ItemDataBound"
                                Width="95%">
                                <ItemTemplate>
                                    <table width="95%">
                                        <tr>
                                            <td style="width: 300px">
                                                &nbsp;<asp:Label ID="lbMeasurementUnitsConvertText" runat="server" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnUpdate" runat="server" 
                                                    Text="<%$ Resources:MyGlobalResources, Update %>" />
                                                &nbsp;&nbsp;
                                                <%--<asp:LinkButton ID="btnDelete" runat="server"
                                                Text="<%$ Resources:MyGlobalResources, Delete %>" OnClientClick='<%$ Resources:ValidationResources, ConfirmIngredientDelete %>'
                                                OnClick="btnDelete_Click"></asp:LinkButton>--%>
                                            </td>
                                        </tr>
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
                            PostBackUrl="~/Admin/MeasurementUnitsConvert.aspx" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




