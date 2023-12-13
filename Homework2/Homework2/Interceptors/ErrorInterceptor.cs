using Grpc.Core;
using Homework2.Exceptions;

namespace Homework2.Interceptors
{
    public class ErrorInterceptor : Grpc.Core.Interceptors.Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>
            (TRequest request, ServerCallContext context, UnaryServerMethod<TRequest,
                TResponse> continuation)
        {
            try
            {
                var response = await continuation(request, context);
                return response;
            }
            catch (Exception ex)
            {
                StatusCode statusCode = ex switch
                {
                    ArgumentNullException => StatusCode.InvalidArgument,
                    ArgumentException => StatusCode.InvalidArgument,
                    InvalidOperationException => StatusCode.FailedPrecondition,
                    FileNotFoundException => StatusCode.NotFound,
                    KeyNotFoundException => StatusCode.NotFound,
                    NotSupportedException => StatusCode.Unimplemented,
                    TimeoutException => StatusCode.DeadlineExceeded,
                    ValidateException =>  StatusCode.InvalidArgument,
                    _ => StatusCode.Internal,
                };
                    
                throw new RpcException(new Status(statusCode, ex.Message));
            }
        }
    }
}
