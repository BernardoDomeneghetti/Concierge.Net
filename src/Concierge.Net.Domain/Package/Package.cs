using System;
using Concierge.Net.Domain.Base;
using Concierge.Net.Domain.Residents;

namespace Concierge.Net.Domain.Packages;

public sealed class Package : AggregateRoot<PackageId>
{
    public ResidentId ResidentId { get; private set; }
    public UnitId UnitId { get; private set; }
    public DateTime ReceivedAtUtc { get; private set; }
    public DateTime? CollectedAtUtc { get; private set; }

    public bool IsCollected => CollectedAtUtc.HasValue;

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
        UnitId unitId)
    {
        var package = new Package(
            PackageId.New(),
            residentId,
            unitId);

        package.AddDomainEvent(
            new PackageReceivedDomainEvent(package.Id));

        return package;
    }

    public void MarkAsCollected()
    {
        if (IsCollected)
            throw new InvalidOperationException("Package already collected.");

        CollectedAtUtc = DateTime.UtcNow;

        AddDomainEvent( new PackageCollectedDomainEvent(Id));
    }
}
