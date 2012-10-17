using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverMultiplex.RealTimeEvents
{
    class RealTimeEventGeneric:RealTimeEvent
    {
        public int returnStatus { get; private set; } //0 = "OK" 1 = "FAILURE" 2 = "INVALID"

        public RealTimeEventGeneric(RealTimeEventType type, int returnStatus)
            : base(type)
        {
            this.returnStatus = returnStatus;
        }
    }
}