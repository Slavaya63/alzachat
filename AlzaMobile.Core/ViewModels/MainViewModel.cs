using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Core.ViewModels;

namespace AlzaMobile.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isHaveChat = false;
        private readonly IUserDialogs _userDialogs;

        public bool IsHaveChat
        {
            get { return _isHaveChat; }
            set { SetProperty(ref _isHaveChat, value); }
        }


        public MainViewModel(IUserDialogs userDialogs)
        {
            this._userDialogs = userDialogs;
        }

        public async override System.Threading.Tasks.Task Initialize()
        {
            base.StartChatHub();
        }

        #region commands

        public MvxCommand InfoCommand => new MvxCommand(DoInfo);

        private async void DoInfo()
        {
            ActionSheetConfig config = new ActionSheetConfig();
            config.Add("Request for chat", MakeRequestForChat);
            config.Cancel = new ActionSheetOption("Cancel");

            var dialog = _userDialogs.ActionSheet(config);
        }

        #endregion

        private async void MakeRequestForChat()
        {
            ChatHub.SendAsync("RequestForChat");

            ChatHub.On("on_connect_to_group", new Action<string>(ConnectToGroup));
        }

        private void ConnectToGroup(string arg)
        {
            
        }
    }
}
