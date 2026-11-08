using Concierge.Net.Resident.Domain.Base;
using Concierge.Net.Resident.Worker.Application.Abstractions.Repositories;
using MongoDB.Driver;

namespace Concierge.Net.Resident.Worker.Infraestructure.Outbox;

public class OutboxRepository(IMongoDatabase database) : IOutboxRepository
{
    private readonly IMongoCollection<OutboxMessage> _collection = database.GetCollection<OutboxMessage>("outbox");

    public async Task AddAsync(params DomainEvent[] events)
    {
        if (events.Length == 0)
            return;

        var messages = events.Select(evt => new OutboxMessage
        {
            Id = evt.EventId,
            Event = evt,
            OccurredOnUtc = evt.OccurredOnUtc
        });

        await _collection.InsertManyAsync(messages);
    }

    public async Task<IEnumerable<DomainEvent>> GetUnprocessed()
    {
        var unprocessed = await _collection
            .Find(m => m.ProcessedAtUtc == null)
            .SortBy(m => m.OccurredOnUtc)
            .ToListAsync();

        return unprocessed.Select(m => m.Event);
    }

    public async Task MarkAsProcessed(Guid eventId)
    {
        var update = Builders<OutboxMessage>.Update.Set(m => m.ProcessedAtUtc, DateTime.UtcNow);
        await _collection.UpdateOneAsync(m => m.Id == eventId, update);
    }
}
