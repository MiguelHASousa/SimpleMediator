using SimpleMediator.Core.Interfaces.Request;

namespace SimpleMediator.Core.Interfaces.Executors;

public interface IRequestExecutor
{
    Task<TResponse> Execute<TRequest, TResponse>(
        TRequest request,
        IRequestHandler<TRequest, TResponse> handler
    )
    where TRequest : IRequest<TResponse>;
}
