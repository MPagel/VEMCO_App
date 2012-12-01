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

namespace ServiceController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, EventSlice.Interfaces.i_Module
    {
        private static SerialPortSlice.SerialPortService s;
        public static System.Collections.ObjectModel.ObservableCollection<ReceiverSlice.Receiver> receivers { get; private set; }
        public MainWindow()
        {   
            s = SerialPortSlice.SerialPortService.getServicer();
            InitializeComponent();
            this.receiversListBox.ItemsSource = s.receivers;
            s.dispatcher.addModule(this);
            s.run();
        }

        public string getModuleName() { return "Service Controller UI"; }

        public void onRealTimeEvent(EventSlice.Interfaces.RealTimeEvent rte)
        {
            if (rte.GetType() == typeof(ReceiverSlice.RealTimeEvents.RunStateChangedReceiver))
            {
                ReceiverSlice.Receiver receiver = ((ReceiverSlice.RealTimeEvents.RunStateChangedReceiver)rte)["receiver"];
                ReceiverSlice.RunState runstate = ((ReceiverSlice.RealTimeEvents.RunStateChangedReceiver)rte)["runstate"];
                if (receiversListBox.Dispatcher.CheckAccess())
                {
                    onChangeRunModeListBox(receiver, runstate);
                }
                else
                {
                    ConsoleLog.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                    {
                        onChangeRunModeListBox(receiver, runstate);
                    }));
                }

            }
            appendToConsoleLog(rte.ToString());
        }

        public void appendToConsoleLog(string text)
        {
            text += '\n';
            if (ConsoleLog.Dispatcher.CheckAccess())
            {
                ConsoleLog.AppendText(text);
            }
            else
            {
                ConsoleLog.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    ConsoleLog.AppendText(text);
                }));
            }
        }

        private void radioRun_Click(object sender, RoutedEventArgs e)
        {
            changeRunMode(ReceiverSlice.RunState.RUN);
        }

        private void RadioButton_Pause(object sender, RoutedEventArgs e)
        {
            changeRunMode(ReceiverSlice.RunState.PAUSE);
        }

        private void RadioButton_Stop(object sender, RoutedEventArgs e)
        {
            changeRunMode(ReceiverSlice.RunState.STORE);
        }

        private void changeRunMode(ReceiverSlice.RunState r) {
            if (((ReceiverSlice.Receiver)this.receiversListBox.SelectedItem) != null)
            {
                ((ReceiverSlice.Receiver)this.receiversListBox.SelectedItem).changeRunMode(r);
            }
        }

        private void onChangeRunModeListBox(ReceiverSlice.Receiver r, ReceiverSlice.RunState newRunState)
        {
            if((receiversListBox.Items.IndexOf(r) == this.receiversListBox.SelectedIndex))
            {
                ListBoxItem lbi = this.receiversListBox.ItemContainerGenerator.ContainerFromItem(r) as ListBoxItem;
                switch(newRunState)
                {
                    case ReceiverSlice.RunState.RUN:
                        lbi.Background = Brushes.AliceBlue;
                        this.tbRunState.Text = "Run";
                        break;
                    case ReceiverSlice.RunState.PAUSE:
                        lbi.Background = Brushes.PapayaWhip;
                        this.tbRunState.Text = "Pause";
                        break;
                    case ReceiverSlice.RunState.STORE:
                        lbi.Background = Brushes.Pink;
                        this.tbRunState.Text = "Stop";
                        break;
                }
            }
        }

        private void onChangeRunModeRadioButtons(ReceiverSlice.Receiver r, ReceiverSlice.RunState newRunState)
        {
            
            if (receiversListBox.SelectedItem != null && (receiversListBox.Items.IndexOf(r) == this.receiversListBox.SelectedIndex))
            {
                this.radioRun.IsChecked = false;
                this.radioPause.IsChecked = false;
                this.radioStop.IsChecked = false;
                switch (newRunState)
                {
                    case ReceiverSlice.RunState.RUN:
                        radioRun.IsChecked = true;
                        break;
                    case ReceiverSlice.RunState.PAUSE:
                        radioPause.IsChecked = true;
                        break;
                    case ReceiverSlice.RunState.STORE:
                        radioStop.IsChecked = true;
                        break;
                }
            }
        }

        private void receiversListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.radioRun.IsChecked = false;
            this.radioPause.IsChecked = false;
            this.radioStop.IsChecked = false;
            if (this.receiversListBox.SelectedItem != null)
            {
                ReceiverSlice.Receiver receiver = ((ReceiverSlice.Receiver)this.receiversListBox.SelectedItem);
                switch (receiver.runState)
                {
                    case ReceiverSlice.RunState.RUN:
                        this.tbRunState.Text = "Run";
                        radioRun.IsChecked = true;
                        break;
                    case ReceiverSlice.RunState.PAUSE:
                        this.tbRunState.Text = "Pause";
                        radioPause.IsChecked = true;
                        break;
                    case ReceiverSlice.RunState.STORE:
                        this.tbRunState.Text = "Stop";
                        radioStop.IsChecked = true;
                        break;
                }
                this.tbSerialNumber.Text = receiver.VEMCO_SerialNumber;
                this.tbModel.Text = receiver.VEMCO_Model;
                this.tbPortNumber.Text = receiver.portName;
            }
        }


    }

    
}
