using System;

namespace Concierge.Net.Application.Abstractions.Context;

public interface ICorrelationContext
{
    Guid CorrelationId { get; }
}
