using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

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

            await connection.StartAsync();
            Console.WriteLine("Client Connected.");
            await connection.InvokeAsync("JoinRoom", _groupId, isClient);

            base.OnStartup(e);
        }
    }

}
