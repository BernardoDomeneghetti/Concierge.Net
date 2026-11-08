using System;

namespace Concierge.Net.Package.Worker.Application.Abstractions.Messaging;

public interface IMessageBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken);
}
