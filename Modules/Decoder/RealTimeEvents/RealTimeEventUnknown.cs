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

        public RealTimeEventUnknown(string message)
            : base(message)
        {
            unknownMessage = message;
        }
    }
}