using Homework2.Exceptions;
using System.Net;

namespace Homework2.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string message = exception.Message;
            HttpStatusCode statusCode = exception switch
            {
                ArgumentNullException => HttpStatusCode.BadRequest,
                ArgumentException => HttpStatusCode.BadRequest,
                InvalidOperationException => HttpStatusCode.PreconditionFailed,
                FileNotFoundException => HttpStatusCode.NotFound,
                KeyNotFoundException => HttpStatusCode.NotFound,
                NotSupportedException => HttpStatusCode.NotImplemented,
                TimeoutException => HttpStatusCode.GatewayTimeout,
                ValidateException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(new ErrorResponse(statusCode, message).ToString());
        }
    }
}
