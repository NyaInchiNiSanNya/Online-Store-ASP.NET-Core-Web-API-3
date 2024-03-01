using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Excpetions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string message)
            : base(message) { }
    }
}
