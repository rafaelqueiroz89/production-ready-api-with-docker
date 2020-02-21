using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Checkout.PaymentGateway.Api.Client
{
    [Serializable]
    public class PaymentGatewayClientException : Exception
    {
        protected PaymentGatewayClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentGatewayClientException"/> class.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public PaymentGatewayClientException(Exception ex) : base($"An exception of type {typeof(PaymentGatewayClientException).Name} occurred", ex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionsNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PaymentGatewayClientException(string message = "") : base(string.IsNullOrEmpty(message) ? "Payment not found." : message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentGatewayClientException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="response">The response.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="innerException">The inner exception.</param>
        public PaymentGatewayClientException(string message, int statusCode, string response, Dictionary<string, IEnumerable<string>> headers, Exception innerException)
            : base($"{message}\n\nStatus: {statusCode}\nResponse: {response}", innerException)
        {
            this.StatusCode = statusCode;
            this.Response = response;
            this.Headers = headers;
        }

        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>The headers.</value>
        public IDictionary<string, IEnumerable<string>> Headers { get; private set; }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <value>The response.</value>
        public string Response { get; private set; }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>The status code.</value>
        public int StatusCode { get; private set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return $"HTTP Response: \n\n{this.Response}\n\n{base.ToString()}";
        }
    }
}