using SimpleMediator.Core.Interfaces.Executors;
using SimpleMediator.Core.Interfaces.HandlerResolver;
using SimpleMediator.Core.Interfaces.Mediator;
using SimpleMediator.Core.Interfaces.Notification;
using SimpleMediator.Core.Interfaces.Request;

namespace SimpleMediator.Core.Implementation.SimpleMediator;

public class SimpleMediator(
    IHandlerResolver resolver,
    IRequestExecutor executor,
    INotificationExecutor notificationExecutor) : ISimpleMediator
{
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = resolver.Resolve(handlerType) 
            ?? throw new InvalidOperationException($"Handler not found for {request.GetType().Name}");
        var typedHandler = (IRequestHandler<IRequest<TResponse>, TResponse>)handler;

        return await executor.Execute((dynamic)request, (dynamic)typedHandler);
    }

    public async Task Publish<TNotification>(TNotification notification) where TNotification : INotification
    {
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(typeof(TNotification));
        var handlersObj = resolver.ResolveAll(handlerType);

        var typedHandlers = handlersObj.Cast<INotificationHandler<TNotification>>();
        await notificationExecutor.Execute(notification, typedHandlers);
    }
}
