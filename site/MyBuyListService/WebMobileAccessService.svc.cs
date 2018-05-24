using System.Text;
using System.ServiceModel.Activation;
using MyBuyList.BusinessLayer;
using System.Runtime.Serialization.Json;
using System.IO;
using MyBuyList.Shared.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System;
using System.ServiceModel.Web;
using System.Net;

namespace MyBuyListService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode =AspNetCompatibilityRequirementsMode.Allowed)]
    public class WebMobileAccessService : IWebMobileAccessService
    {
        public string HelloWorld()
        {
            return "Hello World";
        }

        public string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        public string TranslateQuantity(string quantity)
        {
            try
            {
                string displayQuantity = "";
                string[] arr = quantity.Split(new string[] { ".", "," }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    if (arr[0] != "0")
                        displayQuantity = arr[0];
                }
                if (arr.Length > 1 && arr[1] != "" && arr[1] != "00")
                {
                    while (arr[1].EndsWith("0"))
                    {
                        arr[1] = arr[1].Remove(arr[1].Length - 1, 1);
                    }

                    if (arr[1] == "25")
                    {
                        displayQuantity = "¼" + displayQuantity;
                    }
                    else if (arr[1] == "3" || arr[1] == "33" || arr[1] == "34")
                    {
                        displayQuantity = "⅓" + displayQuantity;
                    }
                    else if (arr[1] == "5")
                    {
                        displayQuantity = "½" + displayQuantity;
                    }
                    else if (arr[1] == "6" || arr[1] == "66" || arr[1] == "67")
                    {
                        displayQuantity = "⅔" + displayQuantity;
                    }
                    else if (arr[1] == "75")
                    {
                        displayQuantity = "¾" + displayQuantity;
                    }
                    else
                    {
                        displayQuantity = quantity;
                        while (displayQuantity.EndsWith("0"))
                        {
                            displayQuantity = displayQuantity.Remove(displayQuantity.Length - 1, 1);
                        }
                        if (displayQuantity.EndsWith("."))
                        {
                            displayQuantity = displayQuantity.Remove(displayQuantity.Length - 1, 1);
                        }
                    }
                }
                return displayQuantity;
            }
            catch
            {
                return null;
            }
        }

        public void UpdateSavedListItem(int id, int quantity)
        {
            try
            {
                BusinessFacade.Instance.UpdateSavedListItem(id, quantity);
            }
            catch { }
        }

        public void UpdateMissingListItem(int id, int quantity)
        {
            try
            {
                BusinessFacade.Instance.UpdateMissingListItem(id, quantity);
            }
            catch { }
        }

        public bool SaveUser(string userName, string password, string email, bool recieveUpdates, string firstName, string lastName, string displayName)
        {
            try
            {
                BusinessFacade.Instance.SaveUser(new User
                {
                    Name = userName,
                    Password = password,
                    Email = email,
                    AgreeToMail = recieveUpdates,
                    FirstName = firstName,
                    LastName = lastName,
                    UserTypeId = 2,
                    DisplayName = displayName
                });

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public string GetFoodList(string prefix)
        {
            try
            {
                IEnumerable<Food> foodList = BusinessFacade.Instance.GetFoodsList().Where(p => p.FoodName.Contains(prefix)).Take(6);
                string result = JsonSerializer<IEnumerable<Food>>(foodList);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public string GetAllFoodList()
        {
            IEnumerable<Food> foodList = BusinessFacade.Instance.GetFoodList();
            string result = JsonSerializer<IEnumerable<Food>>(foodList);
            return result;
        }

        public string ItemsAutocomplete(string prefix)
        {
            try
            {
                string[] foodList = BusinessFacade.Instance.GetFoodList(prefix);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(foodList);
            }
            catch
            {
                return null;
            }
        }

        //public string GetRecipe(int recipeId)
        //{
        //    try
        //    {
        //        Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeId);
        //        string result = JsonSerializer<Recipe>(recipe);
        //        return result;
        //    }
        //    catch(Exception ex)
        //    {
        //        Logger.Write("Service1.GetRecipe -> Failed", ex, Logger.Level.Error);
        //        return null;
        //    }
        //}

        public void UpdateSavedList(int listId, bool shoppingList)
        {
            try
            {
                BusinessFacade.Instance.UpdateSaveList(listId, shoppingList);
            }
            catch { }
        }

        public void DeleteSavedListItem(int itemId)
        {
            try
            {
                BusinessFacade.Instance.DeleteSavedListItem(itemId);
            }
            catch { }
        }

        public string GetSavedListItems(int listId)
        {
            try
            {
                IQueryable<SavedListDetail> savedLists = BusinessFacade.Instance.GetSavedListDetails(listId);
                string result = JsonSerializer<IEnumerable<SavedListDetail>>(savedLists);
                return result;
            }
            catch { return null; }
        }

        public string GetSavedLists(int userId)
        {
            try
            {
                IQueryable<SavedList> savedLists = BusinessFacade.Instance.GetSavedLists(userId);
                string result = JsonSerializer<IEnumerable<SavedList>>(savedLists);
                return result;
            }
            catch { return null; }
        }

        public void DeleteSavedList(int listId)
        {
            try
            {
                BusinessFacade.Instance.DeleteSavedList(listId);
            }
            catch { }
        }

        public string AddSavedListItem(int listId, string itemName, int quantity)
        {
            try
            {
                Food food = BusinessFacade.Instance.GetFood(itemName);
                if (food == null)
                {
                    BusinessFacade.Instance.SaveFood(new Food
                    {
                        FoodName = itemName,
                        FoodCategoryId = 0
                    });
                }

                SavedListDetail savedListDetail = BusinessFacade.Instance.AddSavedListItem(itemName, quantity, listId);
                string result = JsonSerializer<SavedListDetail>(savedListDetail);
                return result;
            }
            catch 
            {
                return null;
            }
        }

        public string AddSavedList(int userId, string listName)
        {
            try
            {
                SavedList list = BusinessFacade.Instance.AddSavedList(userId, listName);
                string result = JsonSerializer<SavedList>(list);
                return result;
            }
            catch { return null; }
        }

        public void RemoveMenuFromShoppingList(int userId, int menuId)
        {
            try
            {
                BusinessFacade.Instance.RemoveMenuFromShoppingList(userId, menuId);
            }
            catch { }
        }

        public string GetMenusInShoppingList(int userId)
        {
            try
            {
                IQueryable<MenusInShoppingList> menusInShoppingList = BusinessFacade.Instance.GetMenusInShoppingList(userId);
                string result = JsonSerializer<IEnumerable<MenusInShoppingList>>(menusInShoppingList);
                return result;
            }
            catch { return null; }
        }

        public void AddMenuToShoppingList(int userId, int menuId, bool check)
        {
            try
            {
                BusinessFacade.Instance.AddMenuToShoppingList(userId, menuId, check);
            }
            catch { }
        }

        public string SearchMenus(int userId, string searchValue)
        {
            try
            {
                IEnumerable<Menu> menus = BusinessFacade.Instance.SearchMenus(userId).Where(p=>p.MenuName.Contains(searchValue) && p.MenuTypeId != 4);
                string result = JsonSerializer<IEnumerable<Menu>>(menus);
                return result;
            }
            catch { return null; }
        }

        public void RemoveRecipeFromShoppingList(int userId, int recipeId)
        {
            try
            {
                BusinessFacade.Instance.RemoveRecipeFromShoppingList(userId, recipeId);
            }
            catch { }
        }

        public string GetSelectedRecipes(int userId)
        {
            try
            {
                IQueryable<RecipesInShoppingList> selectedRecipes = BusinessFacade.Instance.GetSelectedRecipes(userId);
                string result = JsonSerializer<IEnumerable<RecipesInShoppingList>>(selectedRecipes);
                return result;
            }
            catch { return null; }
        }

        public string GetRecipe(int recipeId)
        {
            try
            {
                Recipe recipe = BusinessFacade.Instance.GetRecipe(recipeId);
                string result = JsonSerializer<Recipe>(recipe);
                return result;
            }
            catch { return null; }
        }

        public void DeleteFromMissingList(int id)
        {
            try
            {
                BusinessFacade.Instance.DeleteFromMissingList(id);
            }
            catch
            {
            }
        }

        public string GetMissingListDetails(int userId)
        {
            try
            {
                IQueryable<MissingListDetail> missingListDetail = BusinessFacade.Instance.GetMissingListDetails(userId);
                string result = JsonSerializer<IEnumerable<MissingListDetail>>(missingListDetail);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public void AddRecipeToShoppingList(int userId, int recipeId)
        {
            try
            {
                BusinessFacade.Instance.AddRecipeToShoppingList(userId, recipeId);
            }
            catch { }
        }

        public string SearchRecipes(string searchValue)
        {
            try
            {
                IEnumerable<Recipe> recipes = BusinessFacade.Instance.GetRecipesListByFreeText(searchValue).AsEnumerable().Where(p => p.IsPublic);
                string result = JsonSerializer<IEnumerable<Recipe>>(recipes);
                return result;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void CheckShoppingListItem(int userId, int foodId, bool active)
        {
            try
            {
                BusinessFacade.Instance.CheckShoppingListItem(userId, foodId, active);
            }
            catch
            {

            }
        }

        public string ShoppingList(string userId)
        {
            string result = string.Empty;
            try
            {
                IEnumerable<UserShoppingList> shoppingList = BusinessFacade.Instance.GetShoppingList(int.Parse(userId));
                result = JsonSerializer<IEnumerable<UserShoppingList>>(shoppingList);
                if (result != "[]")
                    return result;
                result = string.Empty;
            }
            catch(Exception ex)
            {
            }

            return result;
        }

        public string AddToMissingList(int userId, string item, int quantity)
        {
            try
            {
                int listId = BusinessFacade.Instance.AddMissingList(userId);

                Food food = BusinessFacade.Instance.GetFood(item);
                if (food == null)
                {
                    BusinessFacade.Instance.SaveFood(new Food
                        {
                            FoodName = item,
                            FoodCategoryId = 0
                        });
                }

                MissingListDetail missingListDetail = BusinessFacade.Instance.AddMissingListItem(item, quantity, listId);
                string result = JsonSerializer<MissingListDetail>(missingListDetail);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string CheckCredencials(string userName, string password)
        {
            return ServiceMethods.CheckCredencials(userName, password);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
