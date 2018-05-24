<%@ WebHandler Language="C#" Class="ShowPicture" %>

using System;
using System.Web;
using System.IO ;
using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;

public class ShowPicture : IHttpHandler 
{       
    public void ProcessRequest (HttpContext context)    {
        
        Stream stream = null;
        context.Response.ContentType = "image/jpeg";
        
        if (context.Request.QueryString["RecipeId"] != null)
        {
            int recipeId = Convert.ToInt32(context.Request.QueryString["RecipeId"]);

            stream = ShowRecipePicture(recipeId);
        }
        else if (context.Request.QueryString["FoodId"] != null)
        {
            int foodId = Convert.ToInt32(context.Request.QueryString["FoodId"]);

            stream = ShowFoodPicture(foodId);
        }
        else if (context.Request.QueryString["MenuId"] != null)
        {
            int menuId = Convert.ToInt32(context.Request.QueryString["MenuId"]);

            stream = ShowMenuPicture(menuId);
        }
        else if (context.Request.QueryString["picture"] != null)
        {
            string filename = context.Request.QueryString["picture"];

            stream = ShowPictureFromFile(filename);
        }
         
        
        byte[] buffer = new byte[4096];

        if (stream != null)
        {
            int byteSeq = stream.Read(buffer, 0, 4096);

            while (byteSeq > 0)
            {
                context.Response.OutputStream.Write(buffer, 0, byteSeq);
                byteSeq = stream.Read(buffer, 0, 4096);
            }
        } 
    }

    public Stream ShowRecipePicture(int recipe_Id)
    {
        Recipe ri = BusinessFacade.Instance.GetRecipe(recipe_Id);
      
        try
        {
            System.Data.Linq.Binary b = ri.Picture;
            return new MemoryStream(b.ToArray());
        }
        catch
        {
            return null;
        }
    }

    public Stream ShowFoodPicture(int food_Id)
    {
        Food f = BusinessFacade.Instance.GetFood(food_Id);

        try
        {
            System.Data.Linq.Binary b = f.Picture;
            return new MemoryStream(b.ToArray());
        }
        catch
        {
            return null;
        }
    }

    public Stream ShowMenuPicture(int menu_Id)
    {
        MyBuyList.Shared.Entities.Menu m = BusinessFacade.Instance.GetMenu(menu_Id);

        try
        {
            System.Data.Linq.Binary b = m.Picture;
            return new MemoryStream(b.ToArray());
        }
        catch
        {
            return null;
        }
    }

    public Stream ShowPictureFromFile(string filename)
    {
        filename = Path.Combine(Path.GetTempPath(), filename);
        try
        {
            if (File.Exists(filename))
            {
                return new MemoryStream(File.ReadAllBytes(filename));
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }
    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}