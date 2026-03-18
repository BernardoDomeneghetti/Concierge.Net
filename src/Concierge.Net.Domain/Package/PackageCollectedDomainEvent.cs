using Concierge.Net.Domain.Base;

namespace Concierge.Net.Domain.Packages;

public class PackageCollectedDomainEvent(PackageId id): DomainEvent
{
    public PackageId Id { get; } = id;
}
