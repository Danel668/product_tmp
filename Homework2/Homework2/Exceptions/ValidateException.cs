using Grpc.Core;

namespace Homework2.Exceptions
{
    public class ValidateException : RpcException
    {
        public ValidateException(Status status, string message) : base(status, message)
        {
        }
    }
}
