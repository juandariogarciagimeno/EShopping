using Microsoft.AspNetCore.RateLimiting;
using EShopping.Shared.Utils;
using EShopping.Shared.Utils.Tracing;

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
builder.Services.AddTraceLogger(true);
builder.UseSerilogWithSeqSinkAndHttpEnricher();

var app = builder.Build();

app.UseTraceLogger(true);

app.MapReverseProxy();
app.UseRateLimiter();

app.Run();