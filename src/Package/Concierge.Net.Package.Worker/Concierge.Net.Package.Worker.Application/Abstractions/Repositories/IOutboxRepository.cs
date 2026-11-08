using System;
using Concierge.Net.Package.Domain.Base;

namespace Concierge.Net.Package.Worker.Application.Abstractions.Repositories;

public interface IOutboxRepository
{
    Task AddAsync(params DomainEvent[] events);
    Task<IEnumerable<DomainEvent>> GetUnprocessed();
    Task MarkAsProcessed(Guid eventId);
}
