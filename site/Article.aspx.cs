using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;

using ProperControls.Pages;

public partial class Article : BasePage
{
    const int FOOTER_ARTICLE_ID = 6; 

    int? ArticleId
    {
        get
        {
            int? articleId = null;
            int id = 0;

            if (!string.IsNullOrEmpty(Request.QueryString["articleId"]))
            {
                bool result = int.TryParse(Request.QueryString["articleId"], out id);
                if (result)
                {
                    articleId = id;
                }
            }

            return articleId;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBuyList.Shared.Entities.Article article = null;

            if (this.ArticleId != null)
            {
                article = BusinessFacade.Instance.GetArticleById(this.ArticleId.Value); 
            }

            this.BindArticleDetails(article);
        }
    }

    private void BindArticleDetails(MyBuyList.Shared.Entities.Article article)
    {
        if (article != null && article.ArticleId != FOOTER_ARTICLE_ID)
        {
            this.lblTitle.Text = article.Title;
            this.lblAbstract.Text = article.Abstract;
            this.lblAuthor2.Text = article.Publisher;
            this.lblDate.Text = "פורסם בתאריך " + article.ModifiedDate.ToShortDateString();
            
            this.articleBody.InnerHtml = article.Body;
        }
        else
        {
            this.lblTitle.Text = "המאמר לא נמצא";
            this.lblAbstract.Visible = false;
            this.lblAuthor1.Visible = false;
            this.lblAuthor2.Visible = false;
            this.lblDate.Visible = false;

            this.articleBody.Visible = false;
        }

        if (this.ArticleId != null && ((BasePage)this.Page).UserType == 1)
        {
            this.btnAdminEdit.Visible = true;
            this.btnAdminEdit.NavigateUrl += string.Format("?ArticleId={0}", this.ArticleId.Value );
        }
    }
}
