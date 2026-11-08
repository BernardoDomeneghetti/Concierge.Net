namespace Concierge.Net.Resident.Api.Application.Abstractions.Context;

public interface ICorrelationContext
{
    Guid CorrelationId { get; }
}
