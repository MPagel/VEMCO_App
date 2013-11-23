using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VR2CSystemService
{
    public partial class Service1 : ServiceBase, EventSlice.Interfaces.i_Module
    {
        private static SerialPortSlice.SerialPortService s;
        private Thread serviceThread = null;
        private ConcurrentDictionary<SerialPortSlice.RealTimeEvents.ServerException, DateTime> serialPortServiceErrorLog = new ConcurrentDictionary<SerialPortSlice.RealTimeEvents.ServerException, DateTime>();
        private const int ERROR_TOLERANCE_PER_PERIOD_DEFAULT = 10;
        private const int ERROR_TOLERANCE_PERIOD_SECONDS_DEFAULT = 600;
        private int error_tolerance_per_period = ERROR_TOLERANCE_PER_PERIOD_DEFAULT;
        private int error_tolerance_period_seconds = ERROR_TOLERANCE_PERIOD_SECONDS_DEFAULT;
        public Service1()
        {
            serviceThread = new Thread(new ThreadStart(this.service));
            

            InitializeComponent();

        }

        public void onRealTimeEvent(EventSlice.Interfaces.RealTimeEvent rte)
        {
            if (rte.GetType() == typeof(ReceiverSlice.RealTimeEvents.NewReceiver))
            {
               
            }


            if (rte.GetType() == typeof(SerialPortSlice.RealTimeEvents.ServerException))
            {
                SerialPortSlice.RealTimeEvents.ServerException se = ((SerialPortSlice.RealTimeEvents.ServerException)rte);
                serialPortServiceErrorLog[se] = DateTime.Now;
            }


        }
        protected override void OnStart(string[] args)
        {
            serviceThread.Start();
        }

        protected override void OnStop()
        {
            serviceThread.Abort();
        }

        private void service()
        {
            while (true)
            {
                s = SerialPortSlice.SerialPortService.getServicer();
                s.dispatcher.addModule(this);
                s.run();

                serialPortServiceErrorLog.Clear();
                do
                {
                    Thread.Sleep(3000);
                    foreach (SerialPortSlice.RealTimeEvents.ServerException se in serialPortServiceErrorLog.Keys.ToList<SerialPortSlice.RealTimeEvents.ServerException>())
                    {
                        TimeSpan ts = DateTime.Now - serialPortServiceErrorLog[se];
                        if (ts.Seconds >= error_tolerance_period_seconds)
                        {
                            DateTime xxx;
                            serialPortServiceErrorLog.TryRemove(se, out xxx);
                        }
                    }
                } while (serialPortServiceErrorLog.Count() <= error_tolerance_per_period);

                s.stop();
                Thread.Sleep(15000);
            }
        }
    }
}
