using Concierge.Net.Package.Domain.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace Concierge.Net.Package.Api.Infraestructure.Outbox;

public sealed class OutboxMessage
{
    [BsonId]
    public Guid Id { get; init; }
    public required DomainEvent Event { get; init; }
    public DateTime OccurredOnUtc { get; init; }
    public DateTime? ProcessedAtUtc { get; set; }
}

