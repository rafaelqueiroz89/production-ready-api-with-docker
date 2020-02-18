using Checkout.PaymentGateway.Domain.Extensions;
using Checkout.PaymentGateway.Infrastructure.SeedWork;

namespace Checkout.PaymentGateway.Domain
{
    /// <summary>
    /// This is a card info aggregation
    /// </summary>
    public class CardInfo : IAggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardInfo"/> class.
        /// </summary>
        /// <param name="maskCardDetails">if set to <c>true</c> [mask card details].</param>
        public CardInfo(bool maskCardDetails)
        {
            if (maskCardDetails)
            {
                this.CardNumber = this.CardNumber.Masked(0, 10);
                this.Cvv = this.Cvv.Masked(0, 1);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardInfo"/> class.
        /// </summary>
        public CardInfo()
        {
        }

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