using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice.Interfaces;
using EventSlice;
using MySql.Data.MySqlClient;
using System.Data;

namespace Database
{
    public class Database : Module
    {
        string connectionString {private get; private set;}

        public Database(Dispatcher dispatcher, string host = "localhost", string db = "csulbsha_sharktopus", string user = "testuser", string pass = "testpass")
            :base(dispatcher)
        {
            connectionString = "Server=" + host + ";Databse=" + db + ";Uid=" + user + ";Pwd=" + pass + ";";
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

        private DatabaseResponse receiverInsert(NewReceiver newReceiver)
        {
            string statement = "INSERT INTO receivers (id) VALUES ('" + newReceiver.serialorsomething + "');");
            return new DatabaseResponse(statement, response);
        }

        private DatabseResponse detectionInsert(RealTimeEventDecoded detection)
        {
            string statement;
            if(detection.payload["sensor_value"] == null)
                statement = "INSERT INTO vue (date, time, frequency_codespace, transmitter_id, receivers_id) VALUES ('" +
                        detection.payload["date"] + "', '" + detection.payload["time"] + "', '" + detection.payload["frequency_codespace"] + "', " + detection.payload["transmitter_id"] + ", '" + detection.payload["receivers_id"] + "');";
            else
                statement = "INSERT INTO vue (date, time, frequency_codespace, transmitter_id, sensor_value, sensor_unit, receivers_id) VALUES ('" +
                        detection.payload["date"] + "', '" + detection.payload["time"] + "', '" + detection.payload["frequency_codespace"] + "', " + detection.payload["transmitter_id"] + ", " + detection.payload["sensor_value"] + ", 'm', '" + detection.payload["receivers_id"] + "');";
            int response = doInsert(statement);
            return new DatabseResponse(statement, response);
        }

        private int doInsert(string statement)
        {
            int response = -1;
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command;
            connection.Open();
            try
            {
                command = connection.CreateCommand();
                command.CommandText = statement;
                response = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {}
            finally
            {
                if(connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return response;
        }
    }
}
