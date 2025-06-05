using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace SysTrack.API.Hubs
{
    public class AgentHub : Hub
    {
        public async Task JoinGroup(string roomId, bool isClient)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task StartMetrics(string roomId)
        {
            await Clients.Group(roomId).SendAsync("StartMetrics");
        }

        public async Task StopMetrics(string roomId)
        {
            await Clients.Group(roomId).SendAsync("StopMetrics");
        }
    }
}
