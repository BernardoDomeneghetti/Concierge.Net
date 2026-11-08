using Concierge.Net.Package.Domain.Base;
using Concierge.Net.Package.Domain.Residents;
using Concierge.Net.Package.Domain.Shared;

namespace Concierge.Net.Package.Domain.Packages;

public class PackageReceivedDomainEvent(
    PackageId packageId,
    ResidentId residentId,
    Email notificationEmail,
    UnitId unitId) : DomainEvent
{
    public PackageId PackageId { get; } = packageId;
    public ResidentId ResidentId { get; } = residentId;
    public Email NotificationEmail { get; set; } = notificationEmail;
    public UnitId UnitId { get; } = unitId;
}
