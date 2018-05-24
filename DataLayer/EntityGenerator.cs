using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MyBuyList.DataLayer
{
    /// <summary>
    /// Allows creation and loading of data items.
    /// </summary>
    internal static class EntityGenerator
    {
        /// <summary>
        /// Creates a single item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T CreateItem<T>(DataRow row) where T : /*StorageItemBase,*/ new()
        {
            T t = new T();
            //t.Read(row);
            
            return t;
        }

        /// <summary>
        /// Create items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static T[] CreateItems<T>(DataRow[] rows) where T : /*StorageItemBase,*/ new()
        {
            List<T> items = new List<T>();
            foreach (DataRow row in rows)
            {
                items.Add(CreateItem<T>(row));
            }

            return items.ToArray();
        }

        /// <summary>
        /// Create items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T[] CreateItems<T>(DataTable table) where T : /*StorageItemBase,*/ new()
        {
            return CreateItems<T>(table.Select());
        }
    }
}
