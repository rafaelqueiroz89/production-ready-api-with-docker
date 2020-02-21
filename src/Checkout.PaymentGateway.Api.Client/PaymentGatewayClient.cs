using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Checkout.PaymentGateway.Api.Client
{
    internal class PaymentGatewayClient : IPaymentGatewayClient
    {
        /// <summary>
        /// The base URL
        /// </summary>
        private readonly string baseUrl;

        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient client;

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentGatewayClient"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="client">The client.</param>
        public PaymentGatewayClient(string baseUrl, HttpClient client)
            : this(baseUrl)
        {
            this.client = client;
        }

        public PaymentGatewayClient(string baseUrl)
        {
            this.client = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(10)
            };

            this.baseUrl = !string.IsNullOrWhiteSpace(baseUrl) ? baseUrl.TrimEnd('/') : "";
        }

        /// <summary>
        /// Requests the payment.
        /// </summary>
        /// <param name="requestPayment"></param>
        /// <returns></returns>
        /// <exception cref="MisClientException">The HTTP status code of the response was not expected (" + (int)responseMessage.StatusCode + "). - null</exception>
        public async Task<JObject> RequestPayment(JObject requestPayment)
        {
            string serializedObject = JsonConvert.SerializeObject(requestPayment);
            var httpContent = new StringContent(serializedObject, Encoding.UTF8, "application/json");

            using (var responseMessage = await this.client.PostAsync($"{this.baseUrl}{SdkConstants.ResourcePayment}", httpContent))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<JObject>(Encoding.UTF8.GetString(await responseMessage.Content.ReadAsByteArrayAsync()));
                }
                else
                {
                    var headers = responseMessage.GetHeaders();
                    var responseData = responseMessage.Content == null ? null : await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                    throw new PaymentGatewayClientException("The HTTP status code of the response was not expected (" + (int)responseMessage.StatusCode + ")."
                        , (int)responseMessage.StatusCode, responseData, headers, null);
                }
            }
        }

        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <param name="paymentId">The payment identifier.</param>
        /// <returns></returns>
        /// <exception cref="PaymentGatewayClientException">The HTTP status code of the response was not expected (" + (int)responseMessage.StatusCode + "). - null</exception>
        public async Task<JObject> GetPaymentAsync(Guid paymentId)
        {
            using (var responseMessage = await this.client.GetAsync($"{this.baseUrl}{SdkConstants.ResourcePayment}?paymentCode={paymentId}"))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<JObject>(Encoding.UTF8.GetString(await responseMessage.Content.ReadAsByteArrayAsync()));
                }
                else if ((int)responseMessage.StatusCode == 404)
                {
                    throw new PaymentGatewayClientException("Payment not found");
                }
                else
                {
                    var headers = responseMessage.GetHeaders();
                    var responseData = responseMessage.Content == null ? null : await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                    throw new PaymentGatewayClientException("The HTTP status code of the response was not expected (" + (int)responseMessage.StatusCode + ").", (int)responseMessage.StatusCode, responseData, headers, null);
                }
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                this.client.Dispose();
            }

            this.disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}