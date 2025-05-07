using EShopping.Shared.BuildingBlocks.Exceptions.Handler;
using EShopping.Shared.Utils.Tracing;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddFeatures();
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddTraceLogger();

builder.UseSerilogWithSeqSinkAndHttpEnricher();

builder.AddDataAccess(builder.Configuration);
builder.Services.AddDataAccessHealthChecks(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddOpenApiExplorer(builder.Configuration);
}

builder.Services.Configure<RouteHandlerOptions>(options => options.ThrowOnBadRequest = true);

var app = builder.Build();

app.UseExceptionHandler(opts => { });
app.UseTraceLogger();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApiExplorer(builder.Configuration);
}


app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});
app.MapFeatures();

app.Run();