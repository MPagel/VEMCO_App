using EventSlice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decoder.RealTimeEvents
{
    public class Decoded: RealTimeEvent
    {
        
        public Decoded(Dictionary<String,String> decoded, RealTimeEvent originatingEvent, String undecoded, String messageType)
            : base("Decoded message type: " + messageType + " from raw message: " + undecoded, originatingEvent)
        {
            this["decodedmessage"] = decoded;
            this["messagetype"] = messageType;
        }
    }
}
