using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice.Interfaces;
using EventSlice;

namespace ConsoleLogger
{
    public class ConsoleLogger : Module
    {
        public ConsoleLogger(Dispatcher dispatcher)
            : base(dispatcher) { }

        public override void onRealTimeEvent(RealTimeEvent rte)
        {
            System.Console.WriteLine(rte);
        }
        
        public override string getModuleName()
        {
            return "ConsoleLogger";
        }
    }
}
