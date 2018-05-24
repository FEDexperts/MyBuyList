using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBuyList.DataLayer
{
    [Serializable]
    public class DataLayerException : ApplicationException
    {
        public DataLayerException(string msg) : base(msg) { }
        public DataLayerException(string msg, params object[] args) : base(string.Format(msg, args)) { }
    }
}
