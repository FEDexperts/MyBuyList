using System;
using MyBuyList.BusinessLayer;
using ProperControls.Pages;
using MyBuyList.Shared.Entities;
using MyBuyList.Shared;

public partial class PageAdmin : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (UserType != AppEnv.USER_ADMIN)
            {
                AppEnv.MoveToDefaultPage();
            }
            else
            {
                this.lblUsers.Text = string.Format("{0} משתמשים", BusinessFacade.Instance.GetUsersNum());
                this.lblFoods.Text = string.Format("{0} מצרכים", BusinessFacade.Instance.GetFoodsList().Length);
                this.lblRecipes.Text = string.Format("{0} מתכונים", BusinessFacade.Instance.GetRecipesNum());
                this.lblMenus.Text = string.Format("{0} תפריטים", BusinessFacade.Instance.GetMenusNum());
            }
        }
    }
}
