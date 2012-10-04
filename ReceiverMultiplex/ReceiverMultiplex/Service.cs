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
        private const int DEFAULT_TTL = 10;

        private Dictionary<String, Receivers> receivers = new Dictionary<String, Receivers>();
        private Parser parser;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            while (true)
            {
                serialPortService();

            }
        }

        protected override void OnStop()
        {
        }

        protected ArrayList serialPortService()
        {
            ArrayList returnList = new ArrayList();
            //check for new COM ports... if there's one that we don't have check to see if it is really a VR2C receiver attached or something else
            foreach (string c in System.IO.Ports.SerialPort.GetPortNames())
            {
                if (!receivers.ContainsKey(c))
                {
                    //!!! We need the default values for the serial port.
                    SerialPort _potentialReceiver = new SerialPort(c, 9600);
                    _potentialReceiver.Write(parser.areYouThere());
                    if (parser.validAreYouThere(_potentialReceiver.ReadLine()))
                    {
                        Receivers r = new Receivers(DEFAULT_TTL, _potentialReceiver, c);
                        returnList.Add(new RealTimeEvents.SerialPortEvent(RealTimeEventType.NEW_RECEIVER, r));
                    }
                }
            }

            //check for COM ports that have disappeared or have TTL = 0
            foreach (String r in receivers.Keys)
            {
                if (Array.IndexOf(SerialPort.GetPortNames(), r) == -1)
                {
                    receivers.Remove(r);
                    Receivers tbr;
                    if (receivers.TryGetValue(r, out tbr))
                    {
                        returnList.Add(new RealTimeEvents.SerialPortEvent(RealTimeEventType.DEL_RECEIVER, tbr));
                    }
                }
            }

            //if TTL = 0, it means that this port has been misbehaving consistently
            //removing it now effectively restarts it during the next service loop
            foreach (Receivers r in receivers.Values)
            {
                if (r.TTL <= 0)
                {
                    receivers.Remove(r.portName);
                    returnList.Add(new RealTimeEvents.SerialPortEvent(RealTimeEventType.DEL_RECEIVER, r));
                }
            }
            
            return returnList;
        }
    }
}
