using System;
namespace AlzaMobile.Core.Models
{
    public class MessageModel
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public MessageType Type { get; set; }
    }

    public enum MessageType
    {
        Incoming,
        Outgoing
    }
}
