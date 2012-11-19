using EventSlice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.RealTimeEvents
{
    /// <summary>
    /// Contains the response from the database after an attempted insertion as well as the command.
    /// </summary>
    public class DatabaseResponse: RealTimeEvent
    {
        public string sql { get; private set; }
        public int response { get; private set; }

        /// <summary>
        /// Constructor for this event.
        /// </summary>
        /// <param name="message">The SQL non-query command (generally an INSERT).</param>
        /// <param name="response">The number of rows affected. -1 indicates a failed insertion.</param>
        /// <param name="originatingEvent">From whence this event came.</param>
        public DatabaseResponse(string message, int response, RealTimeEvent originatingEvent)
            :base("Database module: " + message, originatingEvent)
        {
            this.sql = message;
            this.response = response;
        }

        /// <summary>
        /// A string representation of this event.
        /// </summary>
        /// <returns>A string representation of this event.</returns>
        public override string ToString()
        {
            return sql + '\n' + response + " rows affected.";
        }
    }
}
