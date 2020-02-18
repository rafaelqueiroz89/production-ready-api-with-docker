using System;
using System.Threading.Tasks;

using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Domain.Interfaces;
using Checkout.PaymentGateway.Domain.PaymentAggregate;

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

        public PaymentGatewayRepository()
        {
        }

        /// <summary>
        /// The unit of work
        /// </summary>
        //private readonly IMisDbContextUnitOfWork unitOfWork;

        public Task<Payment> GetPayment(Guid paymentCode)
        {
            throw new NotImplementedException();
        }

        public Task RequestPayment(RequestPayment requestPayment)
        {
            throw new NotImplementedException();
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
                //this.unitOfWork.Context.Dispose();
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
    }
}