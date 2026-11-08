namespace Concierge.Net.Resident.Worker.Application.Abstractions.Messaging;

public interface IMessageBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken);
}
