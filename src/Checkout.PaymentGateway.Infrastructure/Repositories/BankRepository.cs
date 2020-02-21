using System;
using System.Threading.Tasks;

using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Domain.Interfaces;
using Checkout.PaymentGateway.Domain.PaymentAggregate;
using Checkout.PaymentGateway.Infrastructure.Database;

namespace Checkout.PaymentGateway.Infrastructure
{
    public class BankRepository : IBankRepository, IDisposable
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
        /// Initializes a new instance of the <see cref="BankRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public BankRepository(IPaymentGatewayDbContextUnitOfWork unitOfWork)
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
        /// Requests the payment.
        /// </summary>
        /// <param name="requestPayment"></param>
        /// <returns></returns>
        public async Task<BankResponsePayment> RequestPaymentAsync(RequestPayment requestPayment)
        {
            var randomStatus = new Random();

            return await Task.FromResult(new BankResponsePayment()
            {
                PaymentStatus = (PaymentStatusTypes)randomStatus.Next(0, 2), //this will return either Success or Unsuccess
                BankRequestCode = Guid.NewGuid()
            });
        }
    }
}