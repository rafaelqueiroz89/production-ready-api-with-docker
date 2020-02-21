using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.PaymentGateway.Api.Client.Tests
{
    /// <summary>
    ///
    /// </summary>
    public abstract class BaseTest
    {
        /// <summary>
        /// The service
        /// </summary>
        private IServiceCollection service;

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        protected IConfiguration Configuration { get; private set; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="service">The service.</param>
        protected abstract void ConfigureServices(IServiceCollection service);

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest"/> class.
        /// </summary>
        protected BaseTest()
        {
            this.CreateServiceCollection();
            this.ConfigureServices(this.service);
        }

        /// <summary>
        /// Creates the service collection.
        /// </summary>
        private void CreateServiceCollection()
        {
            this.Configuration = this.GetConfigurationRoot();
            this.service = new ServiceCollection();
            this.service.AddScoped<IPaymentGatewayClient, PaymentGatewayClient>();
        }

        /// <summary>
        /// Gets the configuration root.
        /// </summary>
        /// <returns></returns>
        private IConfigurationRoot GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .Build();
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return this.service.BuildServiceProvider().GetService<T>();
        }
    }
}