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

public partial class PageFoodCategory : BasePage
{
    int FoodCategoryId
    {
        get { return ViewState["FoodCategoryId"] == null ? -1 : (int)ViewState["FoodCategoryId"]; }
        set { ViewState["FoodCategoryId"] = value; }
    }

    int? ParentFoodCategoryId
    {
        get { return (int?)ViewState["ParentFoodCategoryId"]; }
        set { ViewState["ParentFoodCategoryId"] = value; }
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
                    this.FoodCategoryId = int.Parse(this.Request["catId"]);
                    FoodCategory FoodCategory = BusinessFacade.Instance.GetFoodCategory(this.FoodCategoryId);
                    if (FoodCategory != null)
                    {
                        this.txtFoodCategoryName.Text = FoodCategory.FoodCategoryName;
                        if (FoodCategory.ParentFoodCategory != null)
                        {
                            this.txtParentFoodCategory.Text = FoodCategory.ParentFoodCategory.FoodCategoryName;
                            this.ParentFoodCategoryId = FoodCategory.ParentCategoryId;
                        }
                        else
                        {
                            this.txtParentFoodCategory.Text = string.Empty;
                            this.ParentFoodCategoryId = null;
                        }

                        this.btnDelete.Visible = FoodCategory.AllowDelete;
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

                this.tvFoodCategories.Nodes.Clear();
                FoodCategory[] FoodCategories = BusinessFacade.Instance.GetFoodCategoriesList();
                this.BuildTree(FoodCategories, null, null);

                this.tvFoodCategories.ShowCheckBoxes = TreeNodeTypes.None;
                this.tvFoodCategories.DataBind();
            }
        }
    }

    private void BuildTree(FoodCategory[] cats, int? parentFoodCategoryId, TreeNode rootNode)
    {
        var list = cats.Where(c => c.ParentCategoryId == parentFoodCategoryId);
        foreach (FoodCategory item in list)
        {
            if (item.FoodCategoryId == this.FoodCategoryId)
            {
                continue;
            }
            
            TreeNode node = new TreeNode(item.FoodCategoryName, item.FoodCategoryId.ToString());
            if (this.ParentFoodCategoryId.HasValue && this.ParentFoodCategoryId.Value == item.FoodCategoryId)
            {
                node.Selected = true;
            }

            if (rootNode == null)
            {
                this.tvFoodCategories.Nodes.Add(node);
            }
            else
            {
                rootNode.ChildNodes.Add(node);
            }

            BuildTree(cats, item.FoodCategoryId, node);
        }
    }

    protected void tvFoodCategories_SelectedNodeChanged(object sender, EventArgs e)
    {
        this.txtParentFoodCategory.Text = tvFoodCategories.SelectedNode.Text;
        this.ParentFoodCategoryId = int.Parse(tvFoodCategories.SelectedNode.Value);
    }

    protected void btnClearParentFoodCategory_Click(object sender, EventArgs e)
    {
        if(this.ParentFoodCategoryId != null)
        {
            TreeNode node = this.tvFoodCategories.SelectedNode;
            if (node != null)
            {
                node.Selected = false;
                this.txtParentFoodCategory.Text = "";
                this.ParentFoodCategoryId = null;
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (this.FoodCategoryId > 0)
        {
            if (BusinessFacade.Instance.DeleteFoodCategory(this.FoodCategoryId))
            {
                this.Response.Redirect("~/Admin/FoodCategoriesList.aspx");
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

        if (BusinessFacade.Instance.SaveFoodCategory(this.FoodCategoryId, this.txtFoodCategoryName.Text, this.ParentFoodCategoryId))
        {
            this.Response.Redirect("~/Admin/FoodCategoriesList.aspx");
        }    
    }
    protected void custValidFoodCategoryName_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(this.txtFoodCategoryName.Text))
        {
            args.IsValid = !BusinessFacade.Instance.CheckDuplicateFoodCategoryName(this.FoodCategoryId, this.txtFoodCategoryName.Text);
        }
        else
        {
            args.IsValid = true;
        }
    }
}
