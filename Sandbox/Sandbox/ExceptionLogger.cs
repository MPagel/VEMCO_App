using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    class ExceptionLogger : Module
    {
        private System.IO.StreamWriter logWriter { get; set; }

        public ExceptionLogger(Dispatcher dispatcher)
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
