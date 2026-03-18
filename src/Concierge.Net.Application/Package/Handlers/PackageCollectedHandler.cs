using Concierge.Net.Application.Abstractions.Messaging;
using Concierge.Net.Domain.Packages;

namespace Concierge.Net.Application.Package.Handlers;

public class PackageCollectedHandler: IDomainEventHandler<PackageCollectedDomainEvent>
{
    public Task HandleAsync(PackageCollectedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
         // enviar email, log, etc
        throw new NotImplementedException();
    }
}
