using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Domain.PaymentAggregate;

using MediatR;

namespace Checkout.PaymentGateway.CQRS.Commands
{
    /// <summary>
    /// Request payment command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{Checkout.PaymentGateway.Domain.PaymentAggregate.ResponsePayment}" />
    public class RequestPaymentCommand : IRequest<ResponsePayment>
    {
        public RequestPaymentCommand(RequestPayment requestPaymentAggregate)
        {
            this.RequestPaymentAggregate = requestPaymentAggregate;
        }

        /// <summary>
        /// Gets the request payment aggregate.
        /// </summary>
        /// <value>
        /// The request payment aggregate.
        /// </value>
        public RequestPayment RequestPaymentAggregate { get; private set; }
    }
}