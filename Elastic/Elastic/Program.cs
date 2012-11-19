using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elastic
{
    static class Program
    {
        
        static SerialPortSlice.SerialPortService service = SerialPortSlice.SerialPortService.getServicer();
        static sui s = new sui(service.dispatcher);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ElasticaForm());
            
            service.run();
            
            while(true);
        }

        /* helper functions */

        

        
    }
}
