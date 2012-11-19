using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FridayThe13th;

namespace Sandbox
{
    class Sandbox
    {
        static List<Decoded> insertions = new List<Decoded>();

        static void Main(string[] args)
        {
            var jsonParser = new JsonParser() { CamelizeProperties = false };
            dynamic config = jsonParser.Parse(System.IO.File.ReadAllText("config.txt"));
            Dispatcher dispatcher = new Dispatcher();
            Database database = new Database(dispatcher, config);
            Decoder decoder = new Decoder(dispatcher);
            Receiver receiver = new Receiver("450052", "VR2C-69", dispatcher);
            Encoder encoder = new Encoder("*450052.0#16,", config);
            ConsoleLogger consoleLogger = new ConsoleLogger(dispatcher);
            dispatcher.addModule(database);
            dispatcher.addModule(decoder);
            dispatcher.addModule(consoleLogger);
            dispatcher.run();

            testUnparsedMessages(dispatcher, receiver, config);
            //testEncoder(encoder);
            //testDecoder(decoder);
            //testDatabase(database);
        }

        static void testUnparsedMessages(Dispatcher dispatcher, Receiver receiver, dynamic config)
        {
            dispatcher.enqueueEvent(new UnparsedMessage("*450052.0#16[0099],2012-10-02 21:14:45,STS,DC=0,PC=0,LV=0.0,BV=3.2,BU=3.6,I=5.3,T=23.9,DU=0.0,RU=0.0,STORAGE,OK,#E3", receiver, null, "450052", "VR2C-69", config));
            dispatcher.enqueueEvent(new UnparsedMessage("*450052.0#16[0014],INVALID,#07", receiver, null, "450052", "VR2C-69", config));
            dispatcher.enqueueEvent(new UnparsedMessage("*450052.0#16[0009],OK,#9A", receiver, null, "450052", "VR2C-69", config));
            dispatcher.enqueueEvent(new UnparsedMessage("450052,000,2012-10-02 21:19:19,STS,DC=0,PC=0,LV=0.0,BV=3.2,BU=3.6,I=2.7,T=23.7,DU=0.0,RU=0.0,XYZ=-0.06:-0.22:0.94,#8C", receiver, null, "450052", "VR2C-69", config));
            dispatcher.enqueueEvent(new UnparsedMessage("450052,001,2012-10-02 21:20:01,A69-9001,30444,#B3", receiver, null, "450052", "VR2C-69", config));
            dispatcher.enqueueEvent(new UnparsedMessage("450052,032,2012-10-02 21:40:42,A69-1303,48823,#C5", receiver, null, "450052", "VR2C-69", config));
            dispatcher.enqueueEvent(new UnparsedMessage("450052,039,2012-10-02 21:42:17,A69-9001,30,444,#CA", receiver, null, "450052", "VR2C-69", config));
            dispatcher.enqueueEvent(new UnparsedMessage("*450052.0#16[0099],VR2C-69:450052,'VEMCO',MAP-113 [ 1105 1303 9001/9002 1420 1430 1601 1602 ],FW=0.0.25,HW=3,OK,#57", receiver, null, "450052", "VR2C-69", config));
            dispatcher.enqueueEvent(new UnparsedMessage("*450052.0#16[0125],2012-10-02 21:47:06,STS,DC=108,PC=1199,LV=0.0,BV=3.2,BU=3.6,I=5.3,T=23.1,DU=0.0,RU=0.1,XYZ=-0.06:-0.22:0.94,STOPPED,OK,#89", receiver, null, "450052", "VR2C-69", config));
            dispatcher.enqueueEvent(new UnparsedMessage("*450052.0#16[0053],232,SI=60,BL=U,BI=1,MA=U,FMT=SER SEQ UTC CS,OK,#8A", receiver, null, "450052", "VR2C-69", config));
            dispatcher.enqueueEvent(new UnparsedMessage("this is some garbage", receiver, null, "450052", "VR2C-69", config));
        }

        static void testEncoder(Encoder encoder)
        {
            Console.WriteLine("-----------------Encoder Test-----------------");
            Console.WriteLine("INFO:{0}INFO, INFO -> " + encoder.build("INFO"));
            Console.WriteLine("INFO:{0}INFO, INFO -> " + encoder.build("INFO"));
            Console.WriteLine("START:{0}START, START -> " + encoder.build("START"));
            Console.WriteLine("STOP:{0}STOP, STOP -> " + encoder.build("STOP"));
            Console.WriteLine("ERASE:{0}ERASE, ERASE -> " + encoder.build("ERASE"));
            Console.WriteLine("RTMINFO:{0}RTMINFO, RTMINFO -> " + encoder.build("RTMINFO"));
            Console.WriteLine("RTMOFF:{0}RTMOFF, RTMOFF -> " + encoder.build("RTMOFF"));
            Console.WriteLine("RTM232:{0}RTM232, RTM232 -> " + encoder.build("RTM232"));
            Console.WriteLine("RTM485:{0}RTM485, RTM485 -> " + encoder.build("RTM485"));
            //Console.WriteLine("RTMPROFILE:{0}RTMPROFILE={1}, RTMPROFILE -> " + encoder.build("RTMPROFILE"));
            Console.WriteLine("RTMPROFILE:{0}RTMPROFILE={1}, RTMPROFILE(1) -> " + encoder.build("RTMPROFILE", new Object[1]{1}));
            //Console.WriteLine("RTMAUTOERASE:{0}RTMAUTOERASE={1}, RTMAUTOERASE -> " + encoder.build("RTMAUTOERASE"));
            Console.WriteLine("RTMAUTOERASE:{0}RTMAUTOERASE={1}, RTMAUTOERASE(1) -> " + encoder.build("RTMAUTOERASE", new Object[1]{1}));
            Console.WriteLine("STORAGE:{0}STORAGE, STORAGE -> " + encoder.build("STORAGE"));
            //Console.WriteLine("TIME:{0}TIME={1}-{2}-{3} {4}:{5}:{6} {7}, TIME -> " + encoder.build("TIME"));
            Console.WriteLine("TIME:{0}TIME={1}-{2}-{3} {4}:{5}:{6} {7}, TIME(2012-11-24 14:45:22 -> " + encoder.build("TIME", new Object[7]{"2012", "11", "24", "14", "45", "22", "+0000"}));
            Console.WriteLine("RESET:{0}RESET, RESET -> " + encoder.build("RESET"));
            Console.WriteLine("QUIT:{0}QUIT, QUIT -> " + encoder.build("QUIT"));
            Console.WriteLine("RESETBATTERY:{0}RESETBATTERY, RESETBATTERY -> " + encoder.build("RESETBATTERY"));
        }

        static void testDecoder(Decoder decoder)
        {
            var jsonParser = new JsonParser() { CamelizeProperties = false };
            dynamic config = jsonParser.Parse(System.IO.File.ReadAllText("config.txt"));
            Console.WriteLine("-----------------Decoder Test-----------------");
            //printDecodedMessage(decoder.Decode(new UnparsedMessage("*450052.0#16[0099],2012-10-02 21:14:45,STS,DC=0,PC=0,LV=0.0,BV=3.2,BU=3.6,I=5.3,T=23.9,DU=0.0,RU=0.0,STORAGE,OK,#E3", null, null, null, null, config)));
            //printDecodedMessage(decoder.Decode(new UnparsedMessage("*450052.0#16[0014],INVALID,#07", null, null, null, null, config)));
            //printDecodedMessage(decoder.Decode(new UnparsedMessage("*450052.0#16[0009],OK,#9A", null, null, null, null, config)));
            //printDecodedMessage(decoder.Decode(new UnparsedMessage("450052,000,2012-10-02 21:19:19,STS,DC=0,PC=0,LV=0.0,BV=3.2,BU=3.6,I=2.7,T=23.7,DU=0.0,RU=0.0,XYZ=-0.06:-0.22:0.94,#8C", null, null, null, null, config)));
            //printDecodedMessage(decoder.Decode(new UnparsedMessage("450052,001,2012-10-02 21:20:01,A69-9001,30444,#B3", null, null, null, null, config)));
            //printDecodedMessage(decoder.Decode(new UnparsedMessage("450052,032,2012-10-02 21:40:42,A69-1303,48823,#C5", null, null, null, null, config)));
            //printDecodedMessage(decoder.Decode(new UnparsedMessage("450052,039,2012-10-02 21:42:17,A69-9001,30,444,#CA", null, null, null, null, config)));
            //printDecodedMessage(decoder.Decode(new UnparsedMessage("*450052.0#16[0099],VR2C-69:450052,'VEMCO',MAP-113 [ 1105 1303 9001/9002 1420 1430 1601 1602 ],FW=0.0.25,HW=3,OK,#57", null, null, null, null, config)));
            //printDecodedMessage(decoder.Decode(new UnparsedMessage("*450052.0#16[0125],2012-10-02 21:47:06,STS,DC=108,PC=1199,LV=0.0,BV=3.2,BU=3.6,I=5.3,T=23.1,DU=0.0,RU=0.1,XYZ=-0.06:-0.22:0.94,STOPPED,OK,#89", null, null, null, null, config)));
            //printDecodedMessage(decoder.Decode(new UnparsedMessage("*450052.0#16[0053],232,SI=60,BL=U,BI=1,MA=U,FMT=SER SEQ UTC CS,OK,#8A", null, null, null, null, config)));
        }
            static void printDecodedMessage(Decoded message)
            {
                insertions.Add(message);
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine(message["unparsedmessage"]);
                Console.WriteLine("\tType is " + message["messagetype"]);
                foreach (string key in message["decodedmessage"].Keys)
                {
                    Console.WriteLine('\t' + key + " -> " + message["decodedmessage"][key]);
                }
            }

        static void testDatabase(Database database)
        {
            Console.WriteLine("-----------------Database Test----------------");
            foreach (Decoded insert in insertions)
            {
                database.onRealTimeEvent(insert);
            }
        }
    }
}
