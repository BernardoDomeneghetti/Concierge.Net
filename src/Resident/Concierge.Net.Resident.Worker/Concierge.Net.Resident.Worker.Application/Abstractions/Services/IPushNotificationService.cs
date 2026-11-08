using Concierge.Net.Resident.Domain.Residents;

namespace Concierge.Net.Resident.Worker.Application.Abstractions.Services;

public interface IPushNotificationService
{
    Task SendAsync(ResidentId residentId, string title, string message, CancellationToken cancellationToken = default);
}
