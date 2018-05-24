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

public partial class PageFoodCategoriesList : System.Web.UI.Page
{
    [Serializable]
    public class SRL_FoodCategory
    {
        public int FoodCategoryId { get; set; }
        public string FoodCategoryName { get; set; }
        public SRL_FoodCategory ParentFoodCategory { get; set; }

        public SRL_FoodCategory(int FoodCategoryId, string FoodCategoryName, SRL_FoodCategory parent)
        {
            this.FoodCategoryId = FoodCategoryId;
            this.FoodCategoryName = FoodCategoryName;
            this.ParentFoodCategory = parent;
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
        this.tvFoodCategories.Nodes.Clear();
        FoodCategory[] FoodCategories = BusinessFacade.Instance.GetFoodCategoriesList();
        this.BuildTree(FoodCategories, null, null);

        this.tvFoodCategories.ShowCheckBoxes = TreeNodeTypes.None;
        this.tvFoodCategories.DataBind();     
    }


    private void BuildTree(FoodCategory[] cats, int? parentFoodCategoryId, TreeNode rootNode)
    {
        var list = cats.Where(c => c.ParentCategoryId == parentFoodCategoryId);
        foreach (FoodCategory item in list)
        {
            TreeNode node = new TreeNode(item.FoodCategoryName, item.FoodCategoryId.ToString());
            node.NavigateUrl = string.Format("~/Admin/FoodCategory.aspx?catId={0}", item.FoodCategoryId);
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
}
