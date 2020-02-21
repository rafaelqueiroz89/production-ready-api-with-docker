using System;

using Checkout.PaymentGateway.Domain;

using CreditCardValidator;

using FluentValidation.Results;

using Xunit;

namespace Checkout.PaymentGateway.Api.Tests
{
    public class InputValidations
    {
        private readonly RequestPaymentAggregateValidator requestPaymentAggregateValidator;

        public InputValidations()
        {
            this.requestPaymentAggregateValidator = new RequestPaymentAggregateValidator();
        }

        [Fact]
        public void InvalidRequestDataRaisesErrors()
        {
            //Arrange
            ValidationResult result;

            //Act
            result = this.requestPaymentAggregateValidator.Validate(this.FakeInvalidValidRequestPayment());

            //Assert
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void ValidRequestData()
        {
            //Arrange
            ValidationResult result;

            //Act
            result = this.requestPaymentAggregateValidator.Validate(this.FakeValidRequestPayment());

            //Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [InlineData("00")]
        [InlineData("50004")]
        public void InvalidCvvData(string cvv)
        {
            //Arrange
            ValidationResult result;
            var request = this.FakeValidRequestPayment();
            request.Card.Cvv = cvv.ToString();

            //Act
            result = this.requestPaymentAggregateValidator.Validate(request);

            //Assert
            Assert.NotEmpty(result.Errors);
        }

        [Theory]
        [InlineData("")]
        [InlineData("999xx444")]
        public void InvalidCardNumberDataRaisesError(string cardNumber)
        {
            //Arrange
            ValidationResult result;
            CreditCardDetector creditCardDetector;
            var request = this.FakeValidRequestPayment();
            request.Card.CardNumber = cardNumber;

            //Act
            result = this.requestPaymentAggregateValidator.Validate(request);

            //Assert
            Assert.NotEmpty(result.Errors);
            Assert.Throws<ArgumentException>(() => creditCardDetector = new CreditCardDetector(""));
        }

        [Theory]
        [InlineData(120)]
        [InlineData(0)]
        public void InvalidExpiryMonthDataRaisesError(int month)
        {
            //Arrange
            ValidationResult result;
            var request = this.FakeValidRequestPayment();
            request.Card.ExpiryMonth = month;

            //Act
            result = this.requestPaymentAggregateValidator.Validate(request);

            //Assert
            Assert.NotEmpty(result.Errors);
        }

        [Theory]
        [InlineData(-150)]
        [InlineData(-1)]
        public void InvalidExpiryYearDataRaisesError(int year)
        {
            //Arrange
            var input = DateTime.Now.Year + year;

            ValidationResult result;
            var request = this.FakeValidRequestPayment();
            request.Card.ExpiryYear = input;
            request.Card.ExpiryYear = DateTime.Now.Year - 2;

            //Act
            result = this.requestPaymentAggregateValidator.Validate(request);

            //Assert
            Assert.NotEmpty(result.Errors);
        }

        [Theory]
        [InlineData("")]
        public void InvalidCardNameDataRaisesError(string cardName)
        {
            //Arrange
            ValidationResult result;
            var request = this.FakeValidRequestPayment();
            request.Card.Name = cardName;

            //Act
            result = this.requestPaymentAggregateValidator.Validate(request);

            //Assert
            Assert.NotEmpty(result.Errors);
        }

        [Theory]
        [InlineData(-500)]
        [InlineData(0)]
        public void InvalidAmountDataRaisesError(decimal amount)
        {
            //Arrange
            ValidationResult result;
            var request = this.FakeValidRequestPayment();
            request.Amount = amount;

            //Act
            result = this.requestPaymentAggregateValidator.Validate(request);

            //Assert
            Assert.NotEmpty(result.Errors);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Dollar")]
        [InlineData("br")]
        public void InvaliCurrencyCodeData(string currency)
        {
            //Arrange
            ValidationResult result;
            var request = this.FakeValidRequestPayment();
            request.CurrencyCode = currency;

            //Act
            result = this.requestPaymentAggregateValidator.Validate(request);

            //Assert
            Assert.NotEmpty(result.Errors);
        }

        private RequestPayment FakeValidRequestPayment()
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

        private RequestPayment FakeInvalidValidRequestPayment()
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