using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;
using ProperControls.Pages;
using ProperServices.Common.Extensions;
using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;

namespace Wizard
{
    public partial class WizDefault : BasePage
    {
        public SRL_User CurrUser
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated == false)
                {
                    HttpContext.Current.Session["CurrUser"] = null;
                    return null;
                }
                if ((HttpContext.Current.Session != null) && (HttpContext.Current.Session["CurrUser"] == null))
                {

                    if (HttpContext.Current.User.Identity.IsAuthenticated == true)
                    {
                        User user = BusinessFacade.Instance.GetUserByUserName(HttpContext.Current.User.Identity.Name);

                        if (user != null)
                        {
                            HttpContext.Current.Session["CurrUser"] = new SRL_User(user);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }

                if (HttpContext.Current.Session != null)
                {
                    return (SRL_User)HttpContext.Current.Session["CurrUser"];
                }
                else
                {
                    return null;
                }

            }
            set
            {
                HttpContext.Current.Session["CurrUser"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindSlides();

            //if (this.CurrUser != null)
            //{
            //    Panel pnlConnected = (Panel)LoginView1.FindControl("pnlConnected");
            //    if (pnlConnected != null)
            //    {
            //        Label lblWelcome = (Label)pnlConnected.FindControl("lblWelcome");

            //        lblWelcome.Text = ("שלום " + this.CurrUser.Name).TrimToMax(20) + "!";

            //        if (this.CurrUser.UserType == 1)
            //        {
            //            HtmlGenericControl rowAdmin = (HtmlGenericControl)pnlConnected.FindControl("rowAdmin");
            //            rowAdmin.Visible = true;

            //        }

            //        HyperLink lnkFavRecipes = (HyperLink)pnlConnected.FindControl("lnkFavRecipes");
            //        HyperLink lnkFavMenus = (HyperLink)pnlConnected.FindControl("lnkFavMenus");
            //        lnkFavRecipes.NavigateUrl = "~/Recipes.aspx?page=1&orderby=LastUpdate&disp=MyFavoriteRecipes";
            //        lnkFavMenus.NavigateUrl = "~/Menus.aspx?page=1&orderby=LastUpdate&disp=MyFavoriteRecipes";

            //        Label lblFavRecipesNum = (Label)pnlConnected.FindControl("lblFavRecipesNum");
            //        Label lblFavMenusNum = (Label)pnlConnected.FindControl("lblFavMenusNum");
            //        Label lblSelectedRecipesNum = (Label)pnlConnected.FindControl("lblSelectedRecipesNum");
            //        lblSelectedRecipesNum.Text = "(" + Utils.SelectedRecipes.Count.ToString() + ")";
            //        //User currentUser = BusinessFacade.Instance.GetUserEx(((BasePage)this.Page).UserId);
            //        //if (currentUser != null)
            //        //{
            //        lblFavRecipesNum.Text = "(" + Utils.FavoriteRecipesAdded.Count + ")";
            //        lblFavMenusNum.Text = "(" + Utils.FavoriteMenusAdded.Count + ")";
            //        //}     
            //    }
            //}

            this.BindRecentMenusAndRecipes();
        }

        private void BindSlides()
        {
            List<SlideInfo> slides = new List<SlideInfo>();
            string baseUrl = ResolveUrl("~/Images/slides");
            string dirName = Server.MapPath(baseUrl);
            DirectoryInfo di = new DirectoryInfo(dirName);
            FileInfo[] filenames = di.GetFiles("*.jpg");

            foreach (FileInfo fi in filenames)
            {
                slides.Add(new SlideInfo()
                               {
                                   Source = baseUrl + "/" + fi.Name
                               });
            }

            rptSlides.DataSource = slides;
            rptSlides.DataBind();
        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            /*
        Panel pnlLoginbox = (Panel)LoginView1.FindControl("pnlLoginbox");
        Login MasterLogin = (Login)pnlLoginbox.FindControl("MasterLogin");
        CheckBox RememberMe = (CheckBox)MasterLogin.FindControl("RememberMe");
        FormsAuthentication.RedirectFromLoginPage(MasterLogin.UserName, RememberMe.Checked);

        if (Request.Url.AbsoluteUri.Contains("default.aspx"))
        {
            Response.Redirect("~/Users/PersonalArea.aspx");
        }
        else if (!string.IsNullOrEmpty(Request["MenuId"]))
        {
            int menuId = int.Parse(this.Request["MenuId"]);
            //  User currUser = BusinessFacade.Instance.GetUserByUserName(MasterLogin.Name);
            //            BusinessFacade.Instance.UpdateMenuUser(menuId, currUser.UserId);
        }
      * */
        }

        protected void MasterLoginStatus_LoggedOut(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage("~/Default.aspx");
        }

        protected void BindRecentMenusAndRecipes()
        {
            MBLSettingsWrapper settingsInfo = BusinessFacade.Instance.GetMBLSettingsWrapper();
            int[] recentRecipes = settingsInfo.RecentRecipes;
            int[] recentMenus = settingsInfo.RecentMenus;

            if (recentRecipes != null)
            {
                this.RebindRecentRecipes(recentRecipes[0], recentRecipes[1]);
            }

            if (recentMenus != null)
            {
                this.RebindRecentMenus(recentMenus[0], recentMenus[1]);
            }
        }

        protected void RebindRecentRecipes(int recipeId1, int recipeId2)
        {
            Recipe recipe1 = BusinessFacade.Instance.GetRecipe(recipeId1);
            Recipe recipe2 = BusinessFacade.Instance.GetRecipe(recipeId2);

            this.rptRecentRecipes.DataSource = new Recipe[2] { recipe1, recipe2 };
            this.rptRecentRecipes.DataBind();
        }

        protected void RebindRecentMenus(int menuId1, int menuId2)
        {
            MyBuyList.Shared.Entities.Menu menu1 = BusinessFacade.Instance.GetMenu(menuId1);
            MyBuyList.Shared.Entities.Menu menu2 = BusinessFacade.Instance.GetMenu(menuId2);

            this.rptRecentMenus.DataSource = new MyBuyList.Shared.Entities.Menu[2] { menu1, menu2 };
            this.rptRecentMenus.DataBind();
        }

        protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Recipe recipe;
            MyBuyList.Shared.Entities.Menu menu;

            recipe = e.Item.DataItem as Recipe;

            if (recipe == null)
            {
                menu = e.Item.DataItem as MyBuyList.Shared.Entities.Menu;
                HyperLink lblMenuName = e.Item.FindControl("lblMenuName") as HyperLink;
                if (lblMenuName != null)
                {
                    lblMenuName.NavigateUrl += menu.MenuId;
                    if (menu.MenuName.Length > 23)
                    {
                        lblMenuName.Text = menu.MenuName.TrimToMax(23);
                    }
                    else
                    {
                        lblMenuName.Text = menu.MenuName;
                    }
                }

                HyperLink lnkRecentMenuPic = e.Item.FindControl("lnkRecentMenuPic") as HyperLink;
                if (lnkRecentMenuPic != null)
                {
                    lnkRecentMenuPic.NavigateUrl += menu.MenuId;
                }

                Image imgThumbnail = e.Item.FindControl("imgThumbnail") as Image;
                if (imgThumbnail != null)
                {
                    imgThumbnail.ImageUrl = ResolveUrl(menu.Picture != null ? string.Format("~/ShowPicture.ashx?MenuId={0}", menu.MenuId) : "~/Images/Img_Default.jpg");
                }

            }
            else
            {
                HyperLink lblRecipeName = e.Item.FindControl("lblRecipeName") as HyperLink;
                if (lblRecipeName != null)
                {
                    lblRecipeName.NavigateUrl += recipe.RecipeId;
                    if (recipe.RecipeName.Length > 23)
                    {
                        lblRecipeName.Text = recipe.RecipeName.TrimToMax(23);
                    }
                    else
                    {
                        lblRecipeName.Text = recipe.RecipeName;
                    }
                }

                HyperLink lnkRecentRecipePic = e.Item.FindControl("lnkRecentRecipePic") as HyperLink;
                if (lnkRecentRecipePic != null)
                {
                    lnkRecentRecipePic.NavigateUrl += recipe.RecipeId;
                }

                Image imgThumbnail = e.Item.FindControl("imgThumbnail") as Image;
                if (imgThumbnail != null)
                {
                    imgThumbnail.ImageUrl = ResolveUrl(recipe.Picture != null ? string.Format("~/ShowPicture.ashx?RecipeId={0}", recipe.RecipeId) : "~/Images/Img_Default.jpg");
                }
            }
        }
    }

    public class SlideInfo
    {
        public string Source { get; set; }
    }
}