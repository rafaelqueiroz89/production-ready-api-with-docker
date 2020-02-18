using System;
using System.Threading.Tasks;

using Checkout.PaymentGateway.Domain.PaymentAggregate;

namespace Checkout.PaymentGateway.Domain.Interfaces
{
    /// <summary>
    /// This is where I execute the commands and queries to the DB
    /// </summary>
    public interface IPaymentGatewayRepository
    {
        /// <summary>
        /// Adds a payment.
        /// </summary>
        /// <param name="requestPayment">The request payment.</param>
        /// <returns></returns>
        Task AddPayment(RequestPayment requestPayment);

        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <param name="requestPayment">The request payment.</param>
        /// <returns></returns>
        Task<ResponsePayment> GetPayment(Guid paymentCode);
    }
}