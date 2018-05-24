using System;
using System.Collections.Generic;
using System.Text;

namespace ProperServices.Common.Mail
{
    class CommonServicesException : ApplicationException
    {
        public CommonServicesException() : base() { }
        public CommonServicesException(string message) : base(message) { }
        public CommonServicesException(string message, Exception innerException) : base(message, innerException) { }
    }
}
