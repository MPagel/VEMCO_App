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
        public RealTimeEventDetection(string receiverSerial, string detectionCounter, DateTime timestamp, string transmitterSerial, string data)
            : base("RealTimeEventDetection receiverSerial: " + receiverSerial + " detectionCounter: " + detectionCounter
                    + " time stamp: " + timestamp + " transmitterSerial: " + transmitterSerial + " data: " + data)
        {
            this.receiverSerial = receiverSerial;
            this.detectionCounter = detectionCounter;
            this.timestamp = timestamp;
            this.transmitterSerial = transmitterSerial;
            this.data = data;
        }

        public RealTimeEventDetection(string eventText, dynamic config)
            : base("Detection Event: " + eventText)
        {

        }
    }
}