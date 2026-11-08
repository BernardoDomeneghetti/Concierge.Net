using Concierge.Net.Resident.Domain.Residents;

namespace Concierge.Net.Resident.Worker.Application.Abstractions.Services;

public interface ISmsService
{
    Task SendAsync(PhoneNumber to, string message, CancellationToken cancellationToken = default);
}
