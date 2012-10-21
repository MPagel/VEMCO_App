using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSlice.Interfaces
{
    public abstract class RealTimeEvent
    {
        private string message;

        public RealTimeEvent(string message)
        {
            this.message = message;
        }

        public string toString()
        {
            return message;
        }
    }
}
