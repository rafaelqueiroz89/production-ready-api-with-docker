using System;
using System.Collections.Generic;

namespace Checkout.PaymentGateway.DataModel.Models
{
    public class PaymentStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentStatus"/> class.
        /// </summary>
        public PaymentStatus()
        {
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;

            this.Payment = new HashSet<PaymentRequest>();
        }

        /// <summary>
        /// Gets or sets the approved position.
        /// </summary>
        /// <value>The approved position.</value>
        public ICollection<PaymentRequest> Payment { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public long Code { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
    }
}