using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSlice.Interfaces
{
    public interface Module
    {
        void onRealTimeEvent(Interfaces.RealTimeEvent realTimeEvent);
    }
}
