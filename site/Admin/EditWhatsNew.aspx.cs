using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;
using ProperControls.Pages;

public partial class Admin_EditWhatsNew : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //present current settings from DB
            MBLSettingsWrapper settingsInfo = BusinessFacade.Instance.GetMBLSettingsWrapper();
            int[] recentRecipes = settingsInfo.RecentRecipes;
            int[] recentMenus = settingsInfo.RecentMenus;

            this.txtNewRecipe1.Text = recentRecipes[0].ToString();
            this.txtNewRecipe2.Text = recentRecipes[1].ToString();
            this.txtNewMenu1.Text = recentMenus[0].ToString();
            this.txtNewMenu2.Text = recentMenus[1].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string recipesStr = this.txtNewRecipe1.Text + "," + this.txtNewRecipe2.Text;
        string menusStr = this.txtNewMenu1.Text + "," + this.txtNewMenu2.Text;

        bool savedRecipes = BusinessFacade.Instance.SaveMBLSettingsRecentItem(recipesStr, "recipe");
        bool savedMenus = BusinessFacade.Instance.SaveMBLSettingsRecentItem(menusStr, "menu");

        if (savedRecipes && savedMenus)
        {
            this.lblStatus.Text = "הנתונים נשמרו";
        }
        else
        {
            this.lblStatus.Text = "חלה שגיאה. הנתונים לא נשמרו";
        }
        this.lblStatus.Visible = true;
    }
}
