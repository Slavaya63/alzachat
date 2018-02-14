using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Service.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string groupName, string message)
        {
            //return Clients.All.InvokeAsync("Send", message);
            Clients.Group(groupName).InvokeAsync("send", message);
        }

        public void JoinGroup(string groupname)
        {
            Groups.AddAsync(Context.ConnectionId, groupname);
        }

        public void LeaveGroup(string groupname)
        {
            Groups.RemoveAsync(Context.ConnectionId, groupname);
        }
    }
}
