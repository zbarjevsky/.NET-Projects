using NmeaParser.Nmea;
using System;
using System.Collections.Generic;
using System.Text;

namespace NMEAParser
{
    /// <summary>
    /// Event argument for the <see cref="NmeaDevice.MessageReceived" />
    /// </summary>
    public sealed class NmeaMessageReceivedEventArgs : EventArgs
    {
        internal NmeaMessageReceivedEventArgs(NmeaMessage message, IReadOnlyList<NmeaMessage> messageParts)
        {
            Message = message;
            MessageParts = messageParts;
        }

        /// <summary>
        /// Gets the nmea message.
        /// </summary>
        /// <value>
        /// The nmea message.
        /// </value>
        public NmeaMessage Message { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is a multi part message.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is multi part; otherwise, <c>false</c>.
        /// </value>
        public bool IsMultipart => Message is IMultiPartMessage;

        /// <summary>
        /// Gets the message parts if this is a multi-part message and all message parts has been received.
        /// </summary>
        /// <value>
        /// The message parts.
        /// </value>
        public IReadOnlyList<NmeaMessage> MessageParts { get; }
    }
}
