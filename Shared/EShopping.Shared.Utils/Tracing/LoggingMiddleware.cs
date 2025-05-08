using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace EShopping.Shared.Utils.Tracing;

public class LoggingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var logger = context.RequestServices.GetService<ILogger<LoggingMiddleware>>();

        logger?.LogInformation("IN Request: {Method} {Path}", context.Request.Method, context.Request.Path);

        await next(context);

        logger?.LogInformation("OUT Request: {Method} {Path}", context.Request.Method, context.Request.Path);
    }
}
