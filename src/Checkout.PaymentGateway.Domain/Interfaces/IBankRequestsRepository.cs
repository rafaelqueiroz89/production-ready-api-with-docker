using System.Threading.Tasks;

using Checkout.PaymentGateway.Domain.PaymentAggregate;

namespace Checkout.PaymentGateway.Domain.Interfaces
{
    /// <summary>
    /// Bank repository
    /// </summary>
    public interface IBankRequestsRepository
    {
        /// <summary>
        /// Requests the payment.
        /// </summary>
        /// <returns></returns>
        Task<ResponsePayment> RequestPayment();
    }
}