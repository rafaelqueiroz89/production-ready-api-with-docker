using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Domain.Exceptions;
using Checkout.PaymentGateway.Domain.Interfaces;
using Checkout.PaymentGateway.Domain.PaymentAggregate;

using FluentValidation.Results;

using MediatR;

[assembly: InternalsVisibleTo("Checkout.PaymentGateway.Api.Tests")]

namespace Checkout.PaymentGateway.CQRS.Commands.Handlers
{
    /// <summary>
    /// Handler for requesting a payment, it is internal to the API microservice
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Checkout.PaymentGateway.CQRS.Commands.RequestPaymentCommand, Checkout.PaymentGateway.Domain.PaymentAggregate.Payment}" />
    internal class RequestPaymentCommandHandler : IRequestHandler<RequestPaymentCommand, BankResponsePayment>
    {
        private readonly IBankRepository bankRepository;
        private readonly IPaymentGatewayRepository paymentGatewayRepository;

        public RequestPaymentCommandHandler(IBankRepository bankRepository, IPaymentGatewayRepository paymentGatewayRepository)
        {
            this.bankRepository = bankRepository;
            this.paymentGatewayRepository = paymentGatewayRepository;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<BankResponsePayment> Handle(RequestPaymentCommand request, CancellationToken cancellationToken)
        {
            RequestPaymentAggregateValidator validator = new RequestPaymentAggregateValidator();
            ValidationResult result = validator.Validate(request.RequestPayment);

            if (!result.IsValid)
            {
                throw new ArgumentValidationException("One or more fields are wrong", result.Errors);
            }
            else
            {
                //save payment
                var paymentCode = await this.paymentGatewayRepository.AddPaymentAsync(request.RequestPayment);

                //change status according to the call to the bank's API
                var response = await this.bankRepository.RequestPaymentAsync(request.RequestPayment);
                response.RequestCode = paymentCode;

                //update with the transaction code + status with what is returned from the bank and update entries in database
                await this.paymentGatewayRepository.UpdatePaymentAsync(paymentCode, response.PaymentStatus, response.BankRequestCode);

                return response;
            }
        }
    }
}