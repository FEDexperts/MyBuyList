using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyBuyList.DataLayer;
using MyBuyList.Shared.Entities;

namespace MyBuyList.BusinessLayer.Managers
{
    class SettingsManager
    {
        internal MBLSettingsWrapper GetMBLSettingsWrapper()
        {
            return DataFacade.Instance.GetMBLSettingsWrapper();
        }

        internal bool SaveMBLSettingsRecentItem(string recentItems, string itemType)
        {
            return DataFacade.Instance.SaveMBLSettingsRecentItem(recentItems, itemType);
        }
    }
}
