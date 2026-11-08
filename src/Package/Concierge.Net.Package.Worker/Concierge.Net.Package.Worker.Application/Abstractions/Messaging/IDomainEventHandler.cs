using Concierge.Net.Package.Domain.Base;

namespace Concierge.Net.Package.Worker.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent>where TEvent : DomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}
