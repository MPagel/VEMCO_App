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
		private RealTimeEventDispatcher dispatcher { private get; private set; }
		private SerialPort serialPort { private get; private set; }
		private dynamic receiverConfig { get; private set; }
		private int firmwareVersion {get; private set;}
		private string commandPreamble { get; private set; }
        private TextReader textReader { get; private set; }

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
			var jsonParser = new JsonParser(){CamelizeProperties = true};
			foreach(string filename in System.IO.Directory.GetFiles(VR2C_COMMAND_FOLDER))
			{
				dynamic config = jsonParser.Parse(System.IO.File.ReadAllText(filename));
				if (config.FwVersion >= fw_use && config.FwVersion <= fw_ver)
				{
					fw_use = config.FwVersion;
					receiverConfig = config;
				}
			}
			if(fw_use < 0) 
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
			dispatcher.enque(new RealTimeEvents.UnparsedDataEvent(
						serialPort.ReadLine(), this));
			// ^- also parse/process INFO and return an integer value representing firmware
			return 0;
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
