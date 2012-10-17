using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice.Interfaces;

namespace Decoder.RealTimeEvents
{
    class RealTimeEventUnknown:RealTimeEvent
    {
        public string unknownMessage { get; private set; }

        public RealTimeEventUnknown(RealTimeEventType type, string message)
            : base(type)
        {
            unknownMessage = message;
        }
    }
}