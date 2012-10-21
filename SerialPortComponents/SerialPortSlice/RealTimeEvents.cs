using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice.Interfaces;
namespace SerialPortSlice.RealTimeEvents
{
    class ServerStartUp : RealTimeEvent
    {
        public ServerStartUp()
            : base("Serial port servicer starting.")
        {
        }
    }

    class ServerStop : RealTimeEvent
    {
        public ServerStop()
            : base("Serial port servicer stopping.")
        {
        }
    }

    class ServerStopped : RealTimeEvent
    {
        public ServerStopped()
            : base("Serial port servicer stopped.")
        {
        }
    }
}
