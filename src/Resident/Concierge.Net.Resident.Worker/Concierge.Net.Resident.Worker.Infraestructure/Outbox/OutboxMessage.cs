using Concierge.Net.Resident.Domain.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace Concierge.Net.Resident.Worker.Infraestructure.Outbox;

public sealed class OutboxMessage
{
    [BsonId]
    public Guid Id { get; init; }
    public required DomainEvent Event { get; init; }
    public DateTime OccurredOnUtc { get; init; }
    public DateTime? ProcessedAtUtc { get; set; }
}
