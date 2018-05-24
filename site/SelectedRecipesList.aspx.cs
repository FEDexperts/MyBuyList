using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using ProperControls.Pages;
using ProperControls.General;

using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;


public partial class SelectedRecipesList : BasePage
{
    private static int multiply = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int currentUserId = ((BasePage)Page).UserId;
            if (currentUserId != -1)
            {
                MyBuyList.Shared.Entities.Menu[] userMenus = BusinessFacade.Instance.GetMenusList(currentUserId);

                MyBuyList.Shared.Entities.Menu[] userMenus1 = userMenus.OrderBy(um => um.MenuName).ToArray();

                //if (userMenus1.Length > 3)
                //{
                //    int[] recentMenuIds = new int[3];

                //    for (int i = 0; i < 3; i++)
                //    {
                //        if (userMenus1[i] != null)
                //        {
                //            this.ddlUserMenus.Items.Add(new ListItem(userMenus1[i].MenuName, userMenus1[i].MenuId.ToString()));
                //            recentMenuIds[i] = userMenus1[i].MenuId;
                //        }
                //    }

                //    MyBuyList.Shared.Entities.Menu[] userMenus2 = userMenus.OrderBy(um => um.MenuName).ToArray();

                //    foreach (MyBuyList.Shared.Entities.Menu item in userMenus2)
                //    {
                //        bool isRecent = false;
                //        foreach (int x in recentMenuIds)
                //        {
                //            if (item.MenuId == x)
                //            {
                //                isRecent = true;
                //            }
                //        }
                //        if (!isRecent)
                //        {
                //            this.ddlUserMenus.Items.Add(new ListItem(item.MenuName, item.MenuId.ToString()));
                //        }
                //    }


                //    string str = "font-weight:bold;font-style:italic;font-size:larger;margin-top:5px;color:black;";

                //    this.ddlUserMenus.Items.Insert(0, new ListItem("תפריטים אחרונים", ""));
                //    this.ddlUserMenus.Items[0].Attributes["style"] = str;
                //    this.ddlUserMenus.Items[0].Attributes["disabled"] = "disabled";
                //    this.ddlUserMenus.Items.Insert(4, new ListItem("תפריטים נוספים", ""));
                //    this.ddlUserMenus.Items[4].Attributes["style"] = str;
                //    this.ddlUserMenus.Items[4].Attributes["disabled"] = "disabled";
                //    this.ddlUserMenus.SelectedIndex = 1;
                //}
                //else
                //{
                //    if (userMenus1.Length != 0)
                //    {
                //        foreach (MyBuyList.Shared.Entities.Menu m in userMenus1)
                //        {
                //            this.ddlUserMenus.Items.Add(new ListItem(m.MenuName, m.MenuId.ToString()));
                //        }
                //    }
                //    else
                //    {  
                //        this.ddlUserMenus.Visible = false;
                //        this.btnAddToExistingMenu.Enabled = false;
                //        this.btnAddToExistingMenu.ImageUrl = "~/Images/btn_AddToExistMenu_Down.png";
                //    }
                //}

                this.rptUserMenus.DataSource = userMenus1;
                this.rptUserMenus.DataBind();
            }
            else
            {
                //this.ddlUserMenus.Visible = false;
                //this.btnAddToExistingMenu.Enabled = false;
                this.btnAddToExistingMenu.Src = "Images/btn_AddToExistMenu_Down.png";
                this.btnAddToExistingMenu.Attributes["onmouseover"] = "this.src='Images/btn_AddToExistMenu_Down.png';";
                this.btnAddToExistingMenu.Attributes["onmouseout"] = "this.src='Images/btn_AddToExistMenu_Down.png';";
                this.btnAddToExistingMenu.Attributes["onmousedown"] = "this.src='Images/btn_AddToExistMenu_Down.png';";
                this.btnAddToExistingMenu.Attributes["onmouseup"] = "this.src='Images/btn_AddToExistMenu_Down.png';";
                this.btnAddToExistingMenu.Attributes["onclick"] = "";
            }

            this.RebindRecipes();            
        }
    }

    protected void rptUserMenus_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HyperLink hp = e.Item.FindControl("lnkMenu") as HyperLink;
        hp.NavigateUrl = string.Format("~/MenuEdit.aspx?menuId={0}", ((MyBuyList.Shared.Entities.Menu)e.Item.DataItem).MenuId);
    }

    //protected void ddlUserMenus_TextChanged(object sender, EventArgs e)
    //{
    //    if (this.ddlUserMenus.SelectedIndex == 0 || this.ddlUserMenus.SelectedIndex == 4)
    //    {
    //        this.ddlUserMenus.SelectedIndex += 1;
    //    }         
    //}

    protected void lnkMenu_Command(object sender, CommandEventArgs e)
    {
        //direct to menu edit
    }

    protected void RebindRecipes()
    {
        Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;

        if (selectedRecipes != null)
        {
            if (selectedRecipes.Count != 0)
            {
                dlRecipes.DataSource = selectedRecipes;
                dlRecipes.DataBind();
            }
            else
            {
                this.divRecipesList.Visible = false;
                this.lblNoRecipesSelected.Visible = true;
                this.lblNoRecipesSelected.Text = "לא נבחרו מתכונים";
            }
        }
    }

    protected void btnAddToNewMenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MenuEdit.aspx");
    }

    protected void btnAddToExistingMenu_Click(object sender, EventArgs e)
    {
        //int menuId;
        //if (int.TryParse(this.ddlUserMenus.SelectedItem.Value, out menuId))
        //{
        //    Response.Redirect(string.Format("~/MenuEdit.aspx?menuId={0}", menuId.ToString()));
        //}
    }

    protected void btnQuickList_Click(object sender, EventArgs e)
    {
        Utils.SelectedRecipesServings.Clear();
        Dictionary<int, int> selectedRecipesServings = Utils.SelectedRecipesServings;
        foreach (RepeaterItem item in dlRecipes.Items)
        {
            int recipeId;
            TextBox txtBox = item.FindControl("txtSelectServings") as TextBox;
            if (txtBox != null && int.TryParse(txtBox.Attributes["recipeId"], out recipeId))
            {
                int servingsSelected;
                if (!string.IsNullOrEmpty(txtBox.Text) && int.TryParse(txtBox.Text, out servingsSelected))
                {
                    selectedRecipesServings.Add(recipeId, servingsSelected);
                }
            }
        }

        Utils.SelectedRecipesServings = selectedRecipesServings;

        Response.Redirect("~/QuickMenu.aspx");
    }

    protected void dlRecipes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem is KeyValuePair<int, Recipe>)
        {
            KeyValuePair<int, Recipe> recipe = (KeyValuePair<int, Recipe>)e.Item.DataItem;
            LinkButton lnkBtn = e.Item.FindControl("lnkRemove") as LinkButton;
            if (lnkBtn != null)
            {
                lnkBtn.Attributes["recipeId"] = recipe.Key.ToString();
            }
            HyperLink hyplink = e.Item.FindControl("lnkRecipeName") as HyperLink;
            if (hyplink != null)
            {
                hyplink.NavigateUrl = string.Format("~/RecipeDetails.aspx?RecipeId={0}", recipe.Key.ToString());
            }
            TextBox txtBox = e.Item.FindControl("txtSelectServings") as TextBox;
            if (txtBox != null)
            {
                txtBox.Attributes["recipeId"] = recipe.Key.ToString();
                if (recipe.Value.ExpectedServings == 0)
                {
                    recipe.Value.ExpectedServings = recipe.Value.Servings;
                    txtBox.Text = recipe.Value.Servings.ToString();
                }
            }
            LinkButton lnkServingsUp = e.Item.FindControl("LinkButtonServingsUp") as LinkButton;
            if (lnkServingsUp != null)
            {
                lnkServingsUp.Attributes["recipeId"] = recipe.Key.ToString();
            }
            LinkButton lnkServingsDown = e.Item.FindControl("LinkButtonServingsDown") as LinkButton;
            if (lnkServingsDown != null)
            {
                lnkServingsDown.Attributes["recipeId"] = recipe.Key.ToString();
            }
        }
    }

    protected void lnkRemove_Click(object sender, EventArgs e)
    {
        Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;

        LinkButton lnkBtn = sender as LinkButton;

        int recipeId;
        if (lnkBtn != null && int.TryParse(lnkBtn.Attributes["recipeId"], out recipeId))
        {
            selectedRecipes.Remove(recipeId);
            Utils.SelectedRecipes = selectedRecipes;
        }
        RebindRecipes();
        upSelectedRecipes.Update();
    }

    protected void btnClearSelectedRecipes_Click(object sender, EventArgs e)
    {
        Utils.SelectedRecipes.Clear();

        this.RebindRecipes();
        this.upSelectedRecipes.Update();
    }

    protected void ChangeServings(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Increase":
                {
                    Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;

                    LinkButton servingsUp = sender as LinkButton;

                    int recipeId;
                    if (servingsUp != null && int.TryParse(servingsUp.Attributes["recipeId"], out recipeId))
                    {
                        selectedRecipes[recipeId].ExpectedServings += selectedRecipes[recipeId].Servings;
                    }
                }
                break;
            case "Decrease":
                {
                    Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;

                    LinkButton servingsDown = sender as LinkButton;

                    int recipeId;
                    bool tryParse = int.TryParse(servingsDown.Attributes["recipeId"], out recipeId);
                    bool allowDecrease = selectedRecipes[recipeId].ExpectedServings != selectedRecipes[recipeId].Servings;

                    if (allowDecrease && tryParse)
                    {
                        selectedRecipes[recipeId].ExpectedServings -= selectedRecipes[recipeId].Servings;
                    }
                }
                break;
        }

        RebindRecipes();
        upSelectedRecipes.Update();
    }
}
