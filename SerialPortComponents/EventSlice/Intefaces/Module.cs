using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSlice.Interfaces
{
    public abstract class Module
    {

        public Dispatcher dispatcher { get; set; }

        public abstract string getModuleName();
        public virtual void onRealTimeEvent(Interfaces.RealTimeEvent realTimeEvent) { }
    }

    
}
