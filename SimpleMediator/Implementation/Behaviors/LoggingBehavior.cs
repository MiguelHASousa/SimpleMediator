using SimpleMediator.Core.Implementation.Delegate;
using SimpleMediator.Core.Interfaces.PipelineBehavior;
using SimpleMediator.Core.Interfaces.Request;

namespace SimpleMediator.Core.Implementation.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
    {
        Console.WriteLine($"[LOG] Handling {typeof(TRequest).Name}");

        var response = await next();

        Console.WriteLine($"[LOG] Handled {typeof(TResponse).Name}");

        return response;
    }
}