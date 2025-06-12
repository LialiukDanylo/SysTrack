using System.Diagnostics;
using Microsoft.AspNetCore.SignalR.Client;
using SysTrack.Agent.Models;
using SysTrack.Agent.Monitoring;
using SysTrack.Shared.Models;

namespace SysTrack.Agent
{
    public class Worker : BackgroundService
    {
        private readonly string _groupId = "TestGroup";
        private readonly string _name = "Agent007";
        private readonly bool isClient = false;
        private bool _isActive = false;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5265/hub")
                .WithAutomaticReconnect()
                .Build();

            connection.Reconnected += async (connectionId) =>
            {
                await connection.InvokeAsync("JoinGroup", _groupId, isClient);
            };

            connection.On("StartMetrics", () =>
            {
                Console.WriteLine(">>> Start sending metrics");
                _isActive = true;
            });

            connection.On("StopMetrics", () =>
            {
                Console.WriteLine(">>> Stop sending metrics");
                _isActive = false;
            });

            _ = Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        await connection.StartAsync();
                        await connection.InvokeAsync("JoinGroup", _groupId, isClient);
                        break;
                    }
                    catch
                    {
                        await Task.Delay(5000);
                    }
                }
            });

            IMetricCollector hwMetricCollector = new HardwareCollector();
            INetworkCollector networkCollector = new NetworkCollector();
            HardwareMetrics hwMetrics = new HardwareMetrics();
            NetworkMetrics networkMetrics = new NetworkMetrics();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_isActive)
                {
                    hwMetricCollector.Update();
                    networkCollector.Update();

                    hwMetrics = hwMetricCollector.GetMetrics();
                    networkMetrics = networkCollector.GetNetworkUsage();

                    int cpuUsage = hwMetrics.CpuLoadPercent;
                    int gpuUsage = hwMetrics.GpuLoadPercent;
                    int ramUsed = hwMetrics.RamUsedMb;

                    //Console.WriteLine($"CPU Usage: {cpuUsage}%");
                    //Console.WriteLine($"GPU Usage: {gpuUsage}%");
                    //Console.WriteLine($"Ram Used: {ramUsed}Mb");

                    foreach (var adapter in networkMetrics.NetworkUsage)
                    {
                        //Console.WriteLine($"Network Usage[{adapter.Key}]: ↓ {adapter.Value.receive / 1024:F2} KB/s, ↑ {adapter.Value.send / 1024:F2} KB/s, ↑");
                    }
                    MetricsData metrics = new MetricsData(_name, cpuUsage, gpuUsage);

                    await connection.InvokeAsync("SendMetrics", _groupId, metrics);
                }
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
