using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;

using Resources;
using System.Drawing;
using ProperServices.Common.Log;

public partial class ucRecipe : System.Web.UI.UserControl
{
    SRL_RecipeCategory[] RecipeCategories
    {
        get { return (SRL_RecipeCategory[])ViewState["RecipeCategories"]; }
        set { ViewState["RecipeCategories"] = value; }
    }

    int RecipeId
    {
        get { return ViewState["RecipeId"] == null ? 0 : (int)ViewState["RecipeId"]; }
        set { ViewState["RecipeId"] = value; }
    }

    Binary RecipePicture
    {
        get { return (Binary)Session["RecipePicture"]; }
        set { Session["RecipePicture"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string numberSeperator = ConfigurationManager.AppSettings["NumberSeperator"];
        }
    }

    public void NewRecipe()
    {
        ShowEditor(0);
    }
    public void EditRecipe(int recipeId)
    {
        ShowEditor(recipeId);
    }

    public void RefreshCategories(SRL_RecipeCategory[] arr)
    {
        this.RecipeCategories_Rebind(arr);
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "setDirty", "setDirty();", true);
        updateCategories.Update();
    }
    public void UpdatePicture(Binary picture)
    {
        this.RecipePicture = picture;
    }

    public void CopyRecipe(int recipeId)
    {
        if (recipeId != 0)
        {
            Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeId);
            if (recipe == null)
            {
                return;
            }
            RecipeId = 0;

            imgTitle.ImageUrl = "~/Images/Header_AddNewRecipe.png"; 
            txtRecipeName.Text = recipe.RecipeName + string.Format(" ({0})", MyGlobalResources.Copy);            
            chkPublic.Checked = recipe.IsPublic;            
            txtPreparationMethod.Text = recipe.PreparationMethod;
            txtRemarks.Text = recipe.Remarks;            
            txtEmbeddedLink.Text = recipe.VideoLink;
            txtServings.Text = recipe.Servings.ToString();
            txtTools.Text = recipe.Tools;

            if (recipe.Description != null)
            {
                txtRecipeDesc.Text = recipe.Description;
            }

            if (recipe.Tags != null)
            {
                txtTags.Text = recipe.Tags;
            }

            if (recipe.DifficultyLevel != null)
            {
                ddlDifficulty.SelectedValue = recipe.DifficultyLevel.Value.ToString();
            }

            if (recipe.PreperationTime != null)
            {
                int unit;
                txtPrepTime.Text = this.GetTimeInCorrectUnits(recipe.PreperationTime.Value, out unit).ToString();
                ddlPrepTimeUnits.SelectedValue = unit.ToString();
            }

            if (recipe.CookingTime != null)
            {
                int unit;
                txtCookTime.Text = this.GetTimeInCorrectUnits(recipe.CookingTime.Value, out unit).ToString();
                ddlCookTimeUnits.SelectedValue = unit.ToString();
            }


            var list = from item in recipe.RecipeCategories
                       select new SRL_RecipeCategory(0, item.CategoryId, item.Category.CategoryName);
            RecipeCategories_Rebind(list.ToArray());

            //Ingredients = new List<SRL_Ingredient>();
            ////Ingredients = new List<Ingredient>();
            //foreach (Ingredient ing in recipe.Ingredients)
            //{
            //    SRL_Ingredient ingItem = new SRL_Ingredient(ing);
            //    //Ingredient ingItem = new Ingredient();
            //    ingItem.RecipeId = 0;
            //    Ingredients.Add(ingItem);
            //}

            //dlistIngredients.DataSource = recipe.Ingredients;
            //dlistIngredients.DataBind();

            RecipePicture = recipe.Picture;            

            //txtFoodName.Text = "";
            //txtFoodName.Text = "";
            //txtQuantity.Text = "";
            //ddlFractions.SelectedIndex = 0;
            //ddlMeasurementUnits.SelectedIndex = 0;
        }
    }

    private void ShowEditor(int recipeId)
    {
        RecipeId = recipeId;
        if (recipeId != 0)
        {
            Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeId);
            SetValues(recipe);
        }
        else
        {
            SetValues(null);
        }
    }

    public bool ReadOnly
    {
        get
        {
            if (ViewState["ReadOnly"] == null)
                ReadOnly = false;

            return (bool)ViewState["ReadOnly"];
        }
        set { ViewState["ReadOnly"] = value; }
    }

    private void SetValues(Recipe recipe)
    {
        try
        {
            if (recipe != null)
            {
                Logger.Write("ucRecipe.SetValues -> Start", Logger.Level.Info);

                imgTitle.ImageUrl = "~/Images/Header_EditRecipe.png";
                txtRecipeName.Text = recipe.RecipeName;
                chkPublic.Checked = recipe.IsPublic;
                txtPreparationMethod.Text = recipe.PreparationMethod;
                txtRemarks.Text = recipe.Remarks;
                txtTools.Text = recipe.Tools;

                txtEmbeddedLink.Text = recipe.VideoLink;

                txtServings.Text = recipe.Servings.ToString();

                if (recipe.Description != null)
                {
                    txtRecipeDesc.Text = recipe.Description;
                }

                if (recipe.Tags != null)
                {
                    txtTags.Text = recipe.Tags;
                }

                if (recipe.DifficultyLevel != null)
                {
                    ddlDifficulty.SelectedValue = recipe.DifficultyLevel.Value.ToString();
                }

                if (recipe.PreperationTime != null)
                {
                    int unit;
                    txtPrepTime.Text = this.GetTimeInCorrectUnits(recipe.PreperationTime.Value, out unit).ToString();
                    ddlPrepTimeUnits.SelectedValue = unit.ToString();
                }

                if (recipe.CookingTime != null)
                {
                    int unit;
                    txtCookTime.Text = this.GetTimeInCorrectUnits(recipe.CookingTime.Value, out unit).ToString();
                    ddlCookTimeUnits.SelectedValue = unit.ToString();
                }

                Logger.Write("ucRecipe.SetValues -> Set Categories", Logger.Level.Info);

                var list = from item in recipe.RecipeCategories
                           select new SRL_RecipeCategory(item.RecipeId, item.CategoryId, item.Category.CategoryName);
                RecipeCategories_Rebind(list.ToArray());

                Logger.Write("ucRecipe.SetValues -> Set Ingrediants", Logger.Level.Info);

                List<SRL_Ingredient> listOfIngediants = new List<SRL_Ingredient>();
                foreach (Ingredient item in recipe.Ingredients)
                {
                    listOfIngediants.Add(new SRL_Ingredient(item));
                }
                Ingridiants.ListOfIngediants = listOfIngediants;

                Logger.Write("ucRecipe.SetValues -> End", Logger.Level.Info);

                RecipePicture = recipe.Picture;
            }
            else
            {
                imgTitle.ImageUrl = "~/Images/Header_AddNewRecipe.png";
                txtRecipeName.Text = "";
                txtRecipeDesc.Text = "";
                txtTags.Text = "";
                txtCategories.Text = "";
                chkPublic.Checked = false;
                txtPreparationMethod.Text = "";
                txtRemarks.Text = "";
                txtEmbeddedLink.Text = "";
                txtTools.Text = "";
                txtServings.Text = "";
                ddlDifficulty.SelectedValue = "0";
                txtPrepTime.Text = "";
                ddlPrepTimeUnits.SelectedValue = "1";
                txtCookTime.Text = "";
                ddlCookTimeUnits.SelectedValue = "1";

                RecipeCategories_Rebind(null);
                RecipePicture = null;
            }
        }
        catch(Exception ex)
        {
            Logger.Write(string.Format("ucRecipe.SetValues -> Failed for recipe {0}", recipe.RecipeId), ex, Logger.Level.Error);
        }
    }
    protected void btnSelectPicture_Click(object sender, EventArgs e)
    {
        if (this.SelectPictureClick != null)
        {
            this.SelectPictureClick.Invoke(this.RecipeId);
        }
    }
    protected void btnCategories_Click(object sender, EventArgs e)
    {        
        if (this.ShowCategoriesClick != null)
        {

            this.ShowCategoriesClick.Invoke(this.RecipeId, this.RecipeCategories);           
        }        
    }

    private void RecipeCategories_Rebind(SRL_RecipeCategory[] arr)
    {
        if (arr != null)
        {
            this.RecipeCategories = arr;            
            string cats = "";
            foreach (SRL_RecipeCategory rc in arr)
            {
                cats += rc.CategoryName + ", ";
            }
            if (!string.IsNullOrEmpty(cats))
            {
                cats = cats.Remove(cats.Length - 2, 2);
            }

            this.txtCategories.Text = cats;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.RecipeCategories = null;
        
        if (this.RecipeId != 0)
        {
            Response.Redirect(string.Format("~/RecipeDetails.aspx?RecipeId={0}", this.RecipeId));
        }
        else 
        {
            Response.Redirect("~/Recipes.aspx");
        }
    } 
       
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Logger.Write("btnSave_Click -> Start", Logger.Level.Info);
        try
        {
            Page.Validate("general");
            if (!Page.IsValid)
            {
                return;
            }

            Recipe recipe;
            if (RecipeId == 0)
            {
                recipe = new Recipe();
            }
            else
            {
                recipe = BusinessFacade.Instance.GetRecipe(this.RecipeId);
            }

            if (recipe == null)
            {
                return; //TODO Exception (record is not exist)
            }

            Logger.Write("btnSave_Click -> Set recipe data", Logger.Level.Info);

            if (fuRecipeImage.HasFile && fuRecipeImage.PostedFile != null && ImageHelper.IsImage(fuRecipeImage.PostedFile.FileName))
            {
                Bitmap bitmap = ImageHelper.ResizeImage(new Bitmap(this.fuRecipeImage.PostedFile.InputStream, false), 300, 231);
                RecipePicture = ImageHelper.GetBitmapBytes(bitmap);
            }

            recipe.RecipeName = txtRecipeName.Text;
            recipe.IsPublic = chkPublic.Checked;
            recipe.Description = txtRecipeDesc.Text;
            recipe.Tags = txtTags.Text;
            recipe.PreparationMethod = txtPreparationMethod.Text;
            recipe.Remarks = txtRemarks.Text;
            recipe.Tools = txtTools.Text;

            if (ddlDifficulty.SelectedValue != "0")
            {
                recipe.DifficultyLevel = int.Parse(ddlDifficulty.SelectedValue);
            }
            else
            {
                recipe.DifficultyLevel = null;
            }

            if (!string.IsNullOrEmpty(txtPrepTime.Text))
            {
                recipe.PreperationTime = GetTimeInMinutes(txtPrepTime.Text, int.Parse(ddlPrepTimeUnits.SelectedValue));
            }
            else
            {
                recipe.PreperationTime = null;
            }

            if (!string.IsNullOrEmpty(this.txtCookTime.Text))
            {
                recipe.CookingTime = this.GetTimeInMinutes(this.txtCookTime.Text, int.Parse(this.ddlCookTimeUnits.SelectedValue));
            }
            else
            {
                recipe.CookingTime = null;
            }

            if (!string.IsNullOrEmpty(this.txtServings.Text))
            {
                recipe.Servings = int.Parse(this.txtServings.Text);
            }
            else
            {
                recipe.Servings = 1;
            }

            recipe.VideoLink = this.txtEmbeddedLink.Text;
            recipe.Picture = this.RecipePicture;

            if (recipe.UserId == 0)
            {
                recipe.UserId = ((BasePage)Page).UserId;
            }

            Logger.Write("btnSave_Click -> Set recipe categories", Logger.Level.Info);

            recipe.RecipeCategories.Clear();
            foreach (SRL_RecipeCategory rcat in this.RecipeCategories)
            {
                recipe.RecipeCategories.Add(new RecipeCategory(rcat.RecipeId, rcat.CategoryId));
            }

            Logger.Write("btnSave_Click -> Set recipe ingrediants", Logger.Level.Info);

            recipe.Ingredients.Clear();

            int index = 0;
            foreach (SRL_Ingredient item in Ingridiants.ListOfIngediants)
            {
                decimal fraction;
                decimal.TryParse(item.FractionValue, out fraction);

                Ingredient ing = new Ingredient
                {
                    //IngredientId = item.Id,
                    RecipeId = RecipeId,
                    FoodId = item.FoodId,
                    FoodName = item.FoodName,
                    MeasurementUnitId = item.MeasurementUnitId,
                    //Quantity = item.IntQuantity + fraction,
                    Remarks = item.Remarks,
                    SortOrder = ++index
                };

                recipe.Ingredients.Add(ing);
            }

            Logger.Write("btnSave_Click -> Save recipe", Logger.Level.Info);

            int returnedRecipeId;
            if (BusinessFacade.Instance.SaveRecipe(recipe, out returnedRecipeId))
            {
                if (RefreshData != null)
                {
                    RefreshData.Invoke();
                }
            }

            Logger.Write("btnSave_Click -> End and redirect", Logger.Level.Info);

            RecipeCategories = null;
            Response.Redirect(string.Format("~/RecipeDetails.aspx?RecipeId={0}", returnedRecipeId));
        }
        catch (Exception ex)
        {
            Logger.Write("Save recipe failed", ex, Logger.Level.Error);
        }
    }

    //protected void custValidIngredients_ServerValidate(object source, ServerValidateEventArgs args)
    //{
    //    args.IsValid = true;
    //    if (string.IsNullOrEmpty(this.txtFoodName.Text))
    //    {
    //        args.IsValid = false;
    //        this.custValidIngredients.ErrorMessage = ValidationResources.FoodNameIsRequired;            
    //    }
    //    else if (string.IsNullOrEmpty(this.txtQuantity.Text) &&
    //             string.IsNullOrEmpty(this.ddlFractions.Text))
    //    {
    //        args.IsValid = false;
    //        this.custValidIngredients.ErrorMessage = ValidationResources.QuantityIsRequired;
    //    }
    //    else if (!string.IsNullOrEmpty(this.txtQuantity.Text))
    //    {
    //        decimal result = 0;
    //        args.IsValid = decimal.TryParse(this.txtQuantity.Text, out result);
    //        this.custValidIngredients.ErrorMessage = ValidationResources.WrongQuantityFieldValue;
    //    }

    //    this.UpdatePanel4.Update();
        
    //}

    //protected void btnAddIngerdient_Command(object sender, CommandEventArgs e)
    //{
    //    try
    //    {
    //        this.Page.Validate("ingredients");
    //        if (!this.Page.IsValid)
    //        {
    //            return;
    //        }

    //        List<SRL_Ingredient> list = Ingredients;
    //        SRL_Ingredient ingredient = null;
    //        //List<Ingredient> list = Ingredients;
    //        //Ingredient ingredient = null;
    //        string commandArg = e.CommandArgument as string;

    //        if (commandArg == MyGlobalResources.Add)
    //        {
    //            ingredient = new SRL_Ingredient();
    //            //ingredient = new Ingredient();
    //            list.Add(ingredient);
    //        }
    //        else if (!string.IsNullOrEmpty(commandArg))
    //        {
    //            int itemIndex = int.Parse(commandArg);
    //            ingredient = list[itemIndex];
    //        }

    //        if (ingredient == null)
    //            return;

    //        //ingredient.FoodName = this.txtFoodName.Text;
    //        //ingredient.Remarks = this.txtFoodRemark.Text;
    //        ingredient.Quantity = 0.0M;
    //        //if (!string.IsNullOrEmpty(this.txtQuantity.Text))
    //        //{
    //        //    ingredient.Quantity = decimal.Parse(this.txtQuantity.Text);
    //        //}
    //        //if (!string.IsNullOrEmpty(ddlFractions.SelectedItem.Value))
    //        //{
    //        //    decimal quantity;
    //        //    decimal.TryParse(ddlFractions.SelectedItem.Value, out quantity);
    //        //    ingredient.Quantity += quantity;
    //        //}

    //        //ingredient.CompleteValue = this.txtQuantity.Text;
    //        //ingredient.FractionValue = this.ddlFractions.SelectedItem.Value;

    //        //ingredient.MeasurementUnitId = int.Parse(this.ddlMeasurementUnits.SelectedItem.Value);
    //        //ingredient.MeasurementUnitName = this.ddlMeasurementUnits.SelectedItem.Text;
    //        //ingredient.MEASUREMENT_NAME = this.ddlMeasurementUnits.SelectedItem.Text;

    //        this.Ingredients = list;
    //        //this.dlistIngredients.DataSource = this.Ingredients.ToArray();
    //        //this.dlistIngredients.DataBind();

    //        //this.txtFoodName.Text = "";
    //        //this.txtFoodRemark.Text = "";
    //        //this.txtQuantity.Text = "";
    //        //this.ddlFractions.Text = "";
    //        //this.ddlMeasurementUnits.SelectedIndex = 0;
    //        //this.txtFoodName.Focus();        

    //        //this.btnAddIngerdient.CommandArgument = MyGlobalResources.Add;
    //        //this.imgAdd.Visible = true;
    //        //this.imgUpdate.Visible = false;

    //        //this.UpdatePanel2.Update();
    //        //this.UpdatePanel3.Update();
    //        //this.UpdatePanel4.Update();
    //    }
    //    catch(Exception ex)
    //    {
    //    }
    //}

    //protected void btnAddIngerdient_Click(object sender, EventArgs e)
    //{
    //    //this.mpeRecipe.Show();

    //    this.Page.Validate("ingredients");
    //    if (!this.Page.IsValid)
    //    {
    //        return;
    //    }

    //    List<SRL_Ingredient> list = this.Ingredients;
    //    SRL_Ingredient ingredient = null;
    //    //List<Ingredient> list = Ingredients;
    //    //Ingredient ingredient = null;
    //    //if (this.btnAddIngerdient.Text == MyGlobalResources.Add)
    //    //{
    //    //    ingredient = new SRL_Ingredient();
    //    //    //ingredient = new Ingredient();
    //    //    list.Add(ingredient);
    //    //}
    //    //else if (!string.IsNullOrEmpty(this.btnAddIngerdient.Attributes["ItemIndex"]))
    //    //{
    //    //    int itemIndex = int.Parse(this.btnAddIngerdient.Attributes["ItemIndex"]);
    //    //    ingredient = list[itemIndex];
    //    //}

    //    if (ingredient == null)
    //        return;

    //    //ingredient.FoodName = this.txtFoodName.Text;
    //    //ingredient.Remarks = this.txtFoodRemark.Text;
    //    //ingredient.Quantity = 0;
    //    //if (!string.IsNullOrEmpty(this.txtQuantity.Text))
    //    //{
    //    //    ingredient.Quantity = decimal.Parse(this.txtQuantity.Text);
    //    //}
    //    //if (!string.IsNullOrEmpty(this.ddlFractions.SelectedItem.Value))
    //    //{
    //    //    ingredient.Quantity += decimal.Parse(this.ddlFractions.SelectedItem.Value);
    //    //}

    //    //ingredient.CompleteValue = this.txtQuantity.Text;
    //    //ingredient.FractionValue = this.ddlFractions.SelectedItem.Value;

    //    //ingredient.MeasurementUnitId = int.Parse(this.ddlMeasurementUnits.SelectedItem.Value);
    //    //ingredient.MeasurementUnitName = this.ddlMeasurementUnits.SelectedItem.Text;
    //    //ingredient.MEASUREMENT_NAME = this.ddlMeasurementUnits.SelectedItem.Text;

    //    this.Ingredients = list;
    //    //this.dlistIngredients.DataSource = this.Ingredients.ToArray();
    //    //this.dlistIngredients.DataBind();

    //    //this.txtFoodName.Text = "";
    //    //this.txtFoodRemark.Text = "";
    //    //this.txtQuantity.Text = "";
    //    //this.ddlFractions.Text = "";
    //    //this.txtFoodName.Focus();

    //    //this.btnAddIngerdient.Text = MyGlobalResources.Add;
    //    //this.btnAddIngerdient.Attributes["ItemIndex"] = "";

    //}

    //protected void btnUpdateIngredient_Click(object sender, EventArgs e)
    //{
    //    LinkButton btn = sender as LinkButton;
    //    DataListItem item = btn.Parent.Parent as DataListItem;

    //    List<SRL_Ingredient> list = this.Ingredients;
    //    SRL_Ingredient ingredient = list[item.ItemIndex];
    //    //List<Ingredient> list = Ingredients;
    //    //Ingredient ingredient = list[item.ItemIndex];

    //    //this.txtFoodName.Text = ingredient.FoodName;
    //    //this.txtFoodRemark.Text = ingredient.Remarks;

    //    //if (!string.IsNullOrEmpty(ingredient.CompleteValue))
    //    //{
    //    //    this.txtQuantity.Text = ingredient.CompleteValue;
    //    //}
    //    //else
    //    //{
    //    //    this.txtQuantity.Text = "";
    //    //}

    //    //if (!string.IsNullOrEmpty(ingredient.FractionValue))
    //    //{
    //    //    this.ddlFractions.Text = ingredient.FractionValue;
    //    //}
    //    //else
    //    //{
    //    //    this.ddlFractions.Text = "";
    //    //}

    //    //this.ddlMeasurementUnits.Text = ingredient.MeasurementUnitId.ToString();
    //    //this.txtFoodName.Focus();

    //    //this.btnAddIngerdient.Text = MyGlobalResources.Update;
    //    //this.btnAddIngerdient.Attributes["ItemIndex"] = item.ItemIndex.ToString();
    //    //this.btnAddIngerdient.CommandArgument = item.ItemIndex.ToString();
    //    //this.imgAdd.Visible = false;
    //    //this.imgUpdate.Visible = true;

    //    //this.UpdatePanel3.Update();
    //    //this.UpdatePanel4.Update();

    //    //this.mpeRecipe.Show();
    //}

    protected void dlistIngredients_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        PlaceHolder buttons = (PlaceHolder)e.Item.FindControl("buttons");
        buttons.Visible = !ReadOnly;
    }

    //protected void btnRemoveIngredient_Click(object sender, EventArgs e)
    //{
    //    LinkButton btn = sender as LinkButton;
    //    DataListItem item = btn.Parent.Parent as DataListItem;

    //    List<SRL_Ingredient> list = this.Ingredients;
    //    //List<Ingredient> list = Ingredients;
    //    list.RemoveAt(item.ItemIndex);
    //    this.Ingredients = list;

    //    //this.dlistIngredients.DataSource = this.Ingredients.ToArray();
    //    //this.dlistIngredients.DataBind();


    //    //this.txtFoodName.Text = "";
    //    //this.txtFoodRemark.Text = "";
    //    //this.txtQuantity.Text = "";
    //    //this.ddlFractions.SelectedIndex = 0;
    //    //this.ddlMeasurementUnits.SelectedIndex = 0;
    //    //this.txtFoodName.Focus();
    //    //this.btnAddIngerdient.CommandArgument = MyGlobalResources.Add;

    //    //this.UpdatePanel2.Update();
    //    //this.UpdatePanel3.Update();
    //    //this.UpdatePanel4.Update();

    //    //this.mpeRecipe.Show();
    //}

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        ToggleReadOnly();
    }

    private void ToggleReadOnly()
    {
        this.txtRecipeName.ReadOnly = ReadOnly;
        this.txtCategories.ReadOnly = ReadOnly;
        this.txtPreparationMethod.ReadOnly = ReadOnly;
        this.txtRemarks.ReadOnly = ReadOnly;
        //this.txtSource.ReadOnly = ReadOnly;
        //this.txtVideoLink.ReadOnly = ReadOnly;
        this.txtServings.ReadOnly = ReadOnly;
        this.txtTools.ReadOnly = ReadOnly;
        bool visible = !ReadOnly;
        //this.btnCategories.Visible = visible;
        this.divOptions.Visible = visible;
        //this.UpdatePanel3.Visible = visible;
        //this.UpdatePanel4.Visible = visible;
        //this.divIngredients.Visible = visible;
        //this.trOptions.Visible = visible;
        //this.trIngredients.Visible = visible;
        //this.trAddIngredients.Visible = visible;
        //this.trAddFood.Visible = visible;

        string css = "ingredients";
        if (ReadOnly)
            css += " ingredientsReadOnly";
        else
            css += " ingredientsEditable";

        //this.pnlIngredients.CssClass = css;

        this.btnsReadOnly.Visible = ReadOnly;
        this.btnsEditable.Visible = !this.btnsReadOnly.Visible;

        this.btnPrint.NavigateUrl = "~/Print.aspx?code=1&recipeId=" + this.RecipeId.ToString();        
    }

    private int GetTimeInMinutes(string time, int unitValue)
    {
        int timeInMinutes = 0;
        if (unitValue == 1) //minutes
        {
            timeInMinutes = decimal.ToInt32(decimal.Parse(time));            
        }
        if (unitValue == 2) //hours
        {
            timeInMinutes = decimal.ToInt32(decimal.Parse(time)*60);
        }

        return timeInMinutes;
    }

    private decimal GetTimeInCorrectUnits(int time, out int unit)
    {
        decimal timeInCorrectUnits = 0;
        unit = 1;

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
                unit = 2;
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
            unit = 2;
        }

        return timeInCorrectUnits;
    }

    public delegate void RefreshHandler();
    public event RefreshHandler RefreshData;

    public delegate void ShowCategoriesHandler(int recipeId, SRL_RecipeCategory[] arr);
    public event ShowCategoriesHandler ShowCategoriesClick;

    public delegate void SelectPictureHandler(int recipeId);
    public event SelectPictureHandler SelectPictureClick;
}
