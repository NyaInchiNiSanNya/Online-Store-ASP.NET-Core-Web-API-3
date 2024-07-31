using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Excpetions
{
    public class BadRegistrationException : Exception
    {
        public BadRegistrationException(string message)
            : base(message) { }
    }
}
