using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MyBuyList.Shared.Entities;
using MyBuyList.Shared.Enums;
using MyBuyList.BusinessLayer;

using ProperControls.Pages;

public partial class Articles : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RebindArticles();
        }
    }

    public int PageSize { get { return 5; } }
    public int CurrentPage
    {
        get
        {
            int page;

            if (!string.IsNullOrEmpty(Request["page"]) && int.TryParse(Request["page"], out page))
            {
                return page;
            }
            else
            {
                return 1;
            }
        }
    }
    public RecipeOrderEnum OrderBy
    {
        get
        {
            RecipeOrderEnum orderBy;

            if (!string.IsNullOrEmpty(Request["orderBy"]))
            {
                try
                {
                    orderBy = (RecipeOrderEnum)Enum.Parse(typeof(RecipeOrderEnum), Request["orderBy"], true);
                    return orderBy;
                }
                catch
                {
                    return RecipeOrderEnum.LastUpdate;
                }
            }
            else
            {
                return RecipeOrderEnum.LastUpdate;
            }
        }
    }

    public int totalPages;

    private void RebindArticles()
    {

        IEnumerable<ArticleView> articles = from art in BusinessFacade.Instance.GetArticlesList()
                                            select new ArticleView()
                                      {
                                          ArticleId = art.ArticleId,
                                          ArticleTitle = art.Title,
                                          ArticleLink = string.Format("~/Article.aspx?articleId={0}", art.ArticleId ),
                                          ArticleAbstract = art.Abstract,
                                          AuthorName = art.Publisher,
                                          PublishDate = art.ModifiedDate.ToShortDateString(),
                                          ArticleThumbnail = ResolveUrl("~/Images/Img_Default.jpg")
                                      };

        this.rptArticles.DataSource = articles;        
        this.rptArticles.DataBind();
    }

    protected void rptArticles_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        // init pagers
        //if (e.Item.ItemType == ListItemType.Header || e.Item.ItemType == ListItemType.Footer)
        //{
        //    PlaceHolder phPager = (PlaceHolder)e.Item.FindControl("phPager");

        //    phPager.Controls.Clear();

        //    bool isEnd = false, isStart = false;

        //    int startPage = 20 * ((this.CurrentPage - 1) / 20) + 1;
        //    int endpage = 20 * ((this.CurrentPage - 1) / 20) + 20;

        //    if (startPage == 1)
        //    {
        //        isStart = true;
        //    }
        //    if (endpage >= this.totalPages)
        //    {
        //        isEnd = true;
        //        endpage = this.totalPages;
        //    }

        //    if (!isStart)
        //    {
        //        HyperLink lnkArrow = new HyperLink();
        //        lnkArrow.Text = "&lt;&lt;";
        //        lnkArrow.NavigateUrl = string.Format("~/Menus.aspx?page={0}&orderby={1}", (startPage - 20).ToString(), this.OrderBy.ToString());
        //        phPager.Controls.Add(lnkArrow);
        //        phPager.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
        //    }

        //    for (int page = startPage; page <= endpage; page++)
        //    {
        //        if (page == this.CurrentPage)
        //        {
        //            Label lblPage = new Label();
        //            lblPage.Text = page.ToString();
        //            phPager.Controls.Add(lblPage);
        //        }
        //        else
        //        {
        //            HyperLink lnkPage = new HyperLink();
        //            lnkPage.NavigateUrl = string.Format("~/Menus.aspx?page={0}&orderby={1}", page, this.OrderBy.ToString());
        //            lnkPage.Text = page.ToString();
        //            phPager.Controls.Add(lnkPage);
        //        }

        //        phPager.Controls.Add(new LiteralControl("&nbsp;"));
        //    }

        //    if (!isEnd)
        //    {
        //        HyperLink lnkArrow = new HyperLink();
        //        lnkArrow.Text = "&gt;&gt;";
        //        lnkArrow.NavigateUrl = string.Format("~/Menus.aspx?page={0}&orderby={1}", (startPage + 20).ToString(), this.OrderBy.ToString());
        //        phPager.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
        //        phPager.Controls.Add(lnkArrow);
        //    }
        //}
    }

    protected void rptArticles_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
    }

    protected class ArticleView
    {
        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleLink { get; set; }
        public string ArticleAbstract { get; set; }        
        public string AuthorName { get; set; }
        public string PublishDate { get; set; }
        public string ArticleThumbnail { get; set; }
    }
}
