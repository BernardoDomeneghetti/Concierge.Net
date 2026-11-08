using Concierge.Net.Package.Worker.Application.Abstractions.Messaging;
using Concierge.Net.Package.Worker.Application.Abstractions.Services;
using Concierge.Net.Package.Domain.Packages;

namespace Concierge.Net.Package.Worker.Application.Packages.Handlers;

public class PackageReceivedEventHandler: IDomainEventHandler<PackageReceivedDomainEvent>
{
    private readonly IEmailService _emailService;

    public PackageReceivedEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task HandleAsync(PackageReceivedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // - registrar log
        // - publicar em fila
        await _emailService.SendAsync(
            domainEvent.NotificationEmail, 
            "Pacote Recebido", 
            $"O pacote {domainEvent.PackageId} foi recebido.", 
            cancellationToken: cancellationToken
        );
    }
}
