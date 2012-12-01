using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice.Interfaces;
using EventSlice;

namespace ConsoleLogger
{
    /// <summary>
    /// Reports real time events to the console.
    /// </summary>
    public class ConsoleLogger : Module
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dispatcher">A reference to the running system real time event dispatcher.</param>
        public ConsoleLogger(Dispatcher dispatcher)
            : base(dispatcher) { }

        /// <summary>
        /// Human-readable text of any real time event dispatched is printed to the console.
        /// </summary>
        /// <param name="rte">Real time event dispatched</param>
        public override void onRealTimeEvent(RealTimeEvent rte)
        {
            System.Console.WriteLine(rte);
        }
        
        /// <summary>
        /// For use by UI components to provide a human readable name for this module.
        /// </summary>
        /// <returns>Name of this module.</returns>
        public override string getModuleName()
        {
            return "Console Logger";
        }
    }
}
