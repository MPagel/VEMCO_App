using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SerialPortSlice;
namespace ServiceControl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SerialPortService s = SerialPortService.getServicer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RRCS(s.receivers.Values.ToList<ReceiverSlice.Receiver>()));
        }
    }
}
