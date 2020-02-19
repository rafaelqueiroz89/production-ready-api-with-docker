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
        Task<Guid> AddPaymentAsync(RequestPayment requestPayment);

        /// <summary>
        /// Updates the payment asynchronous.
        /// </summary>
        /// <param name="requestPayment">The request payment.</param>
        /// <returns></returns>
        Task UpdatePaymentAsync(Guid internalPaymentCode, PaymentStatusTypes paymentStatus, Guid bankPaymentCode);

        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <param name="requestPayment">The request payment.</param>
        /// <returns></returns>
        Task<Payment> GetPaymentAsync(Guid paymentCode);
    }
}