using System;

namespace Concierge.Net.Package.Api.Application.Abstractions.Context;

public interface ICorrelationContext
{
    Guid CorrelationId { get; }
}
