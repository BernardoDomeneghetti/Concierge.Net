using Concierge.Net.Domain.Base;

namespace Concierge.Net.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent>where TEvent : DomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}
