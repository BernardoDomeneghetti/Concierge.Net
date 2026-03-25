using Concierge.Net.Application.Abstractions.Messaging;
using Concierge.Net.Application.Abstractions.Services;
using Concierge.Net.Domain.Packages;

namespace Concierge.Net.Application.Package.Handlers;

public class PackageCollectedEventHandler(IEmailService emailService) : IDomainEventHandler<PackageCollectedDomainEvent>
{
    private readonly IEmailService _emailService = emailService;

    public async Task HandleAsync(PackageCollectedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
         // enviar email, log, etc
        await _emailService.SendAsync(
            domainEvent.NotificationEmail,
            "Encomenda retirada",
            $"Sua encomenda foi retirada por outro morador",
            cancellationToken
        );
    }
}
