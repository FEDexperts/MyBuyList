using System;
using System.Collections;
using System.Collections.Specialized;
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

using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using ProperControls.Pages;
public partial class PrintRecipes : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["MenuId"]))
        {
            int menuId = int.Parse(Request["MenuId"]);
            int? userId = BusinessFacade.Instance.GetMenuUserId(menuId);

            if (userId.HasValue)
            {
                if ((userId.Value == int.Parse(ConfigurationManager.AppSettings["anonymous"])) || (userId.Value == ((BasePage)Page).UserId))
                {
                    //MyBuyList.Shared.Entities.Menu menu = 
                    this.lblMenuName.Text = BusinessFacade.Instance.GetMenu(menuId).MenuName;
                    this.rptRecipes.DataSource = BusinessFacade.Instance.GetMenuRecipesIngrid(menuId); ;
                    this.rptRecipes.DataBind();

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Key", "javascript:window.print();", true);
                }
                else
                {
                    AppEnv.MoveToDefaultPage();
                }
            }
            else
            {
                AppEnv.MoveToDefaultPage();
            }
        }
        else
        {
            AppEnv.MoveToDefaultPage();
        }
    }

    protected void rptRecipes_DataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem rptItem = e.Item as RepeaterItem;

        KeyValuePair<int, List<int>> currRecipeItem = (KeyValuePair<int, List<int>>)rptItem.DataItem;

        UserControls_RecipePrint ucRecipePrint = (UserControls_RecipePrint)rptItem.FindControl("ucRecipePrint");
        ucRecipePrint.Bind(currRecipeItem.Key, currRecipeItem.Value.ToArray<int>());
    }
}
