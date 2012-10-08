using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverMultiplex
{
    class HardwareExceptions: Exception
    {
        private Boolean fatal {get; private set;}
        private string text { get; private set; }

        public HardwareExceptions(string text, Boolean fatal);
    }
}
