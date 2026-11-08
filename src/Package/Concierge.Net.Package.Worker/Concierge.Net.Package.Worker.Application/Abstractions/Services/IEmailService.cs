using System;
using Concierge.Net.Package.Domain.Residents;
using Concierge.Net.Package.Domain.Shared;

namespace Concierge.Net.Package.Worker.Application.Abstractions.Services;

public interface IEmailService
{
    Task SendAsync(Email to, string subject, string body, CancellationToken cancellationToken = default);
}
