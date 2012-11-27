using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Reflection;


namespace EventSlice
{
    /// <summary>
    /// The dispatcher maintains a thread-safe queue of events.  When a new event arrives, the event is dispatched to
    /// any registered Modules.  Any registered module may also enqueue a RealTimeEvent
    /// for distribution.
    /// </summary>
    public class Dispatcher
    {
        private const String MODULES_PATH = "modules"; 
        private int busyWaitTime = 0;
        private Thread serviceThread = null;
        private ConcurrentQueue<Interfaces.RealTimeEvent> realTimeEventQueue = new ConcurrentQueue<Interfaces.RealTimeEvent>();
        private List<Interfaces.Module> modules = new List<Interfaces.Module>();
        
        /// <summary>
        /// Default constructor examines the MODULES_PATH folder for DLL files.  The files are loaded and the
        /// code attempts to intialize them as a module.  If successful, the module is added to the modules list
        /// and real time events will be automatically dispatched to these modules.
        /// </summary>
        public Dispatcher()
        {
            foreach (string filename in System.IO.Directory.GetFiles(MODULES_PATH))
            {
                if(filename.Contains(".dll") || filename.Contains(".DLL"))
                {
                    Assembly DLL = Assembly.LoadFrom(filename);
                    string className = filename.Substring(filename.IndexOf('\\')+1, filename.IndexOf('.')-filename.IndexOf('\\')-1);
                    Type classType = DLL.GetType(String.Format("{0}.{0}",className));
                    Object[] p = {this};
                    try
                    {
                        modules.Add(((Interfaces.Module)Activator.CreateInstance(classType,p)));
                    }
                    catch(Exception e)
                    {
                        Console.Write(e);
                    }
                }
                
            }
            foreach (Interfaces.Module m in modules)
            {
                m.dispatcher = this;
            }
            
        }

        /// <summary>
        /// Add modules not found in the MODULES_PATH folder.
        /// </summary>
        /// <param name="module">A reference to the module to be added.</param>
        public void addModule(Interfaces.Module module)
        {
            modules.Add(module);
        }

        /// <summary>
        /// Remove a module from the list to which real time events are dispatched.
        /// </summary>
        /// <param name="module"></param>
        public void removeModule(Interfaces.Module module)
        {
            modules.Remove(module);
        }

        /// <summary>
        /// Adds a real time event to the queue to be dispatched in order of entry.
        /// </summary>
        /// <param name="realTimeEvent">The real time event to be dispatched</param>
        /// <returns>The count of the number of events in the queue include the one just added or
        /// -1 if the dispatcher is not accepting events.
        /// </returns>
        public int enqueueEvent(Interfaces.RealTimeEvent realTimeEvent)
        {
            if (busyWaitTime > 0)  // busyWaitTime <= 0 indicates the server has not started, is stopping, or has stopped running.
            {

                realTimeEventQueue.Enqueue(realTimeEvent);
                return realTimeEventQueue.Count;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Service runs inside the service thread and waits for events to show up in the queue.  It dispatches the
        /// event in no particular module order, but does so serially.  The events are removed from the queue in FIFO
        /// order.
        /// </summary>
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

        /// <summary>
        /// Begins the process of dispatching events by creating a service thread that concurrently consumes the
        /// queue.
        /// </summary>
        public void run()
        {
            busyWaitTime = 1000;
            serviceThread = new Thread(new ThreadStart(this.service));
            serviceThread.Start();
            while (!serviceThread.IsAlive);
        }

        /// <summary>
        /// Sends the signal to stop the dispatcher from running.  Allows 2.5 seconds for the dispatcher to finish sending
        /// events before forcing the thread to stop.
        /// </summary>
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
