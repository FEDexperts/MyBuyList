using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using ProperServices.Common.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Ingridiants : System.Web.UI.UserControl
{
    public List<SRL_Ingredient> ListOfIngediants
    {
        get
        {
            try
            {
                Logger.Write("Retrieve ListOfIngediants -> Start", Logger.Level.Info);

                if (!string.IsNullOrEmpty(hfIngridiants.Value))
                {
                    Logger.Write(string.Format("Retrieve ListOfIngediants -> Before DeSerialized, {0}", hfIngridiants.Value.Replace("{", "{{").Replace("}", "}}")), Logger.Level.Info);

                    // Convert from JSON to object
                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    List<SRL_Ingredient> ingredients = jsSerializer.Deserialize<List<SRL_Ingredient>>(hfIngridiants.Value);
                    ViewState["Ingredients"] = ingredients;

                    Logger.Write("Retrieve ListOfIngediants -> After DeSerialized", Logger.Level.Info);
                }

                if (ViewState["Ingredients"] == null)
                {
                    ViewState["Ingredients"] = new List<SRL_Ingredient>();
                }

                Logger.Write("Retrieve ListOfIngediants -> End", Logger.Level.Info);

                return (List<SRL_Ingredient>)ViewState["Ingredients"];
            }
            catch(Exception ex)
            {
                Logger.Write("Retrieve ListOfIngediants -> get failed", ex, Logger.Level.Error);
                return null;
            }
        }
        set
        {
            try
            {
                ViewState["Ingredients"] = value;

                Logger.Write("Retrieve ListOfIngediants -> Before Serialized", Logger.Level.Info);

                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                hfIngridiants.Value = jsSerializer.Serialize(value);

                Logger.Write(string.Format("Retrieve ListOfIngediants -> After Serialized, {0}", hfIngridiants.Value.Replace("{", "{{").Replace("}", "}}")), Logger.Level.Info);
            }
            catch(Exception ex)
            {
                Logger.Write("Retrieve ListOfIngediants -> set failed", ex, Logger.Level.Error);
                hfIngridiants.Value = string.Empty;
            }
        }
    }

    public string Ingridiants
    {
        get
        {
            return hfIngridiants.Value;
        }
    }

    public string DecimalSeperator
    {
        get
        {
            return ConfigurationManager.AppSettings["NumberSeperator"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        hfIngridiants.ValueChanged += HfIngridiants_ValueChanged;

        try
        {
            string decimalSeperator = DecimalSeperator;
            ddlFractions.Items.Add(new ListItem("", ""));
            ddlFractions.Items.Add(new ListItem("¼", string.Format("0{0}25", decimalSeperator)));
            ddlFractions.Items.Add(new ListItem("⅓", string.Format("0{0}33", decimalSeperator)));
            ddlFractions.Items.Add(new ListItem("½", string.Format("0{0}5", decimalSeperator)));
            ddlFractions.Items.Add(new ListItem("⅔", string.Format("0{0}66", decimalSeperator)));
            ddlFractions.Items.Add(new ListItem("¾", string.Format("0{0}75", decimalSeperator)));
            ddlFractions.Items.Add(new ListItem("⅛", string.Format("0{0}125", decimalSeperator)));

            ddlMeasurementUnits.DataSource = BusinessFacade.Instance.GetMeasurementUnitsList();
            ddlMeasurementUnits.DataTextField = "UnitName";
            ddlMeasurementUnits.DataValueField = "UnitId";
            ddlMeasurementUnits.DataBind();

            if (!string.IsNullOrEmpty(Ingridiants))
            {
                Logger.Write(string.Format("AjaxIngrediants.Page_Load -> Before Show List, {0}", Ingridiants.Replace("{", "{{").Replace("}", "}}")), Logger.Level.Info);

                string script = string.Format("ShowSavedListIngridiant({0})", Ingridiants);
                Page.ClientScript.RegisterStartupScript(GetType(), "ShowIngridiant", script, true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "ShowIngridiant", "InitAjaxIngediantsControl();", true);
            }
        }
        catch(Exception ex)
        {
            Logger.Write("AjaxIngrediants.Page_Load -> failed", ex, Logger.Level.Error);
        }
    }

    private void HfIngridiants_ValueChanged(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}