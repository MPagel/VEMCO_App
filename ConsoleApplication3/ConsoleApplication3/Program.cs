using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPort mySerialPort = new SerialPort("COM4");

            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            mySerialPort.Open();

            Console.WriteLine("Press any key to continue...");
            Console.WriteLine();
            while (true) ;
            //mySerialPort.Close();
        }

        private static void DataReceivedHandler(
                            object sender,
                            SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //string indata = sp.ReadExisting();
            while (sp.BytesToRead > 0)
            {

                Console.Write(sp.ReadExisting());
                //Console.WriteLine("Data Received:");
                //Console.Write(indata);
            }
        }
    }
}

