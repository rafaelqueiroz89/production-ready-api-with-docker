using System;
using System.Threading;
using System.Threading.Tasks;

using Checkout.PaymentGateway.CQRS.Commands;
using Checkout.PaymentGateway.CQRS.Queries;
using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Domain.Interfaces;
using Checkout.PaymentGateway.Domain.PaymentAggregate;

using MediatR;

using Moq;

using Xunit;

namespace Checkout.PaymentGateway.Api.Tests
{
    public class CommandQuueryTests
    {
        private Mock<IPaymentGatewayRepository> paymentGatewayRepository { get; set; }
        private Mock<IMediator> mediator { get; set; }

        public CommandQuueryTests()
        {
            this.mediator = new Mock<IMediator>();
            this.paymentGatewayRepository = new Mock<IPaymentGatewayRepository>();
        }

        [Fact]
        public async Task ValidateRetrievePaymentQuery()
        {
            //Arrange
            this.mediator.Setup(m => m.Send(It.IsAny<RetrievePaymentQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<Payment>());

            //Act
            await this.mediator.Object.Send(new RetrievePaymentQuery(Guid.Empty));

            //Assert
            this.mediator.Verify(x => x.Send(It.IsAny<RetrievePaymentQuery>(), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task ValidateRequestPaymentCommand()
        {
            //Arrange
            this.mediator.Setup(m => m.Send(It.IsAny<RequestPaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<BankResponsePayment>());

            //Act
            await this.mediator.Object.Send(new RequestPaymentCommand(this.fakeInvalidRequestPayment()));

            //Assert
            this.mediator.Verify(x => x.Send(It.IsAny<RequestPaymentCommand>(), It.IsAny<CancellationToken>()));
        }

        private RequestPayment fakeInvalidRequestPayment()
        {
            return new RequestPayment()
            {
                Amount = 300,
                Card = new CardInfo()
                {
                    CardNumber = "5215",
                    Cvv = "41",
                    ExpiryMonth = 2,
                    ExpiryYear = 2000,
                    Name = "Rafael"
                },
                CurrencyCode = "USD"
            };
        }
    }
}