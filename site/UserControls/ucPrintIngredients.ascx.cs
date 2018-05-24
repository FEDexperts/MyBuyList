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

using MyBuyList.BusinessLayer.Managers;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;

public partial class UserControls_PrintIngredients : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Bind(Ingredient[] ingredients, int recipeServ, int servings)
    {
        if (ingredients.Length > 0)
        {
            this.lblIngridTitle.Text = "מצרכים לארוחה בעלת " + servings + " מנות:";
            //List<SRL_Ingredient> ingredientList = new List<SRL_Ingredient>();
            //foreach (Ingredient currIngredient in ingredients)
            //{
            //    //Ingredient tempIngredien = cuurIngredien;
            //    currIngredient.Quantity = Decimal.Round((currIngredient.Quantity / recipeServ) * servings, 2);
            //    SRL_Ingredient currSRLIngredient = new SRL_Ingredient(currIngredient);

            //    ingredientList.Add(currSRLIngredient);
            //}

            // List<Ingredient> ingredientList = new List<Ingredient>();
            foreach (Ingredient currIngredient in ingredients)
            {
                //Ingredient tempIngredien = cuurIngredien;
                if (recipeServ != 0)
                {
                    currIngredient.Quantity = Decimal.Round((currIngredient.Quantity / recipeServ) * servings, 2);
                }
                else
                {
                    currIngredient.Quantity = currIngredient.Quantity;
                }
                //SRL_Ingredient currSRLIngredient = new SRL_Ingredient(currIngredient);

                // ingredientList.Add(currSRLIngredient);
            }

            this.rptIngridList.DataSource = ingredients;
            this.rptIngridList.DataBind();



        }
    }

    protected void IngridItem_DataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem rptItem = e.Item as RepeaterItem;
        Ingredient ingridItem = (Ingredient)rptItem.DataItem;

        Label value = rptItem.FindControl("lblIngrValue") as Label;

        value.Text = BusinessFacade.Instance.GetNumberWithFreg(ingridItem.Quantity);

        //Label unitName = rptItem.FindControl("lblUnitName") as Label;
        //unitName.Text = BusinessFacade.Instance.GetMeasurementUnit(ingridItem.;
    }

    //protected void Recipe_DataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    RepeaterItem rptItem = e.Item as RepeaterItem;

    //    Label lblFreg = rptItem.FindControl("lblIngrFreg") as Label;
    //    lblFreg.Text = (rptItem.DataItem as SRL_Ingredient).FractionValue;

    //    Label lblComplete = rptItem.FindControl("lblIngrCompleteValue") as Label;
    //    lblComplete.Text = (rptItem.DataItem as SRL_Ingredient).CompleteValue;

    //    Label lblUnit = rptItem.FindControl("lblUnitName") as Label;
    //    lblUnit.Text = (rptItem.DataItem as SRL_Ingredient).MeasurementUnitName;

    //    Label lblName = rptItem.FindControl("lblIngridName") as Label;
    //    lblName.Text = (rptItem.DataItem as SRL_Ingredient).FoodName;

    //    Label lblRemark = rptItem.FindControl("lblRemarks") as Label;
    //    lblRemark.Text = (rptItem.DataItem as SRL_Ingredient).Remarks;

    //}
}
