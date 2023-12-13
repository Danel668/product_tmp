using System.Net;

namespace Homework2.Exceptions
{
    public class ErrorResponse : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }

        public ErrorResponse(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
            ErrorMessage = message;
        }
    }
}
