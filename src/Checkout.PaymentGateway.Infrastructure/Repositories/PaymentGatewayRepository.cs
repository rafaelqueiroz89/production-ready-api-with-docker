using System;
using System.Linq;
using System.Threading.Tasks;

using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Domain.Exceptions;
using Checkout.PaymentGateway.Domain.Extensions;
using Checkout.PaymentGateway.Domain.Interfaces;
using Checkout.PaymentGateway.Infrastructure.Database;

using Model = Checkout.PaymentGateway.DataModel.Models;

namespace Checkout.PaymentGateway.Infrastructure
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Checkout.PaymentGateway.Domain.Interfaces.IPaymentGatewayRepository" />
    /// <seealso cref="System.IDisposable" />
    public class PaymentGatewayRepository : IPaymentGatewayRepository, IDisposable
    {
        /// <summary>
        /// The dispoed
        /// </summary>
        private bool disposed;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IPaymentGatewayDbContextUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentGatewayRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public PaymentGatewayRepository(IPaymentGatewayDbContextUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                this.unitOfWork.Context.Dispose();
            }

            this.disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Adds a payment.
        /// </summary>
        /// <param name="requestPayment">The request payment.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<Guid> AddPaymentAsync(RequestPayment requestPayment)
        {
            var payment = new Model.Payment();
            payment.CardNumber = requestPayment.Card.CardNumber;
            payment.CodeStatus = (int)PaymentStatusTypes.Pending;
            payment.Cvv = requestPayment.Card.Cvv.ToString();
            payment.Name = requestPayment.Card.Name;
            payment.ExpiryMonth = requestPayment.Card.ExpiryMonth;
            payment.ExpiryYear = requestPayment.Card.ExpiryYear;
            payment.PaymentCode = Guid.NewGuid(); //this will be changed to the code provided by the bank, this is temporary

            await this.unitOfWork.Context.Payment.AddAsync(payment);
            await this.unitOfWork.CommitAsync();

            return payment.PaymentCode;
        }

        /// <summary>
        /// Updates the payment asynchronous with the Guid from internalPaymentCode to bankPaymentCode
        /// </summary>
        /// <param name="requestPayment">The request payment.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task UpdatePaymentAsync(Guid internalPaymentCode, PaymentStatusTypes paymentStatus, Guid bankPaymentCode)
        {
            var item = (from payment in this.unitOfWork.Context.Payment
                        where payment.PaymentCode == internalPaymentCode
                        select payment).FirstOrDefault();

            if (item == null)
            {
                throw new PaymentNotFoundException("Could not find internal payment code, contact the administration service");
            }
            else
            {
                item.BankPaymentCode = bankPaymentCode;
                item.CodeStatus = (long)paymentStatus;

                this.unitOfWork.Context.Payment.Update(item);
                await this.unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <param name="paymentCode"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<Domain.PaymentAggregate.Payment> GetPaymentAsync(Guid paymentCode)
        {
            var item = await Task.FromResult((from payment in this.unitOfWork.Context.Payment
                                              where payment.PaymentCode == paymentCode
                                              select payment).FirstOrDefault());

            if (item == null)
            {
                throw new PaymentNotFoundException("Could not find internal payment code, contact the administration service");
            }
            else
            {
                return new Domain.PaymentAggregate.Payment
                {
                    PaymentCode = item.PaymentCode,
                    PaymentStatus = (PaymentStatusTypes)item.CodeStatus,
                    CardInfoAggregate = new CardInfo()
                    {
                        CardNumber = item.CardNumber.Masked(0, 8),
                        Cvv = item.Cvv.Masked(0, 2),
                        ExpiryMonth = item.ExpiryMonth,
                        ExpiryYear = item.ExpiryYear,
                        Name = item.Name
                    }
                };
            }
        }
    }
}