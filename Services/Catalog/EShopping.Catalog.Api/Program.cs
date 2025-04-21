using EShopping.Shared.BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddFeatures();
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.AddDataAccess(builder.Configuration);
builder.Services.AddDataAccessHealthChecks(builder.Configuration);

builder.Services.AddOpenApiExplorer(builder.Configuration);

builder.Services.AddLogging(logbuilder =>
{
    var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

    logbuilder.AddSerilog(logger);
});

builder.Services.Configure<RouteHandlerOptions>(options => options.ThrowOnBadRequest = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApiExplorer(builder.Configuration);
}

app.UseExceptionHandler(opts => { });
app.UseHttpsRedirection();
app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});
app.MapFeatures();

app.Run();