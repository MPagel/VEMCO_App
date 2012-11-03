using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSlice.Interfaces
{
    public abstract class RealTimeEvent
    {
        private string message;
        private string portName;
        private string serialNumber;
        private string model;

        public RealTimeEvent(string message, string portName, string serialNumber, string model)
        {
            this.message = message;
            this.portName = portName;
            this.serialNumber = serialNumber;
            this.model = model;
        }

        public override string ToString()
        {
            return message;
        }
    }
}
