using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xunit;

namespace Checkout.PaymentGateway.Api.Client.Tests
{
    public class ClientTests : BaseTest
    {
        private readonly IPaymentGatewayClient paymentGatewayClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggleServiceTest" /> class.
        /// </summary>
        public ClientTests()
        {
            this.paymentGatewayClient = this.GetService<IPaymentGatewayClient>();
        }

        [Fact]
        public async Task RequestMadePaymentReceivesValidObject()
        {
            //Arrange
            var payment = this.FakeValidObjectRequest();

            //Act
            var bankResponse = await this.paymentGatewayClient.RequestPayment(JsonConvert.DeserializeObject<JObject>(payment));

            //Assert
            Assert.True(bankResponse.HasValues);
        }

        [Fact]
        public async Task RequestUnknownMadePaymentReceivesInvalidObject()
        {
            //Arrange
            var invalidObj = Guid.NewGuid();

            //Act
            try
            {
                await this.paymentGatewayClient.GetPaymentAsync(invalidObj);
            }
            catch (PaymentGatewayClientException ex)
            {
                //Assert
                Assert.Contains("Payment not found", ex.Message);
            }
        }

        [Fact]
        public async Task RequestNewInvalidPaymentReceivesObjectAsyncRaisesException()
        {
            //Arrange
            var invalidObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(this.FakeInvalidObjectRequest());

            //Act
            try
            {
                await this.paymentGatewayClient.RequestPayment(invalidObj);
            }
            catch (PaymentGatewayClientException ex)
            {
                //Assert
                Assert.Contains("error", ex.Message);
            }
        }

        [Fact]
        public async Task RequestNewValidPaymentReceivesObjectAsync()
        {
            //Arrange
            var valid = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(this.FakeValidObjectRequest());

            //Act
            try
            {
                await this.paymentGatewayClient.RequestPayment(valid);
            }
            catch (PaymentGatewayClientException ex)
            {
                //Assert
                Assert.Contains("Error", ex.Message);
            }
        }

        private string FakeInvalidObjectRequest()
        {
            return "{\"amount\": 0,\"currencyCode\": \"string\",\"card\": {\"cardNumber\": \"string\",\"expiryMonth\": 0,\"expiryYear\": 0,\"name\": \"string\",\"cvv\": \"string\"}";
        }

        private string FakeValidObjectRequest()
        {
            return "{\"amount\": 150,\"currencyCode\": \"BRL\",\"card\": {\"cardNumber\": \"4067695317064003\",\"expiryMonth\": 12,\"expiryYear\":" + DateTime.Now.Year + 1 + ",\"name\": \"Robert\",\"cvv\": \"150\"}";
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="service">The service.</param>
        protected override void ConfigureServices(IServiceCollection service)
        {
            service.AddScopedPaymentGatewaySdk("http://localhost:5000");
        }
    }
}