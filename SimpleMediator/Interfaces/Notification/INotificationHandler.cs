namespace SimpleMediator.Core.Interfaces.Notification;

public interface INotificationHandler<TNotification>
    where TNotification : INotification
{
    Task Handle(TNotification notification);
}
