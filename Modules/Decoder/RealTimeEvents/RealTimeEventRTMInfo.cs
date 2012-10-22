using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice;
using EventSlice.Interfaces;

namespace Decoder.RealTimeEvents
{
    class RealTimeEventRTMInfo:RealTimeEvent
    {
        public string RTMMode { get; private set; } //232, 485, or OFF
        public string SI { get; private set; } //status internal, POLL for poll mode
        public string BL { get; private set; } //block length in lines, U for unlimited
        public string BI { get; private set; } //block internval in seconds between blocks, WFS for wait for status
        public string MA { get; private set; } //max age filter in seconds, U for unfiltered
        public string FMT { get; private set; } //format options: SER = serial number, SEQ = sequence number, UTC = ASCII universal time, LCL = ASCII local time, DEC_UTC = decimal universal time, DEC_LCL = decimal local time, CS = checksum

        public RealTimeEventRTMInfo(string RTMMode, string SI, string BL, string BI, string MA, string FMT)
            : base("RTM INFO Mode" + RTMMode + " SI: " + SI + " BL: " + BL + " BI: " + BI + " MA: " + MA + " FMT: " + FMT)
        {
            this.RTMMode = RTMMode;
            this.SI = SI;
            this.BL = BL;
            this.BI = BI;
            this.MA = MA;
            this.FMT = FMT;
        }
    }
}
