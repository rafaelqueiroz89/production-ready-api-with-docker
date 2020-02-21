using System;

using Microsoft.Extensions.DependencyInjection;

namespace Checkout.PaymentGateway.Api.Client
{
    /// <summary>
    ///
    /// <see cref="ServiceCollectionExtensions"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScopedPaymentGatewaySdk(this IServiceCollection services, string endpointUrl)
        {
            if (string.IsNullOrWhiteSpace(endpointUrl))
                throw new PaymentGatewayClientException(
                    new ArgumentException("Endpoint URL cannot be null/empty string", nameof(endpointUrl)));
            return services.AddScoped<IPaymentGatewayClient>(x => new PaymentGatewayClient(endpointUrl));
        }

        public static IServiceCollection AddSingletonPaymentGatewaySdk(this IServiceCollection services, string endpointUrl)
        {
            if (string.IsNullOrWhiteSpace(endpointUrl))
                throw new PaymentGatewayClientException(
                    new ArgumentException("Endpoint URL cannot be null/empty string", nameof(endpointUrl)));
            return services.AddSingleton<IPaymentGatewayClient>(x => new PaymentGatewayClient(endpointUrl));
        }

        public static IServiceCollection AddTransientPaymentGatewaySdk(this IServiceCollection services, string endpointUrl)
        {
            if (string.IsNullOrWhiteSpace(endpointUrl))
                throw new PaymentGatewayClientException(
                    new ArgumentException("Endpoint URL cannot be null/empty string", nameof(endpointUrl)));
            return services.AddTransient<IPaymentGatewayClient>(x => new PaymentGatewayClient(endpointUrl));
        }
    }
}