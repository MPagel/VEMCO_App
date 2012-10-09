using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverMultiplex
{
    public class HardwareExceptions: Exception
    {
        public Boolean fatal {get; private set;}
        public string text { get; private set; }

        public HardwareExceptions(string text, Boolean fatal)
        {
            this.fatal = fatal;
            this.text = text;
        }

       
    }

    public class MalformedData : Exception
    {
        string raw { get; private set; }
        string error { get; private set; }
        
        public MalformedData(string raw, string error)
        {
            this.raw = raw;
            this.error = error;
        }
    }
}
