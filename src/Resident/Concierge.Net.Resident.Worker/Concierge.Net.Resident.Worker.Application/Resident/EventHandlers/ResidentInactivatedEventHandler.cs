using Concierge.Net.Resident.Domain.Residents;
using Concierge.Net.Resident.Worker.Application.Abstractions.Messaging;
using Concierge.Net.Resident.Worker.Application.Abstractions.Services;

namespace Concierge.Net.Resident.Worker.Application.Residents.Handlers;

public class ResidentInactivatedEventHandler(IResidentNotificationService notificationService)
    : IDomainEventHandler<ResidentInactivatedDomainEvent>
{
    private readonly IResidentNotificationService _notificationService = notificationService;

    public async Task HandleAsync(ResidentInactivatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await _notificationService.NotifyAsync(
            domainEvent.ResidentId,
            domainEvent.Email,
            domainEvent.MobilePhone,
            domainEvent.NotificationChannel,
            "Cadastro de Morador Inativado",
            $"Olá {domainEvent.FullName}, seu cadastro foi inativado. Motivo: {domainEvent.Reason}",
            cancellationToken);
    }
}
