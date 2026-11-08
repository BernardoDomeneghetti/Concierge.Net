using Concierge.Net.Package.Worker.Application.Abstractions.Messaging;
using Concierge.Net.Package.Worker.Application.Abstractions.Services;
using Concierge.Net.Package.Domain.Packages;

namespace Concierge.Net.Package.Worker.Application.Packages.Handlers;

public class PackageCancelledEventHandler(IEmailService emailService) : IDomainEventHandler<PackageCancelledDomainEvent>
{
    private readonly IEmailService _emailService = emailService;

    public async Task HandleAsync(PackageCancelledDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await _emailService.SendAsync(
            domainEvent.NotificationEmail,
            "Encomenda cancelada",
            $"A encomenda {domainEvent.PackageId} foi cancelada. Motivo: {domainEvent.Reason}",
            cancellationToken
        );
    }
}

