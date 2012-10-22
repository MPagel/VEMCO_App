using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice.Interfaces;

namespace ReceiverSlice.RealTimeEvents
{
    public class NewReceiver : RealTimeEvent
    {
        Receiver receiver;
        public NewReceiver(Receiver receiver)
            : base("Receiver on " + receiver.portName)
        {
            this.receiver = receiver;
        }


    }

    public class UnparsedMessage : RealTimeEvent
    {
        private Receiver receiver;
        public String unparsedMessage { get; private set; }

        public UnparsedMessage(Receiver receiver, String unparsedMessage)
            : base(unparsedMessage + " from receiver on " + receiver.portName)
        {
            this.receiver = receiver;
            this.unparsedMessage = unparsedMessage;
        }

    }

    public class DelReceiver : RealTimeEvent
    {
        Receiver receiver;
        public DelReceiver(Receiver receiver)
            : base("Receiver on " + receiver.portName + " removed.")
        {
            this.receiver = receiver;
        }

    }

    public class ExcepReceiver : RealTimeEvent
    {
        Receiver receiver;
        Boolean fatal;

        public ExcepReceiver(Receiver receiver, Boolean fatal)
            : base("Receiver o " + receiver.portName + " entered exceptional condition. Fatal: " + fatal)
        {
            this.receiver = receiver;
            this.fatal = fatal;
        }
    }
}
