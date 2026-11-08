using Concierge.Net.Resident.Domain.Residents;
using Concierge.Net.Resident.Worker.Application.Abstractions.Messaging;
using Concierge.Net.Resident.Worker.Application.Abstractions.Services;

namespace Concierge.Net.Resident.Worker.Application.Residents.Handlers;

public class ResidentRegisteredEventHandler(IResidentNotificationService notificationService)
    : IDomainEventHandler<ResidentRegisteredDomainEvent>
{
    private readonly IResidentNotificationService _notificationService = notificationService;

    public async Task HandleAsync(ResidentRegisteredDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await _notificationService.NotifyAsync(
            domainEvent.ResidentId,
            domainEvent.Email,
            domainEvent.MobilePhone,
            domainEvent.NotificationChannel,
            "Cadastro de Morador Realizado",
            $"Olá {domainEvent.FullName}, seu cadastro de morador ({domainEvent.ResidentType}) foi criado com sucesso.",
            cancellationToken);
    }
}
