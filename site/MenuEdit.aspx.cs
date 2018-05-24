using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Drawing;

using MyBuyList.BusinessLayer.Managers;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;

using ProperControls.Pages;
using ProperControls.General;

using Resources;


public partial class MenuEdit : BasePage
{
    private static List<string> _comments;

    protected int? MenuId
    {
        get
        {
            int menuId;

            if (string.IsNullOrEmpty(Request["menuId"]) || !int.TryParse(Request["menuId"], out menuId))
                return null;
            else
                return menuId;
        }
    }

    //change to save Menu to EntityState instead of Session
    MyBuyList.Shared.Entities.Menu CurrentMenu
    {
        get { return HttpContext.Current.Session["currentMenu"] as MyBuyList.Shared.Entities.Menu; }
        set { HttpContext.Current.Session["currentMenu"] = value; }
    }

    MealDayOfWeek[] MealDayOfWeek
    {
        get
        {
            MealDayOfWeek[] array = new MealDayOfWeek[] 
            {
                new MealDayOfWeek(1, MyGlobalResources.Sunday),
                new MealDayOfWeek(2, MyGlobalResources.Monday),
                new MealDayOfWeek(3, MyGlobalResources.Tuesday),
                new MealDayOfWeek(4, MyGlobalResources.Wednesday),
                new MealDayOfWeek(5, MyGlobalResources.Thursday),
                new MealDayOfWeek(6, MyGlobalResources.Friday),
                new MealDayOfWeek(7, MyGlobalResources.Saturday)
            };

            return array;
        }
    }

    [Serializable]
    public class CourseOrMealType
    {
        public int CourseOrMealTypeId { get; set; }
        public string CourseOrMealTypeName { get; set; }

        public CourseOrMealType(int id, string name)
        {
            CourseOrMealTypeId = id;
            CourseOrMealTypeName = name;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (_comments == null) _comments = new List<string>() {"","","","","","",""};
        if (!IsPostBack)
        {
            _comments[0] = "";
            _comments[1] = "";
            _comments[2] = "";
            _comments[3] = "";
            _comments[4] = "";
            _comments[5] = "";
            _comments[6] = "";
            if (MenuId.HasValue)
            {
                EditMenu((int)MenuId);
            }
            else
            {
                NewMenu();
            }
        }

        //ClientScript.RegisterClientScriptInclude("MenuEditDnDScript", "Scripts/MenuEditDnD.js");
        //ScriptManager.RegisterStartupScript(Page, GetType(), "InitiateDragAndDrop", "startDnD()", true);      
    }

    protected void NewMenu()
    {
        MyBuyList.Shared.Entities.Menu menu = new MyBuyList.Shared.Entities.Menu();

        Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;

        if (selectedRecipes != null && selectedRecipes.Count != 0)
        {
            foreach (KeyValuePair<int, Recipe> r in selectedRecipes)
            {
                MenuRecipe recipe = new MenuRecipe();
                recipe.RecipeId = r.Key;
                recipe.Recipe = r.Value;
                recipe.Menu = this.CurrentMenu;
                menu.MenuRecipes.Add(recipe);
            }
        }

        menu.MenuId = 0;
        menu.MenuTypeId = 1;

        this.CurrentMenu = menu;

        this.RebindMenuRecipes();

        this.rbnCategoryOneMeal.Checked = true;
        this.rbnCategoryWeekly.Checked = false;
        this.TablesTitleImg.ImageUrl = "Images/SubHeader_MeatDetails.png";
        this.divNumDiners.Visible = true;

        this.RebindMealsDetails();

        this.BindMenuCategories();
    }

    protected void EditMenu(int menuId)
    {
        try
        {
            titleImg.ImageUrl = "~/Images/Header_EditMenu.png";

            MyBuyList.Shared.Entities.Menu menu = BusinessFacade.Instance.GetMenuEx(menuId);

            if (menu != null)
            {
                if ((((BasePage)Page).UserType != 1) && (menu.UserId != ((BasePage)Page).UserId) && !(menu.IsPublic) || menu.IsDeleted)
                {
                    AppEnv.MoveToDefaultPage();
                }

                Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;

                foreach (MenuRecipe mr in menu.MenuRecipes)
                {
                    selectedRecipes.Add(mr.RecipeId, mr.Recipe);
                }

                if (selectedRecipes != null && selectedRecipes.Count != 0)
                {
                    foreach (KeyValuePair<int, Recipe> r in selectedRecipes)
                    {
                        MenuRecipe menuRecipe = new MenuRecipe { RecipeId = r.Key, Recipe = r.Value, Menu = CurrentMenu };

                        bool containsRecipe = false;
                        foreach (MenuRecipe mr in menu.MenuRecipes)
                        {
                            if (mr.RecipeId == r.Key)
                            {
                                containsRecipe = true;
                                r.Value.Servings = mr.Recipe.Servings;

                                MealRecipe mealRecipe = (from p in menu.Meals
                                                         join p1 in mr.Recipe.MealRecipes on p.MealId equals p1.MealId
                                                         where p.MenuId == menu.MenuId
                                                               && p1.RecipeId == r.Key
                                                         select new MealRecipe
                                                         {
                                                             RecipeId = r.Key,
                                                             Servings = p1.Servings,
                                                             Recipe = r.Value,
                                                             MealId = p.MealId
                                                         }).SingleOrDefault();

                                if (mealRecipe != null)
                                {
                                    r.Value.ExpectedServings = mealRecipe.Servings;
                                    mr.Recipe.ExpectedServings = mealRecipe.Servings;
                                }
                            }
                        }
                        if (!containsRecipe)
                        {
                            menu.MenuRecipes.Add(menuRecipe);
                        }
                    }
                }


                bool isOneMeal = (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal || menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu);

                rbnCategoryOneMeal.Enabled = isOneMeal;
                rbnCategoryWeekly.Enabled = !isOneMeal;


                CurrentMenu = menu;

                RebindMenuRecipes();

                RebindMenuDetails();

                BindMenuCategories();
            }
            else
            {
                AppEnv.MoveToDefaultPage();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void RebindMenuRecipes()
    {
        MyBuyList.Shared.Entities.Menu menu = CurrentMenu;

        Dictionary<int, Recipe> recipes = new Dictionary<int, Recipe>();

        foreach (MenuRecipe r in menu.MenuRecipes)
        {
            recipes.Add(r.RecipeId, r.Recipe);
        }

        if (recipes.Count > 0)
        {
            dlRecipes.DataSource = recipes;
            dlRecipes.DataBind();           
        }
        else
        {
            dlRecipes.Visible = false;
            lblStatus.Visible = true;
            lblStatus.Text = "לא נבחרו מתכונים";
        }
    }

    protected void RebindMenuDetails()
    {
        MyBuyList.Shared.Entities.Menu menu = CurrentMenu;

        if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal || menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
        {
            rbnCategoryOneMeal.Checked = true;
            rbnCategoryWeekly.Checked = false;
            TablesTitleImg.ImageUrl = "Images/SubHeader_MeatDetails.png";
            divNumDiners.Visible = true;
            ddlChooseCourse.Visible = true;
            ddlChooseCourse.SelectedValue = "-1";
            ddlChooseDay.Visible = false;
            ddlChooseMeal.Visible = false;

            if (menu.Meals.Count > 0)
            {
                txtNumDiners.Text = menu.Meals.ToArray()[0].Diners.ToString(); //all meals (courses) should have the same diners number.
            }
        }
        else
        {
            rbnCategoryOneMeal.Checked = false;
            rbnCategoryWeekly.Checked = true;
            TablesTitleImg.ImageUrl = "Images/SubHeader_WeklyMenu.png";  //Change to designer's image.
            divNumDiners.Visible = false;
            ddlChooseCourse.Visible = false;
            ddlChooseDay.Visible = true;
            ddlChooseDay.SelectedValue = "0";
            ddlChooseMeal.Visible = true;
            ddlChooseMeal.SelectedValue = "0";
        }

        chxPulicMenu.Checked = menu.IsPublic;
        txtMenuName.Text = menu.MenuName;
        txtMenuDescription.Text = menu.Description;
        txtMenuTags.Text = menu.Tags;
        txtEmbeddedVideo.Text = menu.EmbeddedVideo;
        txtCategories.Text = this.GetCategoriesString(menu.MenuCategories.ToArray());

        RebindMealsDetails();
    }

    protected void RebindMealsDetails()
    {
        MyBuyList.Shared.Entities.Menu menu = CurrentMenu;

        if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal || menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
        {
            MealDayOfWeek[] fakeDaysOfWeek = new MealDayOfWeek[1] { new MealDayOfWeek(1, MyGlobalResources.Sunday) };
            rptDays.DataSource = fakeDaysOfWeek;
            rptDays.DataBind();
        }

        if (menu.MenuTypeId == (int)MenuTypeEnum.Weekly)
        {
            rptDays.DataSource = MealDayOfWeek;
            rptDays.DataBind();
        }
    }

    protected void rptDays_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        MyBuyList.Shared.Entities.Menu menu = this.CurrentMenu;

        if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal || menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
        {
            System.Web.UI.WebControls.Image tableTopImag = e.Item.FindControl("imgTableTop") as System.Web.UI.WebControls.Image;
            tableTopImag.ImageUrl = "~/Images/bgr_TableMenu.jpg";

            Repeater rptCourses = e.Item.FindControl("rptCourses") as Repeater;

            var list = from item in BusinessFacade.Instance.GetCourseTypes()
                       select new CourseOrMealType(item.CourseTypeId, item.CourseTypeName);
            CourseOrMealType[] courseTypesArray = list.ToArray();

            if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal)
            {
                //CourseOrMealType tempCourseType = courseTypesArray[0];
                //for (int i = 0; i < (courseTypesArray.Length - 1); i++)
                //{
                //    courseTypesArray[i] = courseTypesArray[i + 1];
                //}
                //courseTypesArray[courseTypesArray.Length - 1] = tempCourseType;
                rptCourses.DataSource = courseTypesArray;
            }
            else
            {
                rptCourses.DataSource = courseTypesArray.Where(cmt => cmt.CourseOrMealTypeId == 0);
            }

            rptCourses.DataBind();
        }
        if (menu.MenuTypeId == (int)MenuTypeEnum.Weekly)
        {
            MealDayOfWeek mdow = e.Item.DataItem as MealDayOfWeek;
            if (mdow != null)
            {
                System.Web.UI.WebControls.Image tableTopImag = e.Item.FindControl("imgTableTop") as System.Web.UI.WebControls.Image;
                tableTopImag.ImageUrl = "~/Images/bgr_Table" + mdow.DayId + ".jpg";
            }
            Repeater rptCourses = e.Item.FindControl("rptCourses") as Repeater;

            var list = from item in BusinessFacade.Instance.GetMealTypes()
                       select new CourseOrMealType(item.MealTypeId, item.MealTypeName);

            CourseOrMealType[] mealTypesArray = list.ToArray();
            rptCourses.DataSource = mealTypesArray;
            rptCourses.DataBind();
        }
    }

    protected void rptCourses_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        MyBuyList.Shared.Entities.Menu menu = this.CurrentMenu;

        Meal currentMeal = null;
        MealDayOfWeek currentWeekDay = null;
        int currentWeekDayId = 0;
        string currentMealSignature = "";

        int currentCourseOrMenuTypeId = ((CourseOrMealType)e.Item.DataItem).CourseOrMealTypeId;

        RepeaterItem parentItem = e.Item.Parent.Parent as RepeaterItem;
        if (parentItem != null)
        {
            currentWeekDay = parentItem.DataItem as MealDayOfWeek;
        }
        if (currentWeekDay != null)
        {
            currentWeekDayId = currentWeekDay.DayId;
        }

        TextBox txtComments = null;
        if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal && currentCourseOrMenuTypeId == 6)
        {
            txtComments = e.Item.FindControl("TextBoxComments") as TextBox;
            txtComments.Visible = true;
            txtComments.Attributes["DayId"] = currentWeekDayId.ToString();
            if (_comments != null && _comments.Count != 0) txtComments.Text = _comments[currentWeekDayId-1];
        }

        if (menu.MenuTypeId == (int)MenuTypeEnum.Weekly && currentCourseOrMenuTypeId == 5)
        {
            txtComments = e.Item.FindControl("TextBoxComments") as TextBox;
            txtComments.Visible = true;
            txtComments.Attributes["DayId"] = currentWeekDayId.ToString();
            if (_comments != null && _comments.Count != 0) txtComments.Text = _comments[currentWeekDayId-1];
        }

        if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal)
        {
            foreach (Meal m in this.CurrentMenu.Meals.ToArray())
            {
                if (m.CourseTypeId == currentCourseOrMenuTypeId)
                    currentMeal = m;
                                    
                if (currentCourseOrMenuTypeId == 6 && !string.IsNullOrEmpty(m.Comments))
                {
                    if (string.IsNullOrEmpty(txtComments.Text))
                    {
                        txtComments.Text = m.Comments;
                        _comments[currentWeekDayId - 1] = m.Comments;
                    }
                }
            }

            currentMealSignature = "d0m4c" + currentCourseOrMenuTypeId;
        }
        if (menu.MenuTypeId == (int)MenuTypeEnum.Weekly)
        {
            foreach (Meal m in this.CurrentMenu.Meals.ToArray())
            {
                if (m.MealTypeId == currentCourseOrMenuTypeId && m.DayIndex == currentWeekDayId)
                    currentMeal = m;

                if (currentCourseOrMenuTypeId == 5 && m.DayIndex == currentWeekDayId && !string.IsNullOrEmpty(m.Comments))
                {
                    if (string.IsNullOrEmpty(txtComments.Text))
                    {
                        txtComments.Text = m.Comments;
                        _comments[currentWeekDayId - 1] = m.Comments;
                    }
                }
            }

            currentMealSignature = "d" + currentWeekDayId + "m" + currentCourseOrMenuTypeId + "c0";
        }

        // for identification of relevant meal after recipe was dropped on the table cell.
        HtmlTableCell tdMealRecipes = e.Item.FindControl("tdMealRecipes") as HtmlTableCell;
        if (tdMealRecipes != null)
        {
            tdMealRecipes.Attributes["meal_signature"] = currentMealSignature;
        }

        if (currentMeal == null)
        {
            tdMealRecipes.Controls.Add(new LiteralControl("&nbsp;")); //for IE
            return;
        }
        if (currentMeal.MealRecipes.Count == 0)
        {
            tdMealRecipes.Controls.Add(new LiteralControl("&nbsp;")); //for IE
        }

        if (this.CurrentMenu.MenuTypeId == (int)MenuTypeEnum.OneMeal && txtComments != null)
        {
            txtComments.Text = currentMeal.Comments;
        }

        if (this.CurrentMenu.MenuTypeId == (int)MenuTypeEnum.Weekly)
        {
            HtmlTableCell tdDinersNum = e.Item.FindControl("tdDinersNum") as HtmlTableCell;
            if (tdDinersNum != null)
            {
                tdDinersNum.Style["width"] = "33px";
                tdDinersNum.Style["padding-right"] = "2px";
                tdDinersNum.Style["padding-top"] = "5px";
                tdDinersNum.Style["padding-bottom"] = "5px";
            }

            TextBox txtDinersNum = e.Item.FindControl("txtDinersNum") as TextBox;
            if (txtDinersNum != null)
            {
                txtDinersNum.Attributes["mealSignature"] = this.GetMealSignature(currentMeal);
                txtDinersNum.Text = currentMeal.Diners.ToString();
                txtDinersNum.Visible = true;
            }
        }

        Repeater rpt = e.Item.FindControl("rptRecipes") as Repeater;

        if (rpt != null)
        {
            rpt.DataSource = currentMeal.MealRecipes.ToArray();
            rpt.DataBind();
        }

    }

    protected void rptRecipes_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        MyBuyList.Shared.Entities.Menu menu = this.CurrentMenu;

        Meal meal = null;
        MealRecipe currentMealRecipe = e.Item.DataItem as MealRecipe;
        if (currentMealRecipe != null)
        {
            meal = currentMealRecipe.Meal;
        } 

        LinkButton lnkBtn = e.Item.FindControl("removeFromMeal") as LinkButton;
        if (lnkBtn != null)
        {
            lnkBtn.Attributes["recipeId"] = currentMealRecipe.RecipeId.ToString();
            lnkBtn.Attributes["mealSignature"] = this.GetMealSignature(meal);
        }

        TextBox txtBox = e.Item.FindControl("txtServings") as TextBox;
        if (txtBox != null)
        {
            txtBox.Attributes["recipeId"] = currentMealRecipe.RecipeId.ToString();
            txtBox.Attributes["mealSignature"] = this.GetMealSignature(meal);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        int id = this.CurrentMenu.MenuId;
        this.CurrentMenu = null;
        if (id == 0)
        {
            Response.Redirect("~/Menus.aspx");
        }
        else
        {
            Response.Redirect(string.Format("~/MenuDetails.aspx?menuId={0}", id));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            MyBuyList.Shared.Entities.Menu menu = CurrentMenu;

            int menuTypeId = 0;
            bool isPublic = false;
            string menuName = this.txtMenuName.Text;
            string description = this.txtMenuDescription.Text;
            string tags = this.txtMenuTags.Text;
            string embeddedVideo = this.txtEmbeddedVideo.Text;
            int? diners = null;

            if (rbnCategoryOneMeal.Checked)
            {
                menuTypeId = (int)MenuTypeEnum.OneMeal;

                if (!string.IsNullOrEmpty(txtNumDiners.Text))
                {
                    diners = int.Parse(txtNumDiners.Text);
                }
            }
            else if (rbnCategoryWeekly.Checked)
            {
                menuTypeId = (int)MenuTypeEnum.Weekly;
            }
            if (chxPulicMenu.Checked)
            {
                isPublic = true;
            }

            if (string.IsNullOrEmpty(menuName))
            {
                menuName = string.Empty;
            }
            if (string.IsNullOrEmpty(description))
            {
                description = string.Empty;
            }
            if (string.IsNullOrEmpty(tags))
            {
                tags = string.Empty;
            }
            if (string.IsNullOrEmpty(embeddedVideo))
            {
                embeddedVideo = null;
            }

            if (fuMenuPicture.HasFile && fuMenuPicture.PostedFile != null && ImageHelper.IsImage(fuMenuPicture.PostedFile.FileName))
            {
                Bitmap bitmap = ImageHelper.ResizeImage(new Bitmap(this.fuMenuPicture.PostedFile.InputStream, false), 300, 231);  //see if needs change!
                menu.Picture = ImageHelper.GetBitmapBytes(bitmap);
            }

            int userId = ((BasePage)Page).UserId;
            //can unregistered users add a new recipe? should I deny access to this page to unregistered guests?

            menu.MenuName = menuName;
            menu.Description = description;
            menu.Tags = tags;
            menu.EmbeddedVideo = embeddedVideo;

            if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal || menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
            {
                foreach (Meal meal in menu.Meals.ToArray())
                {
                    meal.Diners = diners;
                }
            }
            if (menu.MenuId == 0)
            {
                menu.MenuTypeId = menuTypeId;
                menu.UserId = userId;
            }

            menu.IsPublic = isPublic;

            for (int i = 0; i < menu.Meals.ToArray().Length; i++)
            {
                if (menu.Meals.ToArray()[i].MealRecipes.Count == 0)
                {
                    menu.Meals.Remove(menu.Meals.ToArray()[i]);
                }
            }

            Meal[] meals = menu.Meals.ToArray();
            RepeaterItem daysItem = null;

            for (int mealInd = 0; mealInd < meals.Length; mealInd++)
            {
                if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal || menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
                {
                    daysItem = rptDays.Items[0];
                    Repeater courses = (Repeater)daysItem.FindControl("rptCourses");
                    if (courses != null)
                        foreach (RepeaterItem coursesItem in courses.Items)
                        {
                            if (coursesItem.ItemIndex == 6)
                            {
                                meals[mealInd].Comments = "";
                                TextBox comment = (TextBox)coursesItem.FindControl("TextBoxComments");
                                if (!string.IsNullOrEmpty(comment.Text))
                                {
                                    meals[mealInd].Comments = comment.Text;
                                }
                            }
                        }
                }

                if (menu.MenuTypeId == (int)MenuTypeEnum.Weekly)
                {
                    int dayIndex = meals[mealInd].DayIndex.Value;
                    daysItem = rptDays.Items[dayIndex - 1];

                    Repeater courses = (Repeater)daysItem.FindControl("rptCourses");
                    if (courses != null)
                        foreach (RepeaterItem coursesItem in courses.Items)
                        {
                            if (coursesItem.ItemIndex == 4)
                            {
                                //TextBox comment = (TextBox) coursesItem.FindControl("TextBoxComments");
                                //if (!string.IsNullOrEmpty(comment.Text))
                                //{
                                //    meals[mealInd].Comments = comment.Text;
                                //}

                                meals[mealInd].Comments = _comments[dayIndex - 1];
                            }
                        }
                }
            }

            int returnedMenuId;
            BusinessFacade.Instance.CreateOrUpdateMenu(menu, out returnedMenuId);

            if (returnedMenuId != 0)
            {
                Utils.SelectedRecipes = null;
                CurrentMenu = null;
                string url = string.Format("~/MenuDetails.aspx?menuId={0}", returnedMenuId);
                Response.Redirect(url);
            }
        }
        catch (Exception exception)
        {
        }
    }

    protected void rbnCategoryOneMeal_CheckedChanged(object sender, EventArgs e)
    {
        if (this.rbnCategoryOneMeal.Checked)
        {
            this.TablesTitleImg.ImageUrl = "Images/SubHeader_MeatDetails.png";
            //this.upMealsDetails.Update();
            this.divNumDiners.Visible = true;
            //this.upNumDiners.Update();
            this.ddlChooseCourse.Visible = true;
            this.ddlChooseCourse.SelectedValue = "-1";
            this.ddlChooseDay.Visible = false;
            this.ddlChooseMeal.Visible = false;
            //this.upChooseCategories.Update();
            
            this.CurrentMenu.MenuTypeId = 1;
        }
        else
        {
            this.TablesTitleImg.ImageUrl = "Images/SubHeader_WeklyMenu.png";
            //this.upMealsDetails.Update();
            this.divNumDiners.Visible = false;
            //this.upNumDiners.Update();
            this.ddlChooseCourse.Visible = false;
            this.ddlChooseDay.Visible = true;
            this.ddlChooseDay.SelectedValue = "0";
            this.ddlChooseMeal.Visible = true;
            this.ddlChooseMeal.SelectedValue = "0";
            //this.upChooseCategories.Update();
           
            this.CurrentMenu.MenuTypeId = 2;
        }

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "setDirty", "setDirty();", true);

        RebindMealsDetails();
    }

    protected void dlRecipes_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.DataItem is KeyValuePair<int, Recipe>)
        {
            KeyValuePair<int, Recipe> recipe = (KeyValuePair<int, Recipe>)e.Item.DataItem;

            LinkButton lnkBtn = e.Item.FindControl("lnkRemove") as LinkButton;
            if (lnkBtn != null)
            {
                lnkBtn.Attributes["recipeId"] = recipe.Key.ToString();
            }

            //lnkBtn = e.Item.FindControl("lnkAddToMenu") as LinkButton;
            //if (lnkBtn != null)
            //{
            //    lnkBtn.Attributes["recipeId"] = recipe.Key.ToString();
            //}

            lnkBtn = e.Item.FindControl("lnkAddToMenu2") as LinkButton;
            if (lnkBtn != null)
            {
                lnkBtn.Attributes["recipeId"] = recipe.Key.ToString();
                lnkBtn.Attributes["recipeName"] = recipe.Value.RecipeName;
                //lnkBtn.Attributes["recipeServings"] = recipe.Value.ExpectedServings.ToString();
            }

            HyperLink hyplink = e.Item.FindControl("lnkRecipeName") as HyperLink;
            if (hyplink != null)
            {
                hyplink.NavigateUrl = string.Format("~/RecipeDetails.aspx?RecipeId={0}", recipe.Key.ToString());
            }

            // for identification of relevant recipe after it was dropped on a cell in the table.

            Panel pnl = e.Item.FindControl("pnlItem") as Panel;
            if (pnl != null)
            {
                pnl.Attributes["recipeId"] = recipe.Key.ToString();
            }

            AjaxControlToolkit.ConfirmButtonExtender cbe = e.Item.FindControl("cbeRemoveMenuRecipe") as AjaxControlToolkit.ConfirmButtonExtender;
            if (cbe != null)
            {
                bool isRecipeInMenuMeals = false;
                foreach (Meal meal in this.CurrentMenu.Meals)
                {
                    if (meal.MealRecipes.SingleOrDefault(mr => mr.RecipeId == recipe.Key) != null)
                    {
                        isRecipeInMenuMeals = true;
                    }
                }

                cbe.Enabled = isRecipeInMenuMeals;
            }
        }
    }

    protected void RefreshMenu(object sender, EventArgs e)
    {
        this.RebindMealsDetails();
        this.upMealsDetails.Update();
        this.RebindMenuRecipes();
        this.upSelectedRecipes.Update();
    }
    
    //note: the "detour" on the way to this method (through a JS function that clicks a hidden button) is neccessary because if this method
    //is called directly from within the "poping" panel, it forces "full" postback for some reason. In this way it is avoided, and selection 
    //flow seems "smoother".
    protected void btnTmpOK_Click(object sender, EventArgs e)
    {
        MyBuyList.Shared.Entities.Menu menu = CurrentMenu;

        //get relevant meal, or create it if it doesn't exist.
        Meal meal;
        int? dayIndex = null, courseTypeId = null;
        int mealTypeId;

        switch ((MenuTypeEnum) menu.MenuTypeId)
        {
            case MenuTypeEnum.QuickMenu:
                mealTypeId = 4;
                courseTypeId = 0;

                meal = menu.Meals.SingleOrDefault(me => me.MealTypeId == mealTypeId);
                break;
            case MenuTypeEnum.OneMeal:
                courseTypeId = int.Parse(ddlChooseCourse.SelectedItem.Value);
                if (courseTypeId == -1)
                {
                    return;
                }

                mealTypeId = 4;

                meal = menu.Meals.SingleOrDefault(me => me.CourseTypeId == courseTypeId);
                break;
            default:
                dayIndex = int.Parse(ddlChooseDay.SelectedItem.Value);
                mealTypeId = int.Parse(ddlChooseMeal.SelectedItem.Value);
                if (dayIndex == 0 || mealTypeId == 0)
                {
                    return;
                }

                meal = menu.Meals.SingleOrDefault(me => me.DayIndex == dayIndex && me.MealTypeId == mealTypeId);
                break;
        }

        if (meal == null)
        {
            meal = new Meal
                {
                    MealId = 0,
                    CourseTypeId = courseTypeId,
                    DayIndex = dayIndex,
                    MealTypeId = mealTypeId,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

            menu.Meals.Add(meal);
        }

        //get recipe id, create MealRecipe, and add to MealRecipes of the current / new meal.
        int recipeId;
        int servingsNum;

        bool succeeded = int.TryParse(hfRecipeId.Value, out recipeId);
        if (succeeded)
        {
            MealRecipe mealRecipe = new MealRecipe();

            MenuRecipe menuRec = menu.MenuRecipes.SingleOrDefault(mr => mr.RecipeId == recipeId);
            if (menuRec != null)
            {
                mealRecipe.Recipe = menuRec.Recipe;

                int expectedServings;
                bool tryParse = int.TryParse(hfExpectedServings.Value, out expectedServings);
                if (tryParse)
                {
                    mealRecipe.ExpectedServings = expectedServings;
                    menuRec.Recipe.ExpectedServings = expectedServings;
                }
            }

            MealRecipe sameMealRecipe = meal.MealRecipes.SingleOrDefault(smr => smr.RecipeId == mealRecipe.RecipeId);
            if (sameMealRecipe == null)
            {
                meal.MealRecipes.Add(mealRecipe);
            }
        }


        //save menu changes and display changed menu
        CurrentMenu = menu;

        RebindMealsDetails();
        upMealsDetails.Update();
        RebindMenuRecipes();
        upSelectedRecipes.Update();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="recipeId"></param>
    /// <param name="mealSignature"></param>
    [WebMethod]
    public static void AddToMenu_DnD(int recipeId, string mealSignature)
    {
        MyBuyList.Shared.Entities.Menu menu = HttpContext.Current.Session["currentMenu"] as MyBuyList.Shared.Entities.Menu;

        Meal meal = null;

        int[] mealIdentifiers = GetMealIdentifiers(mealSignature);

        int dayIndex = mealIdentifiers[0];
        int mealTypeId = mealIdentifiers[1];
        int courseTypeId = mealIdentifiers[2];


        if (mealIdentifiers != null && mealIdentifiers.Length == 3)
        {
            if (menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu) //only one meal in the menu
            {
                meal = menu.Meals.SingleOrDefault(me => me.MealTypeId == mealTypeId);
            }
            else if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal)
            {
                meal = menu.Meals.SingleOrDefault(me => me.CourseTypeId == courseTypeId);
            }
            else
            {
                meal = menu.Meals.SingleOrDefault(me => me.DayIndex == dayIndex && me.MealTypeId == mealTypeId);
            }

            if (meal == null)
            {
                meal = new Meal();
                meal.MealId = 0;
                meal.CourseTypeId = courseTypeId;
                meal.DayIndex = dayIndex;
                meal.MealTypeId = mealTypeId;
                meal.CreatedDate = DateTime.Now;
                meal.ModifiedDate = DateTime.Now;

                menu.Meals.Add(meal);
            }

            //Page page = HttpContext.Current.Handler as Page; 
        }

        MealRecipe mealRecipe = new MealRecipe();

        MenuRecipe menuRec = menu.MenuRecipes.SingleOrDefault(mr => mr.RecipeId == recipeId);
        if (menuRec != null)
        {
            mealRecipe.Recipe = menuRec.Recipe;
            mealRecipe.Servings = menuRec.Recipe.Servings;
            mealRecipe.ExpectedServings = menuRec.Recipe.ExpectedServings;
        }

        MealRecipe sameMealRecipe = meal.MealRecipes.SingleOrDefault(smr => smr.RecipeId == mealRecipe.RecipeId);
        if (sameMealRecipe == null)
        {
            meal.MealRecipes.Add(mealRecipe);
        }
        
        HttpContext.Current.Session["currentMenu"] = menu;
        
    }

    protected void lnkRemove_Click(object sender, EventArgs e)
    {
        MyBuyList.Shared.Entities.Menu menu = this.CurrentMenu;

        LinkButton lnkBtn = sender as LinkButton;

        int recipeId;
        if (lnkBtn != null && int.TryParse(lnkBtn.Attributes["recipeId"], out recipeId))
        { 
            foreach (Meal meal in menu.Meals)
            {
                MealRecipe mealRecipeToRemove = meal.MealRecipes.SingleOrDefault(mr => mr.RecipeId == recipeId);
                if (mealRecipeToRemove != null)
                {
                    meal.MealRecipes.Remove(mealRecipeToRemove);
                }
            }

            MenuRecipe menuRecipeToRemove = menu.MenuRecipes.SingleOrDefault(mr => mr.RecipeId == recipeId);

            if (menuRecipeToRemove != null)
            {
                menu.MenuRecipes.Remove(menuRecipeToRemove);
            }

            Dictionary<int, Recipe> selectedRecipes = Utils.SelectedRecipes;
            selectedRecipes.Remove(recipeId);
        }

        CurrentMenu = menu;

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "setDirty", "setDirty();", true);

        RebindMenuRecipes();
        RebindMealsDetails();
        upSelectedRecipes.Update();
        upMealsDetails.Update();        
    }

    protected void removeFromMeal_Click(object sender, EventArgs e)
    {
        MyBuyList.Shared.Entities.Menu menu = this.CurrentMenu;

        LinkButton lnkBtn = sender as LinkButton;
        if (lnkBtn != null)
        {
            string mealSignature = lnkBtn.Attributes["mealSignature"];
            int recipeId;

            if (!string.IsNullOrEmpty(mealSignature) && int.TryParse(lnkBtn.Attributes["recipeId"], out recipeId))
            {
                int[] mealIdentifiers = GetMealIdentifiers(mealSignature);

                Meal currentMeal = null;              //no mealId at this stage. meal is identified by day / meal type / course type.

                switch (menu.MenuTypeId)
                {
                    case (int)MenuTypeEnum.QuickMenu:
                        currentMeal = menu.Meals.SingleOrDefault(me => me.MealTypeId == mealIdentifiers[1]);
                        break;
                    case (int)MenuTypeEnum.OneMeal:
                        currentMeal = menu.Meals.SingleOrDefault(me => me.CourseTypeId == mealIdentifiers[2]);
                        break;
                    case (int)MenuTypeEnum.Weekly:
                        currentMeal = menu.Meals.SingleOrDefault(me => me.DayIndex == mealIdentifiers[0] && me.MealTypeId == mealIdentifiers[1]);
                        break;
                }

                //if (menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu) //only one meal in the menu
                //{
                //    currentMeal = menu.Meals.SingleOrDefault(me => me.MealTypeId == mealIdentifiers[1]);
                //}
                //if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal)
                //{
                //    currentMeal = menu.Meals.SingleOrDefault(me => me.CourseTypeId == mealIdentifiers[2]);
                //}
                //else
                //{
                //    currentMeal = menu.Meals.SingleOrDefault(me => me.DayIndex == mealIdentifiers[0] && me.MealTypeId == mealIdentifiers[1]);
                //}

                if (currentMeal != null)
                {
                    MealRecipe recipe = currentMeal.MealRecipes.SingleOrDefault(mr => mr.RecipeId == recipeId);
                    if (recipe != null)
                    {
                        currentMeal.MealRecipes.Remove(recipe);
                        //if (currentMeal.MealRecipes.Count == 0)
                        //{
                        //    menu.Meals.Remove(currentMeal);
                        //}
                    }
                }

                this.CurrentMenu = menu;

                RebindMealsDetails();
                this.upMealsDetails.Update();
                this.RebindMenuRecipes();
                this.upSelectedRecipes.Update();
            }
        }
    }

    protected void txtServings_TextChanged(object sender, EventArgs e)
    {
        MyBuyList.Shared.Entities.Menu menu = this.CurrentMenu;

        TextBox txtServings = sender as TextBox;
        if (txtServings != null)
        {
            int recipeId = 0;
            int[] mealIdentifiers = GetMealIdentifiers(txtServings.Attributes["mealSignature"]);
            if (int.TryParse(txtServings.Attributes["recipeId"], out recipeId) && mealIdentifiers != null && mealIdentifiers.Length == 3)
            {
                Meal mealToChange = null;
                // TODO: change all the occurances of this code to "switch", and try to do it with a delegate, that point to different methods
                // according to the menuTypeId. These methods will get a menu object (menuToSave) and a meal object (from menu) or the whole menu object (menu)

                switch (menu.MenuTypeId)
                {
                    case (int)MenuTypeEnum.QuickMenu:
                        mealToChange = menu.Meals.SingleOrDefault(me => me.MealTypeId == mealIdentifiers[1]);
                        break;
                    case (int)MenuTypeEnum.OneMeal:
                        mealToChange = menu.Meals.SingleOrDefault(me => me.CourseTypeId == mealIdentifiers[2]);
                        break;
                    case (int)MenuTypeEnum.Weekly:
                        mealToChange = menu.Meals.SingleOrDefault(me => me.DayIndex == mealIdentifiers[0] && me.MealTypeId == mealIdentifiers[1]);
                        break;
                }

                if (mealToChange != null)
                {
                    MealRecipe recipeToChange = mealToChange.MealRecipes.SingleOrDefault(re => re.RecipeId == recipeId);
                    if (recipeToChange != null)
                    {
                        recipeToChange.Servings = int.Parse(txtServings.Text);
                    }
                }
            }
        }
    }

    protected void txtDinersNum_TextChanged(object sender, EventArgs e)
    {
        MyBuyList.Shared.Entities.Menu menu = this.CurrentMenu;
        int dinersNumber;
        TextBox txtDinersNum = sender as TextBox;

        if (txtDinersNum != null && int.TryParse(txtDinersNum.Text, out dinersNumber))
        {
            Meal mealToChange = null;
            int[] mealIdentifiers = GetMealIdentifiers(txtDinersNum.Attributes["mealSignature"]);

            if (menu.MenuTypeId == (int)MenuTypeEnum.QuickMenu)
            {
                mealToChange = menu.Meals.SingleOrDefault(me => me.MealTypeId == mealIdentifiers[1]);
            }
            if (menu.MenuTypeId == (int)MenuTypeEnum.OneMeal)
            {
                mealToChange = menu.Meals.SingleOrDefault(me => me.CourseTypeId == mealIdentifiers[2]);
            }
            else
            {
                mealToChange = menu.Meals.SingleOrDefault(me => me.DayIndex == mealIdentifiers[0] && me.MealTypeId == mealIdentifiers[1]);
            }

            if (mealToChange != null)
            {
                mealToChange.Diners = dinersNumber;
            }
        }
    }

    protected void btnCatOK_Click(object sender, EventArgs e)
    {
        MyBuyList.Shared.Entities.Menu menu = this.CurrentMenu;

        menu.MenuCategories.Clear();
        string updatedCategoriesStr = "";
        foreach (TreeNode node in this.tvCategories.CheckedNodes)
        {
            MenuCategory mcat = new MenuCategory();
            mcat.MenuId = menu.MenuId;
            mcat.MCategoryId = int.Parse(node.Value);
            menu.MenuCategories.Add(mcat);
            updatedCategoriesStr += node.Text + ", ";
        }
        updatedCategoriesStr = updatedCategoriesStr.Remove(updatedCategoriesStr.Length - 2);

        this.BindMenuCategories();

        this.txtCategories.Text = updatedCategoriesStr;
        this.upCategories.Update();
    }    

    protected void BindMenuCategories()
    {
        this.tvCategories.Nodes.Clear();
        MCategory[] categories = BusinessFacade.Instance.GetMCategoriesList();
        this.BuildTree(categories, null, null);

        this.tvCategories.ShowCheckBoxes = TreeNodeTypes.All;
        this.tvCategories.DataBind();

        this.upTreeViewCategories.Update();
    }

    private void BuildTree(MCategory[] cats, int? parentCategoryId, TreeNode rootNode)
    {
        MyBuyList.Shared.Entities.Menu menu = this.CurrentMenu;

        var list = cats.Where(mc => mc.ParentMCategoryId == parentCategoryId);
        foreach (MCategory item in list)
        {
            TreeNode node = new TreeNode(item.MCategoryName, item.MCategoryId.ToString());
            if (menu.MenuCategories != null &&
                menu.MenuCategories.SingleOrDefault(mc => mc.MCategoryId == item.MCategoryId) != null)
            {
                node.Checked = true;
            }

            if (rootNode == null)
            {
                this.tvCategories.Nodes.Add(node);
            }
            else
            {
                rootNode.ChildNodes.Add(node);
            }

            BuildTree(cats, item.MCategoryId, node);
        }
    }

    protected string GetMealSignature(Meal meal)
    {
        string mealSignature = "";

        string dayIndex = "", courseTypeId = "";

        dayIndex = meal.DayIndex == null ? "0" : meal.DayIndex.ToString();
        courseTypeId = meal.CourseTypeId == null ? "-1" : meal.CourseTypeId.ToString();

        mealSignature += "d";
        mealSignature += dayIndex;
        mealSignature += "m";
        mealSignature += meal.MealTypeId.ToString();
        mealSignature += "c";
        mealSignature += courseTypeId;

        return mealSignature;
    }

    protected static int[] GetMealIdentifiers(string mealSignature)
    {
        int[] identifiers = new int[3];

        int startIndex, length;

        startIndex = mealSignature.IndexOf('d') + 1;
        length = mealSignature.IndexOf('m') - startIndex;
        identifiers[0] = int.Parse(mealSignature.Substring(startIndex, length));
        startIndex = mealSignature.IndexOf('m') + 1;
        length = mealSignature.IndexOf('c') - startIndex;
        identifiers[1] = int.Parse(mealSignature.Substring(startIndex, length));
        startIndex = mealSignature.IndexOf('c') + 1;
        identifiers[2] = int.Parse(mealSignature.Substring(startIndex));

        return identifiers;
    }

    protected string GetCategoriesString(MenuCategory[] categories)
    {
        string str = "";
        foreach (MenuCategory cat in categories)
        {
            str += cat.MCategory.MCategoryName + ", ";
        }

        if (str.Length > 2)
        {
            str = str.Remove(str.Length - 2);
        }

        return str;
    }


    protected void rptRecipes_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
    }

    protected void rptCourses_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
        
    }

    protected void rptDays_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        Repeater rptCourses = e.Item.FindControl("rptCourses") as Repeater;

    }

    protected void CommentsTextChanged(object sender, EventArgs e)
    {
        int DayId;
        int.TryParse(((TextBox) sender).Attributes["DayId"], out DayId);
        _comments[DayId-1] = ((TextBox) sender).Text;
    }

    protected void ChangeServings(object sender, CommandEventArgs e)
    {
        int recipeId = Convert.ToInt32(e.CommandArgument);
        MyBuyList.Shared.Entities.Menu menu = CurrentMenu;

        //Recipe recipe1 = BusinessFacade.Instance.GetRecipe(recipeId);

        MenuRecipe menuRecipe = menu.MenuRecipes.SingleOrDefault(x => x.RecipeId == recipeId);
        if (menuRecipe != null)
        {
            MealRecipe mealRecipe = (from p in menu.Meals
                                     join p1 in menuRecipe.Recipe.MealRecipes on p.MealId equals p1.MealId
                                     where p.MenuId == menu.MenuId
                                           && p1.RecipeId == recipeId && p1.Meal != null
                                     select new MealRecipe
                                         {
                                             RecipeId = recipeId,
                                             Servings = p1.Servings,
                                             Recipe = menuRecipe.Recipe,
                                             MealId = p.MealId
                                         }).SingleOrDefault();

            if (mealRecipe != null)
            {
                switch (e.CommandName)
                {
                    case "Increase":
                        mealRecipe.Servings += menuRecipe.Recipe.Servings;
                        break;
                    case "Decrease":
                        if (mealRecipe.Servings > menuRecipe.Recipe.Servings)
                            mealRecipe.Servings -= menuRecipe.Recipe.Servings;
                        break;
                }

                MealRecipe recipe = menuRecipe.Recipe.MealRecipes.SingleOrDefault(x => x.MealId == mealRecipe.MealId && x.Meal != null);
                if (recipe != null) recipe.Servings = mealRecipe.Servings;
            }
        }
    }
}
