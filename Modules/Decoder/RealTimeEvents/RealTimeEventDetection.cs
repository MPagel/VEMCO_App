using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice;
using EventSlice.Interfaces;


namespace Decoder.RealTimeEvents
{
    class RealTimeEventDetection:RealTimeEvent
    {
        public string receiverSerial { get; private set; }
        public string detectionCounter { get; private set; }
        public DateTime timestamp { get; private set; }
        public string transmitterSerial { get; private set; }
        public string data { get; private set; }

        //receiverSerial + ',' + detectionCounter + ',' + timestamp + ',' + transmitterSerial + ',' + detectionData + hexSum
        public RealTimeEventDetection(RealTimeEventType type, string receiverSerial, string detectionCounter, DateTime timestamp, string transmitterSerial, string data)
            : base(type)
        {
            this.receiverSerial = receiverSerial;
            this.detectionCounter = detectionCounter;
            this.timestamp = timestamp;
            this.transmitterSerial = transmitterSerial;
            this.data = data;
        }
    }
}