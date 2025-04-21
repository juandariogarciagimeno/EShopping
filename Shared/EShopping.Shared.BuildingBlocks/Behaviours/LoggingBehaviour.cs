using EShopping.Shared.BuildingBlocks.CQRS;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Shared.BuildingBlocks.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse>(IServiceProvider ServiceProvider, ILoggerFactory LoggerFactor) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var handlerInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(typeof(TRequest), typeof(TResponse));
            var handler = ServiceProvider.GetService(handlerInterfaceType);
            var handlerType = handler?.GetType();

            ILogger logger;

            if (handlerType != null)
            {
                // Create logger with context of the actual handler type
                logger = LoggerFactor.CreateLogger(handlerType);
            }
            else
            {
                // Fallback logger if handler type can't be resolved
                logger = LoggerFactor.CreateLogger(typeof(LoggingBehaviour<TRequest, TResponse>));
            }

            logger.LogInformation("[START] Handle Request '{Request}' - Response '{Response} with data '{ReqData}' and handler type '{Handler}'",
                typeof(TRequest).Name,
                typeof(TResponse).Name,
                request,
                handlerType?.Name ?? "Unknown");

            try
            {
                var timer = System.Diagnostics.Stopwatch.StartNew();
                var response = await next();

                timer.Stop();
                if (timer.Elapsed.Seconds > 3)
                {
                    logger.LogWarning("[PERFORMANCE] The request '{Request}' took '{ElapsedMilliseconds}' ms to complete",
                        typeof(TRequest).Name,
                        timer.Elapsed.TotalMilliseconds);
                }

                logger.LogInformation("[END] Handled request of type '{RequestType}' successfully", typeof(TRequest).Name);

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[ERROR] Error handling request '{RequestType}'. Error: '{Error}'", typeof(TRequest).Name, ex.Message);
                throw;
            }
        }
    }
}
