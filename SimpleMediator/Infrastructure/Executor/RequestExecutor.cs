using SimpleMediator.Core.Abstractions.Executors;
using SimpleMediator.Core.Abstractions.Request;

namespace SimpleMediator.Core.Infrastructure.Executor;

public class RequestExecutor : IRequestExecutor
{
    public Task<TResponse> Execute<TRequest, TResponse>(
        TRequest request,
        IRequestHandler<TRequest, TResponse> handler,
        CancellationToken cancellationToken) where TRequest : IRequest<TResponse>
    {
        return handler.Handle(request, cancellationToken);
    }
}