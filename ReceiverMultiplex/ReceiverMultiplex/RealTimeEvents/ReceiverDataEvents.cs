﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverMultiplex.RealTimeEvents
{

    class UnparsedDataEvent : RealTimeEvent
    {
        public string payload { get; private set; }
        Receiver r;
        public UnparsedDataEvent(string payload, Receiver r) : base (RealTimeEventType.UNPARSED_RECEIVER)
          
        {
            this.payload = payload;
            this.r = r;
            
        }

    }
    class UnparsedIntroEvent : RealTimeEvent
    {
        public string payload { get; private set; }
        public UnparsedIntroEvent(string payload, string comPort)
            : base(RealTimeEventType.UNPARSED_INTRO)
        {
            this.payload = payload;
        }
    }
    //class TagEvent : RealTimeEvent
    //{
    //}

    //class ReceiverStatusEvent : RealTimeEvent
    //{
    //}

    class MalformedDataException : System.ApplicationException
    {
        public UnparsedDataEvent u { get; private set; }
        public MalformedDataException(UnparsedDataEvent u)
        {
            this.u = u;
        }
    }
}
