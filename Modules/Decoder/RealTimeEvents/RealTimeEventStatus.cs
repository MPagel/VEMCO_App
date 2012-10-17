using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice.Interfaces;

namespace Decoder.RealTimeEvents
{
    class RealTimeEventStatus:RealTimeEvent
    {
        public int DC { get; private set; } //Detection Count
        public int PC { get; private set; } //Ping count
        public double LV { get; private set; } //Line Voltage in Volts
        public double BV { get; private set; } //Battery Voltage in Volts
        public double BU { get; private set; } //Battery used in percent
        public double I { get; private set; } //Current consumption in milliamps
        public double T { get; private set; } //Internal receiver temperature in Celcius
        public double DU { get; private set; } //Detection memory used in percent
        public double RU { get; private set; } //Raw memory used in percent
        public double X { get; private set; } //X-tilt
        public double Y { get; private set; } //Y-tilt
        public double Z { get; private set; } //Z-tilt

        public RealTimeEventStatus(RealTimeEventType type, int DC, int PC, double LV, double BV, double BU, double I, double T, double DU, double RU, double X, double Y, double Z)
            : base(type)
        {
            this.DC = DC;
            this.PC = PC;
            this.LV = LV;
            this.BV = BV;
            this.BU = BU;
            this.I = I;
            this.T = T;
            this.DU = DU;
            this.RU = RU;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
    }
}
