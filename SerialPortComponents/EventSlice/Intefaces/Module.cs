using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSlice.Interfaces
{
    /// <summary>
    /// This class contains the specification for all classes wishing to participate in the event system
    /// must implement.
    /// </summary>
    public abstract class Module : i_Module
    {
        /// <summary>
        /// A reference to the real time event dispatcher where events will be dispatched.
        /// </summary>
        public Dispatcher dispatcher { get; set; }

        

        /// <summary>
        /// The default constructor.  
        /// </summary>
        /// <param name="dispatcher">A reference to the event dispatcher from which real time events will originate.</param>
        public Module(Dispatcher dispatcher)
            { this.dispatcher = dispatcher; }


        /// <summary>
        /// This should return a short, human-readable name for the module.  Note:  Not guaranteed to uniquely identify a
        /// module in a running system.
        /// </summary>
        public abstract string getModuleName();
        /// <summary>
        /// This is the handler each module should implement for events sent from the event dispatcher.
        /// </summary>
        /// <param name="realTimeEvent">The real time event to be handled.</param>
        public virtual void onRealTimeEvent(Interfaces.RealTimeEvent realTimeEvent) { }
    }

    
}
