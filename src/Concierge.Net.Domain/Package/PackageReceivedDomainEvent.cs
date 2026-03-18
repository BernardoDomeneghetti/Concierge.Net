using System;
using Concierge.Net.Domain.Base;

namespace Concierge.Net.Domain.Packages;

public class PackageReceivedDomainEvent(PackageId id) : DomainEvent
{
    public PackageId Id { get; } = id;
}
