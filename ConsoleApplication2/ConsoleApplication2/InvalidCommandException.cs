using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverMultiplex
{
    class InvalidCommandException:Exception
    {
        public InvalidCommandException(String message) : base(message) { }
    }
}
