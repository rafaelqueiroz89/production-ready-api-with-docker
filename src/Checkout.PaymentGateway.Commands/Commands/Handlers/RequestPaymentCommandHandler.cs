using System.Threading;
using System.Threading.Tasks;

using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Domain.Exceptions;
using Checkout.PaymentGateway.Domain.Interfaces;
using Checkout.PaymentGateway.Domain.PaymentAggregate;

using FluentValidation.Results;

using MediatR;

namespace Checkout.PaymentGateway.CQRS.Commands.Handlers
{
    /// <summary>
    /// Handler for requesting a payment, it is internal to the API microservice
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Checkout.PaymentGateway.CQRS.Commands.RequestPaymentCommand, Checkout.PaymentGateway.Domain.PaymentAggregate.Payment}" />
    internal class RequestPaymentCommandHandler : IRequestHandler<RequestPaymentCommand, BankResponsePayment>
    {
        private readonly IBankRepository _bankRepository;
        private readonly IPaymentGatewayRepository _paymentGatewayRepository;

        public RequestPaymentCommandHandler(IBankRepository bankRepository, IPaymentGatewayRepository paymentGatewayRepository)
        {
            this._bankRepository = bankRepository;
            this._paymentGatewayRepository = paymentGatewayRepository;
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
            ValidationResult result = validator.Validate(request.RequestPaymentAggregate);

            if (!result.IsValid)
            {
                throw new ArgumentValidationException("One or more fields are wrong", result.Errors);
            }
            else
            {
                ////save payment with status pending
                //var requestPayment = await this._paymentGatewayRepository.AddPaymentAsync;

                ////change status according to the call to the bank's API
                //requestPayment = await this._bankRepository.RequestPaymentAsync();

                //if (requestPayment.PaymentStatus == PaymentStatusTypes.Unsuccessful)
                //{
                //    throw new PaymentRefusedException("Payment refused");
                //}
                //else
                //{
                //    return requestPayment;
                //}

                return null;
            }
        }
    }
}