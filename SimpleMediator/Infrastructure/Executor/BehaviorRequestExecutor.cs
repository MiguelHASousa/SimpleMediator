using Microsoft.Extensions.DependencyInjection;
using SimpleMediator.Core.Abstractions.Executors;
using SimpleMediator.Core.Abstractions.PipelineBehavior;
using SimpleMediator.Core.Abstractions.Request;
using SimpleMediator.Core.Infrastructure.Delegate;

namespace SimpleMediator.Core.Infrastructure.Executor;

public class BehaviorRequestExecutor(IServiceProvider provider) : IRequestExecutor
{
    public async Task<TResponse> Execute<TRequest, TResponse>(
        TRequest request,
        IRequestHandler<TRequest, TResponse> handler,
        CancellationToken cancellationToken = default
    )
    where TRequest : IRequest<TResponse>
    {
        var behaviors = provider
            .GetServices<IPipelineBehavior<TRequest, TResponse>>()
            .Reverse()
            .ToList();

        RequestHandlerDelegate<TResponse> next = () => handler.Handle(request, cancellationToken);

        foreach (var behavior in behaviors)
        {
            var current = next;
            next = () => behavior.Handle(request, cancellationToken, current);
        }

        return await next();
    }
}