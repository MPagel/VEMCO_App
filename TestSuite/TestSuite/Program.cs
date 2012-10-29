using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FridayThe13th;
using ReceiverSlice;
using System.Diagnostics;
namespace TestSuite
{
    class Program
    {
        static void Main(string[] args)
        {
            //Encoder tests
            Console.WriteLine("Encoder tests");
            string text = System.IO.File.ReadAllText("config/0.txt");
            var jsonParser = new JsonParser() { CamelizeProperties = false };
            dynamic config = jsonParser.Parse(System.IO.File.ReadAllText("config\\0.txt"));
            ReceiverSlice.Encoder enc = new ReceiverSlice.Encoder("111111.2#33", config);
            Console.Write("Build INFO command: ");
            
        }
    }
}
