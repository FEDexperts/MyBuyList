<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemoryUsage.ascx.cs" Inherits="Administration_Controls_MemoryUsage" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Timer1" />
    </Triggers>
    <ContentTemplate>
        <table border="1" cellpadding="5" cellspacing="0">
            <tr>
                <td>
                    Machine Name</td>
                <td>
                    <asp:Label ID="lblMachineName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Application Pool (IIS 6 or above)
                </td>
                <td>
                    <asp:Label ID="lblAppPool" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Process Name</td>
                <td>
                    <asp:Label ID="lblProcessName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Process Start Time</td>
                <td>
                    <asp:Label ID="lblProcessStartTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Memory (KB)
                </td>
                <td>
                    <asp:Label ID="lblMemory" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Virtual Memory (KB)
                </td>
                <td>
                    <asp:Label ID="lblVirtualMemory" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Peak Memory (KB)</td>
                <td>
                    <asp:Label ID="lblPeakMemory" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:Timer ID="Timer1" runat="server" Interval="1000" />
