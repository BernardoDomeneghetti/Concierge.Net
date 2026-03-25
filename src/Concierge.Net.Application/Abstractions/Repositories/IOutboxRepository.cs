using System;
using Concierge.Net.Domain.Base;

namespace Concierge.Net.Application.Abstractions.Repositories;

public interface IOutboxRepository
{
    Task AddAsync(params DomainEvent[] events);
    Task<IEnumerable<DomainEvent>> GetUnprocessed();
    Task MarkAsProcessed(Guid eventId);
}
