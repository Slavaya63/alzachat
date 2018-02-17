using MvvmCross.Core.ViewModels;
using System;
using Microsoft.AspNetCore.SignalR.Client;

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

        protected async void StartChatHub()
        {
            try
            {
                HubConnection chatHub = new Microsoft.AspNetCore.SignalR.Client
                                     .HubConnectionBuilder()
                                     .WithUrl("http://127.0.0.1:5000/chat")
                                     .Build();

                await chatHub.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
