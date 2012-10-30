using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Threading;

using System.Text.RegularExpressions;

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

        private Encoder encoder { get; set; }
        private Dispatcher dispatcher { get; set; }
        private SerialPort serialPort { get; set; }
        private int firmwareVersion { get; set; }
<<<<<<< HEAD
=======
        private String commandPreamble { get; set; }
>>>>>>> eaa8693c2cc7a5f76c4d1b53aebfb2bd79698caf
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
<<<<<<< HEAD
            encoder = null;
            init();
            if (encoder != null)
=======
            int fw_ver = discovery();
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
>>>>>>> eaa8693c2cc7a5f76c4d1b53aebfb2bd79698caf
            {
                this.textReader = new StreamReader(serialPort.BaseStream, serialPort.Encoding);
                Object[] r = {"0"};
                write("RTMPROFILE", r);
                write("START");
                Thread.Sleep(500);
                if (serialPort.BytesToRead > 0)
                {
                    dispatcher.enqueueEvent(new RealTimeEvents.NoteReceiver(this, "Read: " + serialPort.ReadExisting()));
                }
                run();
                dispatcher.enqueueEvent(new RealTimeEvents.NewReceiver(this));
            }
            else
            {
                ReceiverExceptions re = new ReceiverExceptions(this, "(receiver fatal) Failed to configure encoder during init().", true);
                dispatcher.enqueueEvent(new RealTimeEvents.ExcepReceiver(re));
                serialPort.Close();
                throw re;
            }
<<<<<<< HEAD
        }

        public void init()
=======
            run();

            dispatcher.enqueueEvent(new RealTimeEvents.NewReceiver(this));
        }

        public int discovery()
>>>>>>> eaa8693c2cc7a5f76c4d1b53aebfb2bd79698caf
        {
            Dictionary<Int32, List<dynamic>> discoveryMethods = new Dictionary<Int32, List<dynamic>>();
            Dictionary<Int32, String> infoMethods = new Dictionary<Int32, String>();
            String discoveryReturns = "";
            

            var jsonParser = new JsonParser() { CamelizeProperties = false };

<<<<<<< HEAD
            foreach (string filename in System.IO.Directory.GetFiles(VR2C_COMMAND_FOLDER))
            {

                
                dynamic config = jsonParser.Parse(System.IO.File.ReadAllText(filename));
                var dc = config.discovery_commands;
                var fwver = config.firmware_version;
                //try
                //{
                //    var infoa = config.encoder;
                //    infoMethods.Add(fwver, (string)config.encoder.INFO);
                //}
                //finally
                //{
                //}

                try
                {
                    discoveryMethods.Add(((int)fwver), dc);
                }
                catch (Exception e)
                {

                }
                

            }



            List<dynamic> default_discovery = new List<dynamic>();
            default_discovery.Add("*BROADC.A#ST,QUIT");
            default_discovery.Add("*BROADC.A#ST,DISCOVERY");
            default_discovery.Add("*DISCOV.E#RY,DISCOVERY");
            discoveryMethods.Add(-1, default_discovery);

            int discovery_attempts = 0;
            serialPort.ReadExisting();
            while (serialPort.BytesToRead <= 0 && discovery_attempts < 5)
            {
                write_wait = (discovery_attempts + 1) * 100;
                foreach (List<dynamic> l in discoveryMethods.Values)
                {
                    foreach(var command in l) {
                        _write(command);
                    }
                }
                discovery_attempts++;
            }
            if (discovery_attempts >= 5)
            {
                ReceiverExceptions re = new ReceiverExceptions(this, "(receiver note) Not able to discover VEMCO receiver attached on this port.", true);
                dispatcher.enqueueEvent(new RealTimeEvents.ExcepReceiver(re));
                serialPort.Close();
                throw re;
            }

=======
            String discoveryReturns = "";

            //serialPort.Write(crlf, 0, 2);
            //serialPort.Write(crlf, 0, 2);
 //           serialPort.Write("....................");
            //serialPort.Write("*BROADC.A#ST,QUIT");
            //serialPort.Write("*BROADC.A#ST,DISCOVERY");
            
            //serialPort.Write("*DISCOV.E#RY,DISCOVERY");
 //           serialPort.Write("*DISCOV.E#RY,DISCOVERY");
 //           serialPort.Write("*DISCOV.E#RY,DISCOVERY");
 //           serialPort.Write("*450052.0#16,INFO");
//            serialPort.Write(crlf, 0, 2);
 
            //serialPort.Write("\r");
            //Thread.Sleep(100);
            //serialPort.Write("\n");
            //Thread.Sleep(100);
            while (serialPort.BytesToRead <= 0)
            {
                serialPort.Write(crlf, 0, 2);
                Thread.Sleep(100);
                serialPort.Write("*BROADC.A#ST,QUIT");
                serialPort.Write(crlf, 0, 2);
                Thread.Sleep(100);
                serialPort.Write("*BROADC.A#ST,DISCOVERY");
                serialPort.Write(crlf, 0, 2);
                Thread.Sleep(100);
                serialPort.Write("*DISCOV.E#RY,DISCOVERY");
                serialPort.Write(crlf, 0, 2);
                Thread.Sleep(200);
            }
>>>>>>> eaa8693c2cc7a5f76c4d1b53aebfb2bd79698caf
            while (serialPort.BytesToRead > 0)
            {
                discoveryReturns = serialPort.ReadExisting();
            }
            
<<<<<<< HEAD

            string commandPreamble = discoveryReturns.Substring(0, 12) + ",";
            dispatcher.enqueueEvent(new RealTimeEvents.NoteReceiver(this, "(receiver note) command preamble: " + discoveryReturns.Substring(0,12) + ","));
            dispatcher.enqueueEvent(new RealTimeEvents.NoteReceiver(this, "(receiver note) read: " + discoveryReturns));

            infoMethods.Add(-1, commandPreamble + "INFO");
            int info_attempts = 0;
            serialPort.ReadExisting();
            Boolean gotINFO = false;
            String infoReturns = "";
            int read_attempts = 0;

            while (!gotINFO && read_attempts < 5)
            {
                info_attempts = 0;
                serialPort.ReadExisting();
                while (serialPort.BytesToRead <= 0 && info_attempts < 5)
                {
                    foreach (String infoc in infoMethods.Values)
                    {
                        dispatcher.enqueueEvent(new RealTimeEvents.NoteReceiver(this, "(receiver note) attempting INFO command with " + infoc));
                        _write(infoc);
                        Thread.Sleep(500);
                    }
                    info_attempts++;
                }

                while (serialPort.BytesToRead > 0)
                {
                    infoReturns = serialPort.ReadExisting();

                    foreach (string filename in System.IO.Directory.GetFiles(VR2C_COMMAND_FOLDER))
                    {
                        List<String> wordExpansions = new List<String>();
                        dynamic config = jsonParser.Parse(System.IO.File.ReadAllText(filename));
                        foreach (Object o in config.decoder.sentences["info_response"].word_order)
                        {
                            wordExpansions.Add(config.decoder.words[((String)o)]);
                        }
                        if (Regex.IsMatch(infoReturns, String.Format(config.decoder.sentences["info_response"].format, wordExpansions.ToArray<String>())))
                        {
                            gotINFO = true;
                        }

                    }
                }
                read_attempts++;
            }
            if (read_attempts >= 5)
            {
                ReceiverExceptions re = new ReceiverExceptions(this, "(receiver fatal) Not able to get INFO from the VEMCO receiver attached on this port.", true);
                dispatcher.enqueueEvent(new RealTimeEvents.ExcepReceiver(re));
                serialPort.Close();
                throw re;
            }

            int RECEIVER_FW_VERSION = -1;
            if (infoReturns != "")
            {

                int fw_start = infoReturns.IndexOf("FW=");
                int fw_end = infoReturns.IndexOf(",", fw_start);
                string fw_ver = infoReturns.Substring((fw_start + 3), (fw_end - fw_start - 3));
                int fw_ver_firstperiod = fw_ver.IndexOf(".");
                int fw_ver_secondperiod = fw_ver.IndexOf(".", fw_ver_firstperiod + 1);
                string major = fw_ver.Substring(0, fw_ver_firstperiod);
                string minor = fw_ver.Substring(fw_ver_firstperiod + 1, (fw_ver_secondperiod - fw_ver_firstperiod - 1));
                string release = fw_ver.Substring(fw_ver_secondperiod + 1, (fw_ver.Length - fw_ver_secondperiod - 1));
                RECEIVER_FW_VERSION = (Int32.Parse(major) * 10000) + (Int32.Parse(minor) * 100) + (Int32.Parse(release));
            }
            dispatcher.enqueueEvent(new RealTimeEvents.NoteReceiver(this,"(receiver note) detected firmware version: " + RECEIVER_FW_VERSION));
            if (RECEIVER_FW_VERSION >= 0)
            {
                int fw_use = -1;

                foreach (string filename in System.IO.Directory.GetFiles(VR2C_COMMAND_FOLDER))
                {

                    string text = System.IO.File.ReadAllText(filename);
                    dynamic config = jsonParser.Parse(System.IO.File.ReadAllText(filename));

                    if (config.firmware_version >= fw_use && config.firmware_version <= RECEIVER_FW_VERSION)
                    {
                        fw_use = (Int32)config.firmware_version;
                        
                        encoder = new Encoder(commandPreamble, config);
                    }
                }
                if (fw_use < 0 || encoder == null)
                {
                    ReceiverExceptions re = new ReceiverExceptions(this, "(receiver fatal) Unable to parse out FW version from return from INFO command.", true);
                    dispatcher.enqueueEvent(new RealTimeEvents.ExcepReceiver(re));
                    serialPort.Close();
                    throw re;
                }
            }
            else
            {
                ReceiverExceptions re = new ReceiverExceptions(this, "(receiver fatal) Unable to parse out FW version from return from INFO command.", true);
                dispatcher.enqueueEvent(new RealTimeEvents.ExcepReceiver(re));
                serialPort.Close();
                throw re;
            }
            dispatcher.enqueueEvent(new RealTimeEvents.NoteReceiver(this, "(receiver note) Successfully configured encoder with fw version = " + encoder.encoderConfig.firmware_version));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void _write(string text)
        {
            serialPort.Write(crlf, 0, 2);
            Thread.Sleep(100);
            serialPort.Write(text);
            serialPort.Write(crlf, 0, 2);
            Thread.Sleep(100);
            
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void write(string command, object[] arguments)
        {
            _write(encoder.build(command, arguments));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void write(string command)
        {
            _write(encoder.build(command));
=======
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
>>>>>>> eaa8693c2cc7a5f76c4d1b53aebfb2bd79698caf
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
<<<<<<< HEAD
                    dispatcher.enqueueEvent(new RealTimeEvents.UnparsedMessage(this, ret, encoder.encoderConfig));
=======
                    dispatcher.enqueueEvent(new RealTimeEvents.UnparsedMessage(this, ret));
>>>>>>> eaa8693c2cc7a5f76c4d1b53aebfb2bd79698caf
                }
            }
            goState = -1;
        }

    }
}
