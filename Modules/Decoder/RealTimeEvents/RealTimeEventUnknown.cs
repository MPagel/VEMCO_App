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
        public dynamic config { get; private set; }
        public Exception originatingException { get; private set; }

        public RealTimeEventUnknown(string message, dynamic config)
            : base(message)
        {
            this.config = config;
            unknownMessage = message;
            this.originatingException = null;
        }

        public RealTimeEventUnknown(string message, dynamic config, Exception originatingException)
            : base(message)
        {
            this.config = config;
            unknownMessage = message;
            this.originatingException = originatingException;
        }
    }
}