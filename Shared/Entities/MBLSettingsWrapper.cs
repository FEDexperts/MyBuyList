using System;
using System.Collections.Generic;

namespace MyBuyList.Shared.Entities
{
    public class MBLSettingsWrapper
    {     
        public MBLSettingsWrapper(MBLSetting[] settings)
        {
            foreach (MBLSetting set in settings)
            {
                if (set.SettingKey == "RecentMenus")
                {
                    this.recentMenus = set.SettingValue;
                }
                if (set.SettingKey == "RecentRecipes")
                {
                    this.recentRecipes = set.SettingValue;
                }
            }            
        }

        public int[] RecentRecipes
        {
            get { return this.GetArray(this.recentRecipes); }
            set { }
        }

        public int[] RecentMenus
        {
            get { return this.GetArray(this.recentMenus); }
            set { }
        }

        private string recentRecipes;
        private string recentMenus;

        private int[] GetArray(string str)
        {
            List<int> list = new List<int>();
            
            if (!string.IsNullOrEmpty(str))
            {
                string[] tempArray = str.Split(",".ToCharArray(), StringSplitOptions.None);
                int i = 0;
                foreach (string numStr in tempArray)
                {
                    int x = 0;
                    int.TryParse(numStr, out x);
                    list.Add(x);
                    i++;
                }
            }

            int[] arr = new int[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                arr[i] = list[i];
            }

            return arr; 
        }        
    }
}
