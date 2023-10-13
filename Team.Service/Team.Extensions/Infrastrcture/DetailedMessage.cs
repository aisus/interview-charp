using System.Collections.Generic;
using System.Linq;
using Team.Extensions.Infrastrcture.Enums;

namespace Team.Extensions.Infrastrcture
{
    /// <summary>
    /// Entry of a Detailed message.
    /// Contains some text and MessageStatus.
    /// </summary>
    public class MessageEntry
    {
        public MessageStatus Status { get; set; }
        public string Text { get; set; }

        protected MessageEntry()
        {
        }

        public MessageEntry(MessageStatus status, string text)
        {
            Status = status;
            Text = text;
        }

        #region CREATION_METHODS

        public static MessageEntry FromStatus(MessageStatus status, string text)
        {
            return new MessageEntry(status, text);
        }

        public static MessageEntry Error(string text)
        {
            return new MessageEntry(MessageStatus.Error, text);
        }

        public static MessageEntry Warning(string text)
        {
            return new MessageEntry(MessageStatus.Warning, text);
        }

        public static MessageEntry Success(string text)
        {
            return new MessageEntry(MessageStatus.Success, text);
        }

        public static MessageEntry Info(string text)
        {
            return new MessageEntry(MessageStatus.Info, text);
        }

        public static IEnumerable<MessageEntry> FromStatusCollection(MessageStatus status,
            IEnumerable<string> messages)
        {
            return messages.Select(x => new MessageEntry(status, x));
        }

        public static IEnumerable<MessageEntry> ErrorCollection(IEnumerable<string> messages)
        {
            return messages.Select(x => new MessageEntry(MessageStatus.Error, x));
        }

        public static IEnumerable<MessageEntry> WarningCollection(IEnumerable<string> messages)
        {
            return messages.Select(x => new MessageEntry(MessageStatus.Warning, x));
        }

        public static IEnumerable<MessageEntry> SuccessCollection(IEnumerable<string> messages)
        {
            return messages.Select(x => new MessageEntry(MessageStatus.Success, x));
        }

        #endregion
    }

    /// <summary>
    /// Message with string key and list of entries.
    /// Each entry contains text and it's status.
    /// May be used as validation result, or to contain a list of exceptions.
    /// </summary>
    public class DetailedMessage
    {
        /// <summary>
        /// Object key, for example, invalid property name
        /// </summary>
        public string Object { get; set; }

        /// <summary>
        /// Messages, each with it's status
        /// </summary>
        public List<MessageEntry> Messages { get; set; }

        public DetailedMessage()
        {
            Messages = new List<MessageEntry>();
        }

        public DetailedMessage(string objectName) : this()
        {
            Object = objectName;
        }

        public DetailedMessage(string objectName, MessageEntry entry) : this(objectName)
        {
            Messages.Add(entry);
        }

        public DetailedMessage(string objectName, IEnumerable<MessageEntry> entries) : this(objectName)
        {
            Messages = entries.ToList();
        }

        #region CREATION_METHODS

        public static DetailedMessage Empty(string objectName = null)
        {
            return new DetailedMessage(objectName);
        }

        public static DetailedMessage FromError(string message, string objectName = null)
        {
            return new DetailedMessage(objectName, MessageEntry.Error(message));
        }

        public static DetailedMessage FromWarning(string message, string objectName = null)
        {
            return new DetailedMessage(objectName, MessageEntry.Warning(message));
        }

        public static DetailedMessage FromSuccess(string message, string objectName = null)
        {
            return new DetailedMessage(objectName, MessageEntry.Success(message));
        }

        public static DetailedMessage FromInfo(string message, string objectName = null)
        {
            return new DetailedMessage(objectName, MessageEntry.Info(message));
        }

        public static DetailedMessage FromErrorCollection(IEnumerable<string> messages, string objectName = null)
        {
            return new DetailedMessage(objectName, MessageEntry.ErrorCollection(messages));
        }

        public static DetailedMessage FromWarningCollection(IEnumerable<string> messages, string objectName = null)
        {
            return new DetailedMessage(objectName, MessageEntry.WarningCollection(messages));
        }

        public static DetailedMessage FromSuccessCollection(IEnumerable<string> messages, string objectName = null)
        {
            return new DetailedMessage(objectName, MessageEntry.SuccessCollection(messages));
        }

        #endregion
    }
}