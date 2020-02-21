using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Checkout.PaymentGateway.Api.Client.Tests")]

namespace Checkout.PaymentGateway.Api.Client
{
    /// <summary>
    /// API constants endpoints
    /// </summary>
    internal class SdkConstants
    {
        protected SdkConstants()
        {
        }

        /// <summary>
        /// The resource payment
        /// </summary>
        public const string ResourcePayment = "/api/payment";
    }
}