using System;
using Concierge.Net.Application.Abstractions.Messaging;
using Concierge.Net.Application.Abstractions.Repositories;

namespace Concierge.Net.Infraestructure.Outbox;

public class OutboxProcessor(IOutboxRepository outboxRepository, IMessageBus messageBus)
{
    private readonly IOutboxRepository _outboxRepository = outboxRepository;
    private readonly IMessageBus _messageBus = messageBus;

    public async Task ProcessAsync()
    {
        var events = await _outboxRepository.GetUnprocessed();

        foreach (var evt in events)
        {
            await _messageBus.PublishAsync(evt, CancellationToken.None);
            await _outboxRepository.MarkAsProcessed(evt.EventId);
        }
    }


}
