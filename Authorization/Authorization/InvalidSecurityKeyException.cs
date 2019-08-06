using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Authorization
{
    public class InvalidSecurityKeyException : Exception
    {
        public InvalidSecurityKeyException(string message) : base(message)
        {
            

        }
    }
}
