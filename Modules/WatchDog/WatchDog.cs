using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EventSlice.Interfaces;
using ReceiverSlice;
namespace WatchDog
{
    public class WatchDog : Module
    {
        private const int DEFAULT_KILL_SECONDS = 600;
        private const int DEFAULT_BREAK_SILENCE_SECONDS = 300;

        private ConcurrentDictionary<ReceiverSlice.Receiver,DateTime> receivers = new ConcurrentDictionary<ReceiverSlice.Receiver,DateTime>();
        private Thread serviceThread = null;

        public int kill_seconds { get; set; }
        public int break_silence_seconds { get; set; }
        

        public WatchDog(EventSlice.Dispatcher dispatcher)
            :base(dispatcher)
        {
            kill_seconds = DEFAULT_KILL_SECONDS;
            break_silence_seconds = DEFAULT_BREAK_SILENCE_SECONDS;
            serviceThread = new Thread(new ThreadStart(this.service));
            serviceThread.Start();
        }

        private void service()
        {
            while (true)
            {
                Thread.Sleep(15000);
                foreach (ReceiverSlice.Receiver r in receivers.Keys.ToList<ReceiverSlice.Receiver>())
                {
                    if (r.runState == RunState.RUN || r.runState == RunState.PAUSE)
                    {
                        TimeSpan ts = DateTime.Now - receivers[r];
                        if (ts.Seconds >= kill_seconds)
                        {
                            r.TTL = 0;
                        }
                        else if (ts.Seconds >= break_silence_seconds)
                        {
                            r.write("INFO");
                        }
                    }
                }
            }
        }

        public override void onRealTimeEvent(RealTimeEvent rte)
        {
            Type eventType = rte.GetType();
            if(eventType == typeof(ReceiverSlice.RealTimeEvents.NewReceiver))
            {
                ReceiverSlice.RealTimeEvents.NewReceiver nr = ((ReceiverSlice.RealTimeEvents.NewReceiver)rte);
                receivers[nr["receiver"]] = DateTime.Now;
            }
            if (eventType == typeof(ReceiverSlice.RealTimeEvents.DelReceiver))
            {
                DateTime xxx = new DateTime();
                ReceiverSlice.RealTimeEvents.DelReceiver dr = ((ReceiverSlice.RealTimeEvents.DelReceiver)rte);
                receivers.TryRemove(dr["receiver"], out xxx);
            }
            
            if (eventType == typeof(ReceiverSlice.RealTimeEvents.ExcepReceiver))
            {
                ReceiverSlice.RealTimeEvents.ExcepReceiver er = ((ReceiverSlice.RealTimeEvents.ExcepReceiver)rte);
                Receiver r = er["receiver"];
                r.TTL -= 1;
            }

            if (eventType == typeof(ReceiverSlice.RealTimeEvents.UnparsedMessage))
            {
                ReceiverSlice.RealTimeEvents.UnparsedMessage um = ((ReceiverSlice.RealTimeEvents.UnparsedMessage)rte);
                receivers[um["receiver"]] = DateTime.Now;
            }
        }

        public override string getModuleName() { return "Receiver WatchDog"; }
    }
}
