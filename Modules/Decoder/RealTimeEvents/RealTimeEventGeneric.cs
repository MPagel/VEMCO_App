using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice.Interfaces;

namespace Decoder.RealTimeEvents
{
    class RealTimeEventGeneric:RealTimeEvent
    {
        public string returnStatus { get; private set; } //"OK", "FAILURE", "INVALID"

        public RealTimeEventGeneric(string returnStatus)
            : base("Return Status: " + returnStatus)
        {
            this.returnStatus = returnStatus;
        }
    }
}