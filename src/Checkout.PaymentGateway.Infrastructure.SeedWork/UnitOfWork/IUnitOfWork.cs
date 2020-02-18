using System;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Infrastructure.SeedWork
{
    /// <summary>
    /// <see cref="IUnitOfWork"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.IDisposable"/>
    public interface IUnitOfWork<out T> : IDisposable where T : IDisposable
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        T Context { get; }

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();
    }
}