using System;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;

public partial class UC_FooterControl : System.Web.UI.UserControl
{
    const int FOOTER_ARTICLE_ID = 6; 

    protected void Page_Load(object sender, EventArgs e)
    {
        Article footerArticle = BusinessFacade.Instance.GetArticleById(FOOTER_ARTICLE_ID);
        if (footerArticle != null) this.footerContent.InnerHtml = footerArticle.Body;
    }
}
