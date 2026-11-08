using Concierge.Net.Resident.Domain.Base;

namespace Concierge.Net.Resident.Worker.Application.Abstractions.Repositories;

public interface IOutboxRepository
{
    Task AddAsync(params DomainEvent[] events);
    Task<IEnumerable<DomainEvent>> GetUnprocessed();
    Task MarkAsProcessed(Guid eventId);
}
