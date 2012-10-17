using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using EventSlice.Interfaces;

namespace Decoder
{
    public class Decoder:Module
    {
        enum messageTypes { DETECTION, STATUS, GENERIC, RTMINFO, INFO, UNKNOWN };

        //"Words"
        private static string timestamp = "[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}"; //Note: The timestamp given during the main part of the message is given in UTC, regardless of the offset.
        private static string receiverSerial = "[0-9]{6}";
        private static string transmitterSerial = "[A-Z0-9]{3}-[A-Z0-9]{4}"; //Note: The only serials we have seen were specifically of the form [A-Z][0-9]{2}-[0-9]{4}, so this may be more open than need be.
        private static string detectionCounter = "[0-9]{3}";
        private static string hexSum = "#[A-F0-9]{2}";
        private static string detectionData = "([0-9]+,)*[0-9]+"; //Little worried about this one, as it could catch many things. However, this might just solve the different transmitter type problem. Note that it ends with a comma.
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
        private static string detectionEvent = receiverSerial + ',' + detectionCounter + ',' + timestamp + ',' + transmitterSerial + ',' + detectionData + hexSum; //Note that detectionData ends with a comma, and that the transmitterSerial is considered part of the info field
            //[0-9]{6},[0-9]{3},[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2},[A-Z0-9]{3}-[A-Z0-9]{4},([0-9]+,)*#[A-F0-9]{2}\r\n
        //Responses arrive in the format *SSSSSS.P#CC[LLLL],response,status,#HH\r\n
            //The responsePrefix handles the *SSSSSS.P#CC[LLLL], portion.
            //The responseSuffix handles the ,status,#HH portion.
        private static string responsePrefix = "\\*" + receiverSerial + '.' + p + decimalSum + byteCount + ',';
        private static string responseSuffix = ',' + status + ',' + hexSum;
        private static string genericResponse = responsePrefix + responseSuffix.Substring(1); //Most commands do not have a value for "response"
        private static string RTMInfoResponse = RTMMode + ',' + SI + ',' + BL + ',' + BI + ',' + MA + ',' + FMT;
            //232|485|OFF,SI=POLL|X,BL=U|X,BI=WFS|X,MA=U|X,FMT=SER SEQ UTC CS
        private static string infoResponse = infoSerial + receiverSerial + ',' + studyName + ',' + map + " " + codespace + ',' + firmwareVersion + ',' + hardwareVersion;
            //SN, study string, map, codespace list, FW Version, HW Version
        private static string statusResponse = "STS," + DC + ',' + PC + ',' + LV + ',' + BV + ',' + BU + ',' + I + ',' + T + ',' + DU + ',' + RU;
            //STS,DC=X,PC=X,LV=X.X,BV=X.X,BU=X.X,I=X.X,T=X.X,DU=X.X,RU=X.X,XYZ=-X.XX:-Y.YY:Z.ZZ
            /* Note that there are actually two varieties of this, one that is periodically sent out during RTM mode,
            * and one that is explicitly requested by the STATUS command. From our test data, the XYZ will not show up
            * if the receiver is in STORAGE mode. There is an additonal field in the manual STATUS command that
            * tells you what the state of the receiver is. Thanks for not documenting that.*/

        public RealTimeEvent Decode(string message)
        {
            try
            {
                switch (getMessageType(message))
                {
                    case messageTypes.DETECTION:
                        return decodeDetectionEvent(message);
                    case messageTypes.STATUS:
                        return decodeStatusEvent(message);
                    case messageTypes.GENERIC:
                        return decodeGenericEvent(message);
                    case messageTypes.RTMINFO:
                        return decodeRTMInfoEvent(message);
                    case messageTypes.INFO:
                        return decodeInfoEvent(message);
                    case messageTypes.UNKNOWN:
                        return decodeUnknownEvent(message);
                    default:
                        throw new InvalidCommandException("Critical decoding error.");
                }   
            }
            catch (InvalidCommandException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private messageTypes getMessageType(string unparsedMessage)
        {
            if (Regex.IsMatch(unparsedMessage, detectionEvent))
                return messageTypes.DETECTION;
            else if (Regex.IsMatch(unparsedMessage, statusResponse))
                return messageTypes.STATUS;
            else if (Regex.IsMatch(unparsedMessage, genericResponse))
                return messageTypes.GENERIC;
            else if (Regex.IsMatch(unparsedMessage, RTMInfoResponse))
                return messageTypes.RTMINFO;
            else if (Regex.IsMatch(unparsedMessage, infoResponse))
                return messageTypes.INFO;
            else
                return messageTypes.UNKNOWN;
        }

        private RealTimeEvents.RealTimeEventDetection decodeDetectionEvent(string detectionMessage)
        {
            string rSerial;
            string dCounter;
            DateTime tstamp;
            string tSerial;
            string data;

            rSerial = Regex.Match(detectionMessage, receiverSerial).Value;
            dCounter = Regex.Match(detectionMessage, ','+detectionCounter).Value.Substring(1);
            string time = Regex.Match(detectionMessage, timestamp).Value; //YYYY-MM-DD HH:MM:SS
            int year = int.Parse(time.Substring(0,4));
            int month = int.Parse(time.Substring(5,2));
            int day = int.Parse(time.Substring(8,2));
            int hour = int.Parse(time.Substring(11,2));
            int minute = int.Parse(time.Substring(14,2));
            int second = int.Parse(time.Substring(17,2));
            tstamp = new DateTime(year, month, day, hour, minute, second);
            tSerial = Regex.Match(detectionMessage, transmitterSerial).Value;
            data = Regex.Match(detectionMessage, tSerial + ',' + detectionData).Value.Substring(tSerial.Length+1);

            return new RealTimeEvents.RealTimeEventDetection(RealTimeEventType.DETECTION_EVENT, rSerial, dCounter, tstamp, tSerial, data);
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

            return new RealTimeEvents.RealTimeEventStatus(RealTimeEventType.STATUS_EVENT, DC_Value, PC_Value, LV_Value, BV_Value, BU_Value, I_Value, T_Value, DU_Value, RU_Value, X_Value, Y_Value, Z_Value);
        }

        private RealTimeEvents.RealTimeEventGeneric decodeGenericEvent(string genericMessage)
        {
            string Status = Regex.Match(genericMessage, status).Value;
            int returnStatus; //0 = "OK" 1 = "FAILURE" 2 = "INVALID"
            if (Status == "OK")
                returnStatus = 0;
            else if (Status == "FAILURE")
                returnStatus = 1;
            else
                returnStatus = 2;

            return new RealTimeEvents.RealTimeEventGeneric(RealTimeEventType.GENERIC_EVENT, returnStatus);
        }

        private RealTimeEvents.RealTimeEventRTMInfo decodeRTMInfoEvent(string RTMInfoMessage)
        {
            string RTM_Mode = Regex.Match(RTMInfoMessage, RTMMode).Value;
            string SI_Value = Regex.Match(RTMInfoMessage, SI).Value.Substring(3);
            string BL_Value = Regex.Match(RTMInfoMessage, BL).Value.Substring(3);
            string BI_Value = Regex.Match(RTMInfoMessage, BI).Value.Substring(3);
            string MA_Value = Regex.Match(RTMInfoMessage, MA).Value.Substring(3);
            string FMT_Value = Regex.Match(RTMInfoMessage, FMT).Value.Substring(4);

            return new RealTimeEvents.RealTimeEventRTMInfo(RealTimeEventType.RTMINFO_EVENT, RTM_Mode, SI_Value, BL_Value, BI_Value, MA_Value, FMT_Value);
        }

        private RealTimeEvents.RealTimeEventInfo decodeInfoEvent(string infoMessage)
        {
            string serial = Regex.Match(infoMessage, infoSerial).Value + ':' + Regex.Match(infoMessage, receiverSerial).Value;
            string sName = Regex.Match(infoMessage, studyName).Value;
            string Map = Regex.Match(infoMessage, sName + ',' + map).Value.Substring(sName.Length+1);
            string Codespace = Regex.Match(infoMessage, codespace).Value;
            string FW = Regex.Match(infoMessage, firmwareVersion).Value.Substring(3);
            string HW = Regex.Match(infoMessage, hardwareVersion).Value.Substring(3);

            return new RealTimeEvents.RealTimeEventInfo(RealTimeEventType.INFO_EVENT, serial, sName, Map, Codespace, FW, HW);
        }

        private RealTimeEvents.RealTimeEventUnknown decodeUnknownEvent(string unknownMessage)
        {
            return new RealTimeEvents.RealTimeEventUnknown(RealTimeEventType.UNKNOWN_EVENT, unknownMessage);
        }
    }
}