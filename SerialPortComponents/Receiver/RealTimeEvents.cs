using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice.Interfaces;

namespace ReceiverSlice.RealTimeEvents
{
    public class ReceiverEvent
    /// <summary>
    /// This event is generated when a VEMCO receiver is configured on a serial port.
    /// As with all RealTime Events, the data is accessible dynamically.
    /// </summary>
    public class NewReceiver : RealTimeEvent
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="receiver">The Receiver object associated with the VEMCO hardware.</param>
        /// <param name="portName">The name of the serial port it is connected to (i.e. COM1)</param>
        /// <param name="serialNumber">The serial number of the receiver.</param>
        /// <param name="model">The model of the receiver.</param>
        public NewReceiver(Receiver receiver, string portName, string serialNumber, string model)
            : base("Receiver on " + receiver.portName, null)
        {
            this["receiver"] = receiver;
            this["portname"] = portName;
            this["serialnumber"] = serialNumber;
            this["model"] = model;
        }


    }

    /// <summary>
    /// This event is generated when a message is received from the VEMCO hardware.
    /// </summary>
    public class UnparsedMessage : RealTimeEvent
    {
        private Receiver receiver;
        public String unparsedMessage { get; private set; }
        public dynamic config { get; private set; }

        public UnparsedMessage(Receiver receiver, String unparsedMessage, dynamic config)
            : base(unparsedMessage + " from receiver on " + receiver.portName)
        {
            this.receiver = receiver;
            this.unparsedMessage = unparsedMessage;
            this.config = config;
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
        String text = "";
        public ExcepReceiver(Receiver receiver, Boolean fatal)
            : base("Receiver o " + receiver.portName + " entered exceptional condition. Fatal: " + fatal)
        {
            this.receiver = receiver;
            this.fatal = fatal;
        }

        public ExcepReceiver(ReceiverExceptions re)
            : base("Receiver o " + re.receiver.portName + "entered exception condition. Fatal? " + re.fatal + 
             " Exception text: " + re.text)
        {
            this.receiver = re.receiver;
            this.fatal = re.fatal;
            this.text = re.text;
        }
    }

    public class NoteReceiver : RealTimeEvent
    {
        Receiver receiver;

        public NoteReceiver(Receiver receiver, String text)
            : base("(" + receiver.portName + ")" + text)
        {
            this.receiver = receiver;
        }
    }
}
