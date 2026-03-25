using Concierge.Net.Domain.Base;
using Concierge.Net.Domain.Residents;

namespace Concierge.Net.Domain.Packages;

public class PackageReceivedDomainEvent(PackageId id, ResidentId residentId, string notificationEmail, UnitId unitId) : DomainEvent
{
    public PackageId Id { get; } = id;
    public ResidentId ResidentId { get; } = residentId;
    public string NotificationEmail { get; set; } = notificationEmail;
    public UnitId UnitId { get; } = unitId;
}
