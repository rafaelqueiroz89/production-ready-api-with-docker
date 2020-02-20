using System;
using System.Threading;
using System.Threading.Tasks;

using Checkout.PaymentGateway.CQRS.Commands;
using Checkout.PaymentGateway.CQRS.Commands.Handlers;
using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Domain.Interfaces;
using Checkout.PaymentGateway.Domain.PaymentAggregate;

using Moq;

using Xunit;

namespace Checkout.PaymentGateway.Api.Tests
{
    public class HandleRequestsTests
    {
        private readonly Mock<IPaymentGatewayRepository> paymentGatewayRepostiory;
        private readonly Mock<IBankRepository> paymentBankRepostiory;

        public HandleRequestsTests()
        {
            this.paymentGatewayRepostiory = new Mock<IPaymentGatewayRepository>();
            this.paymentBankRepostiory = new Mock<IBankRepository>();
        }

        [Fact]
        public async Task GetPaymentDbReturnsAPaymentWithStatusPending()
        {
            //Arange
            var requestPayment = this.fakeValidRequestPayment();
            var requestPaymentCode = Guid.NewGuid();
            var bankResponse = this.fakeBankResponsePayment(requestPaymentCode, PaymentStatusTypes.Pending);

            // Act
            var command = new RequestPaymentCommand(requestPayment);
            this.paymentGatewayRepostiory.Setup(x => x.AddPaymentAsync(command.RequestPayment)).Returns(Task.FromResult(Guid.NewGuid()));
            this.paymentBankRepostiory.Setup(x => x.RequestPaymentAsync(command.RequestPayment)).Returns(Task.FromResult(bankResponse));

            var handler = new RequestPaymentCommandHandler(this.paymentBankRepostiory.Object, this.paymentGatewayRepostiory.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(PaymentStatusTypes.Pending, result.PaymentStatus);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetPaymentDbReturnsAPaymentWithStatusSuccessful()
        {
            //Arange
            var requestPayment = this.fakeValidRequestPayment();
            var requestPaymentCode = Guid.NewGuid();
            var bankResponse = this.fakeBankResponsePayment(requestPaymentCode, PaymentStatusTypes.Successful);

            // Act
            var command = new RequestPaymentCommand(requestPayment);
            this.paymentGatewayRepostiory.Setup(x => x.AddPaymentAsync(command.RequestPayment)).Returns(Task.FromResult(Guid.NewGuid()));
            this.paymentBankRepostiory.Setup(x => x.RequestPaymentAsync(command.RequestPayment)).Returns(Task.FromResult(bankResponse));
            this.paymentGatewayRepostiory.Setup(x => x.UpdatePaymentAsync(requestPaymentCode, PaymentStatusTypes.Successful, bankResponse.RequestCode)).Returns(Task.CompletedTask);

            var handler = new RequestPaymentCommandHandler(this.paymentBankRepostiory.Object, this.paymentGatewayRepostiory.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(PaymentStatusTypes.Successful, result.PaymentStatus);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetPaymentDbReturnsAPaymentWithStatusUnuccessful()
        {
            //Arange
            var requestPayment = this.fakeValidRequestPayment();
            var requestPaymentCode = Guid.NewGuid();
            var bankResponse = this.fakeBankResponsePayment(requestPaymentCode, PaymentStatusTypes.Unsuccessful);

            // Act
            var command = new RequestPaymentCommand(requestPayment);
            this.paymentGatewayRepostiory.Setup(x => x.AddPaymentAsync(command.RequestPayment)).Returns(Task.FromResult(Guid.NewGuid()));
            this.paymentBankRepostiory.Setup(x => x.RequestPaymentAsync(command.RequestPayment)).Returns(Task.FromResult(bankResponse));
            this.paymentGatewayRepostiory.Setup(x => x.UpdatePaymentAsync(requestPaymentCode, PaymentStatusTypes.Successful, bankResponse.RequestCode)).Returns(Task.CompletedTask);

            var handler = new RequestPaymentCommandHandler(this.paymentBankRepostiory.Object, this.paymentGatewayRepostiory.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(PaymentStatusTypes.Unsuccessful, result.PaymentStatus);
            Assert.NotNull(result);
        }

        private RequestPayment fakeValidRequestPayment()
        {
            return new RequestPayment()
            {
                Amount = 300,
                Card = new CardInfo()
                {
                    CardNumber = "351599334071871",
                    Cvv = "412",
                    ExpiryMonth = 2,
                    ExpiryYear = DateTime.Now.Year + 1,
                    Name = "Queiroz"
                },
                CurrencyCode = "BRL"
            };
        }

        private BankResponsePayment fakeBankResponsePayment(Guid requestCode, PaymentStatusTypes status)
        {
            return new BankResponsePayment()
            {
                BankRequestCode = Guid.NewGuid(),
                PaymentStatus = status,
                RequestCode = requestCode
            };
        }
    }
}