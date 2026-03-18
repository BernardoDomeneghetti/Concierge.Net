using Concierge.Net.Application.Abstractions.Messaging;
using Concierge.Net.Domain.Packages;

namespace Concierge.Net.Application.Package.Handlers;

public class PackageReceivedHandler: IDomainEventHandler<PackageReceivedDomainEvent>
{
    public Task HandleAsync(PackageReceivedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // - chamar serviço de email
        // - registrar log
        // - publicar em fila
        throw new NotImplementedException();
    }
}
