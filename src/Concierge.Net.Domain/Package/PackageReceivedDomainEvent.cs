using Concierge.Net.Domain.Base;
using Concierge.Net.Domain.Residents;
using Concierge.Net.Domain.Shared;

namespace Concierge.Net.Domain.Packages;

public class PackageReceivedDomainEvent(PackageId id, ResidentId residentId, Email notificationEmail, UnitId unitId) : DomainEvent
{
    public PackageId Id { get; } = id;
    public ResidentId ResidentId { get; } = residentId;
    public Email NotificationEmail { get; set; } = notificationEmail;
    public UnitId UnitId { get; } = unitId;
}
