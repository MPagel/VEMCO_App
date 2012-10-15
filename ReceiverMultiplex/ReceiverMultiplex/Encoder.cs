using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverMultiplex
{
    public class Encoder
    {
        private String prefix; //Will be made to be *SSSSSS.P#CC,

        public Encoder(int serial, int p = 0)
            { UpdatePrefix(serial, p); }

        public void UpdatePrefix(int serial, int p = 0) //Sets the prefix part of the commands. This must be called if the serial were to be changed for some reason, or if the p-value was changed.
        {
            try
            {
                if (serial < 0 || serial > 999999)
                    throw new InvalidCommandException("Invalid serial. Valid range is 0-999999.");
                if (p < 0 || p > 9)
                    throw new InvalidCommandException("Invalid p value. Valid range is 0-9.");
                String Serial = serial.ToString();
                String temp = "*";
                for (int i = Serial.Length - 6; i > 0; --i)
                    temp = temp + "0";
                temp = temp + Serial + "." + p + "#";
                int sum = p;
                for (int i = Serial.Length; i > 0; --i)
                    sum += Serial[i];
                if (sum < 10)
                    temp = temp + "0";
                temp = temp + sum + ",";
                prefix = temp;
            }
            catch (InvalidCommandException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public String STATUS() //Read status string
            { return BuildCommand("STATUS"); }

        public String INFO()
            { return BuildCommand("INFO"); }

        public String BAUDRATE(int baud) //Set the serial port baud rate (default rate is 9600, 8N1 protocol)
            { return BuildCommand("BAUDRATE=" + baud.ToString()); }

        public String TIME(int year, int month, int day, int hour, int minutes, int seconds) //Set receiver clock, x = 24 hour UTC time as YYYY-MM-DD HH:MM:SS or local time as YYYY-MM-DD HH:MM:SS +ZZZZ
        {
            try
            {
                String Month;
                String Day;
                String Hour;
                String Minutes;
                String Seconds;

                if( (year < 1000) || (year > 9999) )
                    throw new InvalidCommandException("Invalid year. Valid range is 1000-9999.");
                if( (month < 1) || (month > 12) )
                    throw new InvalidCommandException("Invalid month. Valid range is 1-12");
                else
                {
                    Month = month.ToString();
                    if(month < 10)
                        Month = "0" + Month;
                }
                if( (day < 1) || (day > 31) )
                    throw new InvalidCommandException("Invalid day. Valid range is 1-31.");
                else
                {
                    Day = day.ToString();
                    if(day < 10)
                        Day = "0" + Day;
                }
                if( (hour < 0) || (hour > 23) )
                    throw new InvalidCommandException("Invalid hour. Valid range is 0-23.");
                else
                {
                    Hour = hour.ToString();
                    if(hour < 10)
                        Hour = "0" + Hour;
                }
                if( (minutes < 0) || (minutes > 59) )
                    throw new InvalidCommandException("Invalid minutes. Valid range is 0-59.");
                else
                {
                    Minutes = minutes.ToString();
                    if(minutes < 10)
                        Minutes = "0" + Minutes;
                }
                if( (seconds < 0) || (seconds > 59) )
                    throw new InvalidCommandException("Invalid seconds. Valid range is 0-59.");
                else
                {
                    Seconds = seconds.ToString();
                    if(seconds < 10)
                        Seconds = "0" + Seconds;
                }

                return BuildCommand("TIME=" + year.ToString() + "-" + Month + "-" + Day + " " + Hour + ":" + Minutes + ":" + Seconds);
            }
            catch(InvalidCommandException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public String TIME(int year, int month, int day, int hour, int minutes, int seconds, char offsetSign, int offsetHours, int offsetMinutes)
        {
            try
            {
                String Month;
                String Day;
                String Hour;
                String Minutes;
                String Seconds;
                String Offset;

                if( (year < 1000) || (year > 9999) )
                    throw new InvalidCommandException("Invalid year. Valid range is 1000-9999.");
                if( (month < 1) || (month > 12) )
                    throw new InvalidCommandException("Invalid month. Valid range is 1-12");
                else
                {
                    Month = month.ToString();
                    if(month < 10)
                        Month = "0" + Month;
                }
                if( (day < 1) || (day > 31) )
                    throw new InvalidCommandException("Invalid day. Valid range is 1-31.");
                else
                {
                    Day = day.ToString();
                    if(day < 10)
                        Day = "0" + Day;
                }
                if( (hour < 0) || (hour > 23) )
                    throw new InvalidCommandException("Invalid hour. Valid range is 0-23.");
                else
                {
                    Hour = hour.ToString();
                    if(hour < 10)
                        Hour = "0" + Hour;
                }
                if( (minutes < 0) || (minutes > 59) )
                    throw new InvalidCommandException("Invalid minutes. Valid range is 0-59.");
                else
                {
                    Minutes = minutes.ToString();
                    if(minutes < 10)
                        Minutes = "0" + Minutes;
                }
                if( (seconds < 0) || (seconds > 59) )
                    throw new InvalidCommandException("Invalid seconds. Valid range is 0-59.");
                else
                {
                    Seconds = seconds.ToString();
                    if(seconds < 10)
                        Seconds = "0" + Seconds;
                }
                if( ((offsetSign != '+') && (offsetSign != '-')) || offsetHours > 23 || offsetMinutes > 59)
                    throw new InvalidCommandException("Invalid offset.");
                else
                {
                    Offset = offsetSign + offsetHours.ToString() + offsetMinutes.ToString();
                }

                return BuildCommand("TIME=" + year.ToString() + "-" + Month + "-" + Day + " " + Hour + ":" + Minutes + ":" + Seconds + " " + Offset);
            }
            catch(InvalidCommandException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public String START() //Start recording
            { return BuildCommand("START"); }

        public String STOP() //Stop recording (Recording restarts after an hour)
            { return BuildCommand("STOP"); }

        public String ERASE() //Erase data (must be stopped)
            { return BuildCommand("ERASE"); }

        public String RTMINFO() //Read Real-Time mode configuration
            { return BuildCommand("RTMINFO"); }

        public String RTMOFF() //Disables RTM output
            { return BuildCommand("RTMOFF"); }

        public String RTM232() //Resets RTM state and enables output on the RS232 lines
            { return BuildCommand("RTM232"); }

        public String RTM485() //Resets RTM state and enables output on the RS485 lines
            { return BuildCommand("RTM485"); }

        public String RTMNOW() //Resets RTM schedule (used for Polling)
            { return BuildCommand("RTMNOW"); }

        public String RTMPROFILE(int profile) //Select a RTM output method where x = 0,1,2...
        {
            if (profile < 0)
                throw new InvalidCommandException("Invalid profile. Valid range is nonnegative integers.");
            return BuildCommand("RTMPROFILE=" + profile);
        }

        public String RTMAUTOERASE(int threshold) //Set the auto erase threshold. x = % of log to keep free, 0-50
        {
            if (threshold < 0 || threshold > 50)
                throw new InvalidCommandException("Invalid auto-erase threshold. Valid range is 0-50.");
            return BuildCommand("RTMAUTOERASE=" + threshold);
        }

        public String STORAGE() //Put the receiver in low power state for storage (must be stopped)
            { return BuildCommand("STORAGE"); }

        public String RESET() //Reset receiver (must be stopped)
            { return BuildCommand("RESET"); }

        public String QUIT() //Exit command session (Disables serial drivers)
            { return BuildCommand("QUIT"); }

        public String RESETBATTERY() //Resets the battery indicator. ONLY TO BE USED AFTER INSTALLING A FRESH BATTERY!
            { return BuildCommand("RESETBATTERY"); }

        public String CustomCommand(String command) //Allows for the sending of any other command.
            { return BuildCommand(command); }

        private String BuildCommand(String command) //Returns the actual command
            { return (prefix + command + "\r"); }
    }
}
