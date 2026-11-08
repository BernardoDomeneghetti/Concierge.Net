using Concierge.Net.Package.Domain.Base;
using Concierge.Net.Package.Domain.Residents;
using Concierge.Net.Package.Domain.Shared;

namespace Concierge.Net.Package.Domain.Packages;

public sealed class Package : AggregateRoot<PackageId>
{
    public ResidentId ResidentId { get; private set; }
    public UnitId UnitId { get; private set; }
    public DateTime ReceivedAtUtc { get; private set; }
    public DateTime? CollectedAtUtc { get; private set; }
    public DateTime? CancelledAtUtc { get; private set; }

    public bool IsCollected => CollectedAtUtc.HasValue;
    public bool IsCancelled => CancelledAtUtc.HasValue;

    private Package(
        PackageId id,
        ResidentId residentId,
        UnitId unitId)
        : base(id)
    {
        ResidentId = residentId;
        UnitId = unitId;
        ReceivedAtUtc = DateTime.UtcNow;
    }

    public static Package Receive(
        ResidentId residentId,
        Email notificationEmail,
        UnitId unitId)
    {
        var package = new Package(
            PackageId.New(),
            residentId,
            unitId);

        package.AddDomainEvent(
            new PackageReceivedDomainEvent(package.Id, residentId, notificationEmail, unitId));

        return package;
    }

    public void MarkAsCollected(
        ResidentId collectedBy,
        string signatureHash,
        DateTime collectedAtUtc,
        Email notificationEmail)
    {
        if (IsCollected)
            throw new InvalidOperationException("Package already collected.");

        if (IsCancelled)
            throw new InvalidOperationException("Cannot collect a cancelled package.");

        CollectedAtUtc = collectedAtUtc;

        AddDomainEvent(
            new PackageCollectedDomainEvent(Id, collectedBy, UnitId, signatureHash, collectedAtUtc, notificationEmail));
    }

    public void Cancel(
        string reason,
        DateTime cancelledAtUtc,
        Email notificationEmail)
    {
        if (IsCollected)
            throw new InvalidOperationException("Cannot cancel a collected package.");

        if (IsCancelled)
            throw new InvalidOperationException("Package already cancelled.");

        CancelledAtUtc = cancelledAtUtc;

        AddDomainEvent(
            new PackageCancelledDomainEvent(Id, ResidentId, UnitId, reason, cancelledAtUtc, notificationEmail));
    }
}
