using SimpleMediator.Core.Interfaces.Notification;

namespace SimpleMediator.Core.Interfaces.Executors;

public interface INotificationExecutor
{
    Task Execute<TNotification>(
        TNotification notification,
        IEnumerable<INotificationHandler<TNotification>> handlers
    )
    where TNotification : INotification;
}