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

 // infinite loop           g.onRealTimeEvent(r);
            g.onRealTimeEvent(s); //ok
 // infinite loop           b.onRealTimeEvent(r);
 // infinite loop           b.onRealTimeEvent(s);
// infinite loop            m.onRealTimeEvent(r);
//infinite loop            m.onRealTimeEvent(s);
//infinite loop            n.onRealTimeEvent(r);
//infinite loop            n.onRealTimeEvent(s);
        }
    }

    public class badImplModule : Module
    {

    }

    public class goodImplModule : Module
    {
        
        public void onRealTimeEvent(RealTimeEventSub rtes)
        {
            Console.Write("OK!");
        }
    }

    public class RealTimeEventSub : RealTimeEvent
    {

    }

    public abstract class Module
    {
        delegate void onRealTimeEvent(RealTimeEvent realTimeEvent);

    }


    public interface RealTimeEvent
    {

    }
}
