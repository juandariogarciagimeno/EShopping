using Microsoft.AspNetCore.RateLimiting;
using EShopping.Shared.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
    });
});
builder.Services.AddTraceLogger();
builder.UseSerilog();
builder.AddOpenTelemetry();

var app = builder.Build();

app.UseTraceLogger();

app.MapReverseProxy();
app.UseRateLimiter();

app.Run();