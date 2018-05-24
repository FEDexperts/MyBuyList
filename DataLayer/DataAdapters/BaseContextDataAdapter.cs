using System;
using System.Collections.Generic;

namespace MyBuyList.DataLayer.DataAdapters
{
    internal class BaseContextDataAdapter<T>
        where T: System.Data.Linq.DataContext
    {
        private T _dataContext;
        protected T DataContext
        {
            get { return _dataContext; }
        }

        public BaseContextDataAdapter()
        {
            _dataContext = (T)Activator.CreateInstance(typeof(T), DBUtils.GetConnectionString());
        }
    }
}
