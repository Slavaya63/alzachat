using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Core.ViewModels;
using System.Linq;

namespace AlzaMobile.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isHaveChat = false;
        private readonly IUserDialogs _userDialogs;
        private string _requestCount;

        public string ProfileType { get { return Settings.ProfileType; } }

        private List<string> _clientsIds = new List<string>();

        public bool IsHaveChat
        {
            get { return _isHaveChat; }
            set { SetProperty(ref _isHaveChat, value); }
        }

        public string RequestCount
        {
            get => _requestCount;
            set => SetProperty(ref _requestCount, value);
        }

        public MainViewModel(IUserDialogs userDialogs)
        {
            this._userDialogs = userDialogs;
        }

        public async override System.Threading.Tasks.Task Initialize()
        {
            await base.StartChatHub();
        }

        protected override void BeforeStartChatHub()
        {
            if (ProfileType == "consultant")
            {
                ChatHub.Connected += async () =>
                {
                    await ChatHub.SendAsync("JoinToConsultants");
                    ChatHub.On("request_for_chat", new Action<string>(OnRequestForChat));
                    ChatHub.On("on_connect_to_group", new Action<string>(ConnectToGroup));
                };
            }
        }

        private void OnRequestForChat(string connectionId)
        {
            _clientsIds.Add(connectionId);
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

        #endregion

        private async void MakeRequestForChat()
        {
            await ChatHub.SendAsync("RequestForChat");

            ChatHub.On("on_connect_to_group", new Action<string>(ConnectToGroup));
        }

        private void ConnectToGroup(string arg)
        {

        }
    }
}
