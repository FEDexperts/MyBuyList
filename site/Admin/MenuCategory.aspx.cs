using System;
using System.Collections;
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

using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using ProperControls.Pages;

public partial class MenuCategory : BasePage
{
    int CategoryId
    {
        get { return ViewState["CategoryId"] == null ? 0 : (int)ViewState["CategoryId"]; }
        set { ViewState["CategoryId"] = value; }
    }

    int? ParentCategoryId
    {
        get { return (int?)ViewState["ParentCategoryId"]; }
        set { ViewState["ParentCategoryId"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (((BasePage)Page).UserType != AppEnv.USER_ADMIN)
            {
                AppEnv.MoveToDefaultPage();
            }
            else
            {
                if (!string.IsNullOrEmpty(this.Request["catId"]))
                {
                    this.CategoryId = int.Parse(this.Request["catId"]); 
                    //start changing
                    MCategory category = BusinessFacade.Instance.GetMenuCategory(this.CategoryId);
                    if (category != null)
                    {
                        this.txtCategoryName.Text = category.MCategoryName;
                        if (category.ParentMCategory != null)
                        {
                            this.txtParentCategory.Text = category.ParentMCategory.MCategoryName;
                            this.ParentCategoryId = category.ParentMCategoryId;
                        }
                        else
                        {
                            this.txtParentCategory.Text = string.Empty;
                            this.ParentCategoryId = null;
                        }

                        this.btnDelete.Visible = category.AllowDelete;
                    }
                    else
                    {
                        this.btnDelete.Visible = false;
                    }
                }
                else
                {
                    this.btnDelete.Visible = false;
                }

                this.tvCategories.Nodes.Clear();
                MCategory[] categories = BusinessFacade.Instance.GetMCategoriesList();
                this.BuildTree(categories, null, null);

                this.tvCategories.ShowCheckBoxes = TreeNodeTypes.None;
                this.tvCategories.DataBind();
            }
        }
    }

    private void BuildTree(MCategory[] cats, int? parentCategoryId, TreeNode rootNode)
    {
        var list = cats.Where(c => c.ParentMCategoryId == parentCategoryId);
        foreach (MCategory item in list)
        {
            if (item.MCategoryId == this.CategoryId)
            {
                continue;
            }
            
            TreeNode node = new TreeNode(item.MCategoryName, item.MCategoryId.ToString());
            if (this.ParentCategoryId.HasValue && this.ParentCategoryId.Value == item.MCategoryId)
            {
                node.Selected = true;
            }

            if (rootNode == null)
            {
                this.tvCategories.Nodes.Add(node);
            }
            else
            {
                rootNode.ChildNodes.Add(node);
            }

            BuildTree(cats, item.MCategoryId, node);
        }
    }

    protected void tvCategories_SelectedNodeChanged(object sender, EventArgs e)
    {
        this.txtParentCategory.Text = tvCategories.SelectedNode.Text;
        this.ParentCategoryId = int.Parse(tvCategories.SelectedNode.Value);
    }

    protected void btnClearParentCategory_Click(object sender, EventArgs e)
    {
        if(this.ParentCategoryId != null)
        {
            TreeNode node = this.tvCategories.SelectedNode;
            if (node != null)
            {
                node.Selected = false;
                this.txtParentCategory.Text = "";
                this.ParentCategoryId = null;
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (this.CategoryId > 0)
        {
            if (BusinessFacade.Instance.DeleteMenuCategory(this.CategoryId))
            {
                this.Response.Redirect("~/Admin/MenuCategoriesList.aspx");
            }
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        this.Validate();
        if (!this.IsValid)
        {
            return;
        }

        if (BusinessFacade.Instance.SaveMenuCategory(this.CategoryId, this.txtCategoryName.Text, this.ParentCategoryId))
        {
            this.Response.Redirect("~/Admin/MenuCategoriesList.aspx");
        }    
    }
    protected void custValidCategoryName_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(this.txtCategoryName.Text))
        {
            args.IsValid = !BusinessFacade.Instance.CheckDuplicateMCategoryName(this.CategoryId, this.txtCategoryName.Text);
        }
        else
        {
            args.IsValid = true;
        }
    }
}
