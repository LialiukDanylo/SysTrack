using Microsoft.AspNetCore.SignalR.Client;
using SysTrack.Agent.Models;
using SysTrack.Agent.Monitoring;

namespace SysTrack.Agent
{
    public class Worker : BackgroundService
    {
        private readonly string _groupId = "TestGroup";
        private readonly bool isClient = false;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5265/hub")
            .WithAutomaticReconnect()
            .Build();

            await connection.StartAsync();
            await connection.InvokeAsync("JoinGroup", _groupId, isClient);

            IMetricCollector hwMetricCollector = new HardwareCollector();
            INetworkCollector networkCollector = new NetworkCollector();
            HardwareMetrics hwMetrics = new HardwareMetrics();
            NetworkMetrics networkMetrics = new NetworkMetrics();

            while (!stoppingToken.IsCancellationRequested)
            {
                hwMetricCollector.Update();
                networkCollector.Update();

                hwMetrics = hwMetricCollector.GetMetrics();
                networkMetrics = networkCollector.GetNetworkUsage();

                int cpuUsage = hwMetrics.CpuLoadPercent;
                int gpuUsage = hwMetrics.GpuLoadPercent;
                int ramUsed = hwMetrics.RamUsedMb;

                Console.WriteLine($"CPU Usage: {cpuUsage}%");
                Console.WriteLine($"GPU Usage: {gpuUsage}%");
                Console.WriteLine($"Ram Used: {ramUsed}Mb");

                foreach (var adapter in networkMetrics.NetworkUsage)
                {
                    Console.WriteLine($"Network Usage[{adapter.Key}]: ↓ {adapter.Value.receive / 1024:F2} KB/s, ↑ {adapter.Value.send / 1024:F2} KB/s, ↑");
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
