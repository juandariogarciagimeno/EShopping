using Microsoft.AspNetCore.Http;

namespace EShopping.Shared.Utils.Tracing
{
    public class TraceGeneratorMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var traceId = GenerateTraceId();
            context.Request.Headers.Append("X-Trace-Id", traceId);
            context.Response.Headers.Append("X-Trace-Id", traceId);
            await next(context);
        }

        private static string GenerateTraceId()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).ToUpper();
        }
    }
}
