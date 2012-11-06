using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSlice.Interfaces;
using EventSlice;
using MySql.Data.MySqlClient;
using System.Data;


namespace Databases
{
    public class Database : Module
    {
        private string connectionString {get; set;}

        public Database(Dispatcher dispatcher, string host = "localhost", string db = "csulbsha_sharktopus", string user = "testuser", string pass = "testpass")
        {
            connectionString = "Server=" + host + ";Database=" + db + ";Uid=" + user + ";Pwd=" + pass + ";";
        }

        public override string getModuleName()
            { return "Database"; }
        
        public override void onRealTimeEvent(RealTimeEvent realTimeEvent)
        {
            if(realTimeEvent.GetType() == typeof(Decoder.RealTimeEvents.Decoded))
            {
                Decoder.RealTimeEvents.Decoded rte = (Decoder.RealTimeEvents.Decoded)realTimeEvent;
                string eventType = rte["messagetype"];
                if (eventType == "detection_event")
                    detectionInsert(rte);
            }
            else if(realTimeEvent.GetType() == typeof(ReceiverSlice.RealTimeEvents.NewReceiver))
                receiverInsert((ReceiverSlice.RealTimeEvents.NewReceiver)realTimeEvent);
        }

        private void receiverInsert(ReceiverSlice.RealTimeEvents.NewReceiver newReceiver)
        {
            string statement = "INSERT INTO receivers (id) VALUES ('" + newReceiver["serialnumber"] + "');";
            int response = doInsert(statement);
            dispatcher.enqueueEvent(new Databases.RealTimeEvents.DatabaseResponse(statement, response, newReceiver));
        }

        private void detectionInsert(Decoder.RealTimeEvents.Decoded detection)
        {
            //string fullserialmodel = detection["model"] + ":" + detection["serialnumber"]
            string statement;
            if(detection["decodedmessage"]["sensor_value"] == null)
                statement = "INSERT INTO vue (date, time, frequency_codespace, transmitter_id, receivers_id) VALUES ('" +
                        detection["decodedmessage"]["date"] + "', '" + detection["decodedmessage"]["time"] + "', '" + detection["decodedmessage"]["frequency_codespace"] + "', " + detection["decodedmessage"]["transmitter_id"] + ", '" + detection["decodedmessage"]["receivers_id"] + "');";
            else
                statement = "INSERT INTO vue (date, time, frequency_codespace, transmitter_id, sensor_value, sensor_unit, receivers_id) VALUES ('" +
                        detection["decodedmessage"]["date"] + "', '" + detection["decodedmessage"]["time"] + "', '" + detection["decodedmessage"]["frequency_codespace"] + "', " + detection["decodedmessage"]["transmitter_id"] + ", " + detection["decodedmessage"]["sensor_value"] + ", 'm', '" + detection["decodedmessage"]["receivers_id"] + "');";
            int response = doInsert(statement);
            dispatcher.enqueueEvent(new Databases.RealTimeEvents.DatabaseResponse(statement, response, detection));
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
