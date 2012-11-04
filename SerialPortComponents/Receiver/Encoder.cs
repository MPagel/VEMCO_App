using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverSlice
{
    /// <summary>
    /// The Encoder builds commands as requested by the Receiver (possibly through public write() methods),
    /// by looking up the requested command in the configuration file.  The configuration file specifies
    /// the format and the encoder uses String.Format to build the string that is finally returned.  Before
    /// returning the string, however, it is verified as conforming to the config's spec.
    /// </summary>
    public class Encoder
    {
        private String prefix; //Will be made to be SSSSSS.P#CC,
        /// <summary>
        /// The json configuration object.
        /// </summary>
        public dynamic encoderConfig { get; private set; }

        /// <summary>
        /// This constructor uses the command prefix and encoder.
        /// </summary>
        /// <param name="prefix"> SSSSSS.P#CC where SSSSSS is the serial number of the receiver.</param>
        /// <param name="encoderConfig">Configuration containing 'encoder' entries.</param>
        public Encoder(string prefix, dynamic encoderConfig)
        {
            this.encoderConfig = encoderConfig;
            this.prefix = prefix;
        }

        /// <summary>
        /// Builds a command with no arguments called.
        /// </summary>
        /// <param name="command">The name of the command as defined in the configuration.</param>
        /// <returns>String formatted as a VEMCO command to be sent to the VR2C hardware.</returns>
        /// <remarks>This is a wrapper to the build(string,object) method.  The command prefix does
        /// not need to be passed to support</remarks>
        public String build(string command)
        {
            return(build(command,new Object[0]));
        }

        /// <summary>
        /// Builds a command with arguments.
        /// </summary>
        /// <param name="command">The name of the command as defined in the configuration.</param>
        /// <param name="arguments">The parameters required to complete the build.</param>
        /// <returns>This is a wrapper to the build(string,object) method.</returns>

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

        /// <summary>
        /// Determines whether the processed command is valid for the current json configuration (by firmware version)
        /// </summary>
        /// <param name="processed_commmand">The command that is being validated</param>
        /// <returns>True if the processed command is valid for the current json configuration</returns>
        public Boolean valid(String processed_commmand)
        {
            //regexp magic
            return true;
        }

    }
}
