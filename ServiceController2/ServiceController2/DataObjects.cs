using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceController2
{

    public class DataObjects
    {
        public System.Collections.ObjectModel.ObservableCollection<ReceiverSlice.Receiver> receivers { get; set; }
        public DataObjects() { }
    }
}
