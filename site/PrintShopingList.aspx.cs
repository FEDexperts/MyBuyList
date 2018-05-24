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

using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;

using ProperControls.Pages;


public partial class PrintShopingList : BasePage
{
    private ShoppingFood[] foodsList;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((!string.IsNullOrEmpty(Request["menuId"])) && (((BasePage)Page).UserId != -1))
        {
            int menuId = int.Parse(Request["menuId"]);

            MyBuyList.Shared.Entities.Menu menu = BusinessFacade.Instance.GetMenu(menuId);

            if (((BasePage)Page).CurrUser != null)
            {
                if (menu.UserId == ((BasePage)Page).UserId || menu.IsPublic == true || ((BasePage)Page).UserType == 1)
                {                    
                    Page.Title = "הדרך המטעימה לארגון הרשימה - MyBuyList";
                    BindShoppingList(menu);

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

    private void BindShoppingList(MyBuyList.Shared.Entities.Menu menu)
    {
        this.foodsList = BusinessFacade.Instance.GetMenuShoppingList(menu.MenuId);

        var list = BusinessFacade.Instance.GetMenuShopDepartments(menu.MenuId);
        this.rptMenuShopDepartments.DataSource = list;
        this.rptMenuShopDepartments.DataBind();

        this.rptMenuRecipes.DataSource = BusinessFacade.Instance.GetRecipesInMenuMeals(menu.MenuId).Values.ToArray<Recipe>();
        this.rptMenuRecipes.DataBind();        

        if (menu.UserId == ((BasePage)Page).UserId)
        {
            this.RebindAdditionalItems(menu); 
        }
    }

    protected void rptMenuShopDepartments_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem rptItem = e.Item as RepeaterItem;
        if (rptItem != null && rptItem.ItemType != ListItemType.Header)
        {
            //HtmlTableCell cell = rptItem.FindControl("departmentCell") as HtmlTableCell;
            //Repeater rptShoppingFoods = cell.FindControl("rptShoppingFoods") as Repeater;
            Repeater rptShoppingFoods = rptItem.FindControl("rptShoppingFoods") as Repeater;
            int shopDepartmentId = (rptItem.DataItem as ShopDepartment).ShopDepartmentId;

            if (this.foodsList != null)
            {
                rptShoppingFoods.DataSource = this.foodsList.Where(list => list.ShopDepartmentId == shopDepartmentId).ToArray();
                rptShoppingFoods.DataBind();
            }
        }
    }

    private void RebindAdditionalItems(MyBuyList.Shared.Entities.Menu menu)
    {
        var listAddItems = from el in BusinessFacade.Instance.GetShoppingListAdditionalItems(menu.MenuId)
                           select new
                           {
                               ItemId = el.ShoppingListItemId,
                               ItemName = (el.GeneralItem != null ? el.GeneralItem.GeneralItemName : el.ItemName)
                           };

        this.rptAdditionalItems.DataSource = listAddItems.ToArray();
        this.rptAdditionalItems.DataBind();
        if (listAddItems.Count() > 0)
        {
            this.lblAdditionalItems.Visible = true;
        }
    }   
}
