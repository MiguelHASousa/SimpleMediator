using SimpleMediator.Core.Abstractions.Request;
using SimpleMediator.Core.Infrastructure.Delegate;

namespace SimpleMediator.Core.Abstractions.PipelineBehavior;

public interface IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next
    );
}