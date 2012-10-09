using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverMultiplex
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private Dictionary<String, Receiver> receivers = new Dictionary<String, Receiver>();
        private RealTimeEventDispatcher d = new RealTimeEventDispatcher();

        protected void serialPortsService()
        {
            //check for new COM ports... if there's one that we don't have check to see if it is really a VR2C receiver attached or something else
            foreach (string c in System.IO.Ports.SerialPort.GetPortNames())
            {
                if (!receivers.ContainsKey(c))
                {
                    //!!! We need the default values for the serial port.
                    SerialPort availableCOMPort = new SerialPort(c, 9600);
                    try
                    {
                        Receiver r = new Receiver(availableCOMPort, c, d);
                        receivers.Add(c, r);
                    }
                    catch (HardwareExceptions e)
                    {
                        //!!!TODO
                    }

                }
            }
        }
    }
}
