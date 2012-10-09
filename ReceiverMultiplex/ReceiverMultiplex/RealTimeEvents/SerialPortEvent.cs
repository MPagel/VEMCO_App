using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverMultiplex.RealTimeEvents
{
    class SerialPortEvent : RealTimeEvent
    {
        public Receiver r { get; private set; }

        public SerialPortEvent(RealTimeEventType eventType, Receiver r) : base (eventType) 
        {
        }
    }
}
