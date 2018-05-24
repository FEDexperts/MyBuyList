using MyBuyList.BusinessLayer;
using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using MyBuyList.Shared;
using MyBuyListRest.Responses;
using MyBuyList.Shared.Entities;
using System.Linq;
using System.Collections.Generic;

namespace MyBuyListRest
{
    [DataContract]
    public class Request
    {
        [DataMember]
        public int userId { get; set; }
        [DataMember]
        public int foodId { get; set; }
    }

    [DataContract]
    public class RemoveRecipeRequest
    {
        [DataMember]
        public int userId { get; set; }
        [DataMember]
        public int recipeId { get; set; }
    }

    [DataContract]
    public class AddRecipeRequest
    {
        [DataMember]
        public int userId { get; set; }
        [DataMember]
        public int recipeId { get; set; }
    }

    [DataContract]
    public class NewItemRequest
    {
        [DataMember]
        public int userId { get; set; }
        [DataMember]
        public string foodName { get; set; }
        [DataMember]
        public int measureUnit { get; set; }
        [DataMember]
        public int quantity { get; set; }
    }

    [DataContract]
    public class Response
    {
        [DataMember]
        public string message { get; set; }
    }

    [ServiceContract]
    public interface IShoppingListService
    {
        [OperationContract]
        [WebGet(UriTemplate = "HelloWorld", ResponseFormat = WebMessageFormat.Json)]
        string HelloWorld();

        [OperationContract]
        [WebInvoke(UriTemplate = "Add", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        bool AddItem(NewItemRequest request);

        [OperationContract]
        [WebInvoke(UriTemplate = "Remove", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        Response RemoveItem(Request data);

        [OperationContract]
        [WebGet(UriTemplate = "MeasureUnits", ResponseFormat = WebMessageFormat.Json)]
        MeasureUnitsResponse GetMeasureUnits();

        [OperationContract]
        [WebGet(UriTemplate = "Recipes?userId={userId}", ResponseFormat = WebMessageFormat.Json)]
        IQueryable<RecipesInShoppingList> GetRecipes(string userId);

        [OperationContract]
        [WebGet(UriTemplate = "ShoppingList?userId={userId}", ResponseFormat = WebMessageFormat.Json)]
        ShoppingListResponse GetShoppingList(string userId);

        [OperationContract]
        [WebInvoke(UriTemplate = "removeRecipe", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        Response RemoveRecipe(RemoveRecipeRequest data);

        [OperationContract]
        [WebInvoke(UriTemplate = "addRecipe", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        Response AddRecipe(AddRecipeRequest data);
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ShoppingListService : IShoppingListService
    {
        public bool AddItem(NewItemRequest request)
        {
            try
            {
                BusinessFacade.Instance.AddMissingListItem(request.foodName, request.quantity, request.measureUnit, request.userId);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public Response AddRecipe(AddRecipeRequest data)
        {
            Response response = new Response();
            try
            {
                BusinessFacade.Instance.AddRecipeToShoppingList(data.userId, data.recipeId);
                response.message = "Success";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public MeasureUnitsResponse GetMeasureUnits()
        {
            var response = new MeasureUnitsResponse();
            try
            {
                List<MeasurementUnit> measurementUnit = BusinessFacade.Instance.GetMeasurementUnitsList().ToList();
                response.measureUnits = (from p in measurementUnit
                                         where p.EnabledInShoppingList.HasValue ? p.EnabledInShoppingList.Value : false
                                         select new MeasureUnit
                                         {
                                             unitId = p.UnitId,
                                             unitName = p.UnitName,
                                             enabledInShoppingList = p.EnabledInShoppingList.HasValue ? p.EnabledInShoppingList.Value : false
                                         }).ToList();
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public IQueryable<RecipesInShoppingList> GetRecipes(string userId)
        {
            IQueryable<RecipesInShoppingList> recipes = BusinessFacade.Instance.GetSelectedRecipes(int.Parse(userId));
            return recipes;
        }

        public ShoppingListResponse GetShoppingList(string userId)
        {
            ShoppingListResponse response = new ShoppingListResponse();

            try
            {
                List<UserShoppingList> shoppingList = BusinessFacade.Instance.GetShoppingList(int.Parse(userId));

                if (shoppingList.Any())
                {
                    foreach (UserShoppingList item in shoppingList)
                    {
                        if (!response.categories.Exists(x => x.id == item.CATEGORY_ID))
                        {
                            var categoryItems = shoppingList.Where(x => x.CATEGORY_ID == item.CATEGORY_ID);

                            response.categories.Add(new Responses.Category
                            {
                                id = item.CATEGORY_ID.HasValue ? item.CATEGORY_ID.Value : 0,
                                name = item.CATEGORY_NAME,
                                foodItems = categoryItems.Select(x => new Responses.Food { id = x.FOOD_ID, name = x.FOOD_NAME, quantity = x.FriendlyQuantity, canDelete = x.CAN_DELETE.HasValue ? x.CAN_DELETE.Value : false, measure = x.MEASUREMENT_NAME }).ToList(),
                            });
                        }
                    }
                }
                else
                {
                    response.message = "Shopping list is empty";
                }
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
                if (ex.InnerException != null)
                {
                    response.innerMessage = ex.InnerException.Message;
                }
            }

            return response;
        }

        public string HelloWorld()
        {
            return "Hello World";
        }

        public Response RemoveItem(Request data)
        {
            Response response = new Response();
            try
            {
                BusinessFacade.Instance.RemoveItemFromShoppingList(data.userId, data.foodId);
                response.message = "Success";
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }

        public Response RemoveRecipe(RemoveRecipeRequest data)
        {
            Response response = new Response();
            try
            {
                BusinessFacade.Instance.RemoveRecipeFromShoppingList(data.userId, data.recipeId);
                response.message = "Success";
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
    }
}