namespace Concierge.Net.Domain.Base;

public interface IAggregateRoot
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}

public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id), IAggregateRoot where TId : Id<TId>
{
    private readonly List<DomainEvent> _domainEvents = [];

    public IReadOnlyCollection<DomainEvent> DomainEvents
        => _domainEvents.AsReadOnly();

    protected void AddDomainEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}
