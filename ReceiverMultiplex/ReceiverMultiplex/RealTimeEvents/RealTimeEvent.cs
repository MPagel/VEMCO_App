using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverMultiplex
{
    enum RealTimeEventType {NEW_RECEIVER, DEL_RECEIVER, TAG_RECEIVER, STATUS_RECEIVER, UNPARSED_RECEIVER, UNPARSED_INTRO, DETECTION_EVENT, GENERIC_EVENT, STATUS_EVENT, RTMINFO_EVENT, INFO_EVENT, UNKNOWN_EVENT };
    class RealTimeEvent
    {
        public RealTimeEventType eventType
        {
            get;
            private set;
        }

        public RealTimeEvent(RealTimeEventType eventType)
        {
            this.eventType = eventType;
        }
    }
}
