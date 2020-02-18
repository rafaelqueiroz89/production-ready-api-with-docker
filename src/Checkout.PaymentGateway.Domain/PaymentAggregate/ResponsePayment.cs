using System;

namespace Checkout.PaymentGateway.Domain.PaymentAggregate
{
    /// <summary>
    /// Response of the payment
    /// </summary>
    public class ResponsePayment
    {
        /// <summary>
        /// Gets or sets the payment code.
        /// </summary>
        /// <value>
        /// The payment code.
        /// </value>
        public Guid PaymentCode { get; set; }

        /// <summary>
        /// Gets or sets the payment status.
        /// </summary>
        /// <value>
        /// The payment status.
        /// </value>
        public PaymentStatusTypes PaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the card information aggregate.
        /// </summary>
        /// <value>
        /// The card information aggregate.
        /// </value>
        public CardInfo CardInfoAggregate { get; set; }
    }
}