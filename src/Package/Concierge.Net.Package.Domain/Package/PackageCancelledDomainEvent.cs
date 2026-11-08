using Concierge.Net.Package.Domain.Base;
using Concierge.Net.Package.Domain.Residents;
using Concierge.Net.Package.Domain.Shared;

namespace Concierge.Net.Package.Domain.Packages;

public class PackageCancelledDomainEvent(
    PackageId packageId,
    ResidentId residentId,
    UnitId unitId,
    string reason,
    DateTime cancelledAtUtc,
    Email notificationEmail) : DomainEvent
{
    public PackageId PackageId { get; } = packageId;
    public ResidentId ResidentId { get; } = residentId;
    public UnitId UnitId { get; } = unitId;
    public string Reason { get; } = reason;
    public DateTime CancelledAtUtc { get; } = cancelledAtUtc;
    public Email NotificationEmail { get; } = notificationEmail;
}
