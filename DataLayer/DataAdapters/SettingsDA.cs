using System.Linq;
using System.Data.Linq;

using MyBuyList.Shared.Entities;

namespace MyBuyList.DataLayer.DataAdapters
{
    class SettingsDA : BaseContextDataAdapter<MyBuyListDBContext>
    {
        internal MBLSettingsWrapper GetMBLSettingsWrapper()
        {
            using (DataContext)
            {
                try
                {
                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<MBLSetting>(set => set.SettingKey);

                    DataContext.LoadOptions = dlo;

                    var list = from settingItem in DataContext.MBLSettings
                               select settingItem;
                    MBLSettingsWrapper settingsWrapper = new MBLSettingsWrapper(list.ToArray());

                    return settingsWrapper;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool SaveMBLSettingsRecentItem(string recentItems, string itemType)
        {
            using (DataContext)
            {
                try
                {
                    bool succeeded = false;

                    DataLoadOptions dlo = new DataLoadOptions();
                    dlo.LoadWith<MBLSetting>(set => set.SettingKey);

                    DataContext.LoadOptions = dlo;
                    MBLSetting recentItemsSetting;

                    if (itemType == "recipe")
                    {
                        recentItemsSetting = DataContext.MBLSettings.SingleOrDefault(s => s.SettingKey == "RecentRecipes");
                        recentItemsSetting.SettingValue = recentItems;
                    }
                    else if (itemType == "menu")
                    {
                        recentItemsSetting = DataContext.MBLSettings.SingleOrDefault(s => s.SettingKey == "RecentMenus");
                        recentItemsSetting.SettingValue = recentItems;
                    }

                    DataContext.SubmitChanges();
                    succeeded = true;

                    return succeeded;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
