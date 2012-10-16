using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using FridayThe13th;
using System.IO;

namespace ReceiverMultiplex
{
	class Receiver
	{
        public string portName { get; private set; }
        public int TTL { get; private set; }

        private const int DEFAULT_TTL = 10;
		private const int COM_READ_TIMEOUT_DEFAULT = 500; //milliseconds
		private const int COM_READ_TIMEOUT_SPRIAL = 100; //additional ms to allow for response on next go-'round
		private const string VR2C_COMMAND_FOLDER = "vr2c_commands";

		private RealTimeEventDispatcher dispatcher { get; set; }
		private SerialPort serialPort {  get; set; }
		private dynamic receiverConfig = null;
		private int firmwareVersion {get; set;}
		private string commandPreamble { get; set; }
        private TextReader textReader { get; set; }

		public Receiver(SerialPort serialPort, String portName, RealTimeEventDispatcher dispatcher)
		{
			Dictionary<int, string> configFiles = new Dictionary<int, string>();
            this.TTL = DEFAULT_TTL;
			this.serialPort = serialPort;
			this.portName = portName;
			this.dispatcher = dispatcher;

			int fw_ver = INFO();
			if (fw_ver < 0)
			{
				serialPort.Close();
				throw new HardwareExceptions("INFO command failed to return firmware version.", true);
			}

			int fw_use = -1;
            var jsonParser = new JsonParser() { CamelizeProperties = false };

            foreach (string filename in System.IO.Directory.GetFiles(VR2C_COMMAND_FOLDER))
            {

                string text = System.IO.File.ReadAllText(filename);
                dynamic config = jsonParser.Parse(System.IO.File.ReadAllText(filename));

                if (config.FwVersion >= fw_use && config.FwVersion <= fw_ver)
                {
                    fw_use = (Int32)config.FwVersion;
                    receiverConfig = config;
                }
            }
			if(fw_use < 0 || receiverConfig == null) 
			{
				serialPort.Close();
				throw new HardwareExceptions("Valid json config not found for receiver firmware.",true);
			}

            this.textReader = new StreamReader(serialPort.BaseStream, serialPort.Encoding);

            readHandler();

            dispatcher.enque(new RealTimeEvents.SerialPortEvent(RealTimeEventType.NEW_RECEIVER, this));
        }
		
		public int INFO()
		{
			serialPort.Write(commandPreamble + "INFO" + "\n");
            string infoReturns = serialPort.ReadLine();
            if (infoReturns != "")
            {
                dispatcher.enque(new RealTimeEvents.UnparsedDataEvent(
                            infoReturns, this));
                int fw_start = infoReturns.IndexOf("FW=");
                int fw_end = infoReturns.IndexOf(",", fw_start);
                string fw_ver = infoReturns.Substring((fw_start + 3), (fw_end - fw_start - 3));
                int fw_ver_firstperiod = fw_ver.IndexOf(".");
                int fw_ver_secondperiod = fw_ver.IndexOf(".", fw_ver_firstperiod + 1);
                string major = fw_ver.Substring(0, fw_ver_firstperiod);
                string minor = fw_ver.Substring(fw_ver_firstperiod + 1, (fw_ver_secondperiod - fw_ver_firstperiod - 1));
                string release = fw_ver.Substring(fw_ver_secondperiod + 1, (fw_ver.Length - fw_ver_secondperiod - 1));
                return (Int32.Parse(major) * 10000) + (Int32.Parse(minor) * 100) + (Int32.Parse(release));
            }
            else
            {
                return -1;
            }
			
		}

		public async Task readHandler()
		{
            while (true)
            {
                var ret = string.Empty;
                var buffer = new char[1]; // Not the most efficient...
                while (!ret.Contains('\n'))
                {
                    var charsRead = await textReader.ReadAsync(buffer, 0, 1);
                    if (charsRead == 0)
                    {
                        throw new EndOfStreamException();
                    }
                    ret += charsRead;
                }
                try
                {
                    dispatcher.enque(new RealTimeEvents.UnparsedDataEvent(ret, this));
                }
                catch (MalformedData md)
                {
                    TTL--;
                }
            }
        }
	}
}
