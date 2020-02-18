using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Domain.PaymentAggregate;

using MediatR;

namespace Checkout.PaymentGateway.CQRS.Commands
{
    /// <summary>
    /// Request payment command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{Checkout.PaymentGateway.Domain.PaymentAggregate.ResponsePaymentAggregate}" />
    public class RequestPaymentCommand : IRequest<ResponsePaymentAggregate>
    {
        public RequestPaymentCommand(RequestPaymentAggregate requestPaymentAggregate)
        {
            this.RequestPaymentAggregate = requestPaymentAggregate;
        }

        /// <summary>
        /// Gets the request payment aggregate.
        /// </summary>
        /// <value>
        /// The request payment aggregate.
        /// </value>
        public RequestPaymentAggregate RequestPaymentAggregate { get; private set; }
    }
}