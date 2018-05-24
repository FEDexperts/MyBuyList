using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ACAWebThumbLib;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;

using ProperControls.Pages;
using ProperControls.General;

using Resources;

public partial class PageRecipeDetails : BasePage
{
    protected string FBUrl;

    public int RecipeId
    {
        get { return ViewState["RecipeId"] == null ? 0 : (int)ViewState["RecipeId"]; }
        set { ViewState["RecipeId"] = value; }
    }
    int ViewId
    {
        get { return ViewState["ViewId"] == null ? 0 : (int)ViewState["ViewId"]; }
        set { ViewState["ViewId"] = value; }
    }
    string menuId
    {
        get { return ViewState["menuId"] == null ? string.Empty : (string)ViewState["menuId"]; }
        set { ViewState["menuId"] = value; }
    }

    string lastPage
    {
        get { return ViewState["lastPage"] == null ? string.Empty : (string)ViewState["lastPage"]; }
        set { ViewState["lastPage"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PlaceHolder1.Visible = false;
        PlaceHolder2.Visible = false;

        //this.ucRecipe.RefreshData += new ucRecipe.RefreshHandler(this.RefreshPage);

        //SetDrives();
        //SetCategories();

        if (!this.IsPostBack)
        {
            FBUrl = Request.Url.AbsoluteUri;

            if (string.IsNullOrEmpty(this.Request["recipeId"]))
            {
                AppEnv.MoveToDefaultPage();
            }

            if (Request.UrlReferrer != null)
            {
                int indexEnd;
                int indexStart;
                int length;

                if (Request.UrlReferrer.AbsoluteUri.Contains("menuId="))
                {
                    indexEnd = Request.UrlReferrer.AbsoluteUri.IndexOf("&");
                    indexStart = Request.UrlReferrer.AbsoluteUri.IndexOf("menuId=") + "menuId=".Length;
                    length = indexEnd - indexStart;
                    if (length > 0)
                    {
                        this.menuId = Request.UrlReferrer.AbsoluteUri.Substring(indexStart, length);

                    }
                    else
                    {

                        this.menuId = Request.UrlReferrer.AbsoluteUri.Substring(indexStart);
                    }
                }

                indexStart = Request.UrlReferrer.AbsoluteUri.LastIndexOf("/") + 1;
                indexEnd = Request.UrlReferrer.AbsoluteUri.IndexOf("?");
                length = indexEnd - indexStart;
                if (length > 0)
                {
                    this.lastPage = Request.UrlReferrer.AbsoluteUri.Substring(indexStart, length);
                }
                else
                {
                    this.lastPage = Request.UrlReferrer.AbsoluteUri.Substring(indexStart);
                }
            }

            
            int recipeId = int.Parse(this.Request["recipeId"]);
            Recipe currRecipe = BusinessFacade.Instance.GetRecipe(recipeId);
            if (currRecipe == null)
            {
                AppEnv.MoveToDefaultPage();
            }

            if ((((BasePage)Page).UserType != 1) && (currRecipe.UserId != ((BasePage)Page).UserId) && (!currRecipe.IsPublic))
            {
                AppEnv.MoveToDefaultPage();
            }
            else
            {

                RecipeId = recipeId;
                Page.Title = string.Format(" ארגון רשימת הקניות שלך - MyBuyList - {0}", currRecipe.RecipeName);
                PageDescription.Attributes["content"] = string.Format("פרטי מתכון - {0} מאת {1}", currRecipe.RecipeName, currRecipe.User.DisplayName);

                if (!string.IsNullOrEmpty(Request["view"]))
                {
                    ViewId = int.Parse(Request["view"]);
                }
                btnRecipe.NavigateUrl = "~/PrintRecipe.aspx?recipeId=" + RecipeId;
                btnRecipe_bottom.NavigateUrl = "~/PrintRecipe.aspx?recipeId=" + RecipeId;

                Rebind();
            }
        }
    }

    protected void NutValue_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RecipeTotalNutValues nutValueItem = (RecipeTotalNutValues)e.Item.DataItem;
            HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("tdRepeater");
            string colorCode = "";
            switch (nutValueItem.NutCategoryId)
            {
                case 1:
                    colorCode = "#EE1E3E";
                    break;
                case 2:
                    colorCode = "#FBAB14";
                    break;
                case 3:
                    colorCode = "#A4CB3A";
                    break;
                case 4:
                    colorCode = "#1A98D5";
                    break;
                case 5:
                    colorCode = "#656565";
                    break;
                default:
                    colorCode = "#656565";
                    break;
            }
            td.Style["background-color"] = colorCode;
        }
    }

    private void RefreshPage()
    {
        this.Response.Redirect(this.Request.Url.OriginalString);
    }

    private void Rebind()
    {
        Recipe recipe = BusinessFacade.Instance.GetRecipe(this.RecipeId);
        if (recipe != null)
        {
            PlaceHolder1.Visible = ((BasePage)Page).UserId == recipe.UserId;
            PlaceHolder2.Visible = ((BasePage)Page).UserId == recipe.UserId;

            this.lblRecipeName.Text = recipe.RecipeName;
            this.lblServNumber.Text = recipe.Servings.ToString();

            if (recipe.Tags != null)
            {
                this.lblRecipeTags.Text = recipe.Tags;
            }
            if (recipe.Description != null)
            {
                this.lblRecipeDescription.Text = recipe.Description;
            }

            if (recipe.DifficultyLevel != null)
            {
                this.txtDifficulty.Text = this.GetDifficultyLevelString(recipe.DifficultyLevel.Value);
            }

            if (recipe.PreperationTime != null)
            {
                string units;
                this.lblPrepTime.Text = this.GetTimeInCorrectUnits(recipe.PreperationTime.Value, out units).ToString() + " " + units;
            }

            if (recipe.CookingTime != null)
            {
                string units;
                this.lblCookTime.Text = this.GetTimeInCorrectUnits(recipe.CookingTime.Value, out units).ToString() + " " + units;
            }

            if (!string.IsNullOrEmpty(recipe.Remarks))
            {
                this.lblRemarks.Text = recipe.Remarks;
            }
            else
            {
                this.recipe_remarks.Visible = false;
            }

            if (!string.IsNullOrEmpty(recipe.PreparationMethod))
            {
                this.txtPreparationMethod.Text = recipe.PreparationMethod.Replace("\n", "<br />");
            }

            if (!string.IsNullOrEmpty(recipe.Tools))
            {
                this.txtTools.Text = recipe.Tools.Replace("\n", "<br />");
            }

            bool isInMyFavorites = (recipe.UserFavoriteRecipes.SingleOrDefault(ufr => ufr.UserId == ((BasePage)this.Page).UserId) != null);
            if (isInMyFavorites)
            {
                this.myFavoritesTopTag.Visible = true;
            }
            else
            {
                this.myFavoritesTopTag.Visible = false;
            }

            lblAllFavorites.Text = recipe.UserFavoriteRecipes.Count.ToString();

            lblAllMenus.Text = recipe.MenuRecipes.Count.ToString();

            string str = "";
            foreach (RecipeCategory rc in recipe.RecipeCategories)
            {
                str += rc.Category.CategoryName + ", ";
            }
            str = str.Remove(str.Length - 2);
            this.lblRecipeCategories.Text = str;

            dlistIngredients.DataSource = recipe.Ingredients;
            dlistIngredients.DataBind();

            bool allowRecipeEdit = (bool)((recipe.UserId == ((BasePage)Page).UserId) || (((BasePage)Page).UserType == 1));
            lblEditRecipeSeparator.Visible = allowRecipeEdit;
            lblEditRecipeSeparator_bottom.Visible = allowRecipeEdit;
            //btnEditRecipe.Visible = allowRecipeEdit;
            //btnEditRecipe_bottom.Visible = allowRecipeEdit;
            lblCopyRecipeSeperator.Visible = allowRecipeEdit;
            lblCopyRecipeSeperator_bottom.Visible = allowRecipeEdit;
            btnDeleteRecipe.Visible = allowRecipeEdit;
            btnDeleteRecipe_bottom.Visible = allowRecipeEdit;

            UserFavoriteRecipe ufrep = recipe.UserFavoriteRecipes.SingleOrDefault(ufr => ufr.RecipeId == recipe.RecipeId &&
                                                                                         ufr.UserId == ((BasePage)Page).UserId);
            bool isValidUser = (((BasePage)Page).UserId != -1);
            bool existInFavorites = (ufrep != null);
            lblAddToFavoritesSeparator.Visible = isValidUser;
            lblAddToFavoritesSeparator_bottom.Visible = isValidUser;
            btnAddRecipeToFavorites.Visible = ((!existInFavorites) && isValidUser);            
            btnAddRecipeToFavorites_bottom.Visible = ((!existInFavorites) && isValidUser);
            btnRemoveRecipeFromFavorites.Visible = ((existInFavorites) && isValidUser);
            btnRemoveRecipeFromFavorites_bottom.Visible = ((existInFavorites) && isValidUser);

            if ((isValidUser) && (((BasePage)Page).UserType == 1))
            //if ((isValidUser))
            {
                btnCopyRecipe.Visible = true;
                btnCopyRecipe_bottom.Visible = true;
                lblSeparator3.Visible = true;
                lblSeparator3_bottom.Visible = true;
            }

            //this.btnSendMail.Visible = isValidUser;
            //this.lblSeparator1.Visible = isValidUser;

            //if (allowRecipeEdit && recipe.MenuRecipes.Count > 0)
            if (allowRecipeEdit && recipe.MenuRecipes.Count > 0 &&((BasePage)Page).UserType != 1)
            {
                btnDeleteRecipe.Visible = false;
                btnDeleteRecipe_bottom.Visible = false;
                lblDeleteRecipeDisabled.Visible = true;
                lblDeleteRecipeDisabled_bottom.Visible = true;
                lblDeleteRecipeDisabled.ToolTip = "לא ניתן למחוק את המתכון משום שהוא קיים בתפריט/ים. אם ברצונך למחוק את המתכון - נא ליצור קשר עם מנהלת האתר.";
                lblDeleteRecipeDisabled_bottom.ToolTip = "לא ניתן למחוק את המתכון משום שהוא קיים בתפריט/ים. אם ברצונך למחוק את המתכון - נא ליצור קשר עם מנהלת האתר";
                //btnEditRecipe.Visible = false;
                //btnEditRecipe_bottom.Visible = false;
                lblEditRecipeDisabled.Visible = true;
                lblEditRecipeDisabled_bottom.Visible = true;
                lblEditRecipeDisabled.ToolTip = "לא ניתן לערוך את המתכון משום שהוא קיים בתפריט/ים. אם ברצונך לערוך את המתכון - נא ליצור קשר עם מנהלת האתר.";
                lblEditRecipeDisabled_bottom.ToolTip = "לא ניתן לערוך את המתכון משום שהוא קיים בתפריט/ים. אם ברצונך לערוך את המתכון - נא ליצור קשר עם מנהלת האתר";
            }

            Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;
            bool isSelectedRecipe = (selectedRecipes.Keys.Contains(recipe.RecipeId));

            if (!isSelectedRecipe)
            {
                this.blkAddRemove.Text = "הוסף לרשימת קניות";
                this.blkAddRemove_bottom.Text = "הוסף לרשימת קניות";
            }
            else
            {
                this.blkAddRemove.Text = "הסר מרשימת קניות";
                this.blkAddRemove_bottom.Text = "הסר מרשימת קניות";
                this.blkAddRemove.Style["color"] = "Red";
                this.blkAddRemove_bottom.Style["color"] = "Red";
            }

            bool isCompleteCalculation = false;
            RecipeTotalNutValues[] nutritionalValues = BusinessFacade.Instance.GetRecipeTotalNutValues(this.RecipeId, out isCompleteCalculation);
            if (nutritionalValues == null || nutritionalValues.Length == 0 || !isCompleteCalculation)            
            {
                this.divNutritionalValues.Visible = false;
                this.txtNoDataForNutritionalValues.Visible = true;
                this.txtNoDataForNutritionalValues.Text = MyGlobalResources.NoDataForNutritionalValues.Replace("\n", "<br>");
            }
            else
            {
                if (rptNutritionalValues != null)
                {
                    this.rptNutritionalValues.DataSource = nutritionalValues;
                    this.rptNutritionalValues.DataBind();
                    this.rptNutritionalValues1.DataSource = nutritionalValues;
                    this.rptNutritionalValues1.DataBind();
                }
            }

            if (recipe.Picture != null)
            {
                this.imgRecipePicture.ImageUrl = "~/ShowPicture.ashx?RecipeId=" + recipe.RecipeId;
            }
            else
            {
                this.imgRecipePicture.ImageUrl = "~/Images/Img_Default.jpg"; 
            }

            if (!string.IsNullOrEmpty(recipe.VideoLink) && recipe.VideoLink.Contains("object") && recipe.VideoLink.Contains("embed"))
            {
                //adjustment method may not work for embedded videos that are not from youtube.
                this.recipe_video.InnerHtml = this.AdjustVideo(recipe.VideoLink);
            }
            else
            {
                this.recipe_video.Visible = false;
            }

            this.lnkPublisher.Text = recipe.User.DisplayName;
            this.lblPublishDate.Text = recipe.ModifiedDate.ToString("dd/MM/yyyy");

            string userEmail = string.Empty;
            MyBuyList.Shared.Entities.User currentUser = BusinessFacade.Instance.GetUser(((BasePage)this.Page).UserId);
            if (currentUser != null)
            {
                userEmail = currentUser.Email;
            }
            this.ucSendMailToFriend.BindItemDetails("Recipe", recipe.RecipeId, recipe.RecipeName, userEmail);

            UpdatePanel2.Update();
            UpdatePanel1.Update();
        }
    }

    private void RefreshTopTags()
    {
        Recipe recipe = BusinessFacade.Instance.GetRecipe(this.RecipeId);
        if (recipe != null)
        {
            bool isInMyFavorites = (recipe.UserFavoriteRecipes.SingleOrDefault(ufr => ufr.UserId == ((BasePage)this.Page).UserId) != null);
            if (isInMyFavorites)
            {
                this.myFavoritesTopTag.Visible = true;
            }
            else
            {
                this.myFavoritesTopTag.Visible = false;
            }

            this.lblAllFavorites.Text = recipe.UserFavoriteRecipes.Count.ToString();

            this.lblAllMenus.Text = recipe.MenuRecipes.Count.ToString();
        }
    }

    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    string url;
    //    if (this.ViewId >= (int)PersonalAreaViewEnum.SimpleSearch)
    //    {
    //        url = "~/" + this.lastPage;

    //        if (this.menuId != string.Empty)
    //        {
    //            if (this.ViewId == (int)PersonalAreaViewEnum.CategoriesSearch)
    //            {
    //                if (!string.IsNullOrEmpty(this.Request["categoryId"]))
    //                {
    //                    url += string.Format("?menuId={0}&view={1}&categoryId={2}", this.menuId, this.ViewId, this.Request["categoryId"]);
    //                }
    //                else
    //                {
    //                    url += string.Format("?menuId={0}&view={1}", this.menuId, this.ViewId);
    //                }
    //            }
    //            else if (this.ViewId == (int)PersonalAreaViewEnum.SimpleSearch)
    //            {
    //                if (!string.IsNullOrEmpty(this.Request["text"]))
    //                {
    //                    url += string.Format("?menuId={0}&view={1}&text={2}", this.menuId, this.ViewId, this.Request["text"]);
    //                }
    //                else
    //                {
    //                    url += string.Format("?menuId={0}&view={1}", this.menuId, this.ViewId);
    //                }
    //            }
    //            else if (this.ViewId == (int)PersonalAreaViewEnum.ComplexSearch)
    //            {
    //                //if ((!string.IsNullOrEmpty(this.Request["text"])) && (!string.IsNullOrEmpty(this.Request["servings"])))
    //                // {
    //                url += string.Format("?menuId={0}&view={1}&text={2}&servings={3}", this.menuId, this.ViewId, this.Request["text"], this.Request["servings"]);
    //                //}
    //                //else if (!string.IsNullOrEmpty(this.Request["text"]))
    //                //{
    //                //    url = string.Format("~/MenuRecipes.aspx?menuId={0}&view={1}&text={2}", this.menuId, this.ViewId, this.Request["text"]);
    //                //}
    //                //else if (!string.IsNullOrEmpty(this.Request["servings"]))
    //                //{
    //                //    url = string.Format("~/MenuRecipes.aspx?menuId={0}&view={1}&servings={2}", this.menuId, this.ViewId, this.Request["servings"]);
    //                //}
    //                //else
    //                //{
    //                //    url = string.Format("~/MenuRecipes.aspx?menuId={0}&view={1}", this.menuId, this.ViewId);
    //                //}
    //            }
    //            else
    //            {
    //                url += string.Format("?menuId={0}&view={1}", this.menuId, this.ViewId);
    //            }
    //        }
    //        else
    //        {
    //            if (this.ViewId == (int)PersonalAreaViewEnum.CategoriesSearch)
    //            {
    //                if (!string.IsNullOrEmpty(this.Request["categoryId"]))
    //                {
    //                    url += string.Format("?view={0}&categoryId={1}", this.ViewId, this.Request["categoryId"]);
    //                }
    //                else
    //                {
    //                    url += string.Format("?view={0}", this.ViewId);
    //                }
    //            }
    //            else if (this.ViewId == (int)PersonalAreaViewEnum.SimpleSearch)
    //            {
    //                if (!string.IsNullOrEmpty(this.Request["text"]))
    //                {
    //                    url += string.Format("?view={0}&text={1}", this.ViewId, this.Request["text"]);
    //                }
    //                else
    //                {
    //                    url += string.Format("?view={0}", this.ViewId);
    //                }
    //            }
    //            else if (this.ViewId == (int)PersonalAreaViewEnum.ComplexSearch)
    //            {
    //                url += string.Format("?view={0}&text={1}&servings={2}", this.ViewId, this.Request["text"], this.Request["servings"]);
    //            }
    //            else
    //            {
    //                url += string.Format("?view={0}", this.ViewId);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        url = string.Format("~/Users/PersonalArea.aspx?view={0}", this.ViewId);
    //    }

    //    this.Response.Redirect(url);
    //}

    protected void blkAddRemove_Click(object sender, EventArgs e)
    {
        Recipe recipe = BusinessFacade.Instance.GetRecipe(this.RecipeId);

        string preStr = "";

        Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;

        if (selectedRecipes.Keys.Contains(recipe.RecipeId))
        {
            selectedRecipes.Remove(this.RecipeId);
            Utils.SelectedRecipes = selectedRecipes;
            this.blkAddRemove.Text = "הוסף לרשימת קניות";
            this.blkAddRemove_bottom.Text = "הוסף לרשימת קניות";
            this.blkAddRemove.Style["color"] = "#656565";
            this.blkAddRemove_bottom.Style["color"] = "#656565";
            preStr = "מרשימת הקניות / תפריט הוסר המתכון:";
        }
        else
        {
            selectedRecipes.Add(this.RecipeId, recipe);
            Utils.SelectedRecipes = selectedRecipes;
            this.blkAddRemove.Text = "הסר מרשימת קניות";
            this.blkAddRemove_bottom.Text = "הסר מרשימת קניות";
            this.blkAddRemove.Style["color"] = "Red";
            this.blkAddRemove_bottom.Style["color"] = "Red";
            preStr = "לרשימת הקניות / תפריט התווסף המתכון:";
        }

        this.UpdatePanel1.Update();

        UserControl uc = ((this.Master)).FindControl("HeaderControl1") as UserControl;

        if (uc != null)
        {
            Label lbl = uc.FindControl("lblSelectedRecipesNum") as Label;
            if (lbl != null)
            {
                lbl.Text = "(" + Utils.SelectedRecipes.Count.ToString() + ")";
            }

            UpdatePanel up = uc.FindControl("upSelectedRecipesList") as UpdatePanel;
            if (up != null)
            {
                up.Update();
            }
        }

        ((BasePage)this.Page).DisplayMessage = preStr + "<br/>" + recipe.RecipeName;
    }

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        //StringWriter html = new StringWriter();
        //Server.Execute("~/PrintRecipe.aspx?recipeId=" + this.RecipeId.ToString(), html);
        //string to = BusinessFacade.Instance.GetUser(((BasePage)Page).UserId).Email;
        //try
        //{
        //    ProperServices.Common.Mail.Mailer.SendMail(to, ProperControls.General.Utils.FromEmail, BusinessFacade.Instance.GetRecipe(this.RecipeId).RecipeName, html.ToString(), true);

        //    this.lblResult.Visible = true;
        //    this.lblResult.Text = "המתכון נשלח ל-" + to;
        //}
        //catch
        //{
        //    this.lblResult.Visible = true;
        //    this.lblResult.Text = "בעיה בשליחה";
        //}        
    }

    protected void btnDeleteRecipe_Click(object sender, EventArgs e)
    {
        if (BusinessFacade.Instance.DeleteRecipe(this.RecipeId))
        {
            Response.Redirect("~/Recipes.aspx");
        }
    }

    protected void ucSendMailToFriend_EmailSent(object sender, SendToFriendEventArgs e)
    {
        ((BasePage)this.Page).DisplayMessage = "אימייל נשלח ל- " + e.recipentName ;
    }

    protected void btnEditRecipe_Click(object sender, EventArgs e)
    {
        //Response.Redirect(string.Format("~/RecipeEdit.aspx?recipeId={0}", this.RecipeId));
    }

    protected void btnCopyRecipe_Click(object sender, EventArgs e)
    {
        //this.ucRecipe.CopyRecipe(this.RecipeId);
        Response.Redirect(string.Format("~/RecipeEdit.aspx?recipeId={0}&docopy=1", this.RecipeId));
    }

    protected void btnAddRecipeToFavorites_Click(object sender, EventArgs e)
    {
        int favRecipesNum = 0;
        if (BusinessFacade.Instance.AddRecipeToUserFavorites(((BasePage)Page).UserId, this.RecipeId, out favRecipesNum))
        {
            this.btnAddRecipeToFavorites.Visible = false;
            this.btnAddRecipeToFavorites_bottom.Visible = false;
            this.btnRemoveRecipeFromFavorites.Visible = true;
            this.btnRemoveRecipeFromFavorites_bottom.Visible = true;
            this.RefreshTopTags();
            this.upTopTags.Update();

            List<int> favRecipesList = Utils.FavoriteRecipesAdded;
            favRecipesList.Add(this.RecipeId);
            Utils.FavoriteRecipesAdded = favRecipesList;

            UserControl uc = ((this.Master)).FindControl("HeaderControl1") as UserControl;

            if (uc != null)
            {
                Label lbl = uc.FindControl("lblFavRecipesNum") as Label;
                if (lbl != null)
                {
                    lbl.Text = "(" + Utils.FavoriteRecipesAdded.Count + ")";
                }

                UpdatePanel up = uc.FindControl("upFavorites") as UpdatePanel;
                if (up != null)
                {
                    up.Update();
                }
            }
        }        
    }

    protected void btnRemoveRecipeFromFavorites_Click(object sender, EventArgs e)
    {
        int favRecipesNum = 0;
        if (BusinessFacade.Instance.RemoveUserFavoritesRecipe(((BasePage)Page).UserId, this.RecipeId, out favRecipesNum))
        {
            this.btnAddRecipeToFavorites.Visible = true;
            this.btnAddRecipeToFavorites_bottom.Visible = true;
            this.btnRemoveRecipeFromFavorites.Visible = false;
            this.btnRemoveRecipeFromFavorites_bottom.Visible = false;
            this.RefreshTopTags();
            this.upTopTags.Update();

            List<int> favRecipesList = Utils.FavoriteRecipesAdded;
            if (favRecipesList.Contains(this.RecipeId))
            {
                favRecipesList.Remove(this.RecipeId);
            }
            Utils.FavoriteRecipesAdded = favRecipesList;

            UserControl uc = ((this.Master)).FindControl("HeaderControl1") as UserControl;

            if (uc != null)
            {
                Label lbl = uc.FindControl("lblFavRecipesNum") as Label;
                if (lbl != null)
                {
                    lbl.Text = "(" + Utils.FavoriteRecipesAdded.Count + ")";
                }

                UpdatePanel up = uc.FindControl("upFavorites") as UpdatePanel;
                if (up != null)
                {
                    up.Update();
                }
            }
        }        
    }

    //protected void ShowCategories_Click(int recipeId, SRL_RecipeCategory[] arr)
    //{
    //    this.ucRecipeCats.ShowCategories(recipeId, arr);
    //}
    //protected void SelectPicture_Click(int recipeId)
    //{
    //    this.ucRecipePicture.ShowPicture(recipeId);
    //}

    //protected void RecipePicture_Updated(Binary picture)
    //{
    //    this.ucRecipe.UpdatePicture(picture);
    //}

    //protected void RecipeCategories_RefreshData(SRL_RecipeCategory[] arr)
    //{
    //    this.ucRecipe.RefreshCategories(arr);
    //} 

    protected string AdjustVideo(string embeddedVideoString)
    {
        string str = embeddedVideoString;
        int embedIndex = str.IndexOf("embed");
        int heightIndex = str.IndexOf("height=", embedIndex);
        string heightValue = str.Substring(heightIndex + "height=".Length + 1, 3);
        int widthIndex = str.IndexOf("width=", embedIndex);
        string widthValue = str.Substring(widthIndex + "width=".Length + 1, 3);
        str = str.Replace(widthValue, "443");
        str = str.Replace(heightValue, "360");
        return str;
    }

    private string GetDifficultyLevelString(int level)
    {
        string str = "";

        switch (level)
        {
            case 1:
                str = "קל";
                break;
            case 2:
                str = "בינוני";
                break;
            case 3:
                str = "קשה";
                break;
        }

        return str;
    }

    private decimal GetTimeInCorrectUnits(int time, out string unitString)
    {
        decimal timeInCorrectUnits = 0;
        unitString = "דקות";

        if (time < 120)
        {
            timeInCorrectUnits = (decimal)time;
        }
        else if (time >= 120 && time < 600)
        {
            if (time % 30 == 0)
            {
                int temp = time / 30;
                timeInCorrectUnits = (decimal)temp / 2;
                unitString = "שעות";
            }
            else
            {
                timeInCorrectUnits = (decimal)time;
            }
        }
        else
        {
            int temp = time / 30;
            timeInCorrectUnits = (decimal)temp / 2;
            unitString = "שעות";
        }

        return timeInCorrectUnits;
    }

    protected void SaveImage(object sender, EventArgs e)
    {
        Recipe currRecipe = BusinessFacade.Instance.GetRecipe(RecipeId);
        string category = "ReciepsScreenShots";

        string savePath = Server.MapPath(string.Format(@"~\Images\{0}\", category));

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        string t_strLargeImage = string.Format(@"{0}{1}.jpg", savePath, currRecipe.RecipeName);

        // Create instance
        ThumbMakerClass t_xThumbMaker = new ThumbMakerClass();

        t_xThumbMaker.SetURL(string.Format("http://{0}{1}/ScreenShotRecipe.aspx?recipeId={2}&action=screenshot", Request.Url.Host,
                                           Request.ApplicationPath, RecipeId));
        t_xThumbMaker.SetRegInfo("KRMAXARQW-XTABNYBXW-KMQXRWMKB-BNTQABQTE");
        t_xThumbMaker.StartSnap();

        // Save the image with full size in C#
        bool saveImage = t_xThumbMaker.SaveImage(t_strLargeImage);

        if (saveImage)
        {
            Response.Redirect(string.Format("ScreenShotRecipe.aspx?RecipeId={0}", RecipeId));
        }
    }
}


