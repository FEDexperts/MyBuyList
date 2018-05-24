using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MyBuyListRest.Responses
{
    [DataContract]
    public class IngridiantsResponse
    {
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public Dictionary<int, string> list { get; set; }
    }
}