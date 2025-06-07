using Microsoft.AspNetCore.SignalR;
using SysTrack.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/start/{groupId}", async (string groupId, IHubContext<AgentHub> hubContext) =>
{
    await hubContext.Clients.Group(groupId).SendAsync("StartMetrics");
    return Results.Ok("Start sent");
});

app.MapGet("/stop/{groupId}", async (string groupId, IHubContext<AgentHub> hubContext) =>
{
    await hubContext.Clients.Group(groupId).SendAsync("StopMetrics");
    return Results.Ok("Stop sent");
});

app.MapHub<AgentHub>("/hub");

app.Run();