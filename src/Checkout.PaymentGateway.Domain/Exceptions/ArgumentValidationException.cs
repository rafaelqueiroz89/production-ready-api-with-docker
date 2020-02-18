using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using FluentValidation.Results;

namespace Checkout.PaymentGateway.Domain.Exceptions
{
    /// <summary>
    /// Throw a new exception of type PaymentNotFound
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class ArgumentValidationException : Exception
    {
        public readonly IList<ValidationFailure> Errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ArgumentValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ArgumentValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentValidationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errors">The errors.</param>
        public ArgumentValidationException(string message, IList<ValidationFailure> errors) : this(message)
        {
            this.Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentNotFoundException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected ArgumentValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}