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
        private String unparsedMessage;

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
            : base("Receiver on " + receiver.portName)
        {
            this.receiver = receiver;
        }

    }
}
