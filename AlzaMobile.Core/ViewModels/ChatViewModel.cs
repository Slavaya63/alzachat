using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AlzaMobile.Core.Messages;
using AlzaMobile.Core.Models;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace AlzaMobile.Core.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        private string _editMessage;
        private IMvxMessenger _messager;
        MvxSubscriptionToken _messageToken;


        public ObservableCollection<MessageModel> Messages { get; set; } = new ObservableCollection<MessageModel>();
        public string CurrentGroupId { get; private set; }
        public string EditMessage
        {
            get { return _editMessage; }
            set {SetProperty(ref _editMessage, value);}
        }


        public ChatViewModel(IMvxMessenger messager)
        {
            this._messager = messager;
        }

        public override Task Initialize()
        {
            _messageToken = _messager.Subscribe<NewMessage>(message =>
                         {
                             if (message.Group.Equals(CurrentGroupId))
                             {
                                 this.Messages.Add(new MessageModel
                                 {
                                     Date = DateTime.Now.ToString("D"),
                                     Message = message.Message,
                                     Name = "Incoming",
                                     Type = MessageType.Incoming
                                 });
                             }
                         });
            return base.Initialize();
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            if (parameters.Data.TryGetValue("group_id", out var value))
                CurrentGroupId = value;

            base.InitFromBundle(parameters);
        }

        public MvxCommand SendCommand => new MvxCommand(DoSend);

        private async void DoSend()
        {
            await ChatHub.SendAsync("Send", new object[]{CurrentGroupId, EditMessage});
            EditMessage = string.Empty;
        }
    }
}
