using System;

using Checkout.PaymentGateway.Domain.PaymentAggregate;

using MediatR;

namespace Checkout.PaymentGateway.CQRS.Queries
{
    /// <summary>
    /// Retrieve Payment
    /// </summary>
    /// <seealso cref="MediatR.IRequest{Checkout.PaymentGateway.Domain.PaymentAggregate.ResponsePayment}" />
    public class RetrievePaymentQuery : IRequest<ResponsePayment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RetrievePaymentQuery"/> class.
        /// </summary>
        /// <param name="paymentCode">The payment code.</param>
        public RetrievePaymentQuery(Guid paymentCode)
        {
            this.PaymentCode = paymentCode;
        }

        /// <summary>
        /// Gets the response payment aggregate.
        /// </summary>
        /// <value>
        /// The response payment aggregate.
        /// </value>
        public Guid PaymentCode { get; private set; }
    }
}