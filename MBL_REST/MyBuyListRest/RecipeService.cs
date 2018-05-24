using MyBuyList.BusinessLayer;
using MyBuyList.DataLayer;
using MyBuyListRest.Responses;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace MyBuyListRest
{
    [DataContract]
    public class RecipeServiceResponse
    {
        [DataMember]
        public Exception ex { get; set; }
        //[DataMember]
        //public List<RecipeModel> recipes { get; set; }

        //public RecipeServiceResponse()
        //{
        //    recipes = new List<RecipeModel>();
        //}
    }

    [ServiceContract]
    public interface IRecipeService
    {
        [OperationContract]
        [WebGet(UriTemplate = "HelloWorld", ResponseFormat = WebMessageFormat.Json)]
        string HelloWorld();

        [OperationContract]
        [WebGet(UriTemplate = "/list", ResponseFormat = WebMessageFormat.Json)]
        RecipeServiceResponse GetRecipes();

        //[OperationContract]
        //[WebGet(UriTemplate = "/{id}", ResponseFormat = WebMessageFormat.Json)]
        //RecipeModel GetRecipe(string id);

        //[OperationContract]
        //[WebGet(UriTemplate = "/sort/{name}", ResponseFormat = WebMessageFormat.Json)]
        //List<RecipeModel> GetRecipesByName(string name);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void ShoppingList(string body);

        [OperationContract]
        [WebGet(UriTemplate = "Ingridiants?prefix={prefix}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        IngridiantsResponse GetIngridiants(string prefix);
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RecipeService : IRecipeService
    {
        public string HelloWorld()
        {
            return "Hello World";
        }

        //public RecipeModel GetRecipe(string id)
        //{
        //    return RecipeDL.GetRecipes(int.Parse(id));
        //}

        public RecipeServiceResponse GetRecipes()
        {
            RecipeServiceResponse response = new RecipeServiceResponse();
            //try
            //{
            //    response.recipes = RecipeDL.GetRecipes();
            //}
            //catch(Exception ex)
            //{
            //    response.ex = ex;
            //}
            return response;
        }

        //public List<RecipeModel> GetRecipesByName(string name)
        //{
        //    return RecipeDL.GetRecipesByName(name);
        //}

        public void ShoppingList(string body)
        {
        }

        public IngridiantsResponse GetIngridiants(string prefix)
        {
            IngridiantsResponse response = new IngridiantsResponse()
            {
                message = prefix,
                list = BusinessFacade.Instance.GetFoodList(prefix)
            };
            return response;
        }
    }
}