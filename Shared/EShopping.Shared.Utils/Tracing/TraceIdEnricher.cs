using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace EShopping.Shared.Utils.Tracing;

public class TraceIdEnricher(IHttpContextAccessor contextAccessor) : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var traceId = contextAccessor?.HttpContext?.Request?.Headers?.TryGetValue("X-Trace-Id", out var t) == true
            ? t.ToString()
            : null;

        if (!string.IsNullOrEmpty(traceId))
        {
            logEvent.RemovePropertyIfPresent("Trace");
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("Trace", traceId));
        }
    }
}
