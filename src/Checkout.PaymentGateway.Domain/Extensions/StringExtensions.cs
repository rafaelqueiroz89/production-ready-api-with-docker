namespace Checkout.PaymentGateway.Domain.Extensions
{
    /// <summary>
    /// Extension class to mask a string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Maskeds the specified start.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static string Masked(this string source, int start, int count)
        {
            return source.Masked('x', start, count);
        }

        /// <summary>
        /// Maskeds the specified mask value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="maskValue">The mask value.</param>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static string Masked(this string source, char maskValue, int start, int count)
        {
            var firstPart = source.Substring(0, start);
            var lastPart = source.Substring(start + count);
            var middlePart = new string(maskValue, count);

            return firstPart + middlePart + lastPart;
        }
    }
}