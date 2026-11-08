using Concierge.Net.Resident.Domain.Residents;
using Concierge.Net.Resident.Worker.Application.Abstractions.Messaging;
using Concierge.Net.Resident.Worker.Application.Abstractions.Services;

namespace Concierge.Net.Resident.Worker.Application.Residents.Handlers;

public class ResidentUpdatedEventHandler(IResidentNotificationService notificationService)
    : IDomainEventHandler<ResidentUpdatedDomainEvent>
{
    private readonly IResidentNotificationService _notificationService = notificationService;

    public async Task HandleAsync(ResidentUpdatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await _notificationService.NotifyAsync(
            domainEvent.ResidentId,
            domainEvent.Email,
            domainEvent.MobilePhone,
            domainEvent.NotificationChannel,
            "Cadastro de Morador Atualizado",
            $"Olá {domainEvent.FullName}, seus dados cadastrais foram atualizados em {domainEvent.UpdatedAtUtc:O}.",
            cancellationToken);
    }
}
