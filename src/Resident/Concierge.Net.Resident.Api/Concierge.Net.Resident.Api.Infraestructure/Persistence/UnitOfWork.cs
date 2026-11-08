using Concierge.Net.Resident.Api.Application.Abstractions.Repositories;
using Concierge.Net.Resident.Api.Infraestructure.Outbox;
using Concierge.Net.Resident.Domain.Base;
using MongoDB.Driver;

namespace Concierge.Net.Resident.Api.Infraestructure.Persistence;

public class UnitOfWork(IMongoClient mongoClient, IMongoDatabase database) : IUnitOfWork
{
    private readonly IMongoClient _mongoClient = mongoClient;
    private readonly IMongoCollection<OutboxMessage> _outboxCollection = database.GetCollection<OutboxMessage>("outbox");

    private readonly List<Func<IClientSessionHandle, CancellationToken, Task>> _operations = [];
    private readonly List<IAggregateRoot> _trackedAggregates = [];

    public void RegisterOperation(
        IAggregateRoot aggregate,
        Func<IClientSessionHandle, CancellationToken, Task> operation)
    {
        _trackedAggregates.Add(aggregate);
        _operations.Add(operation);
    }

    public async Task CommitAsync(CancellationToken ct = default)
    {
        using var session = await _mongoClient.StartSessionAsync(cancellationToken: ct);
        session.StartTransaction();

        try
        {
            foreach (var operation in _operations)
            {
                await operation(session, ct);
            }

            var outboxMessages = _trackedAggregates
                .SelectMany(a => a.DomainEvents)
                .Select(evt => new OutboxMessage
                {
                    Id = evt.EventId,
                    Event = evt,
                    OccurredOnUtc = evt.OccurredOnUtc
                })
                .ToList();

            if (outboxMessages.Count > 0)
            {
                await _outboxCollection.InsertManyAsync(session, outboxMessages, cancellationToken: ct);
            }

            await session.CommitTransactionAsync(ct);

            foreach (var aggregate in _trackedAggregates)
            {
                aggregate.ClearDomainEvents();
            }

            _trackedAggregates.Clear();
            _operations.Clear();
        }
        catch
        {
            await session.AbortTransactionAsync(ct);
            throw;
        }
    }
}
