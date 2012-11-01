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
        public string receivers_id { get; private set; }
        public string detectionCounter { get; private set; }
        public DateTime timestamp { get; private set; }
        public string frequency_codespace { get; private set; }
        public int transmitter_id { get; private set; }
        public double sensor_value { get; private set; } //The default -1 means there was no sensor value reported.

        //receiverSerial + ',' + detectionCounter + ',' + timestamp + ',' + transmitterSerial + ',' + detectionData + hexSum
        public RealTimeEventDetection(string receivers_id, string detectionCounter, DateTime timestamp, string frequency_codespace, int transmitter_id, double sensor_value = -1)
            : base("RealTimeEventDetection receivers_id: " + receivers_id + " detectionCounter: " + detectionCounter
                    + " timestamp: " + timestamp + " transmitte_id: " + transmitter_id + " sensor_value: " + sensor_value)
        {
            this.receivers_id = receivers_id;
            this.detectionCounter = detectionCounter;
            this.timestamp = timestamp;
            this.frequency_codespace = frequency_codespace;
            this.transmitter_id = transmitter_id;
            this.sensor_value = sensor_value;
        }

        public RealTimeEventDetection(string eventText, dynamic config)
            : base("Detection Event: " + eventText)
        {

        }
    }
}