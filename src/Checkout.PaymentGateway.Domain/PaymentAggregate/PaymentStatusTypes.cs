namespace Checkout.PaymentGateway.Domain
{
    /// <summary>
    /// This enum represents all the status a payment can have
    /// </summary>
    public enum PaymentStatusTypes
    {
        /// <summary>
        /// The pending status
        /// </summary>
        Pending = 0,

        /// <summary>
        /// The successful status
        /// </summary>
        Successful = 1,

        /// <summary>
        /// The unsuccessful status
        /// </summary>
        Unsuccessful = 2
    }
}