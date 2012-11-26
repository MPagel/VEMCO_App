using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    /// <summary>
    /// This module sends the text of any real time event to the conole.
    /// </summary>
    public class ConsoleLogger : Module
    {


        public ConsoleLogger(Dispatcher dispatcher)
            : base(dispatcher) { }

        /// <summary>
        /// The event dispatcher's hook.
        /// </summary>
        /// <param name="rte">The real time event to be acted upon by this module.</param>
        public override void onRealTimeEvent(RealTimeEvent rte)
        {
            System.Console.WriteLine(rte);
        }
        
        /// <summary>
        /// For use by UI code to get a display name for this module.
        /// </summary>
        /// <returns>Human-readable name of this module</returns>
        public override string getModuleName()
        {
            return "ConsoleLogger";
        }
    }
}
