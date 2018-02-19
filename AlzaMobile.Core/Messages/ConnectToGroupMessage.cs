using System;
using MvvmCross.Plugins.Messenger;

namespace AlzaMobile.Core.Messages
{
    public class ConnectToGroupMessage : MvxMessage
    {
        public ConnectToGroupMessage(object sender, string group)
            : base(sender)
        {
            Group = group;
        }

        public string Group { get; set; }
    }
}
