using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverSlice
{
    /// <summary>
    /// Faults occuring in the Receiver may create and throw this class.
    /// </summary>
    public class ReceiverExceptions: Exception
    {
        /// <summary>
        /// Set to true if the fault requires the serial port to be closed.
        /// </summary>
        public Boolean fatal {get; private set;}
        /// <summary>
        /// Reason for failure (human readable)
        /// </summary>
        public string text { get; private set; }
        /// <summary>
        /// The Receiver object associated with the VEMCO hardware.
        /// </summary>
        public Receiver receiver { get; private set; }
        /// <summary>
        /// The Exception precding this exception.
        /// </summary>
        public Exception originatingException { get; private set; }

        /// <summary>
        /// Generated for faults that occur without a prior exception being generated.
        /// </summary>
        /// <param name="fatal">Set to true if the fault requires the serial port to be closed.</param>
        /// <param name="text">Reason for failure (human readable)</param>
        /// <param name="receiver">The Receiver object associated with the VEMCO hardware.</param>
        public ReceiverExceptions(Receiver receiver, string text, Boolean fatal)
        {
            this.fatal = fatal;
            this.text = text;
            this.receiver = receiver;
            this.originatingException = null;
        }

        /// <summary>
        /// Generated for faults that occur with an associating exception.
        /// </summary>
        /// <param name="fatal">Set to true if the fault requires the serial port to be closed.</param>
        /// <param name="text">Reason for failure (human readable)</param>
        /// <param name="receiver">The Receiver object associated with the VEMCO hardware.</param>
        /// <param name="originatingException">The Exception precding this exception.</param>
        public ReceiverExceptions(Receiver receiver, string text, Boolean fatal, Exception originatingException)
        {
            this.fatal = fatal;
            this.text = text;
            this.receiver = receiver;
            this.originatingException = originatingException;
        }

        /// <summary>
        /// stringifys the event
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return "(FATAL? " + fatal + ")" + this.text;
        }
    }

    /// <summary>
    /// Generated when an encoder object enters a fault mode.
    /// </summary>
    public class EncoderExceptions : Exception
    {
        /// <summary>
        /// Reason for the error.
        /// </summary>
        public string text { get; private set; }
        /// <summary>
        /// Exception resulting from fault (may or may not be present)
        /// </summary>
        public Exception e { get; private set; }
        /// <summary>
        /// The serial number of the device generating this exception.
        /// </summary>
        public string deviceSerial { get; private set; }

        /// <summary>
        /// Error without exception.
        /// </summary>
        /// <param name="commandPrefix">The prefix that incudes the serial number of the device.</param>
        /// <param name="text">The reason this event is being generated.</param>
        public EncoderExceptions(string commandPrefix, string text)
        {
            this.deviceSerial = commandPrefix.Substring(1, 8);
            this.text = "Encoder error: " + text;
            this.e = null;
        }

        /// <summary>
        /// error with exception
        /// </summary>
        /// <param name="commandPrefix">The prefix that incudes the serial number of the device.</param>
        /// <param name="text">The reason this event is being generated.</param>
        /// <param name="e">Exception preceding this exception.</param>
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
