using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice.Interfaces;
using EventSlice;

namespace Database
{
    public class Database : Module
    {
        public Database(Dispatcher dispatcher)
            :base(dispatcher)
        {
            //Set up database parameters
        }

        public override string getModuleName()
            { return "Database"; }

        public override void onRealTimeEvent(RealTimeEvent realTimeEvent)
        {
            
        }

        public override void onRealTimeEvent(RealTimeEventDecoded realTimeEvent)
        {
            string eventType = realTimeEvent.messageType;
            string result;
            if (eventType == "detection_event")
                result = detectionInsert(realTimeEvent);
            
        }

        private string detectionInsert(RealTimeEventDecoded detection)
        {
            string sql;
            if(detection.payload["sensor_value"] == null)
                sql = "INSERT INTO vue (date, time, frequency_codespace, transmitter_id, receivers_id) VALUES ('" +
                        detection.payload["date"] + "', '" + detection.payload["time"] + "', '" + detection.payload["frequency_codespace"] + "', " + detection.payload["transmitter_id"] + ", '" + detection.payload["receivers_id"] + "');";
            else
                sql = "INSERT INTO vue (date, time, frequency_codespace, transmitter_id, sensor_value, sensor_unit, receivers_id) VALUES ('" +
                        detection.payload["date"] + "', '" + detection.payload["time"] + "', '" + detection.payload["frequency_codespace"] + "', " + detection.payload["transmitter_id"] + ", " + detection.payload["sensor_value"] + ", 'm', '" + detection.payload["receivers_id"] + "');";
            return //response from SQL server after magic insertion
        }

    }
}
