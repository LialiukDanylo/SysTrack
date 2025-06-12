using Microsoft.AspNetCore.SignalR;
using SysTrack.API.Services.Interfaces;
using SysTrack.Shared.Models;
namespace SysTrack.API.Hubs
{
    public class AgentHub : Hub
    {
        private readonly IConnectionManager _connectionManager;
        public AgentHub(IConnectionManager connectionManager) 
        { 
            _connectionManager = connectionManager;
        }
        
        public async Task JoinGroup(string groupId, bool isClient)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);

            if (isClient)
            {
                Console.WriteLine($"[{Context.ConnectionId}] client joined {groupId}");
                _connectionManager.AddClient(groupId, Context.ConnectionId);
                await StartMetrics(groupId);
            }
            else
            {
                if (_connectionManager.GetClientsCount(groupId) > 0)
                    _ = StartMetrics(groupId);

                Console.WriteLine($"[{Context.ConnectionId}] agent joined {groupId}");
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (_connectionManager.IsClient(Context.ConnectionId))
            {
                var groupId = _connectionManager.GetGroupId(Context.ConnectionId);
                _connectionManager.RemoveClient(Context.ConnectionId);
                if (_connectionManager.GetClientsCount(groupId) == 0)
                {
                    _ = StopMetrics(groupId);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task StartMetrics(string groupId)
        {
            await Clients.Group(groupId).SendAsync("StartMetrics");
        }

        public async Task StopMetrics(string groupId)
        {
            await Clients.Group(groupId).SendAsync("StopMetrics");
        }

        public async Task SendMetrics(string groupId, MetricsData metrics)
        {
            await Clients.Group(groupId).SendAsync("ReceiveMetrics", metrics);
        }
    }
}
