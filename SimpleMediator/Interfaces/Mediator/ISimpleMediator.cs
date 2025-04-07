using SimpleMediator.Core.Interfaces.Notification;
using SimpleMediator.Core.Interfaces.Request;

namespace SimpleMediator.Core.Interfaces.Mediator;

public interface ISimpleMediator
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    Task Publish<TNotification>(TNotification notification) where TNotification : INotification;
}