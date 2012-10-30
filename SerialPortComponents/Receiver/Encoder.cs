using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverSlice
{
    public class Encoder
    {
        private String prefix; //Will be made to be *SSSSSS.P#CC,
        public dynamic encoderConfig { get; private set; }

        public Encoder(string prefix, dynamic encoderConfig)
        {
            this.encoderConfig = encoderConfig;
            this.prefix = prefix;
        }

        public String build(string command)
        {
            return(build(command,new Object[0]));
        }

        public String build(string command, object[] arguments)
        {           
            try
            {
                List<Object> l = new List<Object>();
                l.Add(this.prefix);
                foreach (Object a in arguments)
                {
                    l.Add(a);
                }
                String built = String.Format(encoderConfig.encoder[command], l.ToArray());
                if (valid(built) == false)
                {
                    throw new EncoderExceptions( prefix,
                        String.Format("Invalid command: {0}  Receiver prefix: {1}", command, prefix));
                }
                return built;
            }
            catch (Exception e)
            {
                throw new EncoderExceptions(prefix,
                    String.Format("Failed to build command (mismatched arguments?): {0} Receiver prefix: {1}", command, prefix), e);
            }
        }


        public Boolean valid(String processed_commmand)
        {
            //regexp magic
            return true;
        }

    }
}
