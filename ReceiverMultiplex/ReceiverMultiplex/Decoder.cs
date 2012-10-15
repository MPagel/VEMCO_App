using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ReceiverMultiplex
{
    public class Decoder
    {
        enum messageTypes { DETECTION, STATUS, GENERIC, RTMINFO, INFO, UNKNOWN };

        //"Words"
        private static string timestamp = "[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}"; //Note: The timestamp given during the main part of the message is given in UTC, regardless of the offset.
        private static string receiverSerial = "[0-9]{6}";
        private static string transmitterSerial = "[A-Z0-9]{3}-[A-Z0-9]{4}"; //Note: The only serials we have seen were specifically of the form [A-Z][0-9]{2}-[0-9]{4}, so this may be more open than need be.
        private static string detectionCounter = "[0-9]{3}";
        private static string hexSum = "#[A-F0-9]{2}";
        private static string detectionData = "([0-9]+,)*"; //Little worried about this one, as it could catch many things. However, this might just solve the different transmitter type problem. Note that it ends with a comma.
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
        private static string RTMType = "(OFF|232|485)";
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
        private static string RTMInfoResponse = RTMType + ',' + SI + ',' + BL + ',' + BI + ',' + MA + ',' + FMT;
            //232|485|OFF,SI=POLL|X,BL=U|X,BI=WFS|X,MA=U|X,FMT=SER SEQ UTC CS
        private static string infoResponse = infoSerial + receiverSerial + ',' + studyName + ',' + map + " " + codespace + ',' + firmwareVersion + ',' + hardwareVersion;
            //SN, study string, map, codespace list, FW Version, HW Version
        private static string statusResponse = "STS," + DC + ',' + PC + ',' + LV + ',' + BV + ',' + BU + ',' + I + ',' + T + ',' + DU + ',' + RU;
            //STS,DC=X,PC=X,LV=X.X,BV=X.X,BU=X.X,I=X.X,T=X.X,DU=X.X,RU=X.X,XYZ=-X.XX:-Y.YY:Z.ZZ
            /* Note that there are actually two varieties of this, one that is periodically sent out during RTM mode,
            * and one that is explicitly requested by the STATUS command. From our test data, the XYZ will not show up
            * if the receiver is in STORAGE mode. There is an additonal field in the manual STATUS command that
            * tells you what the state of the receiver is. Thanks for not documenting that.*/

        public int Decode(string message)
        //Will presumably return an instance of your event class
        {
            int messageType = getMessageType(message);

            return messageType; //magic
        }

        private int getMessageType(string unparsedMessage)
        {
            if (Regex.IsMatch(unparsedMessage, detectionEvent))
                return (int)messageTypes.DETECTION;
            else if (Regex.IsMatch(unparsedMessage, statusResponse))
                return (int)messageTypes.STATUS;
            else if (Regex.IsMatch(unparsedMessage, genericResponse))
                return (int)messageTypes.GENERIC;
            else if (Regex.IsMatch(unparsedMessage, RTMInfoResponse))
                return (int)messageTypes.RTMINFO;
            else if (Regex.IsMatch(unparsedMessage, infoResponse))
                return (int)messageTypes.INFO;
            else
                return (int)messageTypes.UNKNOWN;
        }
    }
}