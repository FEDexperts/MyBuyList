using System;
using System.Collections;
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
using System.Data.Linq;
using System.IO;
using System.Drawing;

using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using System.Text;

public partial class ucRecipePicture : System.Web.UI.UserControl
{
    int RecipeId
    {
        get { return ViewState["RecipeId"] == null ? 0 : (int)ViewState["RecipeId"]; }
        set { ViewState["RecipeId"] = value; }
    }

    public void ShowPicture(int recipeId)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "_onPicturePopupDisplay", "_onSelectPictureLoaded();", true);       
    }
}
