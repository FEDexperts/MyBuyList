using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyBuyListRest.Responses
{
    [DataContract]
    public class MeasureUnit
    {
        [DataMember]
        public int unitId { get; set; }
        [DataMember]
        public string unitName { get; set; }
        [DataMember]
        public bool enabledInShoppingList { get; set; }
    }

    [DataContract]
    public class MeasureUnitsResponse
    {
        [DataMember]
        public List<MeasureUnit> measureUnits { get; set; }
        [DataMember]
        public string message { get; set; }

        public MeasureUnitsResponse()
        {
            measureUnits = new List<MeasureUnit>();
        }
    }
}