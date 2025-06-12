using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
using SysTrack.Shared.Models;

namespace SysTrack.Client
{
    public partial class App : Application
    {
        private readonly string _groupId = "TestGroup";
        private readonly bool isClient = true;

        protected override async void OnStartup(StartupEventArgs e)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5265/hub")
                .WithAutomaticReconnect()
                .Build();

            connection.Reconnected += async (connectionId) =>
            {
                await connection.InvokeAsync("JoinGroup", _groupId, isClient);
            };

            connection.On<MetricsData>("ReceiveMetrics", (metrics) =>
            {
                Debug.WriteLine($"{metrics.Name}\nCPU: {metrics.CpuUsage}%\nGPU: {metrics.GpuUsage}%");
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

            base.OnStartup(e);
            
        }
    }

}
