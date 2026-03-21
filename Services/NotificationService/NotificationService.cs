namespace HRMS.Services;

public class NotificationService : INotificationService
{
    public event Action<NotificationMessage>? OnMessage;

    public void Notify(string message, NotificationType type = NotificationType.Success)
    {
        OnMessage?.Invoke(new NotificationMessage { Message = message, Type = type });
    }
}
