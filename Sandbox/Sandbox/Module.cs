using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    /// <summary>
    /// This class contains the specification all classes wishing to participate in the event system
    /// must implement.
    /// </summary>
    public abstract class Module
    {

        public Dispatcher dispatcher { get; set; }

        public abstract string getModuleName();
        public virtual void onRealTimeEvent(RealTimeEvent realTimeEvent) { }

        public Module(Dispatcher dispatcher)
            { this.dispatcher = dispatcher; }
            
    }

    
}
