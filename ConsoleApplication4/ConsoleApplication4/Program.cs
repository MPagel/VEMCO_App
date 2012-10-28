using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            goodImplModule g = new goodImplModule();
            badImplModule b = new badImplModule();
            Module m = new goodImplModule();
            Module n = new badImplModule();
            RealTimeEvent r = new RealTimeEventSub();
            RealTimeEventSub s = new RealTimeEventSub();

g.onRealTimeEvent(r);
            g.onRealTimeEvent(s); //ok
b.onRealTimeEvent(r);
 b.onRealTimeEvent(s);
            m.onRealTimeEvent(r);
          m.onRealTimeEvent(s);
       n.onRealTimeEvent(r);
          n.onRealTimeEvent(s);
          Console.ReadLine();
        }
    }

    public class badImplModule : Module
    {

    }

    public class goodImplModule : Module
    {
        public goodImplModule() {
            
        }
        public void onRealTimeEvent(RealTimeEventSub rtes)
        {
            Console.WriteLine("OK!");
        }
    }

    public class RealTimeEventSub : RealTimeEvent
    {

    }

    public abstract class Module
    {
        public void onRealTimeEvent(RealTimeEvent realTimeEvent)
        {
            Console.WriteLine(realTimeEvent.GetType());
        }

    }


    public interface RealTimeEvent
    {

    }
}
