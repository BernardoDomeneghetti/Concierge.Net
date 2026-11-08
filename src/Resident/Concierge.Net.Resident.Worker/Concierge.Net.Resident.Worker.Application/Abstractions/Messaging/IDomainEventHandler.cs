using Concierge.Net.Resident.Domain.Base;

namespace Concierge.Net.Resident.Worker.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent> where TEvent : DomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}
