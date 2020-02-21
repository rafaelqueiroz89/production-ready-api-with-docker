using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Checkout.PaymentGateway.Api.Client
{
    /// <summary>
    ///
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <param name="responseMessage">The response message.</param>
        /// <returns></returns>
        public static Dictionary<string, IEnumerable<string>> GetHeaders(this HttpResponseMessage responseMessage)
        {
            var headers = Enumerable.ToDictionary(responseMessage.Headers, h_ => h_.Key, h_ => h_.Value);

            if (responseMessage.Content != null && responseMessage.Content.Headers != null)
            {
                foreach (var item in responseMessage.Content.Headers)
                    headers[item.Key] = item.Value;
            }

            return headers;
        }
    }
}