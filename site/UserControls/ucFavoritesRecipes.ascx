<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFavoritesRecipes.ascx.cs"
    Inherits="ucFavoritesRecipes" %>
<asp:Panel ID="pnlList" runat="server" Width="470px" Height="405px" ScrollBars="Auto">
    <table style="width: 80%">
        <asp:Repeater ID="rptRecipesList" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="width: 200px">
                        <asp:ImageButton ID="btnRemoveFavoriteRecipe" runat="server" ImageUrl="~/Images/x_normal.gif"
                            recipeId='<%#DataBinder.Eval(Container.DataItem ,"RecipeId")%>' AlternateText='<%$ Resources:MyGlobalResources, Remove %>'
                            OnClientClick='<%$ Resources:ValidationResources, ConfirmRemoveFavoriteRecipe %>'
                            OnClick="btnRemoveFavoriteRecipe_Click" />&nbsp;&nbsp;
                        <asp:LinkButton ID="btnViewRecipe" runat="server" Width="400px" ForeColor="Black"
                            Font-Underline="false" recipeId='<%#DataBinder.Eval(Container.DataItem ,"RecipeId")%>'
                            OnMouseOver="EditItem_MouseOver(this);" OnMouseOut="EditItem_MouseOut(this);"
                            Text='<%#DataBinder.Eval(Container.DataItem ,"RecipeName")%>' OnClick="btnViewRecipe_Click"></asp:LinkButton>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
