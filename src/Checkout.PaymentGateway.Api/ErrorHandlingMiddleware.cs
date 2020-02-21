namespace Checkout.PaymentGateway.Api
{
    using System;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using Checkout.PaymentGateway.Domain.Exceptions;

    using Microsoft.AspNetCore.Http;

    using Newtonsoft.Json;

    using Serilog;

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.BadRequest;
            string details = String.Empty;
            StringBuilder stringBuilder = new StringBuilder();

            switch (ex.GetType().Name)
            {
                case nameof(PaymentNotFoundException):
                    code = HttpStatusCode.NotFound;
                    break;

                case nameof(PaymentRefusedException):
                    code = HttpStatusCode.Unauthorized;
                    break;

                case nameof(ArgumentValidationException):
                    code = HttpStatusCode.BadRequest;

                    foreach (var error in ((ArgumentValidationException)ex).Errors)
                    {
                        stringBuilder.Append(error.ToString())
                                     .Append(", ");
                    }

                    break;
            }

            if (ex.InnerException != null)
            {
                details = ex.InnerException.Message;
            }
            else
            {
                if (stringBuilder.Length > 0)
                {
                    details = stringBuilder.ToString();
                }
            }

            var result = JsonConvert.SerializeObject(new
            {
                error = ex.Message,
                details
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            Log.Error(ex, details);

            return context.Response.WriteAsync(result);
        }
    }
}