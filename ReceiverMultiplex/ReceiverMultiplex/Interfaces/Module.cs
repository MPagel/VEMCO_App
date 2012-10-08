using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverMultiplex
{
    interface Module
    {
        void on_new_receiver();
        void on_del_receiver();
    }
}
