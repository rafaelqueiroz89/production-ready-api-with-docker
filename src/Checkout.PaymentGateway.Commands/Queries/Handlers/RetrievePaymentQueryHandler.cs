using System.Threading;
using System.Threading.Tasks;

using Checkout.PaymentGateway.Domain.Exceptions;
using Checkout.PaymentGateway.Domain.PaymentAggregate;

using MediatR;

namespace Checkout.PaymentGateway.CQRS.Queries.Handlers
{
    /// <summary>
    /// Retrieve Payment, gets a previously processed payment
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Checkout.PaymentGateway.CQRS.Queries.RetrievePaymentQuery, Checkout.PaymentGateway.Domain.PaymentAggregate.ResponsePaymentAggregate}" />
    internal class RetrievePaymentQueryHandler : IRequestHandler<RetrievePaymentQuery, ResponsePaymentAggregate>
    {
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentValidationException">One or more fields are wrong</exception>
        /// <exception cref="PaymentRefusedException">Payment refused</exception>
        public Task<ResponsePaymentAggregate> Handle(RetrievePaymentQuery request, CancellationToken cancellationToken)
        {
            throw new PaymentNotFoundException("Payment not found, maybe the code is wrong?");
        }
    }
}