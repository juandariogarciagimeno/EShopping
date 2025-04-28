using EShopping.Basket.Data;
using EShopping.Basket.Features;
using EShopping.Shared.BuildingBlocks.Exceptions.Handler;
using EShopping.Shared.Utils;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddFeatures(builder.Configuration);
builder.Services.AddOpenApiExplorer(builder.Configuration);
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddSerilogFromConfiguration(builder.Configuration);
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddHealthChecks();
builder.Services.AddDataAccessHealthChecks(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddOpenApiExplorer(builder.Configuration);
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApiExplorer(builder.Configuration);
}

app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});
app.UseExceptionHandler(opts => { });
app.UseHttpsRedirection();
app.MapFeatures();


app.Run();