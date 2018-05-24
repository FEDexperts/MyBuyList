<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPrintMealMenu.ascx.cs"
    Inherits="UserControls_ucPrintMenu" %>
<asp:Repeater ID="rpCourses" runat="server" OnItemDataBound="Course_DataBound">
    <ItemTemplate>
        <table style="width: 100%; border-color: Gray;" border="1" cellspacing="0">
            <tr>
                <td style="width: 150px; border-width: 0px">
                    &nbsp;&nbsp;
                    <asp:Label ID="lblCourseTypeName" runat="server" Font-Bold="true" Text='<%#DataBinder.Eval(Container.DataItem ,"CourseTypeName")%>'></asp:Label>
                    -
                    <asp:Label ID="lblDiners" runat="server"></asp:Label>
                    סועדים
                </td>
                <td style="padding-right: 10px; border-width: 0px; border-right: solid 1px gray;">
                    <asp:Repeater ID="rpMealRecipes" runat="server">
                        <ItemTemplate>
                            <table border="0" style="width: 100%;" cellspacing="0">
                                <tr>
                                    <td><asp:Label ID="lblServs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem ,"Servings")%>'></asp:Label>
                                        מנות -
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblbRecipeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem ,"Recipe.RecipeName")%>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:Repeater>
