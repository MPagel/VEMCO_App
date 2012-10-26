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
        public Receiver receiver { get; private set; }

        public ReceiverExceptions(Receiver receiver, string text, Boolean fatal)
        {
            this.fatal = fatal;
            this.text = text;
            this.receiver = receiver;
        }

        
    }

    class InvalidCommandException : Exception
    {
        public InvalidCommandException(String message)
            : base(message) { }
    }
}
