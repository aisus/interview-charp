using System.Collections.Generic;
using Team.Extensions.Infrastrcture.Enums;

namespace Team.Extensions.Infrastrcture
{
    /// <summary>
    /// Collection of DetailedMessages.
    /// Can be used like an ApiResult in cases, where there is no need
    /// to provide an object or status in the API response, for example,
    /// to send a collection of validation errors.
    /// </summary>
    public class MessageResult : List<DetailedMessage>
    {
        public MessageResult()
        {
        }

        public MessageResult(IEnumerable<DetailedMessage> messages) : base(messages)
        {
        }

        public MessageResult(DetailedMessage message) : base(new[] {message})
        {
        }

        public MessageResult(string objectName, MessageStatus status, string message) : base(new List<DetailedMessage>
        {
            new DetailedMessage(objectName, MessageEntry.FromStatus(status, message))
        })
        {
        }

        public static MessageResult FromApiResult<T>(ApiResult<T> result) where T : class
        {
            return result.Messages == null ? new MessageResult() : new MessageResult(result.Messages);
        }
    }
}