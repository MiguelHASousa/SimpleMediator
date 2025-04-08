using SimpleMediator.Core.Abstractions.Notification;
using SimpleMediator.Core.Abstractions.Request;

namespace SimpleMediator.Core.Abstractions.Mediator;

public interface IMediator
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification;
}