using SimpleMediator.Core.Interfaces.Executors;
using SimpleMediator.Core.Interfaces.Notification;

namespace SimpleMediator.Core.Implementation.Executor;

public class NotificationExecutor : INotificationExecutor
{
    public async Task Execute<TNotification>(
        TNotification notification,
        IEnumerable<INotificationHandler<TNotification>> handlers
    ) where TNotification : INotification
    {
        foreach (var handler in handlers)
        {
            await handler.Handle(notification);
        }
    }
}