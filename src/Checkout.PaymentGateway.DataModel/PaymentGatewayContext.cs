using Microsoft.EntityFrameworkCore;

namespace Checkout.PaymentGateway.DataModel
{
    /// <summary>
    /// Database context
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class PaymentGatewayContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FxPositionsContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public PaymentGatewayContext(DbContextOptions<PaymentGatewayContext> options) : base(options)
        {
        }
    }
}