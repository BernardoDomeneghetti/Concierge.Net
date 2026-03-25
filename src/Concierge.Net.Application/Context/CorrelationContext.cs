using System;
using Concierge.Net.Application.Abstractions.Context;

namespace Concierge.Net.Application.Context;

public sealed class CorrelationContext : ICorrelationContext
{
    private static readonly AsyncLocal<Guid?> _correlationId = new();

    public Guid CorrelationId => _correlationId.Value ??= Guid.NewGuid();

    public static void Set(Guid correlationId)
    {
        _correlationId.Value = correlationId;
    }

    public static void Set(string correlationId)
    {
        _correlationId.Value = Guid.Parse(correlationId);
    }
}
