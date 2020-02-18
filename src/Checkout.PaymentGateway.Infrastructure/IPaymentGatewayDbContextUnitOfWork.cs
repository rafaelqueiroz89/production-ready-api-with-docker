using Checkout.PaymentGateway.DataModel;
using Checkout.PaymentGateway.Infrastructure.SeedWork;

namespace Checkout.PaymentGateway.Infrastructure.Database
{
    /// <summary>
    /// IPaymentGatewayDbContextUnitOfWork
    /// </summary>
    /// <seealso cref="Checkout.PaymentGateway.Infrastructure.SeedWork.IUnitOfWork{Checkout.PaymentGateway.DataModel.PaymentGatewayContext}" />
    public interface IPaymentGatewayDbContextUnitOfWork : IUnitOfWork<PaymentGatewayContext>
    {
    }
}