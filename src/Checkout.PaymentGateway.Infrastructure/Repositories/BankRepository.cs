using System;
using System.Threading.Tasks;

using Checkout.PaymentGateway.Domain.Interfaces;
using Checkout.PaymentGateway.Domain.PaymentAggregate;

namespace Checkout.PaymentGateway.Infrastructure
{
    public class BankRepository : IBankRepository, IDisposable
    {
        /// <summary>
        /// The dispoed
        /// </summary>
        private bool disposed;

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

        /// <summary>
        /// Requests the payment to the bank, we should change the implementation here to retrieve from a real source
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<BankResponsePayment> RequestPaymentAsync()
        {
            return await Task.FromResult(new BankResponsePayment()
            {
                PaymentStatus = Domain.PaymentStatusTypes.Successful,
                RequestCode = Guid.NewGuid()
            });
        }
    }
}