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
        public Exception originatingException { get; private set; }

        public ReceiverExceptions(Receiver receiver, string text, Boolean fatal)
        {
            this.fatal = fatal;
            this.text = text;
            this.receiver = receiver;
            this.originatingException = null;
        }

        public ReceiverExceptions(Receiver receiver, string text, Boolean fatal, Exception originatingException)
        {
            this.fatal = fatal;
            this.text = text;
            this.receiver = receiver;
            this.originatingException = originatingException;
        }

        
    }

    public class EncoderExceptions : Exception
    {
        public string text { get; private set; }
        public Exception e { get; private set; }
        public string deviceSerial { get; private set; }

        public EncoderExceptions(string commandPrefix, string text)
        {
            this.deviceSerial = commandPrefix.Substring(1, 8);
            this.text = text;
            this.e = null;
        }

        public EncoderExceptions(string commandPrefix, string text, Exception e)
        {
            this.deviceSerial = commandPrefix.Substring(1, 8);
            this.text = text;
            this.e = e;
        }
    }
    class InvalidCommandException : Exception
    {
        public InvalidCommandException(String message)
            : base(message) { }
    }
}
