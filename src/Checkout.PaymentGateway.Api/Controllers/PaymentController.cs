using System;
using System.Threading.Tasks;

using Checkout.PaymentGateway.CQRS.Commands;
using Checkout.PaymentGateway.CQRS.Queries;
using Checkout.PaymentGateway.Domain;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Checkout.PaymentGateway.Api.Controllers
{
    /// <summary>
    /// The main payment controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/payment")]
    [ApiController]
    [Produces("application/json")]
    public class PaymentController : ControllerBase
    {
        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <exception cref="ArgumentNullException">mediator</exception>
        public PaymentController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <param name="paymentCode">The payment identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetPayment(Guid paymentCode)
        {
            return this.Ok(await this.mediator.Send(new RetrievePaymentQuery(paymentCode)));
        }

        /// <summary>
        /// Requests a payment.
        /// </summary>
        /// <param name="requestPaymentAggregate">The request payment aggregate.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> RequestPayment(RequestPaymentAggregate requestPaymentAggregate)
        {
            return this.Ok(await this.mediator.Send(new RequestPaymentCommand(requestPaymentAggregate)));
        }
    }
}