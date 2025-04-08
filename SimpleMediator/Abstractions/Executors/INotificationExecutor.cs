using SimpleMediator.Core.Abstractions.Notification;

namespace SimpleMediator.Core.Abstractions.Executors;

public interface INotificationExecutor
{
    Task Execute<TNotification>(
        TNotification notification,
        IEnumerable<INotificationHandler<TNotification>> handlers,
        CancellationToken cancellationToken = default
    )
    where TNotification : INotification;
}