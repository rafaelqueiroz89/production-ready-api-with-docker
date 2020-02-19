using System;

namespace Checkout.PaymentGateway.DataModel.Models
{
    /// <summary>
    /// Payment model
    /// </summary>
    public partial class Payment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Payment"/> class.
        /// </summary>
        public Payment()
        {
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        /// <value>
        /// The card number.
        /// </value>
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the payment code.
        /// </summary>
        /// <value>
        /// The payment code.
        /// </value>
        public Guid PaymentCode { get; set; }

        /// <summary>
        /// Gets or sets the CVV.
        /// </summary>
        /// <value>
        /// The CVV.
        /// </value>
        public string Cvv { get; set; }

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
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the paymnent status code navigation.
        /// </summary>
        /// <value>
        /// The paymnent status code navigation.
        /// </value>
        public virtual PaymentStatus PaymnentStatusCodeNavigation { get; set; }

        /// <summary>
        /// Gets the code status.
        /// </summary>
        /// <value>
        /// The code status.
        /// </value>
        public long CodeStatus { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        public Guid BankPaymentCode { get; set; }
    }
}