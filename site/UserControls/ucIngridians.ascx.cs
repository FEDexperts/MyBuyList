using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using Resources;

namespace UserControls
{
    public partial class UcIngridians : System.Web.UI.UserControl
    {
        public delegate void AddItemEvent(SRL_Ingredient ingredient);

        public event AddItemEvent AddItem;

        public List<SRL_Ingredient> Ingredients
        {
            get
            {
                if (ViewState["Ingredients"] == null)
                {
                    ViewState["Ingredients"] = new List<SRL_Ingredient>();
                }
                return (List<SRL_Ingredient>) ViewState["Ingredients"];
            }
            set
            {
                if (value == null)
                {
                    dlistIngredients.DataSource = null;
                }
                else
                {
                    dlistIngredients.DataSource = value.ToArray();
                }

                ViewState["Ingredients"] = value;
                dlistIngredients.DataBind();
            }
        }

        protected void OnAddItem(SRL_Ingredient e)
        {
            if (AddItem != null)
                AddItem(e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtServings.Text = "1";
                //ddlFractions.Items.Add(new ListItem("", ""));
                //ddlFractions.Items.Add(new ListItem("¼", "0.25"));
                //ddlFractions.Items.Add(new ListItem("⅓", "0.33"));
                //ddlFractions.Items.Add(new ListItem("½", "0.50"));
                //ddlFractions.Items.Add(new ListItem("⅔", "0.66"));
                //ddlFractions.Items.Add(new ListItem("¾", "0.75"));


                ddlMeasurementUnits.DataSource = BusinessFacade.Instance.GetMeasurementUnitsList();
                ddlMeasurementUnits.DataTextField = "UnitName";
                ddlMeasurementUnits.DataValueField = "UnitId";
                ddlMeasurementUnits.DataBind();
            }
        }

        protected void btnAddItemCommand(object sender, CommandEventArgs e)
        {
            List<SRL_Ingredient> list = Ingredients;
            SRL_Ingredient ingredient = null;
            string commandArg = e.CommandArgument as string;

            if (string.IsNullOrEmpty(txtFoodName.Text) || string.IsNullOrEmpty(txtQuantity.Text))
                return;

            if (commandArg == MyGlobalResources.Add)
            {
                ingredient = list.Find(f => f.FoodName == txtFoodName.Text);
                if (ingredient == null)
                {
                    ingredient = new SRL_Ingredient();
                    list.Add(ingredient);
                    ingredient.Quantity = 0;
                }
            }
            else if (!string.IsNullOrEmpty(commandArg))
            {
                int itemIndex = int.Parse(commandArg);
                ingredient = list[itemIndex];
                ingredient.Quantity = 0;
            }

            if (ingredient == null)
                return;

            ingredient.FoodName = txtFoodName.Text;
            if (!string.IsNullOrEmpty(txtQuantity.Text))
            {
                ingredient.Quantity += decimal.Parse(txtQuantity.Text);
            }
            //if (!string.IsNullOrEmpty(ddlFractions.SelectedItem.Value))
            //{
            //    ingredient.Quantity += decimal.Parse(ddlFractions.SelectedItem.Value);
            //}

            int completeValue = ((int) ingredient.Quantity);
            //decimal fractionValue = ingredient.Quantity - completeValue;

            ingredient.CompleteValue = completeValue.ToString();
            //ingredient.FractionValue = fractionValue.ToString();

            ingredient.MeasurementUnitId = int.Parse(ddlMeasurementUnits.SelectedItem.Value);
            ingredient.MeasurementUnitName = ddlMeasurementUnits.SelectedItem.Text;

            Ingredients = list;
            dlistIngredients.DataSource = Ingredients.ToArray();
            dlistIngredients.DataBind();

            txtFoodName.Text = "";
            txtQuantity.Text = "";
            //ddlFractions.Text = "";
            ddlMeasurementUnits.SelectedIndex = 0;

            btnAddIngerdient.CommandArgument = MyGlobalResources.Add;
            imgAdd.Visible = true;
            imgUpdate.Visible = false;

            UpdatePanel2.Update();
            UpdatePanel3.Update();
            UpdatePanel4.Update();

            OnAddItem(ingredient);
        }

        protected void custValidIngredients_ServerValidate(object source, ServerValidateEventArgs args)
        {
            
        }

        protected void dlistIngredients_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            
        }

        protected void btnUpdateIngredient_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            DataListItem item = btn.Parent.Parent as DataListItem;

            List<SRL_Ingredient> list = Ingredients.ToList();
            SRL_Ingredient ingredient = list[item.ItemIndex];

            txtFoodName.Text = ingredient.FoodName;

            if (!string.IsNullOrEmpty(ingredient.CompleteValue))
            {
                txtQuantity.Text = ingredient.CompleteValue;
            }
            else
            {
                txtQuantity.Text = "";
            }

            //if (!string.IsNullOrEmpty(ingredient.FractionValue))
            //{
            //    ddlFractions.Text = ingredient.FractionValue;
            //}
            //else
            //{
            //    ddlFractions.Text = "";
            //}

            ddlMeasurementUnits.Text = ingredient.MeasurementUnitId.ToString();
            
            btnAddIngerdient.CommandArgument = item.ItemIndex.ToString();
            imgAdd.Visible = false;
            imgUpdate.Visible = true;

            UpdatePanel3.Update();
            UpdatePanel4.Update();
        }

        protected void btnRemoveIngredient_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            if (btn != null)
            {
                DataListItem item = btn.Parent.Parent as DataListItem;

                List<SRL_Ingredient> list = Ingredients;
                if (item != null) list.RemoveAt(item.ItemIndex);
                Ingredients = list;
            }

            dlistIngredients.DataSource = Ingredients.ToArray();
            dlistIngredients.DataBind();


            txtFoodName.Text = "";
            txtQuantity.Text = "";
            //ddlFractions.SelectedIndex = 0;
            ddlMeasurementUnits.SelectedIndex = 0;
            btnAddIngerdient.CommandArgument = MyGlobalResources.Add;

            UpdatePanel2.Update();
            UpdatePanel3.Update();
            UpdatePanel4.Update();
        }
    }
}