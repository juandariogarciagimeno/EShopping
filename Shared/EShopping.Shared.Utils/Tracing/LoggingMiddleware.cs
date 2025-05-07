using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace EShopping.Shared.Utils.Tracing;

public class LoggingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string traceId = context.Request.Headers.TryGetValue("x-trace-id", out var traceIdHeader) ? traceIdHeader.ToString() : string.Empty;
        if (!string.IsNullOrEmpty(traceId))
        {
            LogContext.PushProperty("TraceId", traceId);

            var logger = context.RequestServices.GetService<ILogger<LoggingMiddleware>>();

            logger?.LogInformation("IN Request: {TraceId} {Method} {Path}", traceId, context.Request.Method, context.Request.Path);

            await next(context);

            logger?.LogInformation("OUT Request: {TraceId} {Method} {Path}", traceId, context.Request.Method, context.Request.Path);
        }
    }
}
