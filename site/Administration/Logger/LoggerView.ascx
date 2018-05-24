<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoggerView.ascx.cs" Inherits="Administration_Logger_LoggerView" %>
<style type="text/css">
    .style1
    {
        width: 268435456px;
    }
</style>
<div style="background-color: White">
    <table border="1">
        <tr>
            <td>
                Connection:
            </td>
            <td colspan="4">
                <asp:DropDownList ID="ddlConnections" runat="server" DataTextField="ConnectionString"
                    DataValueField="Name" />
                <br />
                Sql server custom connection string:<asp:TextBox ID="txtCustomConnection" runat="server"
                    Width="400px" />
                <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" />
            </td>
        </tr>
        <tr>
            <td>
                Log table:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtLogTableName" Text="Log" Width="50px" />
            </td>
            <td>
                Page size:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPageSize" Text="20" Width="50px" />
                <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="txtPageSize"
                    FilterType="Numbers" />
            </td>
            <td width="80%">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updColors">
                    <ContentTemplate>
                        <asp:Label runat="server" Text="Message:" /><asp:TextBox ID="txtMessage" runat="server" /><asp:Label
                            runat="server" Text="Color:" /><asp:TextBox runat="server" ID="txtColor" />
                        <asp:Button runat="server" ID="btnAddColor" OnClick="btnAddColor_Click" Text="Add Color" />
                        <asp:GridView ID="gridColors" runat="server" ShowHeader="False" OnRowDataBound="gridColors_RowDataBound"
                            OnRowCommand="gridColors_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Key" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRemove" runat="server" CommandName="RemoveColor" CommandArgument='<%# Eval("Key") %>'
                                            Text="remove" /></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <proper:ProperDialog ID="ProperDialog2" runat="server" OKButtonID="OK">
                <asp:Label runat="server" Text="Already in use" ID="lblInUse" />
                        <p style="text-align: center;">
                <asp:Button ID="OK" runat="server" Text="OK" /></p>
                        </proper:ProperDialog></ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                Where clause
            </td>
            <td colspan="4">
                <asp:TextBox runat="server" ID="txtFilter" Width="99%" TextMode="MultiLine" Rows="3" />
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td class="style1">
                <div style="max-height: 500px; overflow: auto">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            please wait...</ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGo" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="grid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                OnPageIndexChanging="grid_PageIndexChanging" PageSize="30" OnRowDataBound="grid_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" HeaderStyle-Width="50px" />
                                    <asp:BoundField DataField="Level" HeaderText="Level" SortExpression="Level" HeaderStyle-Width="50px" />
                                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:dd/MM/yyyy HH:mm:ss.fff}"
                                        HeaderStyle-Width="150px" />
                                    <asp:TemplateField HeaderText="Message" SortExpression="Message" HeaderStyle-Width="600px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMessage" runat="server" Text='<%# Eval("Message") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Exception" HeaderText="Exception" SortExpression="Exception"
                                        HeaderStyle-Width="500px" />
                                </Columns>
                            </asp:GridView>
                            <asp:Label runat="server" ID="lblError" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</div>
