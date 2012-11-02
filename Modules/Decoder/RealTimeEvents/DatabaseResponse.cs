using EventSlice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decoder.RealTimeEvents
{
    class DatabaseResponse: RealTimeEvent
    {
        string sql { get; private set; }
        int response { get; private set; }

        public DatabaseResponse(string message, int response)
            :base(message)
        {
            this.sql = message;
            this.response = response;
        }

        public override string ToString()
        {
            return sql + '\n' + response + " rows affected.";
        }
    }
}
