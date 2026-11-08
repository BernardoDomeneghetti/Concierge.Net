using Concierge.Net.Resident.Domain.Base;
using Concierge.Net.Resident.Domain.Shared;

namespace Concierge.Net.Resident.Domain.Residents;

public class ResidentUpdatedDomainEvent(
    ResidentId residentId,
    string fullName,
    Email email,
    PhoneNumber mobilePhone,
    UnitId unitId,
    ResidentType residentType,
    NotificationChannel notificationChannel,
    DateTime updatedAtUtc) : DomainEvent
{
    public ResidentId ResidentId { get; } = residentId;
    public string FullName { get; } = fullName;
    public Email Email { get; } = email;
    public PhoneNumber MobilePhone { get; } = mobilePhone;
    public UnitId UnitId { get; } = unitId;
    public ResidentType ResidentType { get; } = residentType;
    public NotificationChannel NotificationChannel { get; } = notificationChannel;
    public DateTime UpdatedAtUtc { get; } = updatedAtUtc;
}
