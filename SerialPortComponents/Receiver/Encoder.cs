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
        private dynamic encoderConfig;

        public Encoder(string prefix, dynamic encoderConfig)
        {
            this.encoderConfig = encoderConfig;
            this.prefix = prefix;
        }

        public String build(string command, object[] arguments)
        {
            try
            {
                String built = String.Format(encoderConfig.encoder[command], arguments);
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
