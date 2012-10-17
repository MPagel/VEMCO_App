using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice.Interfaces;

namespace Decoder.RealTimeEvents
{
    class RealTimeEventInfo:RealTimeEvent
    {
        public string serial { get; private set; }
        public string studyName { get; private set; }
        public string map { get; private set; }
        public string codespace { get; private set; }
        public string firmwareVersion { get; private set; }
        public string hardwareVersion { get; private set; }

        public RealTimeEventInfo(RealTimeEventType type, string serial, string studyName, string map, string codespace, string firmwareVersion, string hardwareVersion)
            : base(type)
        {
            this.serial = serial;
            this.studyName = studyName;
            this.map = map;
            this.codespace = codespace;
            this.firmwareVersion = firmwareVersion;
            this.hardwareVersion = hardwareVersion;
        }
    }
}
