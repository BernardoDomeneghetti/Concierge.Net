using Concierge.Net.Application.Abstractions.Messaging;
using Concierge.Net.Application.Abstractions.Services;
using Concierge.Net.Domain.Packages;

namespace Concierge.Net.Application.Package.Handlers;

public class PackageReceivedHandler: IDomainEventHandler<PackageReceivedDomainEvent>
{
    private readonly IEmailService _emailService;

    public PackageReceivedHandler(IEmailService emailService)
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
            $"O pacote {domainEvent.Id} foi recebido.", 
            cancellationToken: cancellationToken
        );
    }
}
