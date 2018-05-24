using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Linq;

using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using System.Text;

public partial class ucRecipeCategories : System.Web.UI.UserControl
{
    int RecipeId
    {
        get { return ViewState["RecipeId"] == null ? 0 : (int)ViewState["RecipeId"]; }
        set { ViewState["RecipeId"] = value; }
    }

    SRL_RecipeCategory[] RecipeCategories
    {
        get { return (SRL_RecipeCategory[])ViewState["RecipeCategories"]; }
        set { ViewState["RecipeCategories"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tvCategories.Nodes.Clear();
            Category[] categories = BusinessFacade.Instance.GetCategoriesList();
            this.BuildTree(categories, null, null);

            this.tvCategories.ShowCheckBoxes = TreeNodeTypes.All;
            this.tvCategories.DataBind();

            this.updatePanel.Update();
        }
    }

    public void storeRecipeCategories(int recipeId)
    {        
        if (recipeId != 0)
        {
            Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeId);
            if (recipe != null)
            {
                var list = from item in recipe.RecipeCategories
                           select new SRL_RecipeCategory(item.RecipeId, item.CategoryId, item.Category.CategoryName);
                this.RecipeCategories = list.ToArray();
                this.RecipeId = recipe.RecipeId;
            }
        }
    }

    public void ShowCategories(int recipeId, SRL_RecipeCategory[] recipeCategories)
    {
        this.RecipeId = recipeId;
        this.RecipeCategories = recipeCategories;

        this.tvCategories.Nodes.Clear();
        Category[] categories = BusinessFacade.Instance.GetCategoriesList();
        this.BuildTree(categories, null, null);

        this.tvCategories.ShowCheckBoxes = TreeNodeTypes.All;
        this.tvCategories.DataBind();
       
        this.updatePanel.Update();
        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "_onCatsPopupDisplay", "_onCategoriesLoaded();", true);
        this.mpeCategories.Show();
    }

    public void ShowCategories(SRL_RecipeCategory[] recipeCategories)
    {
        this.RecipeId = 0;
        this.RecipeCategories = recipeCategories;

        this.tvCategories.Nodes.Clear();
        Category[] categories = BusinessFacade.Instance.GetCategoriesList();
        this.BuildTree(categories, null, null);

        this.tvCategories.ShowCheckBoxes = TreeNodeTypes.All;
        this.tvCategories.DataBind();

        this.updatePanel.Update();
        this.mpeCategories.Show();
    }

    private void BuildTree(Category[] cats, int? parentCategoryId, TreeNode rootNode)
    {
        var list = cats.Where(c => c.ParentCategoryId == parentCategoryId);
        foreach (Category item in list)
        {
            TreeNode node = new TreeNode(item.CategoryName, item.CategoryId.ToString());
            if (this.RecipeCategories != null &&
                this.RecipeCategories.SingleOrDefault(rc => rc.RecipeId == this.RecipeId &&
                                                            rc.CategoryId == item.CategoryId) != null)
            {
                node.Checked = true;
            }

            if (rootNode == null)
            {
                this.tvCategories.Nodes.Add(node);
            }
            else
            {
                rootNode.ChildNodes.Add(node);
            }

            BuildTree(cats, item.CategoryId, node);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.mpeCategories.Hide();

        List<SRL_RecipeCategory> list = new List<SRL_RecipeCategory>();
        foreach (TreeNode node in this.tvCategories.CheckedNodes)
        {
            SRL_RecipeCategory recipeCat = new SRL_RecipeCategory(this.RecipeId, int.Parse(node.Value), node.Text);
            list.Add(recipeCat);
        }
        this.RecipeCategories = list.ToArray();        

        if (this.RefreshData != null)
        {
            this.RefreshData.Invoke(this.RecipeCategories);
        }
    }

    public delegate void RefreshHandler(SRL_RecipeCategory[] arr);
    public event RefreshHandler RefreshData;
}
