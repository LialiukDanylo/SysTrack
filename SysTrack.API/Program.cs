using Microsoft.AspNetCore.SignalR;
using SysTrack.API.Services.Interfaces;
using SysTrack.API.Services;
using SysTrack.API.Hubs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddSingleton<IConnectionManager, ConnectionManager>();

var app = builder.Build();

app.MapControllers();

app.MapHub<AgentHub>("/hub");

app.Run();