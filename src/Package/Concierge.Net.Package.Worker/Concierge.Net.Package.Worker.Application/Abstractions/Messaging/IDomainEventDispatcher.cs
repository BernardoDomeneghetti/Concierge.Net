using Concierge.Net.Package.Domain.Base;

namespace Concierge.Net.Package.Worker.Application.Abstractions.Messaging;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<DomainEvent> events, CancellationToken cancellationToken = default);
}
