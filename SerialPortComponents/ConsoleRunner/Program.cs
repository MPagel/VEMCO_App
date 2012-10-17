using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SerialPortSlice;
namespace ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPortService s = SerialPortService.getServicer();
            s.run();
            Thread.Sleep(60000);
            s.stop();
        }
    }
}
