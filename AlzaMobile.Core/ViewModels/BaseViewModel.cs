using MvvmCross.Core.ViewModels;
using System;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

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
                ChatHub = new Microsoft.AspNetCore.SignalR.Client
                                     .HubConnectionBuilder()
                                     .WithUrl("http://127.0.0.1:5000/chat")
                                     .WithConsoleLogger()
                                     .Build();

                BeforeStartChatHub();

                await ChatHub.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected virtual void BeforeStartChatHub()
        {   }
    }
}
