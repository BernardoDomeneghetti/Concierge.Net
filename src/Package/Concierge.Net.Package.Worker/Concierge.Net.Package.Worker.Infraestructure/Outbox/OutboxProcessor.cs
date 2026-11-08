using Concierge.Net.Package.Worker.Application.Abstractions.Messaging;
using Concierge.Net.Package.Worker.Application.Abstractions.Repositories;

namespace Concierge.Net.Package.Worker.Infraestructure.Outbox;

public class OutboxProcessor(
    IOutboxRepository outboxRepository,
    IDomainEventDispatcher domainEventDispatcher,
    IMessageBus messageBus)
{
    private readonly IOutboxRepository _outboxRepository = outboxRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher = domainEventDispatcher;
    private readonly IMessageBus _messageBus = messageBus;

    public async Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        var events = await _outboxRepository.GetUnprocessed();

        foreach (var evt in events)
        {
            await _domainEventDispatcher.DispatchAsync([evt], cancellationToken);
            await _messageBus.PublishAsync(evt, cancellationToken);
            await _outboxRepository.MarkAsProcessed(evt.EventId);
        }
    }
}
