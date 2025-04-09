using SimpleMediator.Core.Abstractions.Executors;
using SimpleMediator.Core.Abstractions.HandlerResolver;
using SimpleMediator.Core.Abstractions.Mediator;
using SimpleMediator.Core.Abstractions.Notification;
using SimpleMediator.Core.Abstractions.Request;

namespace SimpleMediator.Core.Infrastructure.Mediator;

public class Mediator(
    IHandlerResolver resolver,
    IRequestExecutor executor,
    INotificationExecutor notificationExecutor) : IMediator
{
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = resolver.Resolve(handlerType) 
            ?? throw new InvalidOperationException($"Handler not found for {request.GetType().Name}");

        return await executor.Execute((dynamic)request, (dynamic)handler, cancellationToken);
    }

    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
    {
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(typeof(TNotification));
        var handlersObj = resolver.ResolveAll(handlerType);

        await notificationExecutor.Execute(notification, (dynamic)handlersObj, cancellationToken);
    }
}