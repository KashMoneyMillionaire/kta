using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/snapshots", async (Snapshot snapshot, [FromServices] DaprClient dapr) =>
{
    await dapr.SaveStateAsync("snapshots", snapshot.LicensePlate, snapshot);
});

app.Run();

record Snapshot(int GateId, SnapshotStatus Status, string LicensePlate, DateTimeOffset Time);

enum SnapshotStatus
{
    Enter,
    Continue,
    Exit
}