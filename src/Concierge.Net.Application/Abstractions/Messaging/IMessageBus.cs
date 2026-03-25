using System;

namespace Concierge.Net.Application.Abstractions.Messaging;

public interface IMessageBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken);
}
