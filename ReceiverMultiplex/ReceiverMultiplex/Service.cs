using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO.Ports;



namespace ReceiverMultiplex
{
    public partial class Service : ServiceBase
    {
        
        private Dictionary<String, Receiver> receivers = new Dictionary<String, Receiver>();
        private Parser parser;
        private RealTimeEventDispatcher d;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            while (true)
            {
                serialPortsService();
            }
        }

        protected override void OnStop()
        {
        }

        
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
                        receivers.Add(c,r);
                    }
                    catch (HardwareExceptions e)
                    {
                        //!!!TODO
                    }

                }
            }

            //check for COM ports that have disappeared or have TTL = 0
            foreach (String r in receivers.Keys)
            {
                if (Array.IndexOf(SerialPort.GetPortNames(), r) == -1)
                {
                    receivers.Remove(r);
                    Receiver tbr;
                    if (receivers.TryGetValue(r, out tbr))
                    {
                        d.enque((new RealTimeEvents.SerialPortEvent(RealTimeEventType.DEL_RECEIVER, tbr)));
                    }
                }
            }

            //if TTL = 0, it means that this port has been misbehaving consistently
            //removing it now effectively restarts it during the next service loop
            foreach (Receiver r in receivers.Values)
            {
                if (r.TTL <= 0)
                {
                    receivers.Remove(r.portName);
                    d.enque(new RealTimeEvents.SerialPortEvent(RealTimeEventType.DEL_RECEIVER, r));
                }
            }
            
        }
    }
}
