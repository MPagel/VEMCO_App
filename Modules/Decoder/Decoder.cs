using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using EventSlice.Interfaces;
using EventSlice;

namespace Decoder
{
    public class Decoder:Module
    {
        public override string getModuleName()
            { return "Decoder"; }
        //private enum messageTypes{DETECTION, STATUS, GENERIC, RTMINFO, INFO, UNKNOWN};
        //"Words"
        /*private static string timestamp = "[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}"; //Note: The timestamp given during the main part of the message is given in UTC, regardless of the offset.
        private static string receiverId = "([0-9]{6})";
        private static string frequencyCodespace = "[A-Z0-9]{3}-[A-Z0-9]{4}"; //Note: The only serials we have seen were specifically of the form [A-Z][0-9]{2}-[0-9]{4}, so this may be more open than need be.
        private static string detectionCounter = "[0-9]{3}";
        private static string hexSum = "#[A-F0-9]{2}";
        private static string transmitterId_sensorValue = "([0-9]+,)*[0-9]+"; //Doesn't always have a sensor value.
        private static string status = "(OK|FAILURE|INVALID)";
        private static string p = "[0-9]"; //Currently always 0, but we should provide as much functionality for the possibility of that changing in the future.
        private static string decimalSum = "#[0-9]{2}";
        private static string byteCount = "\\[[0-9]{4}\\]"; //Escape sequences for escape sequences...
        private static string infoSerial = "[0-9A-Z]+-[0-9A-Z]+:";
        private static string studyName = "'[0-9A-Z]*'";
        private static string map = "[A-Z0-9]+-[0-9]+"; //I have no idea what this is.
        private static string codespace = "\\[ ([0-9]{4}( |/))*\\]"; //Nor this.
        private static string firmwareVersion = "FW=([0-9]+\\.)*[0-9]+";
        private static string hardwareVersion = "HW=[0-9]+";
        private static string DC = "DC=[0-9]+";
        private static string PC = "PC=[0-9]+";
        private static string LV = "LV=[0-9]+\\.[0-9]+";
        private static string BV = "BV=[0-9]+\\.[0-9]+";
        private static string BU = "BU=[0-9]+\\.[0-9]+";
        private static string I = "I=[0-9]+\\.[0-9]+";
        private static string T = "T=[0-9]+\\.[0-9]+";
        private static string DU = "DU=[0-9]+\\.[0-9]+";
        private static string RU = "RU=[0-9]+\\.[0-9]+";
        private static string XYZ = "XYZ=-?[0-9]+\\.[0-9]+:-?[0-9]+\\.[0-9]+:-?[0-9]+\\.[0-9]+"; //This doesn't show up if the lousy thing is in storage mode.
        private static string state = "[A-Z]+"; //RECORDING or STORAGE is what we've seen
        private static string RTMMode = "(OFF|232|485)";
        private static string SI = "SI=(POLL|[0-9]+)";
        private static string BL = "BL=(U|[0-9]+)";
        private static string BI = "BI=(WFS|[0-9]+)";
        private static string MA = "MA=(U|[0-9]+)";
        private static string FMT = "FMT=([A-Z_ ])*";
        private static string endline = "\\r\\n";

        //"Sentences"
        private static string detectionEvent = receiverId + ',' + detectionCounter + ',' + timestamp + ',' + frequencyCodespace + ',' + detectionData + hexSum; //Note that detectionData ends with a comma, and that the transmitterSerial is considered part of the info field
            //[0-9]{6},[0-9]{3},[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2},[A-Z0-9]{3}-[A-Z0-9]{4},([0-9]+,)*#[A-F0-9]{2}\r\n
        //Responses arrive in the format *SSSSSS.P#CC[LLLL],response,status,#HH\r\n
            //The responsePrefix handles the *SSSSSS.P#CC[LLLL], portion.
            //The responseSuffix handles the ,status,#HH portion.
        private static string responsePrefix = "\\*" + receiverId + '.' + p + decimalSum + byteCount + ',';
        private static string responseSuffix = ',' + status + ',' + hexSum;
        private static string genericResponse = responsePrefix + responseSuffix.Substring(1); //Most commands do not have a value for "response"
        private static string RTMInfoResponse = RTMMode + ',' + SI + ',' + BL + ',' + BI + ',' + MA + ',' + FMT;
            //232|485|OFF,SI=POLL|X,BL=U|X,BI=WFS|X,MA=U|X,FMT=SER SEQ UTC CS
        private static string infoResponse = infoSerial + receiverId + ',' + studyName + ',' + map + " " + codespace + ',' + firmwareVersion + ',' + hardwareVersion;
            //SN, study string, map, codespace list, FW Version, HW Version
        private static string statusResponse = "STS," + DC + ',' + PC + ',' + LV + ',' + BV + ',' + BU + ',' + I + ',' + T + ',' + DU + ',' + RU;
            //STS,DC=X,PC=X,LV=X.X,BV=X.X,BU=X.X,I=X.X,T=X.X,DU=X.X,RU=X.X,XYZ=-X.XX:-Y.YY:Z.ZZ
            /* Note that there are actually two varieties of this, one that is periodically sent out during RTM mode,
            * and one that is explicitly requested by the STATUS command. From our test data, the XYZ will not show up
            * if the receiver is in STORAGE mode. There is an additonal field in the manual STATUS command that
            * tells you what the state of the receiver is. Thanks for not documenting that.*/

        private Dictionary<string,string> decodeDetectionEvent(Dictionary<string,string> payload)
        {
            string rSerial;
            string dCounter;
            DateTime tstamp;
            string frequency_codespace;
            int transmitter_id;
            double sensor_value = -1;

            rSerial = Regex.Match(detectionMessage, receiverId).Value;
            dCounter = Regex.Match(detectionMessage, ','+detectionCounter).Value.Substring(1);
            string time = Regex.Match(detectionMessage, timestamp).Value; //YYYY-MM-DD HH:MM:SS
            int year = int.Parse(time.Substring(0,4));
            int month = int.Parse(time.Substring(5,2));
            int day = int.Parse(time.Substring(8,2));
            int hour = int.Parse(time.Substring(11,2));
            int minute = int.Parse(time.Substring(14,2));
            int second = int.Parse(time.Substring(17,2));
            tstamp = new DateTime(year, month, day, hour, minute, second);
            frequency_codespace = Regex.Match(detectionMessage, frequencyCodespace).Value;
            string tID_sValue = Regex.Match(detectionMessage, frequency_codespace + ',' + transmitterId_sensorValue).Value.Substring(frequency_codespace.Length+1);
            if (tID_sValue.Contains(','))
            {
                transmitter_id = int.Parse(tID_sValue.Substring(0, tID_sValue.IndexOf(',')));
                sensor_value = double.Parse(tID_sValue.Substring(tID_sValue.IndexOf(',')+1));
            }
            else
                transmitter_id = int.Parse(tID_sValue);

            return new RealTimeEvents.RealTimeEventDetection(rSerial, dCounter, tstamp, frequency_codespace, transmitter_id, sensor_value);
        }

        private RealTimeEvents.RealTimeEventStatus decodeStatusEvent(string statusMessage)
        {
            int DC_Value = int.Parse(Regex.Match(statusMessage, DC).Value.Substring(3));
            int PC_Value = int.Parse(Regex.Match(statusMessage, PC).Value.Substring(3));
            double LV_Value = double.Parse(Regex.Match(statusMessage, LV).Value.Substring(3));
            double BV_Value = double.Parse(Regex.Match(statusMessage, BV).Value.Substring(3));
            double BU_Value = double.Parse(Regex.Match(statusMessage, BU).Value.Substring(3));
            double I_Value = double.Parse(Regex.Match(statusMessage, I).Value.Substring(2));
            double T_Value = double.Parse(Regex.Match(statusMessage, T).Value.Substring(2));
            double DU_Value = double.Parse(Regex.Match(statusMessage, DU).Value.Substring(3));
            double RU_Value = double.Parse(Regex.Match(statusMessage, RU).Value.Substring(3));

            string[] XYZ_Values = Regex.Match(statusMessage, XYZ).Value.Substring(4).Split(':');
            double X_Value = double.Parse(XYZ_Values[0]);
            double Y_Value = double.Parse(XYZ_Values[1]);
            double Z_Value = double.Parse(XYZ_Values[2]);

            return new RealTimeEvents.RealTimeEventStatus(DC_Value, PC_Value, LV_Value, BV_Value, BU_Value, I_Value, T_Value, DU_Value, RU_Value, X_Value, Y_Value, Z_Value);
        }

        public void Decode(string message, dynamic config)
        {
            Dictionary<string,string> payload = new Dictionary<string,string>();
            string messageType  = getMessageType(message, config);
            Match matches;
            foreach(string word in config.decoder[messageType].word_order)
            {
                String wordRegex = ((String)config.decoder.words[word]);
                matches = Regex.Match(message, wordRegex);
                if (matches.Success)
                    payload.Add(word, matches.Groups[1].ToString());
                else
                    payload.Add(word, null);
            }
            dispatcher.enqueueEvent(new RealTimeEvents.RealTimeEventDecoded(message, config, messageType, payload));
        }

        private RealTimeEvents.RealTimeEventRTMInfo decodeRTMInfoEvent(string RTMInfoMessage)
        {
            string RTM_Mode = Regex.Match(RTMInfoMessage, RTMMode).Value;
            string SI_Value = Regex.Match(RTMInfoMessage, SI).Value.Substring(3);
            string BL_Value = Regex.Match(RTMInfoMessage, BL).Value.Substring(3);
            string BI_Value = Regex.Match(RTMInfoMessage, BI).Value.Substring(3);
            string MA_Value = Regex.Match(RTMInfoMessage, MA).Value.Substring(3);
            string FMT_Value = Regex.Match(RTMInfoMessage, FMT).Value.Substring(4);

            return new RealTimeEvents.RealTimeEventRTMInfo(RTM_Mode, SI_Value, BL_Value, BI_Value, MA_Value, FMT_Value);
        }

        private RealTimeEvents.RealTimeEventInfo decodeInfoEvent(string infoMessage)
        {
            string serial = Regex.Match(infoMessage, infoSerial).Value + ':' + Regex.Match(infoMessage, receiverId).Value;
            string sName = Regex.Match(infoMessage, studyName).Value;
            string Map = Regex.Match(infoMessage, sName + ',' + map).Value.Substring(sName.Length+1);
            string Codespace = Regex.Match(infoMessage, codespace).Value;
            string FW = Regex.Match(infoMessage, firmwareVersion).Value.Substring(3);
            string HW = Regex.Match(infoMessage, hardwareVersion).Value.Substring(3);

            dispatcher.enqueueEvent(new RealTimeEvents.RealTimeEventDecoded(message, config, messageType, payload));
        }

        private string getMessageType(string unparsedMessage, dynamic config) 
        {     
            foreach (dynamic sentence in config.decoder.sentences)
            {
                try
                {
                    List<String> wordExpansions = new List<String>();
                    foreach(Object o in sentence.word_orders) 
                    {
                        wordExpansions.Add(config.decoder.words[((String)o)]);
                    }
                    if(Regex.IsMatch(unparsedMessage, String.Format(sentence.format, wordExpansions)))
                    {
                        return sentence;
                    }

                } finally {
                }
            }
            EventSlice.Interfaces.ModuleException me = new ModuleException(this,
                "Unparsed message (" + unparsedMessage + ") does not match definition in this config.");
            dispatcher.enqueueEvent(new RealTimeEvents.RealTimeEventUnknown(me.exceptionText,config));
            throw me;
            
        }
   
        public override void onRealTimeEvent(RealTimeEvent rte)
        {
            if(rte.GetType() == typeof(ReceiverSlice.RealTimeEvents.UnparsedMessage))
            {
                rte=rte;
            }
            //Decode(unparsedMessage.unparsedMessage, unparsedMessage.config);
        }
    }
}