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
        private const int COM_READ_TIMEOUT_DEFAULT = 500; //milliseconds
        private const int COM_READ_TIMEOUT_SPRIAL = 100; //additional ms to allow for response on next go-'round
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
                pollReceivers();

            }
        }

        protected override void OnStop()
        {
        }

        protected void pollReceivers()
        {
            foreach (Receiver r in receivers.Values)
            {
                r.serialPort.Write(parser.pollReceiver());
                try
                {
                    d.dispatch(new RealTimeEvents.UnparsedDataEvent(RealTimeEventType.UNPARSED_RECEIVER,
                        r.serialPort.ReadTo(">"),r));
                }
                catch (System.TimeoutException e)
                {
                    r.TTL--;
                    r.serialPort.WriteTimeout += COM_READ_TIMEOUT_SPRIAL;
                }
                catch (RealTimeEvents.MalformedDataException e) 
                {
                    r.TTL--;
                }
            }
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
                    availableCOMPort.Write(parser.areYouThere());
                    try
                    {
                        d.dispatch(new RealTimeEvents.UnparsedIntroEvent(
                        availableCOMPort.ReadTo("#"), c));
                        Receiver r = new Receiver(DEFAULT_TTL, availableCOMPort, c);
                        new RealTimeEvents.SerialPortEvent(RealTimeEventType.NEW_RECEIVER, r);
                    }
                    catch (RealTimeEvents.MalformedDataException e)
                    {
                        //Should be logged?
                    }
                    /*
                    if (parser.validAreYouThere(availableCOMPort.ReadLine()))
                    {
                        Receiver r = new Receiver(DEFAULT_TTL, availableCOMPort, c);
                        returnList.Add(new RealTimeEvents.SerialPortEvent(RealTimeEventType.NEW_RECEIVER, r));
                    }
                    */
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
                        returnList.Add(new RealTimeEvents.SerialPortEvent(RealTimeEventType.DEL_RECEIVER, tbr));
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
                    returnList.Add(new RealTimeEvents.SerialPortEvent(RealTimeEventType.DEL_RECEIVER, r));
                }
            }
            
        }
    }
}
