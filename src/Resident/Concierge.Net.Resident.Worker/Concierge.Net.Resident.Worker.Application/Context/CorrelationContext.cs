using Concierge.Net.Resident.Worker.Application.Abstractions.Context;

namespace Concierge.Net.Resident.Worker.Application.Context;

public sealed class CorrelationContext : ICorrelationContext
{
    private static readonly AsyncLocal<Guid?> Current = new();

    public Guid CorrelationId
    {
        get
        {
            if (Current.Value == null)
                Current.Value = Guid.NewGuid();

            return Current.Value.Value;
        }
    }

    public static void Set(Guid correlationId)
    {
        Current.Value = correlationId;
    }
}
