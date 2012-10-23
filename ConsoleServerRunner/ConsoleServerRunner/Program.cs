using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerialPortSlice;
using System.Threading;

namespace ConsoleServerRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPortService s = SerialPortService.getServicer();
            s.run();
            while (true) ;
            s.stop();
            Console.ReadLine();

        }
    }
}
