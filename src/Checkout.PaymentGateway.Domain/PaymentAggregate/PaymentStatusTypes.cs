namespace Checkout.PaymentGateway.Domain
{
    /// <summary>
    /// This enum represents all the status a payment can have
    /// </summary>
    public enum PaymentStatusTypes
    {
        /// <summary>
        /// The successful status
        /// </summary>
        Successful = 0,

        /// <summary>
        /// The unsuccessful status
        /// </summary>
        Unsuccessful = 1
    }
}