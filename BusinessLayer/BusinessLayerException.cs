using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBuyList.BusinessLayer
{
    [Serializable]
    public class BusinessLayerException : ApplicationException
    {
        public BusinessLayerException(string msg) : base(msg) { }
        public BusinessLayerException(string msg, params object[] args) : base(string.Format(msg, args)) { }
    }
}
