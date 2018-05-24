using System;
using System.Collections.Generic;
using System.Text;

namespace ProperControls
{
    [Serializable]
    public class UIException : ApplicationException
    {
        public UIException(string msg) : base(msg) { }
        public UIException(string msg, params object[] args) : base(string.Format(msg, args)) { }
    }
}
