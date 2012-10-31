﻿using System;
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
    /// <summary>
    /// This class encompasses the functionality required to connect to and communicate with a VR2C
    /// receiver connected via serial port.
    /// </summary>
    public class Receiver
    {
        /// <summary>
        /// Human readable name for the port to which the Receiver is attached (i.e. COM1)
        /// </summary>
        public string portName { get; private set; }
        /// <summary>
        /// Time-To-Live is a relative measure of the number of serious errors this object
        /// has received in its lifetime.  When TTL = 0, the serial port should be closed 
        /// and this object removed from the runtime.  
        /// </summary>
        public int TTL { get; private set; }


        private const int DEFAULT_TTL = 10;
        private const int COM_READ_TIMEOUT_DEFAULT = 500; //milliseconds
        private const int COM_READ_TIMEOUT_SPRIAL = 100; //additional ms to allow for response on next go-'round
        private const string VR2C_COMMAND_FOLDER = "config";
        private static char[] crlf = new char[2] { '\x0D', '\x0A' };

        private Encoder encoder { get; set; }
        private Dispatcher dispatcher { get; set; }
        private SerialPort serialPort { get; set; }
        private int firmwareVersion { get; set; }
        private TextReader textReader { get; set; }
        private int goState = 1;
        private int write_wait = 100;

        /// <summary>
        /// Public constructor for the Receiver class.
        /// </summary>
        /// <param name="serialPort">The serial port object to which the receiver is connected</param>
        /// <param name="portName">The name of the port (i.e. COM1, COM2, etc.)</param>
        /// <param name="dispatcher">The event queue dispatcher where events generated by this class are sent</param>
        /// <remarks>
        /// After opening the port, the constructor calls the init() method which determines whether the hardware connect
        /// is, in fact, a VEMCO reciever and then proceeds to configure it.  After returning, the constructor then instructs
        /// the receiver to start sending "Real Time" data.  Finally the run() method is called and the objects stays in
        /// the run method until it shutdown() is called.  With the exception of whether the class in run()ing or not, no state
        /// is maintained by the class.
        /// </remarks>
        public Receiver(SerialPort serialPort, String portName, Dispatcher dispatcher)
        {
            Dictionary<int, string> configFiles = new Dictionary<int, string>();
            this.TTL = DEFAULT_TTL;
            this.serialPort = serialPort;
            this.portName = portName;
            this.dispatcher = dispatcher;

            serialPort.Open();
            encoder = null;
            init();
            if (encoder != null)
            {
                
                this.textReader = new StreamReader(serialPort.BaseStream, serialPort.Encoding);
                Object[] r = {"0"};
                write("RTMPROFILE", r);
                write("START");
                Thread.Sleep(500);
                while (serialPort.BytesToRead > 0)
                {
                    dispatcher.enqueueEvent(new RealTimeEvents.NoteReceiver(this, "(receiver note) Read: " + serialPort.ReadExisting()));
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
        }
        /// <summary>
        /// Called by the constructor to determine whether a VEMCO receiver is attached (vs another kind of 
        /// serial device), what the firmware is and which configuration file should be used.
        /// </summary>
        /// <remarks>
        /// Because one of the goals of this method is to determine the firmware (and corresponding configuration file),
        /// for the most part we must try both "default" commands and all those available in the configuration files.
        /// One assumption here is that future changes to protocol's INFO and discovery methods will not interfere with
        /// the operation of prior receivers.  And although we can take some care to provide as much flexibility for
        /// changes in the protocol, there are clearly some limits.  For example, we cannot anticipate a change to
        /// a binary format.
        /// 
        /// The init() method first attempts to "discover" the connected receiver.  Generally a receiver will not respond
        /// to commands that are not prefaced by the serial number, etc.  Since we do not have the serial number prior to 
        /// running this method, we must use the VEMCO's broadcast and discover commands.
        /// 
        /// Next the method issues INFO commands in order to scrape out the firmware version.  We assume here that the 
        /// manufacturer's protocol will remain stable within a firmware version.  Also note that so long as the protocol
        /// itself does not change, there is no need for additional configuration files.  
        /// 
        /// Assuming that these two tasks are completed without bailing, then init() command completes and returns
        /// control to the constructor.
        /// </remarks>
        /// <exception cref="ReceiverExceptions">Thrown when either the discovery
        /// or info phase is not able to acquire the needed information</exception>
        public void init()
        {
            Dictionary<Int32, List<dynamic>> discoveryMethods = new Dictionary<Int32, List<dynamic>>();
            Dictionary<Int32, String> infoMethods = new Dictionary<Int32, String>();
            String discoveryReturns = "";
            

            var jsonParser = new JsonParser() { CamelizeProperties = false };

            foreach (string filename in System.IO.Directory.GetFiles(VR2C_COMMAND_FOLDER))
            {

                
                dynamic config = jsonParser.Parse(System.IO.File.ReadAllText(filename));
                var dc = config.discovery_commands;
                var fwver = config.firmware_version;
                try
                {
                    infoMethods.Add(fwver, (string)config.encoder["INFO"]);
                }
                catch(Exception e)
                {
                }

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

            while (serialPort.BytesToRead > 0)
            {
                discoveryReturns = serialPort.ReadExisting();
            }
            

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

        /// <summary>
        /// This is a private method that sends raw text to the serial port.  The public methods are preferable.
        /// </summary>
        /// <param name="text">Raw text to be sent to the serial port</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void _write(string text)
        {
            serialPort.Write(crlf, 0, 2);
            Thread.Sleep(100);
            serialPort.Write(text);
            serialPort.Write(crlf, 0, 2);
            Thread.Sleep(100);
            
        }

        /// <summary>
        /// Formats and sends a command to the receiver hardware.
        /// </summary>
        /// <param name="command">A command corresponding to an entry in the encoder section of the config file</param>
        /// <param name="arguments">An array containing any arguments to the command.</param>
        /// <remarks>Use write(String command) instead if you have no arguments.</remarks>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void write(string command, object[] arguments)
        {
            _write(encoder.build(command, arguments));
        }

        /// <summary>
        /// Formats and sends a command to the receiver hardware.
        /// </summary>
        /// <param name="command">A command corresponding to an entry in the encoder section of the config file</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void write(string command)
        {
            _write(encoder.build(command));
        }

        /// <summary>
        /// Shuts down the receiver as cleanly as possible.
        /// </summary>
        /// <remarks>
        /// The method first switches the state of the receiver to shutdown (goState = 0).  If there is data
        /// that happens to show up within 2.5 seconds of this command being execute, the read is completed and
        /// the corresponding event, if any, is sent to the dispatcher.  If the read completes before, then the 
        /// serial port is shutdown early.  At the end of 2.5 seconds, regardless of whether a read is being performed,
        /// the serial port is shutdown.
        /// </remarks>
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

        
        /// <summary>
        /// Objects of this class spend most of their execution time in this method.  It simply waits asynchronously (control
        /// is returned to the constructor and then returned here on an interrupt) for data.  When data is read it is packaged
        /// and sent to the event dispatcher.  
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ReceiverExceptions">Thrown when the EOF is reached on the read stream (contains an 
        /// EndOfStreamException</exception>
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
                        EndOfStreamException eose = new EndOfStreamException();
                        ReceiverExceptions re = new ReceiverExceptions(this,"End of stream reached on serial port.",true,eose);
                        dispatcher.enqueueEvent(new RealTimeEvents.ExcepReceiver(re));
                        throw re;
                    }
                    ret += buffer[0];
                }

                if (ret.Length > 1)
                {
                    dispatcher.enqueueEvent(new RealTimeEvents.UnparsedMessage(this, ret, encoder.encoderConfig));
                }
            }
            goState = -1;
        }

    }
}