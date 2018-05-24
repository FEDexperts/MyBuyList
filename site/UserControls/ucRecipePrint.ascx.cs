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
using System.IO;

public partial class UserControls_RecipePrint : System.Web.UI.UserControl
{
    Recipe recipe;
    int RecipeId
    {
        get { return recipe == null ? 0 : recipe.RecipeId; }
    }

    public void Bind(int recipeId, int[] ServingsList)
    {
        this.recipe = BusinessFacade.Instance.GetRecipe(recipeId);

        if (ServingsList == null)
        {
            ServingsList = new int[] { recipe.Servings };
        }

        if (recipe.Ingredients.Count > 0)
        {
            this.lvServs.DataSource = ServingsList;
            this.lvServs.DataBind();
        }
        else
        {
            this.lvServs.Visible = false;
        }

        Ingredient[] toPrint = recipe.Ingredients.Where(i => i.Food.PrintPicture == true).ToArray();
        Dictionary<int, string> foodArray = new Dictionary<int, string>();

        foreach (Ingredient ing in toPrint)
        {
            if (!foodArray.ContainsKey(ing.FoodId))
            {
                foodArray.Add(ing.FoodId, ing.Food.FoodName);
            }
        }

        if (foodArray != null && foodArray.Count > 0)
        {
            if (foodArray.Count > 3)
            {
                Random random = new Random();
                int[] arrIndex = new int[3];
                arrIndex[0] = random.Next(0, foodArray.Count - 1);
                arrIndex[1] = random.Next(0, foodArray.Count - 1);
                arrIndex[2] = random.Next(0, foodArray.Count - 1);

                this.imgContainer1.ImageUrl = "~/ShowPicture.ashx?FoodId=" + foodArray.ElementAt(arrIndex[0]).Key;
                this.imgContainer1.Visible = true;
                this.lblimgContainer1.Text = foodArray.ElementAt(arrIndex[0]).Value;

                this.imgContainer2.ImageUrl = "~/ShowPicture.ashx?FoodId=" + foodArray.ElementAt(arrIndex[1]).Key;
                this.imgContainer2.Visible = true;
                this.lblimgContainer2.Text = foodArray.ElementAt(arrIndex[1]).Value;

                this.imgContainer3.ImageUrl = "~/ShowPicture.ashx?FoodId=" + foodArray.ElementAt(arrIndex[2]).Key;
                this.imgContainer3.Visible = true;
                this.lblimgContainer3.Text = foodArray.ElementAt(arrIndex[2]).Value;
            }
            else
            {
                int n = 1;
                foreach (KeyValuePair<int, string> ing in foodArray)
                {
                    string namePic = "imgContainer" + n.ToString();
                    string nameLbl = "lblimgContainer" + n.ToString();

                    Image img = (Image)this.FindControl(namePic);

                    if (img != null)
                    {
                        img.ImageUrl = "~/ShowPicture.ashx?FoodId=" + ing.Key;
                        img.Visible = true;

                        Label lbl = (Label)this.FindControl(nameLbl);
                        lbl.Text = ing.Value;
                    }
                    ++n;
                }
            }
        }
        

        this.lblRecipeName.Text = recipe.RecipeName;
        this.ltrlPrep.Text = recipe.PreparationMethod.Replace("\n", "<br />");
        this.lblPrepCommon.Text = string.Format("( ייתכן ומצוינות כמויות באופן ההכנה. במידה וכן, הן מתייחסות למס' המנות המקורי ({0} מנות) וצריך להתאים אותן למס' המנות הרלוונטי בעת ההכנה. )", recipe.Servings);
        if (recipe.Tools != null)
        {
            this.ltrlTools.Text = recipe.Tools.Replace("\n", "<br />");
        }
        else
        {
            this.lblTools.Visible = false;
        }
    }

    protected void Servs_DataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem lvItem = e.Item as ListViewDataItem;
        int servings = (int)lvItem.DataItem;

        UserControls_PrintIngredients uc = lvItem.FindControl("printIngredients") as UserControls_PrintIngredients;
        uc.Bind(recipe.Ingredients.ToArray<Ingredient>(), recipe.Servings, (int)lvItem.DataItem);

    }
}
