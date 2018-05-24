using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyBuyListRest.Responses
{
    [DataContract]
    public class ShoppingListResponse
    {
        [DataMember]
        public List<Category> categories { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string innerMessage { get; set; }

        public ShoppingListResponse()
        {
            categories = new List<Category>();
        }
    }

    [DataContract]
    public class Category
    {
        [DataMember]
        public int? id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public List<Food> foodItems { get; set; }

        public Category()
        {
            foodItems = new List<Food>();
        }
    }

    [DataContract]
    public class Food
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string quantity { get; set; }
        [DataMember]
        public bool? canDelete { get; set; }
        [DataMember]
        public string measure{ get; set; }
    }
}