<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSummeryList.ascx.cs"
    Inherits="UserControls.UcSummeryList" %>
<div style="color: #ee1e3e; font-size: 22pt; font-weight: bold;
    font-family: Arial">
    <asp:Label ID="LabelTile" runat="server" Text="<%$ Resources:MyGlobalResources, SummeryListTitle %>"></asp:Label>
</div>
<asp:Panel ID="pnlIngredients" runat="server" BorderWidth="1px" BorderColor="#656565"
    BorderStyle="Solid" Width="200px" Height="100%" Font-Bold="true" Font-Size="12px"
    ScrollBars="Vertical" Style="margin-bottom: 32px;" CssClass="ingredients ingredientsEditable">
    <div style="margin-left: 5px; margin-right: 5px;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:DataList ID="dlistIngredients" runat="server" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="#dedede"
                    Width="200px" OnItemDataBound="dlistIngredients_ItemDataBound">
                    <ItemTemplate>
                        <%--<asp:PlaceHolder runat="server" ID="buttons">
                            <asp:LinkButton ID="btnUpdateIngredient" runat="server" Text="<%$ Resources:MyGlobalResources, Update %>"
                                class="UpdateLink" OnClick="btnUpdateIngredient_Click" OnClientClick="SuspendLeaveConfirmation()"></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="btnRemoveIngredient" runat="server" Text="<%$ Resources:MyGlobalResources, Remove %>"
                                class="DeleteLink" OnClick="btnRemoveIngredient_Click" OnClientClick="setDirty(); SuspendLeaveConfirmation()"></asp:LinkButton>&nbsp;</asp:PlaceHolder>--%>
                        <asp:Literal ID="ltrDisplayIngredient" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DisplayIngredient")%>' />
                    </ItemTemplate>
                </asp:DataList>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Panel>
