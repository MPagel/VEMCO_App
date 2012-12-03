using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.Concurrent;
using System.Threading;

namespace ServiceController2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, EventSlice.Interfaces.i_Module
    {
        private static SerialPortSlice.SerialPortService s;
        private Thread serviceThread = null;
        private ConcurrentDictionary<SerialPortSlice.RealTimeEvents.ServerException, DateTime> serialPortServiceErrorLog = new ConcurrentDictionary<SerialPortSlice.RealTimeEvents.ServerException, DateTime>();
        private const int ERROR_TOLERANCE_PER_PERIOD_DEFAULT = 10;
        private const int ERROR_TOLERANCE_PERIOD_SECONDS_DEFAULT = 600;
        private int error_tolerance_per_period = ERROR_TOLERANCE_PER_PERIOD_DEFAULT;
        private int error_tolerance_period_seconds = ERROR_TOLERANCE_PERIOD_SECONDS_DEFAULT;

        public MainWindow()
        {
            serviceThread = new Thread(new ThreadStart(this.service));
            serviceThread.Start();

            InitializeComponent();
            
            this.receiverRunState.ItemsSource = Enum.GetValues(typeof(ReceiverSlice.RunState)).Cast<ReceiverSlice.RunState>();
        }

        public string getModuleName() { return "User Interface Module"; }

        public void onRealTimeEvent(EventSlice.Interfaces.RealTimeEvent rte)
        {
            if (rte.GetType() == typeof(ReceiverSlice.RealTimeEvents.NewReceiver))
            {
                receiversListBox.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                    {
                        ReceiverSlice.RealTimeEvents.NewReceiver nr = ((ReceiverSlice.RealTimeEvents.NewReceiver)rte);
                        receiversListBox.ScrollIntoView(nr["receiver"]);
                    }));
            }


            if (rte.GetType() == typeof(SerialPortSlice.RealTimeEvents.ServerException))
            {
                SerialPortSlice.RealTimeEvents.ServerException se = ((SerialPortSlice.RealTimeEvents.ServerException)rte);
                serialPortServiceErrorLog[se] = DateTime.Now;
            }


            if (this.statusText.Dispatcher.CheckAccess())
            {
                statusText.Text = rte.ToString();
            }
            else
            {
                statusText.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    statusText.Text = rte.ToString();
                }));
            }
        }

        private void service()
        {
            while (true)
            {
                s = SerialPortSlice.SerialPortService.getServicer();
                receiversListBox.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                    {
                        this.receiversListBox.ItemsSource = s.receivers;
                    }));
                s.dispatcher.addModule(this);
                s.run();

                serialPortServiceErrorLog.Clear();
                do
                {
                    Thread.Sleep(3000);
                    foreach (SerialPortSlice.RealTimeEvents.ServerException se in serialPortServiceErrorLog.Keys.ToList<SerialPortSlice.RealTimeEvents.ServerException>())
                    {
                        TimeSpan ts = DateTime.Now - serialPortServiceErrorLog[se];
                        if (ts.Seconds >= error_tolerance_period_seconds)
                        {
                            DateTime xxx;
                            serialPortServiceErrorLog.TryRemove(se, out xxx);
                        }
                    }
                } while (serialPortServiceErrorLog.Count() <= error_tolerance_per_period);

                s.stop();
                Thread.Sleep(15000);
            }
        }
    }
}
