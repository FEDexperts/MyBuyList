using System;
using System.Net;
using System.Text;
using System.Web;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;

public partial class RecipeDetailsScreenshot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request["recipeId"])) return;
        
        int RecipeId = int.Parse(Request["recipeId"]);
        Recipe currRecipe = BusinessFacade.Instance.GetRecipe(RecipeId);
        string category = "ReciepsScreenShots";
        
        byte[] data;
        string url;
        using (WebClient client = new WebClient())
        {
            url = string.Format("http://{0}{1}/Images/{2}/{3}.jpg", Request.Url.Host, Request.ApplicationPath,
                                       category, currRecipe.RecipeName);
            data = client.DownloadData(url);
        }
        try
        {

            string fileName = HttpUtility.UrlPathEncode(string.Format("{0}.jpg", currRecipe.RecipeName));

            Response.Clear();
            Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            Response.Buffer = true;
            Response.AddHeader("Content-Length", data.Length.ToString());
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
            Response.AddHeader("Expires", "0");
            Response.AddHeader("Pragma", "cache");
            Response.AddHeader("Cache-Control", "no-cache");
            Response.ContentType = string.Format("image/JPEG");
            Response.AddHeader("Accept-Ranges", "bytes");
            Response.BinaryWrite(data);
            Response.Flush();
            Response.End();
        }
        catch (Exception)
        {
        }
    }
}