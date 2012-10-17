using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSlice.Interfaces
{
    interface Module
    {
        public void onRealTimeEvent(Interfaces.RealTimeEvent realTimeEvent);
    }
}
