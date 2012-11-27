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

namespace ServiceController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            SerialPortSlice.SerialPortService s = SerialPortSlice.SerialPortService.getServicer();
            InitializeComponent();
            Binding bindingReceiverList = new Binding();
            System.Collections.ObjectModel.ObservableCollection<ReceiverSlice.Receiver> ocReceivers =
                new System.Collections.ObjectModel.ObservableCollection<ReceiverSlice.Receiver>(s.receivers.Values.ToList<ReceiverSlice.Receiver>());
            this.receiversListBox.ItemsSource = ocReceivers;
            s.run();
        }
    }
}
