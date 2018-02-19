using System;
using MvvmCross.Plugins.Messenger;

namespace AlzaMobile.Core.Messages
{
    public class NewMessage : MvxMessage
    {
        public NewMessage(object sender, string group, string message)
            : base(sender)
        {
            this.Group = group;
            this.Message = message;
        }

        public string Group { get; set; }
        public string Message { get; set; }
    }
}
