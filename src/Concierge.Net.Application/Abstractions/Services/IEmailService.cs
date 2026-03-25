using System;
using Concierge.Net.Domain.Residents;
using Concierge.Net.Domain.Shared;

namespace Concierge.Net.Application.Abstractions.Services;

public interface IEmailService
{
    Task SendAsync(Email to, string subject, string body, CancellationToken cancellationToken = default);
}
