using EShopping.Ordering.Api;
using EShopping.Ordering.Application;
using EShopping.Ordering.Infrastructure;
using EShopping.Shared.Utils;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

builder.UseSerilog();
builder.AddOpenTelemetry();

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
