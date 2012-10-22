using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SerialPortSlice;
using System.IO;

namespace ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            SerialPortService s = SerialPortService.getServicer();
            s.run();
            Thread.Sleep(10000);
            s.stop();
        }
    }
}
