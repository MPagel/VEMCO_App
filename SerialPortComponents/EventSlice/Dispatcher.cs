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
        
        public Dispatcher()
        {
            foreach (string filename in System.IO.Directory.GetFiles(MODULES_PATH))
            {
                if(filename.Contains(".dll") || filename.Contains(".DLL"))
                {
                    Assembly DLL = Assembly.LoadFrom(filename);
                    string className = filename.Substring(filename.IndexOf('\\')+1, filename.IndexOf('.')-filename.IndexOf('\\')-1);
                    Type classType = DLL.GetType(String.Format("{0}.{0}",className));
                    
                    try
                    {
                        modules.Add(((Interfaces.Module)Activator.CreateInstance(classType)));
                    }
                    catch(Exception e)
                    {
                        
                    }
                }
                
            }
            foreach (Interfaces.Module m in modules)
            {
                m.dispatcher = this;
            }
            //if (File.Exists(MODULES_PATH))
            //{
            //    // Execute the method from the requested .dll using reflection (System.Reflection).
            //    Assembly DLL = Assembly.LoadFrom(MODULES_PATH);
            //    Type classType = DLL.GetType(String.Format("{0}.{1}", strNmSpaceNm, strClassNm));
            //    if (classType != null)
            //    {
            //        // Create class instance.
            //        classInst = Activator.CreateInstance(classType);

            //        // Invoke required method.
            //        MethodInfo methodInfo = classType.GetMethod(strMethodName);
            //        if (methodInfo != null)
            //        {
            //            object result = null;
            //            result = methodInfo.Invoke(classInst, new object[] { dllParams });
            //            return result.ToString();
            //        }
            //    }
            //}
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
            serviceThread = new Thread(new ThreadStart(this.service));
            serviceThread.Start();
            while (!serviceThread.IsAlive);
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
