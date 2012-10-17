using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace EventSlice
{
    public class Dispatcher
    {
        private int busyWaitTime = 0;
        private Thread serviceThread = null;
        private ConcurrentQueue<Interfaces.RealTimeEvent> realTimeEventQueue = new ConcurrentQueue<Interfaces.RealTimeEvent>();
        private List<Interfaces.Module> modules = new List<Interfaces.Module>();

        public Dispatcher()
        {
        }

        public void addModule(Interfaces.Module module)
        {
            modules.Add(module);
        }

        public void remoteModule(Interfaces.Module module)
        {
            modules.Remove(module);
        }

        public int enqueueEvent(Interfaces.RealTimeEvent realTimeEvent)
        {
            realTimeEventQueue.Enqueue(realTimeEvent);
            return realTimeEventQueue.Count;
        }

        private void service()
        {
            Interfaces.RealTimeEvent rte;
            do
            {
                while (realTimeEventQueue.Count > 0)
                {
                    if (realTimeEventQueue.TryDequeue(out rte))
                    {
                        foreach (Interfaces.Module m in modules)
                        {
                            m.onRealTimeEvent(rte);
                        }
                    }
                }
            } while (busyWaitTime > 0);
            busyWaitTime = -1;

        }

        public void run()
        {
            busyWaitTime = 1000;
            serviceThread = new Thread(new ThreadStart(service));
        }
        public void stop()
        {
            busyWaitTime = 0;
            for (int i = 0; i <= 5; i++)
            {
                if (busyWaitTime == -1)
                {
                    serviceThread.Abort();
                    return;
                }
                Thread.Sleep(500);
            }
            serviceThread.Abort();

        }
    }
}
