using Checkout.PaymentGateway.DataModel.Models;

using Microsoft.EntityFrameworkCore;

namespace Checkout.PaymentGateway.DataModel
{
    /// <summary>
    /// Database context
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public partial class PaymentGatewayContext : DbContext
    {
        /// <summary>
        /// Gets or sets the payment.
        /// </summary>
        /// <value>
        /// The payment.
        /// </value>
        public virtual DbSet<Payment> Payment { get; set; }

        /// <summary>
        /// Gets or sets the payment status.
        /// </summary>
        /// <value>
        /// The payment status.
        /// </value>
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentGatewayContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public PaymentGatewayContext(DbContextOptions<PaymentGatewayContext> options) : base(options)
        {
        }
    }
}