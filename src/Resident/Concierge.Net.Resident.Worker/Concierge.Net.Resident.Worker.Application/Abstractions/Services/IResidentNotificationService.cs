using Concierge.Net.Resident.Domain.Residents;
using Concierge.Net.Resident.Domain.Shared;

namespace Concierge.Net.Resident.Worker.Application.Abstractions.Services;

public interface IResidentNotificationService
{
    Task NotifyAsync(
        ResidentId residentId,
        Email email,
        PhoneNumber mobilePhone,
        NotificationChannel preferredChannels,
        string subject,
        string message,
        CancellationToken cancellationToken = default);
}
