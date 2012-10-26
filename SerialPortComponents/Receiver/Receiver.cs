﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Threading;

using FridayThe13th;
using EventSlice;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;


namespace ReceiverSlice
{
    public class Receiver
    {

        public string portName { get; private set; }
        public int TTL { get; private set; }


        private const int DEFAULT_TTL = 10;
        private const int COM_READ_TIMEOUT_DEFAULT = 500; //milliseconds
        private const int COM_READ_TIMEOUT_SPRIAL = 100; //additional ms to allow for response on next go-'round
        private const string VR2C_COMMAND_FOLDER = "config";

        private Dispatcher dispatcher { get; set; }
        private SerialPort serialPort { get; set; }
        private dynamic receiverConfig = null;
        private int firmwareVersion { get; set; }
        private String commandPreamble { get; set; }
        private TextReader textReader { get; set; }
        private int goState = 1;
        private int write_wait = 100;



        private static char[] crlf = new char[2] { '\x0D', '\x0A' };
        private static char[] lfcr = new char[2] { '\x0A', '\x0D' };
        private static string crlf_string = new string(crlf);
        
        public Receiver(SerialPort serialPort, String portName, Dispatcher dispatcher)
        {
            Dictionary<int, string> configFiles = new Dictionary<int, string>();
            this.TTL = DEFAULT_TTL;
            this.serialPort = serialPort;
            this.portName = portName;
            this.dispatcher = dispatcher;

            serialPort.Open();
            discovery();
            //if (fw_ver < 0)
            //{
            //    serialPort.Close();
            //    dispatcher.enqueueEvent(new RealTimeEvents.ExcepReceiver(this, true));
            //    throw new ReceiverExceptions("INFO command failed to return firmware version.", true);
            //}

            //int fw_use = -1;
            //var jsonParser = new JsonParser() { CamelizeProperties = false };

            //foreach (string filename in System.IO.Directory.GetFiles(VR2C_COMMAND_FOLDER))
            //{

            //    string text = System.IO.File.ReadAllText(filename);
            //    dynamic config = jsonParser.Parse(System.IO.File.ReadAllText(filename));

            //    if (config.FwVersion >= fw_use && config.FwVersion <= fw_ver)
            //    {
            //        fw_use = (Int32)config.FwVersion;
            //        receiverConfig = config;
            //    }
            //}
            //if (fw_use < 0 || receiverConfig == null)
            //{
            //    serialPort.Close();
            //    dispatcher.enqueueEvent(new RealTimeEvents.ExcepReceiver(this, true));
            //    throw new ReceiverExceptions("Valid json config not found for receiver firmware.", true);
            //}

            this.textReader = new StreamReader(serialPort.BaseStream, serialPort.Encoding);
            serialPort.Write(crlf, 0, 2);
            serialPort.Write("*450052.0#16,RTMPROFILE=0");
            serialPort.Write(crlf, 0, 2);
            serialPort.Write("*450052.0#16,START");
            serialPort.Write(crlf, 0, 2);
            Thread.Sleep(500);
            if (serialPort.BytesToRead > 0)
            {
                dispatcher.enqueueEvent(new RealTimeEvents.NoteReceiver(this,"Read: " + serialPort.ReadExisting()));
            }
            run();

            dispatcher.enqueueEvent(new RealTimeEvents.NewReceiver(this));
        }

        public dynamic discovery()
        {
            Dictionary<Int32, List<dynamic>> discoveryMethods = new Dictionary<Int32, List<dynamic>>();
            String discoveryReturns = "";
            

            var jsonParser = new JsonParser() { CamelizeProperties = false };

            foreach (string filename in System.IO.Directory.GetFiles(VR2C_COMMAND_FOLDER))
            {

                string text = System.IO.File.ReadAllText(filename);
                dynamic config = jsonParser.Parse(System.IO.File.ReadAllText(filename));
                var dc = config.discovery_commands;
                var fwver = config.firmware_version;
                if(Object.ReferenceEquals(((ObjectHandle)dc).Unwrap().GetType(),typeof(List<dynamic>)) &&
                    Object.ReferenceEquals(((ObjectHandle)fwver).Unwrap().GetType(),typeof(Int32)))
                {
                    discoveryMethods.Add(fwver, dc);
                }

            }

            List<dynamic> default_discovery = new List<dynamic>();
            default_discovery.Add("*BROADC.A#ST,QUIT");
            default_discovery.Add("*BROADC.A#ST,DISCOVERY");
            default_discovery.Add("*DISCOV.E#RY,DISCOVERY");
            discoveryMethods.Add(-1, default_discovery);

            int discovery_attempts = 0;
            while (serialPort.BytesToRead <= 0 && discovery_attempts < 5)
            {
                write_wait = (discovery_attempts + 1) * 100;
                foreach (List<dynamic> l in discoveryMethods.Values)
                {
                    foreach(var command in l) {
                        write(command);
                    }
                }
                discovery_attempts++;
            }
            if (discovery_attempts >= 5)
            {

            }

            while (serialPort.BytesToRead > 0)
            {
                discoveryReturns = serialPort.ReadExisting();
            }
            
            commandPreamble = crlf + discoveryReturns.Substring(0, 12) + ",";
            dispatcher.enqueueEvent(new RealTimeEvents.NoteReceiver(this, "(receiver note) command preamble: " + discoveryReturns.Substring(0,12) + ","));
            dispatcher.enqueueEvent(new RealTimeEvents.NoteReceiver(this, "(receiver note) read: " + discoveryReturns));
            //string infoReturns = serialPort.ReadLine();
            //if (infoReturns != "")
            //{
                
            //    int fw_start = infoReturns.IndexOf("FW=");
            //    int fw_end = infoReturns.IndexOf(",", fw_start);
            //    string fw_ver = infoReturns.Substring((fw_start + 3), (fw_end - fw_start - 3));
            //    int fw_ver_firstperiod = fw_ver.IndexOf(".");
            //    int fw_ver_secondperiod = fw_ver.IndexOf(".", fw_ver_firstperiod + 1);
            //    string major = fw_ver.Substring(0, fw_ver_firstperiod);
            //    string minor = fw_ver.Substring(fw_ver_firstperiod + 1, (fw_ver_secondperiod - fw_ver_firstperiod - 1));
            //    string release = fw_ver.Substring(fw_ver_secondperiod + 1, (fw_ver.Length - fw_ver_secondperiod - 1));
            //    return (Int32.Parse(major) * 10000) + (Int32.Parse(minor) * 100) + (Int32.Parse(release));
            //}
            //else
            //{
            //    return -1;
            //}
            return 1;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void write(string text)
        {
            serialPort.Write(crlf, 0, 2);
            Thread.Sleep(100);
            serialPort.Write(text);
            serialPort.Write(crlf, 0, 2);
            Thread.Sleep(100);
            
        }

        public void shutdown()
        {
            goState = 0;
            for (int i = 0; i <= 5; i++)
            {

                if (goState == -1)
                {
                    dispatcher.enqueueEvent(new RealTimeEvents.DelReceiver(this));
                    serialPort.Close();
                    return;
                }
                Thread.Sleep(500);
            }
            dispatcher.enqueueEvent(new RealTimeEvents.DelReceiver(this));
            serialPort.Close();
        }

        
        public async Task run()
        {
            while (goState > 0)
            {
                var ret = string.Empty;
                var buffer = new char[1]; // Not the most efficient...
                while (!ret.Contains("\n") && (!ret.Contains("\r")))
                {
                    var charsRead = await textReader.ReadAsync(buffer, 0, 1);
                    if (charsRead == 0)
                    {
                        throw new EndOfStreamException();
                    }
                    ret += buffer[0];
                }

                if (ret.Length > 1)
                {
                    dispatcher.enqueueEvent(new RealTimeEvents.UnparsedMessage(this, ret));
                }
            }
            goState = -1;
        }

    }
}
