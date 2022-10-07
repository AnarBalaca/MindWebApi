using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.EntityExceptions
{
    public class EntityCouldNotFoundException : Exception
    {
        private const string message = "Sorry Entity Couldnt Found";
        public EntityCouldNotFoundException():base(message)
        {

        }
    }
}
