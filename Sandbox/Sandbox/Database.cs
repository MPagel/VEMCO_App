using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Sandbox
{
    /// <summary>
    /// This module handles the insertion of detections and status events into the database.
    /// </summary>
    public class Database : Module
    {
        private Dictionary<string, List<string>> sensor_calibrations { get; set; }
        private string connectionString { get; set; }
        private System.IO.StreamWriter logWriter { get; set; }

        /// <summary>
        /// The constructor for the Database module.
        /// </summary>
        /// <param name="dispatcher">The Dispatcher this object will receive messages from.</param>
        /// <param name="transmitters">The JSON file containing the sensor tag calibrations.</param>
        /// <param name="host">The host name of the database to connect to.</param>
        /// <param name="db">The name of the database to connect to.</param>
        /// <param name="user">The username of the database to connect to.</param>
        /// <param name="pass">The password for the user.</param>
        public Database(Dispatcher dispatcher, dynamic config, string host = "localhost", string db = "csulbsha_sharktopus", string user = "testuser", string pass = "testpass")
            : base(dispatcher)
        {
            connectionString = "Server=" + host + ";Database=" + db + ";Uid=" + user + ";Pwd=" + pass + ";";
            updateSensorCalibrations(config);
            logWriter = new System.IO.StreamWriter(config.log_file, true);
        }

        /// <summary>
        /// Returns the human readable name of this module.
        /// </summary>
        /// <returns>The name of this module.</returns>
        public override string getModuleName()
        { return "Database"; }

        /// <summary>
        /// Updates the list of of sensor tag calibrations from the JSON file. This should be called when the JSON file is updated.
        /// </summary>
        /// <param name="calibrations">The JSON file containing the calibration values.</param>
        public void updateSensorCalibrations(dynamic config)
        {
            this.sensor_calibrations = new Dictionary<string, List<string>>();
            foreach (string transmitter in config.transmitters.Keys)
            {
                List<string> temp = new List<string>(((string)config.transmitters[transmitter]).Split(','));
                if (temp.Count == 3)
                {
                    double test;
                    if(double.TryParse(temp[1], out test) && double.TryParse(temp[2], out test))
                        sensor_calibrations.Add((string)transmitter, temp);
                }
            }
        }

        /// <summary>
        /// The hook for the event dispatcher. Determines the type of message, and if applicable, makes a database insertion.
        /// </summary>
        /// <param name="realTimeEvent"></param>
        public override void onRealTimeEvent(RealTimeEvent realTimeEvent)
        {
            int response = 0;
            if (realTimeEvent.GetType() == typeof(Decoded))
            {
                Decoded rte = (Decoded)realTimeEvent;
                string eventType = rte["messagetype"];
                if (eventType == "detection_event")
                    response = detectionInsert(rte);
                else if (eventType == "status_response")
                    response = statusInsert(rte);
                if (response > 1)
                    response = 1;
            }
            //return response;
        }

        // We are no longer responsible for populating the receivers table in the database with new receivers.
        //private void receiverInsert(ReceiverSlice.RealTimeEvents.NewReceiver newReceiver)
        //{
        //    string statement = "INSERT INTO receivers (id) VALUES ('" + newReceiver["serialnumber"] + "');";
        //    int response = doInsert(statement);
        //    dispatcher.enqueueEvent(new Databases.RealTimeEvents.DatabaseResponse(statement, response, newReceiver));
        //}

        /// <summary>
        /// Makes an insertion into the vue table of the database for a detection event.
        /// </summary>
        /// <param name="detection">The RealTimeEvent object containing all necessary information from the detection.</param>
        /// <returns>The number of rows affected by the insertion.</returns>
        private int detectionInsert(Decoded detection)
        {
            string date = detection["decodedmessage"]["date"];
            string time = detection["decodedmessage"]["time"];
            string frequency_codespace = detection["decodedmessage"]["frequency_codespace"];
            string transmitter_id = detection["decodedmessage"]["transmitter_id"];
            string receiver_model_id = detection["model"] + '-' + detection["serialnumber"];
            string sensor_value = detection["decodedmessage"]["sensor_value"];
            string sensor_type = "NULL";
            string transmitter_codespace_id = frequency_codespace + '-' + transmitter_id;
            if (sensor_value != "NULL")
            {
                if (sensor_calibrations.ContainsKey(transmitter_codespace_id))
                {
                    sensor_type = '\'' + sensor_calibrations[transmitter_codespace_id][0] + '\'';
                    sensor_value = getCalibratedSensorValue(sensor_type, double.Parse(sensor_value), double.Parse(sensor_calibrations[transmitter_codespace_id][1]), double.Parse(sensor_calibrations[transmitter_codespace_id][2])).ToString();
                }
                else
                    sensor_type = "'A2D'";
            }
            string statement = "INSERT INTO vue (date, time, frequency_codespace, transmitter_id, sensor_value, sensor_unit, receivers_id) VALUES ('" +
                    date + "', '" + time + "', '" + frequency_codespace + "', " + transmitter_id + ", " + sensor_value + ", " + sensor_type + ", '" + receiver_model_id + "');";
            int response = doInsert(statement);
            dispatcher.enqueueEvent(new DatabaseResponse(statement, response, detection));
            return response;
        }

        /// <summary>
        /// Returns the calibrated value for the A2D value given as well as the type of reading and the coefficients.
        /// </summary>
        /// <param name="sensor_type">The type of measurement, for example "p", "t", or "m".</param>
        /// <param name="raw_value">The A2D value.</param>
        /// <param name="a">The high-order coefficient.</param>
        /// <param name="b">The low-order coefficient.</param>
        /// <returns>The calibrated value.</returns>
        private double getCalibratedSensorValue(string sensor_type, double raw_value, double a, double b)
        {
            double calibratedValue = raw_value;
            sensor_type = sensor_type.ToLower();
            if (sensor_type == "t" || sensor_type == "m") //linear -- ax + b
                calibratedValue = a * raw_value + b;
            else if (sensor_type == "p") //quadratic -- ax² + b
                calibratedValue = a * raw_value * raw_value + b;
            return calibratedValue;
        }

        /// <summary>
        /// Makes an insertion into the receiver_status table of the databse for a status event.
        /// </summary>
        /// <param name="status">The RealTimeEvent object containing all necessary information from the status event.</param>
        /// <returns>The number of rows affected by the insertion.</returns>
        private int statusInsert(Decoded status)
        {
            string receiver_model_id = status["model"] + '-' + status["serialnumber"];
            string date = status["decodedmessage"]["date"];
            string time = status["decodedmessage"]["time"];
            string dc = status["decodedmessage"]["dc"];
            string pc = status["decodedmessage"]["pc"];
            string lv = status["decodedmessage"]["lv"];
            string bv = status["decodedmessage"]["bv"];
            string bu = status["decodedmessage"]["bu"];
            string i = status["decodedmessage"]["i"];
            string t = status["decodedmessage"]["t"];
            string du = status["decodedmessage"]["du"];
            string ru = status["decodedmessage"]["ru"];
            string xyz = status["decodedmessage"]["xyz"];
            if (xyz != "NULL")
                xyz = '\'' + xyz + '\'';
            string statement = "INSERT INTO receiver_status (id, date, time, detection_count, ping_count, line_voltage, battery_voltage, battery_used, current, temperature, detection_memory, raw_memory, xyz_orientation) VALUES ('" +
                        receiver_model_id + "', '" + date + "', '" + time + "', " + dc + ", " + pc + ", " + lv + ", " + bv + ", " + bu + ", " + i + ", " + t + ", " + du + ", " + ru + ", " + xyz + ");";
            int response = doInsert(statement);
            dispatcher.enqueueEvent(new DatabaseResponse(statement, response, status));
            return response;
        }

        /// <summary>
        /// Attempts to make the actual insertion into the database.
        /// </summary>
        /// <param name="statement">The SQL statement to be performed on the database.</param>
        /// <returns>The number of rows affected by the insertion.</returns>
        private int doInsert(string statement)
        {
            int response = -1;
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command;
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = statement;
                response = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                logWriter.WriteLine("Insertion failure at " + DateTime.Now + ':');
                logWriter.WriteLine("Statement: " + statement);
                logWriter.WriteLine("Error: " + e.Message);
                logWriter.WriteLine();
                logWriter.Flush();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return response;
        }
    }
}