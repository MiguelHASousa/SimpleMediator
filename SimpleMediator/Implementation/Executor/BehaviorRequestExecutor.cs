using Microsoft.Extensions.DependencyInjection;
using SimpleMediator.Core.Implementation.Delegate;
using SimpleMediator.Core.Interfaces.Executors;
using SimpleMediator.Core.Interfaces.PipelineBehavior;
using SimpleMediator.Core.Interfaces.Request;

namespace SimpleMediator.Core.Implementation.Executor;

public class BehaviorRequestExecutor(IServiceProvider provider) : IRequestExecutor
{
    public async Task<TResponse> Execute<TRequest, TResponse>(
        TRequest request,
        IRequestHandler<TRequest, TResponse> handler
    )
    where TRequest : IRequest<TResponse>
    {
        var behaviors = provider
            .GetServices<IPipelineBehavior<TRequest, TResponse>>()
            .Reverse()
            .ToList();

        RequestHandlerDelegate<TResponse> next = () => handler.Handle(request);

        foreach (var behavior in behaviors)
        {
            var current = next;
            next = () => behavior.Handle(request, current);
        }

        return await next();
    }
}