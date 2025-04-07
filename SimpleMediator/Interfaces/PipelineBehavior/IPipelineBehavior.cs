using SimpleMediator.Core.Implementation.Delegate;
using SimpleMediator.Core.Interfaces.Request;

namespace SimpleMediator.Core.Interfaces.PipelineBehavior;

public interface IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next
    );
}