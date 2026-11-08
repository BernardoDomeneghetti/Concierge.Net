using System;

namespace Concierge.Net.Package.Worker.Application.Abstractions.Context;

public interface ICorrelationContext
{
    Guid CorrelationId { get; }
}
