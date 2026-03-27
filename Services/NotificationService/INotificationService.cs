namespace HRMS.Services;

public interface INotificationService
{
    event Action<NotificationMessage>? OnMessage;
    void Notify(string message, NotificationType type = NotificationType.Success);
    void NotifyError(string message, string? details = null);
}

public class NotificationMessage
{
    public string Message { get; set; } = "";
    public string? Details { get; set; }
    public NotificationType Type { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}

public enum NotificationType
{
    Success,
    Error,
    Info,
    Warning,
    SystemError
}
