using Concierge.Net.Domain.Base;
using Concierge.Net.Domain.Residents;

namespace Concierge.Net.Domain.Packages;

public class PackageCollectedDomainEvent(
    PackageId packageId,
    ResidentId collectedByResidentId,
    UnitId unitId,
    string signatureHash,
    DateTime collectedAtUtc,
    string notificationEmail) : DomainEvent
{
    public PackageId PackageId { get; } = packageId;
    public ResidentId CollectedByResidentId { get; } = collectedByResidentId;
    public string NotificationEmail { get; set; } = notificationEmail;
    public UnitId UnitId { get; } = unitId;
    public string SignatureHash { get; } = signatureHash;
    public DateTime CollectedAtUtc { get; } = collectedAtUtc;
}
