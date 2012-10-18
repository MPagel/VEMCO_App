using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice.Interfaces;

namespace ConsoleLogger
{
    public class ConsoleLogger : Module
    {
        public void onRealTimeEvent(RealTimeEvent rte)
        {
            System.Console.Write(rte);
        }
        
    }
}
