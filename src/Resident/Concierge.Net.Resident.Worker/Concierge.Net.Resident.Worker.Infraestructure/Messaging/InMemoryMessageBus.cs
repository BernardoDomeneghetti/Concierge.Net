using Concierge.Net.Resident.Worker.Application.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace Concierge.Net.Resident.Worker.Infraestructure.Messaging;

public sealed class InMemoryMessageBus(ILogger<InMemoryMessageBus> logger) : IMessageBus
{
    private readonly ILogger<InMemoryMessageBus> _logger = logger;

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event published to message bus: {MessageType}", message?.GetType().Name ?? typeof(T).Name);
        return Task.CompletedTask;
    }
}
