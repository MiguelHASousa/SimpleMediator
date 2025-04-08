using SimpleMediator.Core.Abstractions.Executors;
using SimpleMediator.Core.Abstractions.Notification;

namespace SimpleMediator.Core.Infrastructure.Executor;

public class NotificationExecutor : INotificationExecutor
{
    public async Task Execute<TNotification>(
        TNotification notification,
        IEnumerable<INotificationHandler<TNotification>> handlers,
        CancellationToken cancellationToken
    ) where TNotification : INotification
    {
        foreach (var handler in handlers)
        {
            await handler.Handle(notification, cancellationToken);
        }
    }
}