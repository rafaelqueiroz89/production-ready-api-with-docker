using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Checkout.PaymentGateway.Domain.Exceptions;
using Checkout.PaymentGateway.Domain.Interfaces;
using Checkout.PaymentGateway.Domain.PaymentAggregate;

using MediatR;

[assembly: InternalsVisibleTo("Checkout.PaymentGateway.Api.Tests")]

namespace Checkout.PaymentGateway.CQRS.Queries.Handlers
{
    /// <summary>
    /// Retrieve Payment, gets a previously processed payment
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Checkout.PaymentGateway.CQRS.Queries.RetrievePaymentQuery, Checkout.PaymentGateway.Domain.PaymentAggregate.Payment}" />
    internal class RetrievePaymentQueryHandler : IRequestHandler<RetrievePaymentQuery, Payment>
    {
        private readonly IPaymentGatewayRepository paymentGatewayRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RetrievePaymentQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentGatewayRepository">The payment gateway repository.</param>
        public RetrievePaymentQueryHandler(IPaymentGatewayRepository paymentGatewayRepository)
        {
            this.paymentGatewayRepository = paymentGatewayRepository;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentValidationException">One or more fields are wrong</exception>
        /// <exception cref="PaymentRefusedException">Payment refused</exception>
        public async Task<Payment> Handle(RetrievePaymentQuery request, CancellationToken cancellationToken)
        {
            return await this.paymentGatewayRepository.GetPaymentAsync(request.PaymentCode);
        }
    }
}