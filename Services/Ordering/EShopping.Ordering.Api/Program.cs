using EShopping.Ordering.Api;
using EShopping.Ordering.Application;
using EShopping.Ordering.Infrastructure;
using EShopping.Shared.BuildingBlocks.Exceptions.Handler;
using EShopping.Shared.Utils;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddOpenApiExplorer(builder.Configuration);
}

var app = builder.Build();
app.UseApiServices();

await app.UseMigrations(builder.Environment);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApiExplorer(builder.Configuration);
}

app.Run();
