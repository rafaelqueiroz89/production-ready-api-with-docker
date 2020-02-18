using System;
using System.Linq;

using Checkout.PaymentGateway.Infrastructure.SeedWork;

using CreditCardValidator;

using FluentValidation;
using FluentValidation.Validators;

namespace Checkout.PaymentGateway.Domain
{
    /// <summary>
    /// This class represents a payment aggregate
    /// </summary>
    public class RequestPayment : IAggregateRoot
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the card.
        /// </summary>
        /// <value>
        /// The card.
        /// </value>
        public CardInfo Card { get; set; }
    }

    /// <summary>
    /// Validator for the request payment aggregate
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Checkout.PaymentGateway.Domain.RequestPayment}" />
    public sealed class RequestPaymentAggregateValidator : AbstractValidator<RequestPayment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestPaymentAggregateValidator"/> class.
        /// </summary>
        public RequestPaymentAggregateValidator()
        {
            this.EnsureInstanceNotNull(this);

            this.RuleFor(r => r.Card.CardNumber).NotEmpty().SetValidator(new CardNumberValidator());
            this.RuleFor(r => r.Card.ExpiryMonth).NotEmpty().InclusiveBetween(1, 12);
            this.RuleFor(r => r.Card.ExpiryYear).NotEmpty().GreaterThanOrEqualTo(DateTime.Now.Year);
            this.RuleFor(r => r.Card.Name).NotEmpty();
            this.RuleFor(r => r.Amount).NotEmpty().GreaterThan(0);

            this.RuleFor(r => r.CurrencyCode).Length(3).NotEmpty();

            this.RuleFor(r => r.Card.Cvv).InclusiveBetween(100, 9999);
        }

        /// <summary>
        /// Validates the card number
        /// </summary>
        /// <seealso cref="FluentValidation.Validators.PropertyValidator" />
        private sealed class CardNumberValidator : PropertyValidator
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CardNumberValidator"/> class.
            /// </summary>
            public CardNumberValidator() : base("Invalid card number")
            {
            }

            /// <summary>
            /// Returns true if ... is valid.
            /// </summary>
            /// <param name="context">The context.</param>
            /// <returns>
            ///   <c>true</c> if the specified context is valid; otherwise, <c>false</c>.
            /// </returns>
            protected override bool IsValid(PropertyValidatorContext context)
            {
                var cardNumber = context.PropertyValue.ToString();

                if (string.IsNullOrEmpty(cardNumber))
                {
                    return false;
                }

                if (!cardNumber.All(char.IsDigit))
                {
                    return false;
                }

                var detector = new CreditCardDetector(cardNumber);
                return detector.IsValid();
            }
        }
    }
}