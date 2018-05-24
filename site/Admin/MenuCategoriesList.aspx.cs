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

public partial class MenuCategoriesList : BasePage
{
    [Serializable]
    public class SRL_Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public SRL_Category ParentCategory { get; set; }

        public SRL_Category(int categoryId, string categoryName, SRL_Category parent)
        {
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
            this.ParentCategory = parent;
        }
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
                this.Rebind();
            }     
        }
    }

    private void Rebind()
    {
        this.tvCategories.Nodes.Clear();
        MCategory[] categories = BusinessFacade.Instance.GetMCategoriesList();
        this.BuildTree(categories, null, null);

        this.tvCategories.ShowCheckBoxes = TreeNodeTypes.None;
        this.tvCategories.DataBind();     
    }


    private void BuildTree(MCategory[] cats, int? parentCategoryId, TreeNode rootNode)
    {
        var list = cats.Where(c => c.ParentMCategoryId == parentCategoryId);
        foreach (MCategory item in list)
        {
            TreeNode node = new TreeNode(item.MCategoryName, item.MCategoryId.ToString());
            node.NavigateUrl = string.Format("~/Admin/MenuCategory.aspx?catId={0}", item.MCategoryId);
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
}
