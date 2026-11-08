using Concierge.Net.Resident.Domain.Base;
using Concierge.Net.Resident.Worker.Application.Abstractions.Messaging;

namespace Concierge.Net.Resident.Worker.Infraestructure.Messaging;

public sealed class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task DispatchAsync(IEnumerable<DomainEvent> events, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in events)
        {
            var eventType = domainEvent.GetType();
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(handlerType);

            if (_serviceProvider.GetService(enumerableType) is not IEnumerable<object> handlers)
                continue;

            var handleMethod = handlerType.GetMethod(nameof(IDomainEventHandler<DomainEvent>.HandleAsync));
            if (handleMethod is null)
                continue;

            foreach (var handler in handlers)
            {
                if (handleMethod.Invoke(handler, [domainEvent, cancellationToken]) is Task task)
                {
                    await task;
                }
            }
        }
    }
}
