using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FridayThe13th;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)

        {
            dynamic receiverConfig;
            int fw_ver = 25;
            string VR2C_COMMAND_FOLDER = "vr2c_commands";
            int fw_use = -1;
            var jsonParser = new JsonParser() { CamelizeProperties = true };
            Console.Write(System.IO.Directory.GetCurrentDirectory());
            foreach (string filename in System.IO.Directory.GetFiles(VR2C_COMMAND_FOLDER))
            {
                dynamic config = jsonParser.Parse(System.IO.File.ReadAllText(filename));
                if (config.FwVersion >= fw_use && config.FwVersion <= fw_ver)
                {
                    fw_use = config.FwVersion;
                    receiverConfig = config;
                }
            }
            Console.Write("\nDone.");
            Console.ReadKey();
        }
    }
}
