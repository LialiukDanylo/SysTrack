using SysTrack.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.MapHub<AgentHub>("/hub");

app.Run();