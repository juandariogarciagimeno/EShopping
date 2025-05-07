using Grpc.Core;
using Grpc.Core.Interceptors;

namespace EShopping.Basket.Features
{
    public class MetadataInterceptor(IHttpContextAccessor contextAccessor) : Interceptor
    {
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var metadata = context.Options.Headers ?? new Metadata();
            var traceId = contextAccessor.HttpContext?.Request?.Headers["X-Trace-Id"].FirstOrDefault();

            if (!string.IsNullOrEmpty(traceId))
            {
                metadata.Add("x-trace-id", traceId); // lowercase header name is safer
            }

            var updatedContext = new ClientInterceptorContext<TRequest, TResponse>(
                context.Method,
                context.Host,
                context.Options.WithHeaders(metadata));

            return base.AsyncUnaryCall(request, updatedContext, continuation);
        }
    }
}
