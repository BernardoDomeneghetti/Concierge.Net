namespace Concierge.Net.Resident.Worker.Application.Abstractions.Context;

public interface ICorrelationContext
{
    Guid CorrelationId { get; }
}
