using Concierge.Net.Resident.Domain.Base;

namespace Concierge.Net.Resident.Worker.Application.Abstractions.Messaging;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<DomainEvent> events, CancellationToken cancellationToken = default);
}
