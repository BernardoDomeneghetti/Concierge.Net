using System;
using Concierge.Net.Application.Abstractions.Context;
using Concierge.Net.Application.Abstractions.Messaging;
using Concierge.Net.Domain.Base;
using Microsoft.Extensions.Logging;

namespace Concierge.Net.Application.Decorators;

public sealed class DomainEventHandlerLoggingDecorator<TEvent> 
    : IDomainEventHandler<TEvent>
    where TEvent : DomainEvent
{
    private readonly IDomainEventHandler<TEvent> _inner;
    private readonly ILogger<DomainEventHandlerLoggingDecorator<TEvent>> _logger;
    private readonly ICorrelationContext _correlationContext;

    public DomainEventHandlerLoggingDecorator(
        IDomainEventHandler<TEvent> inner,
        ILogger<DomainEventHandlerLoggingDecorator<TEvent>> logger,
        ICorrelationContext correlationContext)
    {
        _inner = inner;
        _logger = logger;
        _correlationContext = correlationContext;
    }

    public async Task HandleAsync(
        TEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        var correlationId = _correlationContext.CorrelationId;
        var eventName = typeof(TEvent).Name;

        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = correlationId,
            ["EventName"] = eventName,
            ["EventId"] = domainEvent.EventId
        }))
        {
            try
            {
                _logger.LogInformation("Handling event");

                await _inner.HandleAsync(domainEvent, cancellationToken);

                _logger.LogInformation("Handled event successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling event");
                throw;
            }
        }
    }
}
