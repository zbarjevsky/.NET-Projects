using NmeaParser.Nmea;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace NMEAParser
{
    public class NMEAParser
    {
        private static readonly Dictionary<string, Dictionary<int, NmeaMessage>> MultiPartMessageCache = new Dictionary<string, Dictionary<int, NmeaMessage>>();

        public static List<NmeaMessage> ReadFile(string fileName)
        {
            List<NmeaMessage> messages = new List<NmeaMessage>();

            string[] lines = File.ReadAllLines(fileName);

            MultiPartMessageCache.Clear();
            foreach (string line in lines)
            {
                NmeaMessage msg = NmeaMessage.Parse(line);
                if (msg != null)
                    OnMessageReceived(msg, messages);
            }

            return messages;
        }

        private static void OnMessageReceived(NmeaMessage msg, List<NmeaMessage> messages)
        {
            if (msg == null)
                return;

            List<NmeaMessage> messageParts = new List<NmeaMessage>();
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
                        messageParts = new List<NmeaMessage>(dic.Values);
                        MessageReceived(msg, messageParts, messages);
                    }
                }
            }
            else
            {
                MessageReceived(msg, null, messages);
            }
        }

        private static void MessageReceived(NmeaMessage msg, List<NmeaMessage> messageParts, List<NmeaMessage> messages)
        {
            Debug.WriteLine(msg.MessageType + ": " + msg.ToString());

            //output.Text = string.Join("\n", messages.ToArray());
            //output.Select(output.Text.Length - 1, 0); //scroll to bottom

            if (msg is NmeaParser.Nmea.Rmc)
                messages.Add(msg as NmeaParser.Nmea.Rmc);

            return;

            if (msg is NmeaParser.Nmea.Gsv gpgsv)
            {
                if (messageParts != null)
                {
                    messages.Add(new Gsv(messageParts));
                }
            }
            else if (msg is NmeaParser.Nmea.Rmc)
                messages.Add(msg as NmeaParser.Nmea.Rmc);
            else if (msg is NmeaParser.Nmea.Gga)
                messages.Add(msg as NmeaParser.Nmea.Gga);
            else if (msg is NmeaParser.Nmea.Gsa)
                messages.Add(msg as NmeaParser.Nmea.Gsa);
            else if (msg is NmeaParser.Nmea.Gll)
                messages.Add(msg as NmeaParser.Nmea.Gll);
            else if (msg is NmeaParser.Nmea.Garmin.Pgrme)
                messages.Add(msg as NmeaParser.Nmea.Garmin.Pgrme);
            else //unknown
                messages.Add(msg);
       }
    }
}
