﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Threading;

using ReceiverSlice;
using EventSlice;

namespace SerialPortSlice
{
    
    public class SerialPortService
    {
        private static SerialPortService iam = null;
        private Thread serviceThread = null;
        private Dictionary<String, Receiver> receivers = new Dictionary<String, Receiver>();
        private Dispatcher d = new Dispatcher();

        private int serviceTime = 0;

        private SerialPortService()
        {
        }
    
        public static SerialPortService getServicer() 
        {
            if (iam == null)
            {
                try
                {
                    iam = new SerialPortService();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            System.Diagnostics.Debug.Assert(iam != null);
            return iam;
        }

        public void run()
        {
            if (serviceThread != null)
            {
                serviceThread = new Thread(new ThreadStart(this.serialPortsService));
            }
        }

        public void stop()
        {
            if (serviceThread != null && serviceThread.IsAlive)
            {
                int waitTimeForShutdown = receivers.Count;
                serviceTime = 0;

                for (int i = 0; i <= waitTimeForShutdown; i++)
                {
                    if (serviceTime == -1)
                    {
                        serviceThread.Abort();
                        serviceThread = null;
                        return;
                    }
                }
                serviceThread.Abort();
                serviceThread = null;
            }
        }

        private void serialPortsService()
        {
            serviceTime = 1000;
            do {
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
                        catch (ReceiverExceptions e)
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
    //                        d.enque((new RealTimeEvents.SerialPortEvent(RealTimeEventType.DEL_RECEIVER, tbr)));
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
     //                   d.enque(new RealTimeEvents.SerialPortEvent(RealTimeEventType.DEL_RECEIVER, r));
                    }
                }
                Thread.Sleep(serviceTime);
            } while (serviceTime > 0);

            foreach (Receiver r in receivers.Values)
            {
                r.shutdown();
                receivers.Remove(r.portName);
            }
            serviceTime = -1;
        }
    }

    
}