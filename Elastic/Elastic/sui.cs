using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elastic
{
    class sui : EventSlice.Interfaces.Module
    {

        public sui(EventSlice.Dispatcher d)
            : base(d) { }


        public override string getModuleName()
        { return "Server User Interface"; }

         /// <summary>
        /// The hook for the event dispatcher.
        /// </summary>
        /// <param name="rte">realtime event</param>
        public override void onRealTimeEvent(EventSlice.Interfaces.RealTimeEvent rte)
        {

        }


    }
}
