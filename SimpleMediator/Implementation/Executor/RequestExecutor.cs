using SimpleMediator.Core.Interfaces.Executors;
using SimpleMediator.Core.Interfaces.Request;

namespace SimpleMediator.Core.Implementation.Executor;

public class RequestExecutor : IRequestExecutor
{
    public Task<TResponse> Execute<TRequest, TResponse>(
        TRequest request,
        IRequestHandler<TRequest, TResponse> handler) where TRequest : IRequest<TResponse>
    {
        return handler.Handle(request);
    }
}