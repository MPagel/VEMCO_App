using EventSlice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decoder.RealTimeEvents
{
    public class RealTimeEventDecoded: RealTimeEvent
    {
        public String undecoded {get; private set;}
        public dynamic config  {get; private set;}
        public String messageType  {get; private set;}
        public Dictionary<String,String> payload  {get; private set;}

        public RealTimeEventDecoded(String undecoded, dynamic config, String messageType, Dictionary<String, String> payload)
            : base("Decoded message type: " + messageType + " from raw message: " + undecoded)
        {
            this.undecoded = undecoded;
            this.config = config;
            this.messageType = messageType;
            this.payload = payload;
        }
    }
}
