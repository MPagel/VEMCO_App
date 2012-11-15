﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Sandbox
{
    /// <summary>
    /// This module decodes raw text messages sent from the receiver into a format useable by other modules or server
    /// components.
    /// </summary>
    public class Decoder:Module
    {
        /// <summary>
        /// Returns the human readable name of this module.
        /// </summary>
        /// <returns>The name of this module.</returns>
        public override string getModuleName()
            { return "Decoder"; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Decoder(Dispatcher dispatcher)
            :base(dispatcher) {}

        /// <summary>
        /// Parses a mesage sent by the Receiver -- for example, a detection or status message. Enqueues the decoded message.
        /// </summary>
        /// <param name="unparsedMessage">The unparsed message event generated by the Receiver class.</param>
        public Decoded Decode(UnparsedMessage unparsedMessage)
        {
            Dictionary<String, String> payload = new Dictionary<String, String>();
            string message = unparsedMessage["unparsedmessage"];
            dynamic config = unparsedMessage["configuration"];
            string messageType  = getMessageType(message, config);
            Match matches;
            foreach(string word in config.decoder.sentences[messageType].word_order)
            {
                String wordRegex = ((String)config.decoder.words[word]);
                matches = Regex.Match(message, wordRegex);
                if (matches.Success)
                    payload.Add(word, matches.Groups[1].ToString());
                else
                    payload.Add(word, "NULL");
            }
            return new Decoded(payload, unparsedMessage, message, messageType);
        }

        /// <summary>
        /// Given the raw text of the message and the configuration for this firmware version, this method
        /// determines the type of message.
        /// </summary>
        /// <param name="unparsedMessage">The unparsed message event generated by the Receiver class.</param>
        /// <param name="config">json configuration file corresponding to a receivers firmware version</param>
        /// <returns>type of message</returns>
        private string getMessageType(string unparsedMessage, dynamic config) 
        {     
            foreach (string sentence in config.decoder.sentences.Keys)
            {
                try
                {
                    //List<String> wordExpansions = new List<String>();
                    //foreach(Object o in config.decoder.sentences[sentence].word_order) 
                    //{
                    //    wordExpansions.Add(config.decoder.words[((String)o)]);
                    //}
                    if(Regex.IsMatch(unparsedMessage, config.decoder.sentences[sentence].format))
                    {
                        return sentence;
                    }

                } finally {
                }
            }
            ModuleException me = new ModuleException(this,
                "Unparsed message (" + unparsedMessage + ") does not match definition in this config.");
            //dispatcher.enqueueEvent(new RealTimeEvents.RealTimeEventUnknown(me.exceptionText,config));
            throw me;
        }
   
        /// <summary>
        /// The hook for the event dispatcher.
        /// </summary>
        /// <param name="rte"></param>
        public override void onRealTimeEvent(RealTimeEvent rte)
        {
            if(rte.GetType() == typeof(UnparsedMessage))
            {
                UnparsedMessage unparsedMessage = (UnparsedMessage)rte;
                Decode(unparsedMessage);
            }
        }
    }
}