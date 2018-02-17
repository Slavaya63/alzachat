using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Service.Hubs
{
    public class ChatHub : Hub
    {
        public async void Send(string groupName, string message)
        {
            await Clients.Group(groupName).InvokeAsync("new_message", message);
        }

        public async void RequestForChat()
        {
            await Clients.Group("consultants").InvokeAsync("request_for_chat", Context.ConnectionId);
        }

        public async void AcceptForChat(string clientConnectionId)
        {
            // generate name of group
            var group = Guid.NewGuid().ToString();

            // add users to group
            await this.Groups.AddAsync(Context.ConnectionId, group);
            await this.Groups.AddAsync(clientConnectionId, group);

            // notify users
            await Clients.Client(Context.ConnectionId).InvokeAsync("on_connect_to_group", group);
            await Clients.Client(clientConnectionId).InvokeAsync("on_connect_to_group", group);
        }

        public async void JoinToConsultants()
        {
            await this.Groups.AddAsync(Context.ConnectionId, "consultants");
        }
    }
}
