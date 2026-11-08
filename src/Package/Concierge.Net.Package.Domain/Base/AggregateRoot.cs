namespace Concierge.Net.Package.Domain.Base;

public interface IAggregateRoot
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}

public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id), IAggregateRoot where TId : Id<TId>
{
    private List<DomainEvent>? _domainEvents = [];

    private List<DomainEvent> Events => _domainEvents ??= [];

    public IReadOnlyCollection<DomainEvent> DomainEvents
        => Events.AsReadOnly();

    protected void AddDomainEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent
        => Events.Add(domainEvent);

    public void ClearDomainEvents()
        => Events.Clear();
}
