using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ReceiverMultiplex
{
    class Receivers
    {
        public int TTL;
        public SerialPort serialPort;
        public string portName;
        public Receivers(int TTL, SerialPort serialPort, String portName)
        {
            this.TTL = TTL;
            this.serialPort = serialPort;
            this.portName = portName;
        }
    }
}
