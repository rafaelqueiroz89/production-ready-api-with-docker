using System.Threading.Tasks;

using Checkout.PaymentGateway.DataModel;
using Checkout.PaymentGateway.Infrastructure.SeedWork;

namespace Checkout.PaymentGateway.Infrastructure.Database
{
    /// <summary>
    /// This is the unit of work implementation
    /// </summary>
    /// <seealso cref="Checkout.PaymentGateway.Infrastructure.SeedWork.UnitOfWorkTemplate{Checkout.PaymentGateway.DataModel.PaymentGatewayContext}" />
    /// <seealso cref="Checkout.PaymentGateway.Infrastructure.Database.IPaymentGatewayDbContextUnitOfWork" />
    public class PaymentGatewayDbContextUnitOfWork : UnitOfWorkTemplate<PaymentGatewayContext>, IPaymentGatewayDbContextUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FxPositionsDbContextUnitOfWork"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public PaymentGatewayDbContextUnitOfWork(PaymentGatewayContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <returns></returns>
        public override async Task CommitAsync()
        {
            await this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public override void Dispose()
        {
            this.Context.Dispose();
        }
    }
}