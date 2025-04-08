using SimpleMediator.Core.Abstractions.Request;

namespace SimpleMediator.Core.Abstractions.Executors;

public interface IRequestExecutor
{
    Task<TResponse> Execute<TRequest, TResponse>(
        TRequest request,
        IRequestHandler<TRequest, TResponse> handler,
        CancellationToken cancellationToken = default
    )
    where TRequest : IRequest<TResponse>;
}
