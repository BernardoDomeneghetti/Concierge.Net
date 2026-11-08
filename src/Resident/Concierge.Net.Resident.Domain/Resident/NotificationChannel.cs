namespace Concierge.Net.Resident.Domain.Residents;

[Flags]
public enum NotificationChannel
{
    None = 0,
    Email = 1,
    Sms = 2,
    Push = 4,
    All = Email | Sms | Push
}
