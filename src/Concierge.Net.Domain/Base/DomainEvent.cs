using System;

namespace Concierge.Net.Domain.Base;

public abstract class DomainEvent
{
    public Guid EventId { get; }
    public DateTime OccurredOnUtc { get; }

    protected DomainEvent()
    {
        EventId = Guid.NewGuid();
        OccurredOnUtc = DateTime.UtcNow;
    }
}
