using Concierge.Net.Resident.Domain.Base;
using Concierge.Net.Resident.Domain.Shared;

namespace Concierge.Net.Resident.Domain.Residents;

public class ResidentInactivatedDomainEvent(
    ResidentId residentId,
    string fullName,
    Email email,
    PhoneNumber mobilePhone,
    UnitId unitId,
    string reason,
    DateTime inactivatedAtUtc,
    NotificationChannel notificationChannel) : DomainEvent
{
    public ResidentId ResidentId { get; } = residentId;
    public string FullName { get; } = fullName;
    public Email Email { get; } = email;
    public PhoneNumber MobilePhone { get; } = mobilePhone;
    public UnitId UnitId { get; } = unitId;
    public string Reason { get; } = reason;
    public DateTime InactivatedAtUtc { get; } = inactivatedAtUtc;
    public NotificationChannel NotificationChannel { get; } = notificationChannel;
}
