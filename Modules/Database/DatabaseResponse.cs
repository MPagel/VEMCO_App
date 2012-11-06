using EventSlice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.RealTimeEvents
{
    public class DatabaseResponse: RealTimeEvent
    {
        public string sql { get; private set; }
        public int response { get; private set; }

        public DatabaseResponse(string message, int response, RealTimeEvent originatingEvent)
            :base("Database module: " + message, originatingEvent)
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
