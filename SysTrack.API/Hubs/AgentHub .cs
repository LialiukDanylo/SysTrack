using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace SysTrack.API.Hubs
{
    public class AgentHub : Hub
    {
        public async Task JoinGroup(string groupId, bool isClient)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        public async Task StartMetrics(string groupId)
        {
            await Clients.Group(groupId).SendAsync("StartMetrics");
        }

        public async Task StopMetrics(string groupId)
        {
            await Clients.Group(groupId).SendAsync("StopMetrics");
        }
    }
}
