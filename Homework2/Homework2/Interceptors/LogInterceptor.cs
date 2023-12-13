using Grpc.Core;

namespace Homework2.Interceptors
{
    public class LogInterceptor : Grpc.Core.Interceptors.Interceptor
    {
        private readonly ILogger<LogInterceptor> _logger;

        public LogInterceptor(ILogger<LogInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>
            (TRequest request, ServerCallContext context, UnaryServerMethod<TRequest,
                TResponse> continuation)
        {
            _logger.LogInformation("Был вызван метод: {MethodName}. Запрос: {Request}",
                context.Method, request);

            var response = await continuation(request, context);

            _logger.LogInformation("Ответ Метода {MethodName}: {Response}",
                context.Method, response);

            return response;
        }
    }
}
