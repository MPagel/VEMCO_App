using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using EventSlice.Interfaces;
using EventSlice;

namespace Decoder
{
    public class Decoder:Module
    {

        public override string getModuleName()
        {
            return "Decoder";
        }

        public void Decode(string message, dynamic config)
        {
            Dictionary<string,string> payload = new Dictionary<string,string>();
            string messageType  = getMessageType(message, config);
            
            foreach(string word in config.decoder[messageType].word_order)
            {
                String wordRegex = ((String)config.decoder.words[word]);
                int wordOffset = ((Int32)config.decoder[messageType].offset);
                payload.Add(word, Regex.Match(message, wordRegex).Value.Substring(wordOffset));
            }

            dispatcher.enqueueEvent(new RealTimeEvents.RealTimeEventDecoded(message, config, messageType, payload));
        }

        

        private string getMessageType(string unparsedMessage, dynamic config) 
        {     
            foreach (dynamic sentence in config.decoder.sentences)
            {
                try
                {
                    List<String> wordExpansions = new List<String>();
                    foreach(Object o in sentence.word_orders) 
                    {
                        wordExpansions.Add(config.decoder.words[((String)o)]);
                    }
                    if(Regex.IsMatch(unparsedMessage, String.Format(sentence.format, wordExpansions)))
                    {
                        return sentence;
                    }

                } finally {
                }
            }
            EventSlice.Interfaces.ModuleException me = new ModuleException(this,
                "Unparsed message (" + unparsedMessage + ") does not match definition in this config.");
            dispatcher.enqueueEvent(new RealTimeEvents.RealTimeEventUnknown(me.exceptionText,config));
            throw me;
            
        }
   
        public override void onRealTimeEvent(RealTimeEvent rte)
        {
            if(rte.GetType() == typeof(ReceiverSlice.RealTimeEvents.UnparsedMessage))
            {
                rte=rte;
            }
            //Decode(unparsedMessage.unparsedMessage, unparsedMessage.config);
        }
    }
}