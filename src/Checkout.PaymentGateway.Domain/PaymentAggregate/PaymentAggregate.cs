using System;

namespace Checkout.PaymentGateway.Domain
{
    public class PaymentAggregate
    {
        /// <summary>
        /// Gets or sets the payment status.
        /// </summary>
        /// <value>
        /// The payment status.
        /// </value>
        public PaymentStatusTypes PaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the payment code.
        /// </summary>
        /// <value>
        /// The payment code.
        /// </value>
        public Guid PaymentCode { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the card.
        /// </summary>
        /// <value>
        /// The card.
        /// </value>
        public CardInfoAggregate Card { get; set; }
    }
}