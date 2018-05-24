
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Script.Services;

using MyBuyList.BusinessLayer;
using MyBuyList.Shared.Entities;
using System.Web.UI;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.None)]
[ScriptService]
public class AutoComplete : WebService
{
    public AutoComplete()
    {
    }

    [WebMethod]
    public string[] GetCompletionGeneralItemsList(string prefixText)
    {
        return BusinessFacade.Instance.GetGeneralItemsList(prefixText);
    }

    //[WebMethod]
    //public object[] GetCompletionFoodList(string prefixText)
    //{
    //    string[] list = BusinessFacade.Instance.GetFoodList(prefixText);
    //    List<object> items = new List<object>();

    //    foreach(string str in list)
    //    {
    //        string id = str.Split(new []{"|"}, StringSplitOptions.RemoveEmptyEntries)[0];
    //        string value = str.Split(new []{"|"}, StringSplitOptions.RemoveEmptyEntries)[1];
    //        var item = new Pair(id, value);
    //        items.Add(item);
    //    }
    //    return items.ToArray();
    //}
}

