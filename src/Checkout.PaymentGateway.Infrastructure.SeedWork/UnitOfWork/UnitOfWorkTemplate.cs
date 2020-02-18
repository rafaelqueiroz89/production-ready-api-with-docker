using System;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Infrastructure.SeedWork
{
    /// <summary>
    /// Abstraction of the unit of work pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Checkout.PaymentGateway.Infrastructure.IUnitOfWork{T}" />
    public abstract class UnitOfWorkTemplate<T> : IUnitOfWork<T> where T : IDisposable
    {
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkTemplate{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected UnitOfWorkTemplate(T context)
        {
            this.disposed = false;

            this.Context = context;
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public T Context { get; }

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <returns></returns>
        public abstract Task CommitAsync();

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
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            this.disposed = true;
        }
    }
}