using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverSlice
{
    public class ReceiverExceptions: Exception
    {
        public Boolean fatal {get; private set;}
        public string text { get; private set; }

        public ReceiverExceptions(string text, Boolean fatal)
        {
            this.fatal = fatal;
            this.text = text;
        }

        
    }

    class InvalidCommandException : Exception
    {
        public InvalidCommandException(String message)
            : base(message) { }
    }
}
