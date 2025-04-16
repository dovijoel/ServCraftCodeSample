using Microsoft.EntityFrameworkCore;
using ServCraftCodeSample.Api.Hubs;
using ServCraftCodeSample.Infrastructure;
using ServCraftCodeSample.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddSignalR();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// this would only normally be done manually, automatically applying migrations for demo purposes
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ChatDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.MapHub<ChatHub>("/chat");

app.MapDefaultEndpoints();

app.Run();
