using Concierge.Net.Resident.Domain.Shared;

namespace Concierge.Net.Resident.Worker.Application.Abstractions.Services;

public interface IEmailService
{
    Task SendAsync(Email to, string subject, string body, CancellationToken cancellationToken = default);
}
