using System;
using Concierge.Net.Application.Abstractions.Repositories;
using Concierge.Net.Domain.Base;

namespace Concierge.Net.Infraestructure.Outbox;

public class OutboxRepository: IOutboxRepository
{
    public Task AddAsync(params DomainEvent[] events)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DomainEvent>> GetUnprocessed()
    {
        throw new NotImplementedException();
    }

    public Task MarkAsProcessed(Guid eventId)
    {
        throw new NotImplementedException();
    }
}
