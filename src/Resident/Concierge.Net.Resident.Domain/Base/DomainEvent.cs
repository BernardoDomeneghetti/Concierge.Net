namespace Concierge.Net.Resident.Domain.Base;

public abstract class DomainEvent
{
    public Guid EventId { get; private set; }
    public DateTime OccurredOnUtc { get; private set; }

    protected DomainEvent()
    {
        EventId = Guid.NewGuid();
        OccurredOnUtc = DateTime.UtcNow;
    }
}
