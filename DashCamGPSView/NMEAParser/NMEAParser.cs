using NmeaParser.Nmea;
using System;
using System.Collections.Generic;
using System.IO;

namespace NMEAParser
{
    public class NMEAParser
    {
        private readonly Dictionary<string, Dictionary<int, NmeaParser.Nmea.NmeaMessage>> MultiPartMessageCache = new Dictionary<string, Dictionary<int, NmeaParser.Nmea.NmeaMessage>>();

        public void ReadFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                NmeaMessage msg = NmeaMessage.Parse(line);
            }
        }
        private void OnMessageReceived(NmeaParser.Nmea.NmeaMessage msg)
        {
            if (msg == null)
                return;
            NmeaParser.Nmea.NmeaMessage[]? messageParts = null;
            if (msg is IMultiPartMessage multi)
            {
                string messageType = msg.MessageType.Substring(2); //We don't care about the two first characters. Ie GPGSV, GLGSV, GAGSV etc are all part of the same multi-part message
                if (MultiPartMessageCache.ContainsKey(messageType))
                {
                    var dic = MultiPartMessageCache[messageType];
                    if (dic.ContainsKey(multi.MessageNumber - 1) && !dic.ContainsKey(multi.MessageNumber))
                    {
                        dic[multi.MessageNumber] = msg;
                    }
                    else //Something is out of order. Clear cache
                        MultiPartMessageCache.Remove(messageType);
                }
                else if (multi.MessageNumber == 1)
                {
                    MultiPartMessageCache[messageType] = new Dictionary<int, NmeaParser.Nmea.NmeaMessage>(multi.TotalMessages);
                    MultiPartMessageCache[messageType][1] = msg;
                }
                if (MultiPartMessageCache.ContainsKey(messageType))
                {
                    var dic = MultiPartMessageCache[messageType];
                    if (dic.Count == multi.TotalMessages) //We have a full list
                    {
                        MultiPartMessageCache.Remove(messageType);
                        messageParts = dic.Values.ToArray();
                    }
                }
            }

            MessageReceived?.Invoke(this, new NmeaMessageReceivedEventArgs(msg, messageParts));
        }
    }
}
