using EventSlice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decoder.RealTimeEvents
{
    /// <summary>
    /// Contains all necessary information from a message sent from the receiver that has been decoded.
    /// </summary>
    public class Decoded: RealTimeEvent
    {
        /// <summary>
        /// Constructor for this event.
        /// </summary>
        /// <param name="decoded">A map of the parameters of the message to their values. For example, "receiver_id" -> "450028"</param>
        /// <param name="originatingEvent">From whence this event came.</param>
        /// <param name="undecoded">The raw message.</param>
        /// <param name="messageType">The type of message. For example, a detection.</param>
        public Decoded(Dictionary<String,String> decoded, RealTimeEvent originatingEvent, String undecoded, String messageType)
            : base("Decoded message type: " + messageType + " from raw message: " + undecoded, originatingEvent)
        {
            this["decodedmessage"] = decoded;
            this["messagetype"] = messageType;
        }
    }
}
