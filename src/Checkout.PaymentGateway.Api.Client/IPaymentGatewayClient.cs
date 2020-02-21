using System;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Checkout.PaymentGateway.Api.Client
{
    /// <summary>
    /// Client interface
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IPaymentGatewayClient : IDisposable
    {
        /// <summary>
        /// Requests the payment.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        Task<JObject> RequestPayment(JObject requestPayment);

        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <param name="paymentId">The payment identifier.</param>
        /// <returns></returns>
        Task<JObject> GetPaymentAsync(Guid paymentId);
    }
}