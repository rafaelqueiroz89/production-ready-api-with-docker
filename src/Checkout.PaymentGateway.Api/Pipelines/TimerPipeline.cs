using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Serilog;

namespace Checkout.PaymentGateway.Api
{
    /// <summary>
    /// This is a timer pipeline
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="MediatR.IPipelineBehavior{TRequest, TResponse}" />
    public class TimerPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="next">The next.</param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
#if (DEBUG)

            {
                Log.Debug("----------->>> ------------------- <<<-----------");
                Log.Debug($"----------->>> Mediator Handling {typeof(TRequest).Name} <<<-----------");
                Log.Debug("----------->>> ------------------- <<<-----------");

                var timer = Stopwatch.StartNew();

                var result = await next();

                var elapsed = timer.Elapsed;

                Log.Debug("----------->>> ------------------- <<<-----------");
                Log.Debug($"----------->>> Mediator Handled {typeof(TRequest).Name} in {elapsed.TotalMilliseconds} ms.<<<-----------");
                Log.Debug("----------->>> ------------------- <<<-----------");

                return result;
            }
#else

            {
                return await next();
            }
#endif
        }
    }
}