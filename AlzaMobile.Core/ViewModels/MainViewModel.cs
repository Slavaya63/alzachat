using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Core.ViewModels;
using System.Linq;
using System.Collections.ObjectModel;
using MvvmCross.Plugins.Messenger;
using AlzaMobile.Core.Messages;

namespace AlzaMobile.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private string _requestCount;
        private IMvxMessenger _messenger;
        private ObservableCollection<string> _openChats = new ObservableCollection<string>();
        MvxSubscriptionToken onGroupToken;



        public string ProfileType { get { return Settings.ProfileType; } }

        public ObservableCollection<string> OpenChats 
        {
            get { return _openChats; }
            set 
            {
                SetProperty(ref _openChats, value); 
                RaisePropertyChanged(nameof(IsHaveChat));
            }
        }

        private List<string> _clientsIds = new List<string>();

        public bool IsHaveChat
        {
            get { return _openChats.Any(); }
        }

        public string RequestCount
        {
            get => _requestCount;
            set => SetProperty(ref _requestCount, value);
        }


        public MainViewModel(IUserDialogs userDialogs, IMvxMessenger messenger)
        {
            this._messenger = messenger;
            this._userDialogs = userDialogs;
            OpenChats.CollectionChanged += (sender, e) => RaisePropertyChanged(nameof(IsHaveChat));
        }

        public async override System.Threading.Tasks.Task Initialize()
        {
            await base.StartChatHub();

            onGroupToken = _messenger.SubscribeOnThreadPoolThread<ConnectToGroupMessage>(obj =>
                         {
                             //if (Settings.ProfileType == "client")
                             OpenChats.Add(obj.Group);
                         });
        }

        protected override void BeforeStartChatHub()
        {
            if (ProfileType == "consultant")
            {
                ChatHub.Connected += async () =>
                {
                    await ChatHub.SendAsync("JoinToConsultants");
                    ChatHub.On("request_for_chat", new Action<string>(OnRequestForChat));
                };
            }
        }


        private void OnRequestForChat(string connectionId)
        {
            _clientsIds.Add(connectionId);
            _userDialogs.Toast("New request!");
            //OpenChats.Add("New request!");
        }

        #region commands

        public MvxCommand InfoCommand => new MvxCommand(DoInfo);

        private void DoInfo()
        {
            if (ProfileType == "client")
                DoClientInfo();
            else
                DoConsultantInfo();
        }

        private void DoConsultantInfo()
        {
            ActionSheetConfig config = new ActionSheetConfig();
            config.Add("Accept request", AcceptRequest);
            config.Cancel = new ActionSheetOption("Cancel");

            var dialog = _userDialogs.ActionSheet(config);
        }

        private async void AcceptRequest()
        {
            await ChatHub.SendAsync("AcceptForChat", _clientsIds.First());
        }

        private void DoClientInfo()
        {
            ActionSheetConfig config = new ActionSheetConfig();
            config.Add("Request for chat", MakeRequestForChat);
            config.Cancel = new ActionSheetOption("Cancel");

            var dialog = _userDialogs.ActionSheet(config);
        }

        public MvxCommand<string> SelectItemCommand => new MvxCommand<string>(DoSelectItem);

        private void DoSelectItem(string arg)
        {
            ShowViewModel<ChatViewModel>(new Dictionary<string, string> { { "group_id", arg } });
        }

        #endregion

        private async void MakeRequestForChat()
        {
            await ChatHub.SendAsync("RequestForChat");
        }

    }
}
