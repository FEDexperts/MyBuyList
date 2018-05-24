<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProperDevMasterPage.master" AutoEventWireup="true"
    EnableEventValidation="true" Theme="Standard" CodeFile="FoodsList.aspx.cs" Inherits="PageFoodsList"
    Title="<%$ Resources:MyGlobalResources, Foods %>" %>

<%@ MasterType VirtualPath="~/MasterPages/ProperDevMasterPage.master" %>
<asp:Content ID="RightContent" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td style="width: 435px">
                                    <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Text="<%$ Resources:MyGlobalResources, Foods %>"></asp:Label>&nbsp;
                                    <asp:Button ID="btnAdd" runat="server" PostBackUrl="~/Admin/Food.aspx" Text="<%$ Resources:MyGlobalResources, Add %>" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnBack" runat="server" PostBackUrl="~/Admin/Admin.aspx" Text="<%$ Resources:MyGlobalResources, Back %>"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <ajaxToolkit:TabContainer ID="Tabs" runat="server" ActiveTabIndex="0" AutoPostBack="true"
                            OnActiveTabChanged="Tabs_ActiveTabChanged" Width="468px">
                            <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="אב">
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="גד">
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="הו">
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="זח">
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="טי">
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel6" runat="server" HeaderText="כל">
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel7" runat="server" HeaderText="מנ">
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel8" runat="server" HeaderText="סע">
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel9" runat="server" HeaderText="פצ">
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel10" runat="server" HeaderText="קר">
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel11" runat="server" HeaderText="שת">
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel12" runat="server" HeaderText="אחר">
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel13" runat="server" HeaderText="חדשים">
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                        <asp:Panel ID="pnlHeader" runat="server" Width="466px" Height="26px" BorderColor="Gray"
                            BackColor="LightGray" BorderWidth="1" Style="position: relative; top: -18px;
                            border-bottom-width: 0px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="thFoodName" runat="server" Font-Bold="true" Height="15px"
                                Text="<%$ Resources:MyGlobalResources, FoodName%>" />
                        </asp:Panel>
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Vertical" Width="466px" Height="322px"
                            BorderColor="Gray" BorderWidth="1" Style="position: relative; top: -19px;">
                            <table cellpadding="0" cellspacing="0" style="width: 90%">
                                <asp:Repeater ID="rptFoods" runat="server" OnItemDataBound="rptFoods_ItemDataBound">
                                    <ItemTemplate>
                                        <tr style="height: 25px">
                                            <td style="width:320px">
                                                &nbsp;&nbsp;<%#DataBinder.Eval(Container.DataItem ,"FoodName")%>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnUpdate" runat="server" Text="<%$ Resources:MyGlobalResources, Update %>"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnDelete" runat="server" Text="<%$ Resources:MyGlobalResources, Delete %>"
                                                    OnClick="btnDelete_Click" FoodId='<%#DataBinder.Eval(Container.DataItem ,"FoodId")%>'></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
