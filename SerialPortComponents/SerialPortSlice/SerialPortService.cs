using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Collections.ObjectModel;

using ReceiverSlice;
using EventSlice;

namespace SerialPortSlice
{

    /// <summary>
    /// This class monitors the system for changes in the serial port enumeration (new or removed serial ports).  
    /// When a new serial port is discovered a Receiver is created.  When the serial
    /// port is removed from the system enumeration or a Receiver has TTL = 0, the device
    /// is removed from the service.
    /// </summary>
    public class SerialPortService
    {
        private static SerialPortService iam = null;

        /// <summary>
        /// A reference to the real time event dispatcher.
        /// </summary>
        public Dispatcher dispatcher { get; private set; }

        //public Dictionary<String, Receiver> receivers { get; private set; }
        public System.Collections.ObjectModel.ObservableCollection<Receiver> receivers { get; private set; }

        private Thread serviceThread = null;

        private int serviceTime = 0;

        private SerialPortService()
        {
            //this.receivers = new Dictionary<String, Receiver>();
            this.receivers = new ObservableCollection<Receiver>();
            if (dispatcher == null)
            {
                dispatcher = new Dispatcher();
            }
        }

        /// <summary>
        /// The Serial Port Service is a singleton.
        /// </summary>
        /// <returns>A static reference to the singleton SerialPortService</returns>
        public static SerialPortService getServicer()
        {
            if (iam == null)
            {
                iam = new SerialPortService();
            }
            System.Diagnostics.Debug.Assert(iam != null);
            return iam;
        }

        /// <summary>
        /// Instructs the service to begin listening for VEMCO receivers attached to serial ports.
        /// </summary>
        public void run()
        {
            if (serviceThread == null)
            {
                serviceThread = new Thread(new ThreadStart(this.serialPortsService));
            }
            serviceThread.Start();
            while (!serviceThread.IsAlive) ;
            dispatcher.run();
            dispatcher.enqueueEvent(new RealTimeEvents.ServerStartUp());
        }

        /// <summary>
        /// Instructs the service to stop listening for receivers and to unmount any receivers running. 
        /// </summary>
        /// <remarks>
        /// This method allows each receiver several seconds (2.5 at the time of writing) to shutdown before
        /// simply being forced off.  In the intervening period, data may be received from the serial port
        /// and events subsequently dispatched.
        /// </remarks>
        public void stop()
        {

            dispatcher.enqueueEvent(new RealTimeEvents.ServerStop());

            foreach (Receiver r in receivers)
            {
                r.shutdown();
            }
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

            dispatcher.enqueueEvent(new RealTimeEvents.ServerStopped());
            dispatcher.stop();
        }


        /// <summary>
        /// This is the primary service loop which executes in its own thread.  It is started by run()
        /// and is destroyed by shutdown().
        /// </summary>
        private void serialPortsService()
        {
            serviceTime = 1000;
            do
            {
                //check for new COM ports... if there's one that we don't have check to see if it is really a VR2C receiver attached or something else
                foreach (string c in System.IO.Ports.SerialPort.GetPortNames())
                {
                    bool r_contains = false;
                    foreach (Receiver x in receivers)
                    {
                        if (x.portName.Equals(c))
                        {
                            r_contains = true;
                        }
                    }
                    if (!r_contains)
                    {
                        //!!! We need the default values for the serial port.
                        SerialPort availableCOMPort = new SerialPort(c, 9600);
                        try
                        {
                            Receiver r = new Receiver(availableCOMPort, c, dispatcher);
                            receivers.Add(r);
                        }

                        catch (Exception e)
                        {
                            dispatcher.enqueueEvent(new RealTimeEvents.ServerException(e, false));
                        }

                    }
                }

                //check for COM ports that have disappeared or have TTL = 0

                foreach (Receiver r in receivers.ToArray<Receiver>())
                {
                    if (Array.IndexOf(SerialPort.GetPortNames(), r.portName) == -1)
                    {

                        dispatcher.enqueueEvent(new ReceiverSlice.RealTimeEvents.DelReceiver(
                            r, r.portName, null, null, null));

                        receivers.Remove(r);
                    }
                }

                //if TTL = 0, it means that this port has been misbehaving consistently
                //removing it now effectively restarts it during the next service loop

                foreach (Receiver r in receivers.ToList<Receiver>())
                {
                    if (r.TTL <= 0)
                    {
                        receivers.Remove(r);
                        dispatcher.enqueueEvent(new ReceiverSlice.RealTimeEvents.DelReceiver(
                                r, r.portName, null, null, null));
                    }
                }
                Thread.Sleep(serviceTime);
            } while (serviceTime > 0);

            foreach (Receiver r in receivers.ToList<Receiver>())
            {
                r.shutdown();
                receivers.Remove(r);
            }
            serviceTime = -1;
        }

            

    }



}
