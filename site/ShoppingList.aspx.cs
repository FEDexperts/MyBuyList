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
using System.Web.Mail;
using MyBuyList.BusinessLayer;
using MyBuyList.BusinessLayer.Managers;
using MyBuyList.Shared.Entities;
using System.IO;

using System.Collections.Generic;
using System.Text;
using ProperControls.Pages;

public partial class PageShoppingList : BasePage
{
    int MenuId
    {
        get { return ViewState["MenuId"] == null ? 0 : (int)ViewState["MenuId"]; }
        set { ViewState["MenuId"] = value; }
    }

    private ShoppingFood[] foodsList;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.Request["menuId"]))
            {
                this.MenuId = int.Parse(this.Request["menuId"]);

                int menuTypeId = BusinessFacade.Instance.GetMenuType(this.MenuId).MenuTypeId;
                //this.Master.SetLeftBackgroundImage(menuTypeId);
                MyBuyList.Shared.Entities.Menu menu = BusinessFacade.Instance.GetMenu(this.MenuId);

                if (menu.UserId == 0)
                {
                    if (((BasePage)Page).CurrUser != null)
                    {
                        if (((BasePage)Page).UserId != -1)
                        {
                            BusinessFacade.Instance.UpdateMenuUser(this.MenuId, ((BasePage)Page).UserId);
                            menu = BusinessFacade.Instance.GetMenu(this.MenuId);
                        }
                    }
                }

                if (((BasePage)Page).CurrUser != null)
                {
                    if (menu.UserId == ((BasePage)Page).UserId || menu.IsPublic == true || ((BasePage)Page).UserType == 1)
                    {
                        this.foodsList = BusinessFacade.Instance.GetMenuShoppingList(this.MenuId);

                        ShopDepartment[] list = BusinessFacade.Instance.GetMenuShopDepartments(this.MenuId);
                        this.rptMenuShopDepartments.DataSource = list;
                        this.rptMenuShopDepartments.DataBind();

                        this.rptMenuRecipes.DataSource = BusinessFacade.Instance.GetRecipesInMenuMeals(this.MenuId).Values.ToArray<Recipe>();
                        this.rptMenuRecipes.DataBind();

                        this.btnPrintShoppingList_bottom.NavigateUrl = "~/PrintShopingList.aspx?menuId=" + this.MenuId.ToString();
                        this.btnPrintShoppingList_top.NavigateUrl = "~/PrintShopingList.aspx?menuId=" + this.MenuId.ToString();

                        if (menu.UserId == ((BasePage)Page).UserId)
                        {
                            this.RebindAdditionalItems();
                            this.UpdatePanel2.Visible = true;
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

    protected void btnDeleteAdditionalItem_Click(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        if (!string.IsNullOrEmpty(btn.Attributes["itemId"]))
        {
            int itemId = int.Parse(btn.Attributes["itemId"]);
            if (BusinessFacade.Instance.DeleteShoppingListAdditionalItem(itemId))
            {
                this.RebindAdditionalItems();
                this.UpdatePanel2.Update();
            }
        }
    }

    protected void btnAddShoppingListItem_Click(object sender, EventArgs e)
    {
        // !!! Now that menu is no longer private - should we just leave it in view state and erase once the user leaves???
        string itemName = this.txtAddShoppingListItem.Text;
        if (!string.IsNullOrEmpty(itemName))
        {
            if (BusinessFacade.Instance.AddShoppingListAdditionalItem(this.MenuId, itemName))
            {
                this.RebindAdditionalItems();
                this.txtAddShoppingListItem.Text = string.Empty;
                //this.txtAddShoppingListItem.Focus();
                this.UpdatePanel2.Update();
            }
        }
    }

    private void RebindAdditionalItems()
    {
        var listAddItems = from el in BusinessFacade.Instance.GetShoppingListAdditionalItems(this.MenuId)
                           select new
                           {
                               ItemId = el.ShoppingListItemId,
                               ItemName = (el.GeneralItem != null ? el.GeneralItem.GeneralItemName : el.ItemName + " ● ")
                           };

        this.rptAdditionalItems.DataSource = listAddItems.ToArray();
        this.rptAdditionalItems.DataBind();
    }

    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", string.Format("window.open( 'PrintShopingList.aspx?menuId={0}', null, 'height=600,width=500,status=yes,toolbar=no,menubar=no,location=no, scrollbars=1' );", this.MenuId), true);
    //}

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        LinkButton lnkBtn = sender as LinkButton;        
      
        string subject = "רשימת קניות";

        StringWriter html = new StringWriter();
        Server.Execute("~/PrintShopingList.aspx?menuId=" + this.MenuId.ToString(), html);

        string to = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId).Email;

        try
        {
            ProperServices.Common.Mail.Mailer.SendMail(to, ProperControls.General.Utils.FromEmail, subject, html.ToString(), true);

            if (lnkBtn != null && lnkBtn.ID == "btnSendMail_top")
            {
                this.lblResultTop.Visible = true;
                this.lblResultTop.Text = "הרשימה נשלחה ל-" + to;
                this.upResultLabelTop.Update();        
            }
            else if (lnkBtn != null && lnkBtn.ID == "btnSendMail_bottom")
            {
                this.lblResultBottom.Visible = true;
                this.lblResultBottom.Text = "הרשימה נשלחה ל-" + to;
                this.upResultLabelBottom.Update();
            }
        }
        catch
        {
            if (lnkBtn != null && lnkBtn.ID == "btnSendMail_top")
            {
                this.lblResultTop.Visible = true;
                this.lblResultTop.Text = "בעיה בשליחה";
                this.upResultLabelTop.Update();                
            }
            else if (lnkBtn != null && lnkBtn.ID == "btnSendMail_bottom")
            {
                this.lblResultBottom.Visible = true;
                this.lblResultBottom.Text = "בעיה בשליחה";
                this.upResultLabelBottom.Update();
            }            
        }       
    }

    //protected void btnMenuMeals_Click(object sender, EventArgs e)
    //{
    //    string url;
    //    if (this.Master.IsFromHome)
    //    {
    //        url = string.Format("~/MenuMeals.aspx?menuId={0}&isfromhome={1}", this.MenuId, 1);
    //    }
    //    else
    //    {
    //        url = string.Format("~/MenuMeals.aspx?menuId={0}", this.MenuId);
    //    }
    //    this.Response.Redirect(url);
    //}

}
