using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Service.Hubs
{
    public class ChatHub : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("Send", message);
        }
    }
}
