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
using MyBuyList.BusinessLayer.Managers;
using MyBuyList.Shared.Entities;

using Resources;
using ProperControls.Pages;

public partial class ucSearchByCategories : System.Web.UI.UserControl
{
    int? ItemIndex
    {
        get { return (int?)this.ViewState["ItemIndex"]; }
        set { this.ViewState["ItemIndex"] = value; }
    }

    int? CategoryId
    {
        get { return (int?)this.ViewState["CategoryId"]; }
        set { this.ViewState["CategoryId"] = value; }
    }

    SRL_Category[] Categories
    {
        get 
        {
            if (this.ViewState["Categories"] == null)
            {
                int UserId = -1;

                if (((BasePage)Page).CurrUser != null)
                {
                    UserId = ((BasePage)Page).UserId;
                }
                var list = from item in BusinessFacade.Instance.GetRecipesCategoriesList(UserId)
                           select new SRL_Category(item.CategoryId, item.CategoryName, item.ParentCategoryId, item.RecipesCount);

                this.ViewState["Categories"] = list.ToArray();
            }
            return (SRL_Category[])this.ViewState["Categories"]; 
        }
        set { this.ViewState["Categories"] = value; }
    }

    SRL_Recipe[] Results
    {
        get
        {
            return (SRL_Recipe[])this.ViewState["Results"];
        }
        set
        {
            this.ViewState["Results"] = value;
        }
    }
    
    SRL_Recipe[] ResultsStored
    {
        get 
        {

            return (SRL_Recipe[])this.Session["ResultsStored"]; 
        }
        set 
        {
            this.Session["ResultsStored"] = value; 
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadRootCategories();

            if (!string.IsNullOrEmpty(this.Request["view"]))
            {

                if ((this.ResultsStored != null) && (int.Parse(this.Request["view"]) == (int)PersonalAreaViewEnum.CategoriesSearch))
                {
                    if (!string.IsNullOrEmpty(this.Request["categoryId"]))
                    {
                        this.CategoryId = int.Parse(this.Request["categoryId"]);
                        this.BuildCategoriesPath();
                    }
                    this.gridRecipesList.DataSource = this.ResultsStored;
                    this.gridRecipesList.DataBind();
                    this.lblResultsCount.Text = string.Format(MyGlobalResources.FoundNoRecipes, this.ResultsStored.Length);
                    this.lblComment.Visible = (this.ResultsStored.Length > 0);
                    this.Session.Remove("ResultsStored");
                }
            }
        }

        this.BuildCategoriesPath(); //should be executed every entry in the server
    }

    public void LoadRootCategories()
    {
        this.rptCategories.DataSource = this.Categories.Where(cat => !cat.ParentCategoryId.HasValue).ToArray(); ;
        this.rptCategories.DataBind();

        this.gridRecipesList.DataSource = null;
        this.gridRecipesList.DataBind();
        this.lblResultsCount.Text = "";
        this.lblComment.Visible = false;
        this.CategoryId = 0;
    }

    private void BuildCategoriesPath()
    {
        this.cellCategoriesPath.InnerText = "";
        SRL_Category category = this.Categories.SingleOrDefault(cat => cat.CategoryId == this.CategoryId);
        while (category != null)
        {

            LinkButton btnCat = new LinkButton();
            btnCat.Text = category.CategoryName;
            btnCat.Attributes["CategoryId"] = category.CategoryId.ToString();
            btnCat.Click += new EventHandler(this.btnSelectCategory_Click);

            this.cellCategoriesPath.Controls.AddAt(0, btnCat);

            Literal ltr = new Literal();
            ltr.Text = " >> ";
            this.cellCategoriesPath.Controls.AddAt(0, ltr);

            category = this.Categories.SingleOrDefault(cat => cat.CategoryId == category.ParentCategoryId);

        }

        LinkButton btnRoot = new LinkButton();
        btnRoot.Text = MyGlobalResources.All;
        btnRoot.Attributes["CategoryId"] = "0";
        btnRoot.Click += new EventHandler(this.btnSelectCategory_Click);
        this.cellCategoriesPath.Controls.AddAt(0, btnRoot);

    }

    protected void btnSelectCategory_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if(!string.IsNullOrEmpty(btn.Attributes["categoryId"]))
        {
            int categoryId = int.Parse(btn.Attributes["categoryId"]);
            if (categoryId == 0)
            {
                this.LoadRootCategories();
            }
            else
            {
                var list = this.Categories.Where(cat => cat.ParentCategoryId == categoryId).ToArray();
                if (list.Length > 0)
                {
                    this.rptCategories.DataSource = list;
                    this.rptCategories.DataBind();
                    this.ItemIndex = null;
                }
                else
                {
                    if (this.ItemIndex != null)
                    {
                        RepeaterItem rptItem = this.rptCategories.Items[this.ItemIndex.Value];
                        LinkButton link = rptItem.FindControl("btnCategory") as LinkButton;
                        if (link != null)
                        {
                            link.BackColor = System.Drawing.Color.Transparent;
                        }
                    }
                    btn.BackColor = System.Drawing.Color.LightSteelBlue;
                    this.ItemIndex = (btn.NamingContainer as RepeaterItem).ItemIndex;
                }
                
                this.Results = (from item in BusinessFacade.Instance.GetRecipesByCategory(categoryId, ((BasePage)Page).UserId)
                                select new SRL_Recipe(item)).ToArray();
                this.gridRecipesList.DataSource = this.Results;
                this.gridRecipesList.DataBind();
                this.lblResultsCount.Text = string.Format(MyGlobalResources.FoundNoRecipes, this.Results.Length);
                this.lblComment.Visible = (this.Results.Length > 0);
            }

            this.CategoryId = categoryId;
            this.BuildCategoriesPath();
        }     
    }
    protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        SRL_Category category = e.Item.DataItem as SRL_Category;
        if (category != null)
        {           
            LinkButton btn = e.Item.FindControl("btnCategory") as LinkButton;
            btn.Text += " (" + category.RecipesCount + ")";
        }
    }

    protected void gridRecipesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridRecipesList.DataSource = this.Results;
        this.gridRecipesList.PageIndex = e.NewPageIndex;
        this.gridRecipesList.DataBind();
    }

    protected void gridRecipesList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#dddddd'";
            e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='Transparent'";

            Label lbl = e.Row.FindControl("lblUser") as Label;
            //Recipe recipe = BusinessFacade.Instance.GetRecipe(((SRL_Recipe)e.Row.DataItem).RecipeId);
            //string name = string.Empty;

            //if ((recipe != null) && (recipe.User != null))
            //{

            //    if (!string.IsNullOrEmpty(recipe.User.DisplayName))
            //    {
            //        name = recipe.User.DisplayName;
            //    }
            //    else
            //    {
            //        name = recipe.User.Name;
            //    }
            //}
            lbl.Text = @"נוסף על ידי " + ((SRL_Recipe)e.Row.DataItem).Name;
           // lbl.Text = @"נוסף על ידי " + (BusinessFacade.Instance.GetRecipe(((SRL_Recipe)e.Row.DataItem).RecipeId)).User.DisplayName;
        }
    }

    protected void btnAddRecipeToMenu_Click(object sender, EventArgs e)
    {      
        if (this.AddRecipeToCallback != null)
        {
            ImageButton btn = sender as ImageButton;
            int recipeId = int.Parse(btn.Attributes["recipeId"]);
            this.AddRecipeToCallback(recipeId);
        }
    }


    protected void lbRecipeName_Clicked(object sender, EventArgs e)
    {
        if (this.Results != null)
        {
            this.ResultsStored = this.Results;
        }
        else
        {
            if (this.CategoryId.HasValue)
            {
                this.ResultsStored = (from item in BusinessFacade.Instance.GetRecipesByCategory(this.CategoryId.Value, ((BasePage)Page).UserId)
                                      select new SRL_Recipe(item)).ToArray();
            }
        }

        LinkButton btn = (LinkButton)sender;
        int recipeId = int.Parse(btn.Attributes["recipeId"]);

        Response.Redirect(string.Format("~/RecipeDetails.aspx?recipeId={0}&view=5&categoryId={1}", recipeId, this.CategoryId));
    }

    public delegate void AddRecipeToMenuHandler(int recipeId);
    public event AddRecipeToMenuHandler AddRecipeToCallback;
}
