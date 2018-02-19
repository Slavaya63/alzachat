using MvvmCross.Core.ViewModels;
using System;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Platform;
using AlzaMobile.Core.Messages;

namespace AlzaMobile.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        protected static HubConnection ChatHub { get; private set; }

        private string _title;

        public string Title 
        { 
            get { return _title; }
            set {SetProperty(ref _title, value);}
        }

        protected async Task StartChatHub()
        {
            try
            {
                var messenger = Mvx.Resolve<IMvxMessenger>();

                ChatHub = new Microsoft.AspNetCore.SignalR.Client
                                     .HubConnectionBuilder()
                                     .WithUrl("http://127.0.0.1:5000/chat")
                                     .WithConsoleLogger()
                                     .Build();

                BeforeStartChatHub();

                ChatHub.On("on_connect_to_group", new Action<string>(obj => 
                                                                     messenger.Publish(new ConnectToGroupMessage(this, obj))));
                ChatHub.On("new_message", new Action<string, string>((group, message) => 
                                                                     messenger.Publish(new NewMessage(this, group, message))));;

                await ChatHub.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected virtual void BeforeStartChatHub(){}
    }
}
