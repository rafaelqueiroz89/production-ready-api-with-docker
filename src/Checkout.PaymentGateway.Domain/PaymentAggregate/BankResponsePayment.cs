﻿using System;

using Checkout.PaymentGateway.Infrastructure.SeedWork;

namespace Checkout.PaymentGateway.Domain.PaymentAggregate
{
    /// <summary>
    /// Domain object from the bank
    /// </summary>
    /// <seealso cref="Checkout.PaymentGateway.Infrastructure.SeedWork.IAggregateRoot" />
    public class BankResponsePayment : IAggregateRoot
    {
        /// <summary>
        /// Gets or sets the request code.
        /// </summary>
        /// <value>
        /// The request code.
        /// </value>
        public Guid RequestCode { get; set; }

        /// <summary>
        /// Gets or sets the payment status.
        /// </summary>
        /// <value>
        /// The payment status.
        /// </value>
        public PaymentStatusTypes PaymentStatus { get; set; }
    }
}