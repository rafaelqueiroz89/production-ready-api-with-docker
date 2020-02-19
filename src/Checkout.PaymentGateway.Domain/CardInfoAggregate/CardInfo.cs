using Checkout.PaymentGateway.Infrastructure.SeedWork;

namespace Checkout.PaymentGateway.Domain
{
    /// <summary>
    /// This is a card info aggregation
    /// </summary>
    public class CardInfo : IAggregateRoot
    {
        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        /// <value>
        /// The card number.
        /// </value>
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the expiry month.
        /// </summary>
        /// <value>
        /// The expiry month.
        /// </value>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// Gets or sets the expiry year.
        /// </summary>
        /// <value>
        /// The expiry year.
        /// </value>
        public int ExpiryYear { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the CVV.
        /// </summary>
        /// <value>
        /// The CVV.
        /// </value>
        public string Cvv { get; set; }
    }
}