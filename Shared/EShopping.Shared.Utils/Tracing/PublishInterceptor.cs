using MassTransit;
using Microsoft.AspNetCore.Http;

namespace EShopping.Shared.Utils.Tracing;
public class PublishInterceptor(IHttpContextAccessor accessor) : IPublishObserver
{
    public Task PostPublish<T>(PublishContext<T> context) where T : class
    {
        return Task.CompletedTask;
    }

    public Task PrePublish<T>(PublishContext<T> context) where T : class
    {
        var traceId = accessor?.HttpContext?.Request?.Headers.TryGetValue("X-Trace-Id", out var t) == true ? t.ToString() : null;
        if (!string.IsNullOrEmpty(traceId))
        {
            context.Headers.Set("Trace", traceId);
        }

        return Task.CompletedTask;
    }

    public Task PublishFault<T>(PublishContext<T> context, Exception exception) where T : class
    {
        return Task.CompletedTask;
    }
}

public class ConsumeInterceptor : IConsumeObserver
{
    public Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
    {
        return Task.CompletedTask;
    }

    public Task PostConsume<T>(ConsumeContext<T> context) where T : class
    {
        return Task.CompletedTask;
    }

    public Task PreConsume<T>(ConsumeContext<T> context) where T : class
    {
        if (context.Headers.TryGetHeader("Trace", out object? t) && t is string trace)
        {
            Serilog.Context.LogContext.PushProperty("Trace", trace);
        }

        return Task.CompletedTask;
    }
}
