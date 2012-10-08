using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverMultiplex
{
    interface Parser
    {
        String areYouThere(); //This should call STATUS or INFO followed by setting RTMMODE=2
        String pollReceiver(); //The RTMNOW command

    }
}
