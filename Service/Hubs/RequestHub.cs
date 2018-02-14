using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Service.Hubs
{
    public class RequestHub : Hub
    {
        public Task RequestForChat()
        {
            return Clients.Group("consultants").InvokeAsync("request_for_chat", Context.ConnectionId);
        }
    }
}
