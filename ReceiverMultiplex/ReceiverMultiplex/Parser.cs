using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverMultiplex
{
    interface Parser
    {
        String areYouThere();
        Boolean validAreYouThere(String reply);
    }
}
